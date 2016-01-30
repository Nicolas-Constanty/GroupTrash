using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Interface wins.
/// Sets skulls gauge according to player victories.
/// </summary>
public class InterfaceWins : MonoBehaviour {

	private Image[] sprites;

	[Range(1, 2)]
	public int player = 1;

	void OnEnable ()
	{
		int i = 0;
		sprites = new Image[gameObject.transform.childCount];
		foreach (Image s in GetComponentsInChildren<Image>())
		{
			if (i > 0)
				sprites [i - 1] = s;
			++i;
		}

		int wins = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GameManager> ().GetPlayerVictories (player);
		for (i = 0; i < wins; i++)
		{
			sprites [i].color = (player == 1) ? Color.red : Color.green;
		}
	}
}
