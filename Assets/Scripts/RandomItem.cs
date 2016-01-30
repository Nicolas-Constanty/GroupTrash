using UnityEngine;
using System.Collections.Generic;
using Character;

public class RandomItem : MonoBehaviour {

	private List<Part> items = new List<Part>();
	private List<Part> allitems = new List<Part>();
	public Body[] bodies;
	private Part empty = null;

	void Start ()
	{
        Generate();    
	}

    void Generate()
    {
        for (int i = 0; i < bodies.Length; i++)
        {
            int dice = Random.Range(0, bodies.Length);
            items.Add(bodies[dice].getPart(i));
            allitems.Add(items[i]);
            bodies[dice].setPart(i, empty); 
        }

    }
}
