using UnityEngine;
using Character;
using XInputDotNetPure;
using System.Collections;

public class Item : MonoBehaviour {

	// Related to the controller
	bool playerIndexSet = false;
	PlayerIndex playerIndex;
	GamePadState state;
	GamePadState prevState;

	bool playerIndexSet2 = false;
	PlayerIndex playerIndex2;
	GamePadState state2;
	GamePadState prevState2;

    private float timer = 0f;
    private Part _part = new Part();
    public Part Part
    {
        get { return _part; }
        set {   _part.damage = value.damage;
                _part.hp = value.hp;
                _part.candies = value.candies;
                _part.special = value.special;
                _part.speed = value.speed;
                _part.sprite = value.sprite;
                _part.spriteParts = value.spriteParts;
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (_part.hp < 0)
            return;

        if ((coll.gameObject.layer == LayerMask.NameToLayer("Player1") || coll.gameObject.layer == LayerMask.NameToLayer("Player2")) && timer < 0.05)
        {
            Item hitted = coll.gameObject.GetComponentInChildren<Item>();
            float magnitude = transform.parent.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude;

			StartCoroutine (ControllerVibration ());

            if (coll.transform.parent.GetChild(0).GetComponentInChildren<Rigidbody2D>().velocity.magnitude > magnitude)
                hitted.Part.hp -= (int)magnitude + Part.damage;

			if (hitted.Part.hp < 0)
            {
                Transform parent = hitted.transform.parent;
                foreach (HingeJoint2D joint in parent.GetComponentsInChildren<HingeJoint2D>())
                {
                    LooseJoint(joint);
                }
                foreach (FixedJoint2D joint in parent.GetComponentsInChildren<FixedJoint2D>())
                {
                    LooseJoint(joint);
                }
            }

            timer = 0f;
        }

        timer += Time.deltaTime;
    }

    private void LooseJoint(Joint2D joint)
    {
        GameManager gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        if (joint.gameObject.name.Equals("Head") || joint.gameObject.name.Equals("Body"))
        {
            gm.SetWin(transform.parent.gameObject);
            //gm.confetis.SetActive(true);
        }

        if (joint.GetComponent<CandiesEmitter>() != null)
            joint.GetComponent<CandiesEmitter>().EmitCandies();

        //GameObject.Find("Supporters").GetComponent<Animator>().SetTrigger("Houra");

        Destroy(joint);

		// Tilt camera
		Camera.main.GetComponent<CameraManager>().TiltCamera(0.25f);
    }

	void Update()
	{
		if (!playerIndexSet || !prevState.IsConnected)
		{
			PlayerIndex testPlayerIndex = (PlayerIndex)0;
			GamePadState testState = GamePad.GetState (testPlayerIndex);
			if (testState.IsConnected)
			{
				Debug.Log (string.Format ("GamePad found {0}", testPlayerIndex));
				playerIndex = testPlayerIndex;
				playerIndexSet = true;
			}
		}

		prevState = state;
		state = GamePad.GetState(playerIndex);

		if (!playerIndexSet2 || !prevState2.IsConnected)
		{
			PlayerIndex testPlayerIndex = (PlayerIndex)1;
			GamePadState testState = GamePad.GetState (testPlayerIndex);
			if (testState.IsConnected)
			{
				Debug.Log (string.Format ("GamePad found {0}", testPlayerIndex));
				playerIndex2 = testPlayerIndex;
				playerIndexSet2 = true;
			}
		}

		prevState2 = state2;
		state2 = GamePad.GetState(playerIndex2);
	}


	IEnumerator ControllerVibration()
	{
		if (playerIndexSet)
			GamePad.SetVibration (playerIndex, 1, 1);
		if (playerIndexSet2)
			GamePad.SetVibration (playerIndex, 1, 1);
		
		yield return new WaitForSeconds (0.2f);

		if (playerIndexSet)
			GamePad.SetVibration (playerIndex, 0, 0);
		if (playerIndexSet2)
			GamePad.SetVibration (playerIndex2, 0, 0);
	}
}
