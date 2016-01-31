using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using Character;

public class BottomPanel : MonoBehaviour {

    // PANEL //
    public PlayerSelect visual;
    public Sprite actifTab;
    public Sprite inactifTab;
    public Sprite actifSlot;
    public Sprite inactifSlot; 

    public Image firstTab;
    public Image secondTab;

    public GameObject Inventory;
    public GameObject Shop;
    public Carac Caracs;
    public Carac CaracsPerso;
    public Carac CaracTotal;
    [Range(1, 2)]
    public int manette = 1;

    private bool                        _isShop = false;
    private Transform                   _itemActive;
    private Dictionary<string, string>  _axis = new Dictionary<string, string>();
    private int                         _current;
    private int                         _type = (int)PART.HEAD;
    private bool                        _move;
    private int                         _lastType;
    private bool                        _click = false;

    // ITEMS //

    public Body[] bodies;
    public Part[] shopContent;

    private const int MAX_RANDOM = 12;

    //private List<Part> _items = new List<Part>();
    private Dictionary<int, List<Part>> _allitems = new Dictionary<int, List<Part>>();
    private Dictionary<int, List<Part>> _items;
    private GameManager _GM;

    private string[] NAME = { "Head", "Left Arm", "Body", "Right Arm", "Left Leg", "Right Leg" };

    public Dictionary<int, List<Part>> Items
    {
        get { return _items; }
        set { _items = value; }
    }

    // Use this for initialization
    void Start () {

        _GM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        // INIT MENUTAB
        _items = _GM.GetPlayerParts(manette);

        // INIT MENU
        _allitems.Add((int)PART.HEAD, new List<Part>());
        _allitems.Add((int)PART.BODY, new List<Part>());
        _allitems.Add((int)PART.LEFTARM, new List<Part>());
        _allitems.Add((int)PART.RIGHTARM, new List<Part>());
        _allitems.Add((int)PART.LEFTLEG, new List<Part>());
        _allitems.Add((int)PART.RIGHTLEG, new List<Part>());

        _itemActive = Inventory.transform.GetChild(0);
        _itemActive.GetComponent<Image>().sprite = actifSlot;
        Shop.SetActive(false);

        // INIT AXIS
        _axis.Add("LB", "LB" + manette.ToString());
        _axis.Add("RB", "RB" + manette.ToString());
        _axis.Add("Move", "Move" + manette.ToString());
        _axis.Add("Submit", "Submit" + manette.ToString());
        Generate();
        _lastType = 0;
        displayMenu();
        ActiveItem(Inventory.transform.GetChild(0));
        CaracsPerso.setObject(Inventory.transform.GetChild(visual.Index).GetComponent<Item>().Part, NAME[visual.Index]);
        CaracTotal.setObject(mixParts(), "Total");
    }

    private Part mixParts()
    {
        int damage = _GM.GetPlayerPart(manette, (int)PART.HEAD).damage +
                    _GM.GetPlayerPart(manette, (int)PART.BODY).damage +
                    _GM.GetPlayerPart(manette, (int)PART.LEFTARM).damage +
                    _GM.GetPlayerPart(manette, (int)PART.RIGHTARM).damage +
                    _GM.GetPlayerPart(manette, (int)PART.LEFTLEG).damage +
                    _GM.GetPlayerPart(manette, (int)PART.RIGHTLEG).damage;

        int hp = _GM.GetPlayerPart(manette, (int)PART.HEAD).hp +
                    _GM.GetPlayerPart(manette, (int)PART.BODY).hp +
                    _GM.GetPlayerPart(manette, (int)PART.LEFTARM).hp +
                    _GM.GetPlayerPart(manette, (int)PART.RIGHTARM).hp +
                    _GM.GetPlayerPart(manette, (int)PART.LEFTLEG).hp +
                    _GM.GetPlayerPart(manette, (int)PART.RIGHTLEG).hp;

        int speed = _GM.GetPlayerPart(manette, (int)PART.HEAD).speed +
                    _GM.GetPlayerPart(manette, (int)PART.BODY).speed +
                    _GM.GetPlayerPart(manette, (int)PART.LEFTARM).speed +
                    _GM.GetPlayerPart(manette, (int)PART.RIGHTARM).speed +
                    _GM.GetPlayerPart(manette, (int)PART.LEFTLEG).speed +
                    _GM.GetPlayerPart(manette, (int)PART.RIGHTLEG).speed;

        int special = _GM.GetPlayerPart(manette, (int)PART.HEAD).special +
                    _GM.GetPlayerPart(manette, (int)PART.BODY).special +
                    _GM.GetPlayerPart(manette, (int)PART.LEFTARM).special +
                    _GM.GetPlayerPart(manette, (int)PART.RIGHTARM).special +
                    _GM.GetPlayerPart(manette, (int)PART.LEFTLEG).special +
                    _GM.GetPlayerPart(manette, (int)PART.RIGHTLEG).special;

        int candies = _GM.GetPlayerPart(manette, (int)PART.HEAD).candies +
                    _GM.GetPlayerPart(manette, (int)PART.BODY).candies +
                    _GM.GetPlayerPart(manette, (int)PART.LEFTARM).candies +
                    _GM.GetPlayerPart(manette, (int)PART.RIGHTARM).candies +
                    _GM.GetPlayerPart(manette, (int)PART.LEFTLEG).candies +
                    _GM.GetPlayerPart(manette, (int)PART.RIGHTLEG).candies;

        Part mix = new Part();
        mix.damage = damage / 6;
        mix.hp = hp / 6;
        mix.speed = speed / 6;
        mix.special = special / 6;
        mix.candies = candies / 6;
        return mix;
    }

    private void displayMenu()
    {
        for (int i = 0; i < Inventory.transform.childCount; i++)
        {
            if (i < _items[_type].Count)
            {
                //print(_items[_type][i]);
                Inventory.transform.GetChild(i).GetComponent<Item>().Part = _items[_type][i];
                Inventory.transform.GetChild(i).transform.GetChild(0).GetComponent<Image>().sprite = _items[_type][i].sprite;
            }
        }
        for (int i = 0; i < shopContent.Length; i++)
        {
            if (i < shopContent.Length)
            {
                Shop.transform.GetChild(i).GetComponent<Item>().Part = shopContent[i];
                Shop.transform.GetChild(i).transform.GetChild(0).GetComponent<Image>().sprite = shopContent[i].sprite;
            }
        }
        ActiveItem(Inventory.transform.GetChild(0));
    }

	// Update visual
	private void UpdateCharacter()
	{
		//visual.buttons [_type].GetComponent<Image> ().sprite = _items[_type][_current].sprite;
		visual.buttons [0].GetComponent<Image> ().sprite = _GM.GetPlayerPart(manette, (int)PART.HEAD).sprite;
		visual.buttons [1].GetComponent<Image> ().sprite = _GM.GetPlayerPart(manette, (int)PART.LEFTARM).sprite;
		visual.buttons [2].GetComponent<Image> ().sprite = _GM.GetPlayerPart(manette, (int)PART.BODY).sprite;
		visual.buttons [3].GetComponent<Image> ().sprite = _GM.GetPlayerPart(manette, (int)PART.RIGHTARM).sprite;
		visual.buttons [4].GetComponent<Image> ().sprite = _GM.GetPlayerPart(manette, (int)PART.LEFTLEG).sprite;
		visual.buttons [5].GetComponent<Image> ().sprite = _GM.GetPlayerPart(manette, (int)PART.RIGHTLEG).sprite;
	}

    public void Generate()
    {
        foreach (Body body in bodies)
        {
            _allitems[(int)PART.HEAD].Add(body.getPart(0));
            _allitems[(int)PART.BODY].Add(body.getPart(1));
            _allitems[(int)PART.LEFTARM].Add(body.getPart(2));
            _allitems[(int)PART.RIGHTARM].Add(body.getPart(3));
            _allitems[(int)PART.LEFTLEG].Add(body.getPart(4));
            _allitems[(int)PART.RIGHTLEG].Add(body.getPart(5));
        }
        for (int i = 0; i < 6; i++)
        {
            int dice = Random.Range(0, bodies.Length);
            _items[i].Add(bodies[dice].getPart(i));
            _allitems[i].Remove(bodies[dice].getPart(i));
            _GM.SetPlayerPart(manette, i, bodies[dice].getPart(i));
        }
        for (int i = 0; i < MAX_RANDOM; i++)
        {
            if (_allitems.Count > 0)
            {
                int type = Random.Range(0, 6);
                int dice = Random.Range(0, _allitems[type].Count);
                if (_allitems[type][dice] != null)
                {
                    _items[type].Add(_allitems[type][dice]);
                    _allitems[type][dice] = null;
                }
				
            }
        }
		UpdateCharacter ();
    }

    // Update is called once per frame
    void Update () {
        _type = visual.Index;
        if (_type != _lastType)
            displayMenu();
	    if (Input.GetAxis(_axis["LB"]) != 0 && _isShop)
        {
            _isShop = false;
            firstTab.sprite = actifTab;
            secondTab.sprite = inactifTab;
            Shop.SetActive(false);
            Inventory.SetActive(true);
            _current = 0;
            ActiveItem(Inventory.transform.GetChild(0));
        }
        else if (Input.GetAxis(_axis["RB"]) != 0 && !_isShop)
        {
            _isShop = true;
            firstTab.sprite = inactifTab;
            secondTab.sprite = actifTab;
            Inventory.SetActive(false);
            Shop.SetActive(true);
            _current = 0;
            ActiveItem(Shop.transform.GetChild(0));
        }
        Move();
        Select();
        _lastType = _type;
    }

    private void Select()
    {
        if (Input.GetAxis(_axis["Submit"]) != 0 && !_click)
        {
            StartCoroutine(waitClick());
            _GM.SetPlayerPart(manette, _type, _itemActive.GetComponent<Item>().Part);
            CaracTotal.setObject(mixParts(), "Total");
			UpdateCharacter ();
        }
    }

    private void ActiveItem(Transform obj)
    {
        _itemActive.GetComponent<Image>().sprite = inactifSlot;
        _itemActive = obj;
        _itemActive.GetComponent<Image>().sprite = actifSlot;
        Caracs.setObject(obj.GetComponent<Item>().Part, NAME[_type]);
    }

    //public void Maj()
    //{
    //    if (_isShop)
    //        Caracs.setObject(Shop.transform.GetChild(visual.Index).GetComponent<Item>().Part, NAME[_type]);
    //    else
    //        Caracs.setObject(Inventory.transform.GetChild(visual.Index).GetComponent<Item>().Part, NAME[visual.Index]);
    //}

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
                    ActiveItem(Shop.transform.GetChild(_current));
                }
                else
                {
                    _current = Shop.transform.childCount - 1;
                    ActiveItem(Shop.transform.GetChild(_current));
                }
            }
            else if (Input.GetAxis(_axis["Move"]) > 0 && !_move)
            {
                StartCoroutine(canMove());
                if (_current < Shop.transform.childCount - 1)
                {
                    ++_current;
                    ActiveItem(Shop.transform.GetChild(_current));
                }
                else
                {
                    _current = 0;
                    ActiveItem(Shop.transform.GetChild(_current));
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
                    ActiveItem(Inventory.transform.GetChild(_current));
                }
                else
                {
                    _current = Inventory.transform.childCount - 1;
                    ActiveItem(Inventory.transform.GetChild(_current));
                }
            }
            else if (Input.GetAxis(_axis["Move"]) > 0 && !_move)
            {
                StartCoroutine(canMove());
                if (_current < Inventory.transform.childCount - 1)
                {
                    ++_current;
                    ActiveItem(Inventory.transform.GetChild(_current));
                }
                else
                {
                    _current = 0;
                    ActiveItem(Inventory.transform.GetChild(_current));
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
    IEnumerator waitClick()
    {
        _click = true;
        yield return new WaitForSeconds(0.15f);
        _click = true;
    }
}
