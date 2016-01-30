using UnityEngine;
using System.Collections.Generic;

public class RandomItem : MonoBehaviour {

	[System.Serializable]
	public class Part
	{
		public Sprite sprite;
		// Add stats
		public int damage;
		public int speed;
		public int candies;
		public int special;
	}

    [System.Serializable]
    public class Body
	{
		public Part head;
		public Part body;
		public Part leftArm;
		public Part rightArm;
		public Part leftLeg;
		public Part rightLeg;
        private enum PART {HEAD, BODY, LEFTARM, RIGHTARM, LEFTLEG, RIGHTLEG};

		public Part getPart(int part)
        {
            if (part == (int)PART.HEAD)
				return head;
            else if (part == (int)PART.BODY)
				return body;
            else if (part == (int)PART.LEFTARM)
				return leftArm;
            else if (part == (int)PART.RIGHTARM)
				return rightArm;
            else if (part == (int)PART.LEFTLEG)
				return leftLeg;
			return rightLeg;
        }

		public void setPart(int part, Part spr)
        {
            if (part == (int)PART.HEAD)
				head = spr;
            else if (part == (int)PART.BODY)
				body = spr;
            else if (part == (int)PART.LEFTARM)
				leftArm = spr;
            else if (part == (int)PART.RIGHTARM)
				rightArm = spr;
            else if (part == (int)PART.LEFTLEG)
				leftLeg = spr;
			rightLeg = spr;
        }
    }

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
