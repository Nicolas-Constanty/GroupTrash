using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerSelect : MonoBehaviour {

	[SerializeField]
	private GameObject[] buttonsP1;
	private int buttonIdxP1 = 0;

	[SerializeField]
	private GameObject[] buttonsP2;
	private int buttonIdxP2 = 0;

	[SerializeField]
	private Text timer;
	[SerializeField]
	private int waitTime = 30;
	private int remainingTime;

	private bool isP1MovingH = false;
	private bool isP1MovingV = false;
	private bool isP2Moving = false;

	void Start ()
	{
		// Selectionne la tête par défaut
		buttonsP1[buttonIdxP1].GetComponent<Image>().color = Color.red;
		buttonsP2 [buttonIdxP2].GetComponent<Image> ().color = Color.red;
		remainingTime = waitTime;
	}
	
	void Update ()
	{
		MoveCursorP1 ();
		// TODO: add P2

		if (remainingTime > 0)
		{
			remainingTime = waitTime - (int)Time.timeSinceLevelLoad;
			timer.text = remainingTime.ToString ();
		}
		else
		{
			timer.text = "Go !";
		}
	}

	void MoveCursorP1()
	{
		float verticalP1 = Input.GetAxisRaw ("Vertical");
		float horizontalP1 = Input.GetAxisRaw ("Horizontal");

		if (verticalP1 != 0 && !isP1MovingV)
		{
			isP1MovingV = true;
			buttonsP1[buttonIdxP1].GetComponent<Image>().color = Color.white;
			buttonIdxP1 -= (int)verticalP1;
			buttonIdxP1 = (buttonIdxP1 < 0) ? buttonsP1.Length - 1 : (buttonIdxP1 > buttonsP1.Length - 1) ? 0 : buttonIdxP1;
			buttonsP1[buttonIdxP1].GetComponent<Image>().color = Color.red;
		}
		if (horizontalP1 != 0 && !isP1MovingH)
		{
			isP1MovingH = true;
			buttonsP1 [buttonIdxP1].GetComponent<SpriteManager> ().NextSprite ();
		}
		if (verticalP1 == 0)
		{
			isP1MovingV = false;
		}
		if (horizontalP1 == 0)
		{
			isP1MovingH = false;
		}
	}
}
