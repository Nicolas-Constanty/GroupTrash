using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerSelect : MonoBehaviour {

	public EventSystem ev;

	[SerializeField]
	private GameObject[] buttonsP1;
	private int buttonIdxP1 = 0;

	[SerializeField]
	private GameObject[] buttonsP2;
	private int buttonIdxP2 = 0;

	[Header("Player 1")]
	public List<Sprite> head1;
	public List<Sprite> torso1;
	public List<Sprite> armLeft1;
	public List<Sprite> armRight1;
	public List<Sprite> legLeft1;
	public List<Sprite> legRight1;

	[Header("Player 2")]
	public List<Sprite> head2;
	public List<Sprite> torso2;
	public List<Sprite> armLeft2;
	public List<Sprite> armRight2;
	public List<Sprite> legLeft2;
	public List<Sprite> legRight2;

	private int buttonIdx = 0;

	bool isP1Moving = false;

	void Start ()
	{
		// Selectionne la tête par défaut
		buttonsP1[buttonIdxP1].GetComponent<Image>().color = Color.red;
		buttonsP2 [buttonIdxP2].GetComponent<Image> ().color = Color.red;
	}
	
	void Update ()
	{
		float verticalP1 = Input.GetAxisRaw ("Vertical");
		if (verticalP1 != 0 && !isP1Moving)
		{
			isP1Moving = true;
			Debug.Log ("Vertical axis = " + verticalP1);
			buttonsP1[buttonIdxP1].GetComponent<Image>().color = Color.white;
			buttonIdxP1 -= (int)verticalP1;
			buttonIdxP1 = (buttonIdxP1 < 0) ? buttonsP1.Length - 1 : (buttonIdxP1 > buttonsP1.Length - 1) ? 0 : buttonIdxP1;
			buttonsP1[buttonIdxP1].GetComponent<Image>().color = Color.red;
		}
		if (verticalP1 == 0)
		{
			isP1Moving = false;
		}
	}
}
