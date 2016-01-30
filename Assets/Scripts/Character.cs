using UnityEngine;

namespace Character
{
    [System.Serializable]
	public class Part
	{
		public Sprite sprite;
		// Add stats
		public int damage;
		public int speed;
		public int candies;
		public int special;

        public Part(Sprite spr, int d, int s, int c, int spe)
        {
            sprite = spr;
            damage = d;
            speed = s;
            candies = c;
            special = spe;
        }
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

        public Body(Part h, Part b, Part la, Part ra, Part ll, Part rl)
        {
            head = h;
            body = b;
            leftArm = la;
            rightArm = ra;
            leftLeg = ll;
            rightLeg = rl;
        }

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

