// //
// // Created by  veyrie_f
// //
//
using System;
using UnityEngine;

namespace Character
{
    public enum PART { HEAD, LEFTARM, BODY, RIGHTARM, LEFTLEG, RIGHTLEG };
    
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
}

