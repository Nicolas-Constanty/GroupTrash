using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.UI;
using Character;

public class PlayerSelect : MonoBehaviour {

    //public enum VISUAL { HEAD, LEFTARM, BODY, RIGHTARM, LEFTLEG, RIGHTLEG };
    [Range(1, 2)]
    public int control;

	[SerializeField]
	private GameObject[] buttons;
	private int buttonIdx = 0;
    public int Index
    {
        get { return buttonIdx; }
        set { buttonIdx = value; }
    }
   

    private bool isP1MovingH = false;
	private bool isP1MovingV = false;

	void Start ()
	{
		// Selectionne la tête par défaut
		buttons[buttonIdx].GetComponent<Image>().color = Color.red;
	}
	
	void Update ()
	{
		MoveCursor ();
	}

	void MoveCursor()
	{
		float verticalP1 = Input.GetAxisRaw ("Vertical" + control.ToString());
		float horizontalP1 = Input.GetAxisRaw ("Horizontal" + control.ToString());

		if (verticalP1 != 0 && !isP1MovingV)
		{
			isP1MovingV = true;
			buttons[buttonIdx].GetComponent<Image>().color = Color.white;
			buttonIdx -= (int)verticalP1;
			buttonIdx = (buttonIdx < 0) ? buttons.Length - 1 : (buttonIdx > buttons.Length - 1) ? 0 : buttonIdx;
			buttons[buttonIdx].GetComponent<Image>().color = Color.red;
		}
		if (horizontalP1 != 0 && !isP1MovingH)
		{
			isP1MovingH = true;
			buttons [buttonIdx].GetComponent<SpriteManager> ().NextSprite ();
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
