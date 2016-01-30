using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	[SerializeField]
	private GameObject canvasPause;
	[SerializeField]
	private GameObject canvasSelection;
	[SerializeField]
	private GameObject canvasHUD;
	[SerializeField]
	private GameObject canvasWin;

	PlayerStats player1;
	PlayerStats player2;

	[HideInInspector]
	public GameState state;
	private GameState lastState;

	// Enum for current game state
	public enum GameState
	{
		SELECTION,
		PLAYING,
		PAUSE,
		WIN
	}

	void Start ()
	{
		state = GameState.SELECTION;
		lastState = state;

		player1.playerObj = GameObject.FindGameObjectWithTag ("Player1");
		player2.playerObj = GameObject.FindGameObjectWithTag ("Player2");
	}
	
	void Update ()
	{
		for (int i = 0; i < 20; i++)
		{
			if (Input.GetKeyDown ("joystick 1 button " + i))
			{
				Debug.Log ("Button " + i + " pressed !");
			}
		}

		// Sets pause
		if (Input.GetKeyDown ("joystick 1 button 9"))
		{
			SetPause ();
		}

		if (player1 == null)
		{
			player1.playerObj = GameObject.FindGameObjectWithTag ("Player1");
		}

		if (player2 == null)
		{
			player2.playerObj = GameObject.FindGameObjectWithTag ("Player2");
		}
	}

	void SetPause ()
	{
		if (canvasPause.activeSelf)
		{
			canvasPause.SetActive (false);
			Time.timeScale = 1;
			state = lastState;
		}
		else
		{
			canvasPause.SetActive (true);
			Time.timeScale = 0;
			lastState = state;
			state = GameState.PAUSE;
		}
	}

	public void SetWin(Player player)
	{
		lastState = state;
		state = GameState.WIN;
		canvasWin.SetActive (true);

		if (player == player1.playerObj)
		{
		}
		else
		{

		}
	}

	class PlayerStats
	{
		GameObject _playerObj;
		int _nbVictories;
		int _nbCandies;

		public GameObject playerObj
		{
			get { return _playerObj; }
			set { _playerObj = value; }
		}

		public int nbVictories
		{
			get { return _nbVictories; }
			set { _nbVictories = value; }
		}

		public int nbCandies
		{
			get { return _nbCandies; }
			set { _nbCandies = value; }
		}
	}
}
