// //
// // Created by  veyrie_f
// //
//
using System;
using UnityEngine;

namespace Character
{
    public enum PART { HEAD, LEFTARM, BODY, RIGHTARM, LEFTLEG, RIGHTLEG };
    public enum ADVANCE { TLEFT, TRIGHT, ALEFT, ARIGHT };

    [System.Serializable]
	public class Part
	{
		public Sprite sprite;
        public Sprite[] spriteParts = new Sprite[2];
		// Add stats
		public int damage;
        public int hp;
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

		virtual public Part getPart(int part)
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

		virtual public void setPart(int part, Part spr)
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

    [System.Serializable]
    public class AdvanceBody
    {
        public GameObject head;
        public GameObject body;
        public GameObject leftArm;
        public GameObject rightArm;
        public GameObject leftLeg;
        public GameObject rightLeg;
        public GameObject tLeft;
        public GameObject tRight;
        public GameObject aLeft;
        public GameObject aRight;

        public void Coffee(int player, GameManager gM)
        {
            body.GetComponent<Item>().Part = gM.GetPlayerPart(player, (int)PART.BODY);
            body.GetComponent<SpriteRenderer>().sprite = body.GetComponent<Item>().Part.spriteParts[0];

            head.GetComponent<Item>().Part = gM.GetPlayerPart(player, (int)PART.HEAD);
            head.GetComponent<SpriteRenderer>().sprite = head.GetComponent<Item>().Part.spriteParts[0];

            leftArm.GetComponent<Item>().Part = gM.GetPlayerPart(player, (int)PART.LEFTARM);
            leftArm.GetComponent<SpriteRenderer>().sprite = leftArm.GetComponent<Item>().Part.spriteParts[0];

            rightArm.GetComponent<Item>().Part = gM.GetPlayerPart(player, (int)PART.RIGHTARM);
            rightArm.GetComponent<SpriteRenderer>().sprite = rightArm.GetComponent<Item>().Part.spriteParts[0];

            leftLeg.GetComponent<Item>().Part = gM.GetPlayerPart(player, (int)PART.LEFTLEG);
            leftLeg.GetComponent<SpriteRenderer>().sprite = leftLeg.GetComponent<Item>().Part.spriteParts[0];

            rightLeg.GetComponent<Item>().Part = gM.GetPlayerPart(player, (int)PART.RIGHTLEG);
            rightLeg.GetComponent<SpriteRenderer>().sprite = rightLeg.GetComponent<Item>().Part.spriteParts[0];

            tLeft.GetComponent<Item>().Part = leftLeg.GetComponent<Item>().Part;
            tLeft.GetComponent<SpriteRenderer>().sprite = tLeft.GetComponent<Item>().Part.spriteParts[1];

            tRight.GetComponent<Item>().Part = rightLeg.GetComponent<Item>().Part;
            tRight.GetComponent<SpriteRenderer>().sprite = tRight.GetComponent<Item>().Part.spriteParts[1];

            aLeft.GetComponent<Item>().Part = leftArm.GetComponent<Item>().Part;
            aLeft.GetComponent<SpriteRenderer>().sprite = aLeft.GetComponent<Item>().Part.spriteParts[1];

            aRight.GetComponent<Item>().Part = rightArm.GetComponent<Item>().Part;
            aRight.GetComponent<SpriteRenderer>().sprite = aRight.GetComponent<Item>().Part.spriteParts[1];
        }
    }
}

