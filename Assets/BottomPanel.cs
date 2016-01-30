using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using Character;

public class BottomPanel : MonoBehaviour {

    // PANEL //
    public Sprite actifTab;
    public Sprite inactifTab;
    public Sprite actifSlot;
    public Sprite inactifSlot;

    public Image firstTab;
    public Image secondTab;

    public GameObject Inventory;
    public GameObject Shop;
    [Range(1, 2)]
    public int manette;

    private bool                        _isShop = false;
    private Image                       _itemActive;
    private Dictionary<string, string>  _axis = new Dictionary<string, string>();
    private int                         _current;
    private bool                        _move;

    // ITEMS //

    public Body[] bodies;
    public Part[] shopContent;

    private const int MAX_RANDOM = 12;

    private List<Part> _items = new List<Part>();
    private List<Part> _allitems = new List<Part>();

    public List<Part> Items
    {
        get { return _items; }
        set { _items = value; }
    }

    // Use this for initialization
    void Start () {
        _itemActive = Inventory.transform.GetChild(0).gameObject.GetComponent<Image>();
        _itemActive.sprite = actifSlot;
        Shop.SetActive(false);
        _axis.Add("LB", "LB" + manette.ToString());
        _axis.Add("RB", "RB" + manette.ToString());
        _axis.Add("Move", "Move" + manette.ToString());
        Generate();
        for (int i = 0; i < Inventory.transform.childCount; i++)
        {
            if (i < _items.Count)
                Inventory.transform.GetChild(i).transform.GetChild(0).GetComponent<Image>().sprite = _items[i].sprite;
        }
        for (int i = 0; i < shopContent.Length; i++)
        {
            if (i < shopContent.Length)
                Shop.transform.GetChild(i).transform.GetChild(0).GetComponent<Image>().sprite = shopContent[i].sprite;
        }
    }

    public void Generate()
    {
        foreach (Body body in bodies)
        {
            _allitems.Add(body.getPart(0));
            _allitems.Add(body.getPart(1));
            _allitems.Add(body.getPart(2));
            _allitems.Add(body.getPart(3));
            _allitems.Add(body.getPart(4));
            _allitems.Add(body.getPart(5));
        }
        for (int i = 0; i < bodies.Length; i++)
        {
            int dice = Random.Range(0, bodies.Length);
            _items.Add(bodies[dice].getPart(i));
            _allitems.Remove(_items[i]);
        }
        for (int i = 0; i < MAX_RANDOM; i++)
        {
            if (_allitems.Count > 0)
            {
                int dice = Random.Range(0, _allitems.Count);
                _items.Add(_allitems[dice]);
                _allitems.Remove(_allitems[dice]);
            }
        }
    }

    // Update is called once per frame
    void Update () {
	    if (Input.GetAxis(_axis["LB"]) != 0 && _isShop)
        {
            _isShop = false;
            firstTab.sprite = actifTab;
            secondTab.sprite = inactifTab;
            Shop.SetActive(false);
            Inventory.SetActive(true);
            _current = 0;
            ActiveItem(Inventory.transform.GetChild(0).gameObject.GetComponent<Image>());
        }
        else if (Input.GetAxis(_axis["RB"]) != 0 && !_isShop)
        {
            _isShop = true;
            firstTab.sprite = inactifTab;
            secondTab.sprite = actifTab;
            Inventory.SetActive(false);
            Shop.SetActive(true);
            _current = 0;
            ActiveItem(Shop.transform.GetChild(0).gameObject.GetComponent<Image>());
        }
        Move();
    }

    private void ActiveItem(Image img)
    {
        _itemActive.sprite = inactifSlot;
        _itemActive = img;
        _itemActive.sprite = actifSlot;
    }

    private void Move()
    {
        if (_isShop)
        {
            if (Input.GetAxis(_axis["Move"]) < 0 && !_move)
            {
                StartCoroutine(canMove());
                if (_current > 0)
                {
                    --_current;
                    ActiveItem(Shop.transform.GetChild(_current).GetComponent<Image>());
                }
                else
                {
                    _current = Shop.transform.childCount - 1;
                    ActiveItem(Shop.transform.GetChild(_current).GetComponent<Image>());
                }
            }
            else if (Input.GetAxis(_axis["Move"]) > 0 && !_move)
            {
                StartCoroutine(canMove());
                if (_current < Shop.transform.childCount - 1)
                {
                    ++_current;
                    ActiveItem(Shop.transform.GetChild(_current).GetComponent<Image>());
                }
                else
                {
                    _current = 0;
                    ActiveItem(Shop.transform.GetChild(_current).GetComponent<Image>());
                }
            }
        }
        else
        {
            if (Input.GetAxis(_axis["Move"]) < 0 && !_move)
            {
                StartCoroutine(canMove());
                if (_current > 0)
                {
                    --_current;
                    ActiveItem(Inventory.transform.GetChild(_current).GetComponent<Image>());
                }
                else
                {
                    _current = Inventory.transform.childCount - 1;
                    ActiveItem(Inventory.transform.GetChild(_current).GetComponent<Image>());
                }
            }
            else if (Input.GetAxis(_axis["Move"]) > 0 && !_move)
            {
                StartCoroutine(canMove());
                if (_current < Inventory.transform.childCount - 1)
                {
                    ++_current;
                    ActiveItem(Inventory.transform.GetChild(_current).GetComponent<Image>());
                }
                else
                {
                    _current = 0;
                    ActiveItem(Inventory.transform.GetChild(_current).GetComponent<Image>());
                }
            }
        }
    }

    IEnumerator  canMove()
    {
        _move = true;
        yield return new WaitForSeconds(0.15f);
        _move = false;
    }
}
