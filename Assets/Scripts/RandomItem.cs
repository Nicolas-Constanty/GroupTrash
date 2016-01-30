using UnityEngine;
using System.Collections.Generic;
using Character;

public class RandomItem : MonoBehaviour {
	public Body[] bodies;

    private const int MAX_RANDOM = 12;

    private List<Part> _items = new List<Part>();
    private List<Part> _allitems = new List<Part>();

    public List<Part> Items
    {
        get { return _items; }
        set { _items = value; }
    }

    void Awake ()
	{
        Generate();    
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
            int dice = Random.Range(0, _allitems.Count);
            _items.Add(_allitems[dice]);
            _allitems.Remove(_items[dice]);
        }
    }

    public Part this[int i]
    {
        get
        {
            return _items[i];
        }
        set
        {
            _items[i] = value;
        }
    }
}
