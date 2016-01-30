using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// Gets candies from GM to update the data
public class InterfaceCandies : MonoBehaviour {

	private GameManager gm;
	[SerializeField]
	private Text text;

	[Range(1, 2)]
	public int player = 1;

	void Awake ()
	{
		gm = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GameManager> ();
	}

	void OnEnable()
	{
		text.text = (gm.GetNbCandies (player)).ToString();
	}
}
