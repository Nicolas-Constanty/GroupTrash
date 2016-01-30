using UnityEngine;
using System.Collections.Generic;

public class RandomItem : MonoBehaviour {

    [System.Serializable]
    public class Body {
        public Sprite head;
        public Sprite body;
        public Sprite leftArm;
        public Sprite rightArm;
        public Sprite leftLeg;
        public Sprite rightLeg;
        private enum PART { HEAD, BODY, LEFTARM, RIGHTARM, LEFTLEG, RIGHTLEG};

        public Sprite getPart(int part)
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
        public void setPart(int part, Sprite spr)
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
    private List<Sprite> items;
    private List<Sprite> allitems;
    public Body[] bodies;
    private Sprite empty;
    // Use this for initialization
    void Start () {
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

	// Update is called once per frame
	void Update () {
	
	}
}
