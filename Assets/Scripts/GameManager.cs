using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Character;

public class GameManager : MonoBehaviour {

	[SerializeField]
	private GameObject canvasPause;
	[SerializeField]
	private GameObject canvasSelection;
	[SerializeField]
	private GameObject canvasHUD;
	[SerializeField]
	private GameObject canvasWin;
	[SerializeField]
	private GameObject canvasWinText;

	PlayerStats player1 = new PlayerStats();
	PlayerStats player2 = new PlayerStats();

	public GameObject player1Prefab;
	public GameObject player2Prefab;

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

		canvasHUD.SetActive (false);
		canvasPause.SetActive (false);
		canvasWin.SetActive (false);
		SetSelection ();
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

	public void StartGame()
	{
		canvasSelection.SetActive (false);
		canvasHUD.SetActive (true);
		player1.playerObj = (GameObject)Instantiate (player1Prefab);
		player2.playerObj = (GameObject)Instantiate (player2Prefab);
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
			player2.nbVictories++;
		}
		else
		{
			player1.nbVictories++;
		}

		Destroy (player1.playerObj);
		Destroy (player2.playerObj);
		// TODO: add candies and drops
	}

	public void SetSelection()
	{
		canvasSelection.SetActive (true);
	}

	public void SetPlayerParts(int playerID, Dictionary<int, List<Part>> _items)
	{
		if (playerID == 1)
		{
			player1.items = _items;
		}
		else if (playerID == 2)
		{
			player2.items = _items;
		}
	}

	public Dictionary<int, List<Part>> GetPlayerParts(int playerID)
	{
		if (playerID == 1)
		{
			return player1.items;
		}
		else if (playerID == 2)
		{
			return player2.items;
		}
		return null;
	}

	class PlayerStats
	{
		GameObject _playerObj;
		int _nbVictories;
		int _nbCandies;

		Dictionary<int, List<Part>> _items;

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

		public Dictionary<int, List<Part>> items
		{
			get { return _items; }
			set { _items = value; }
		}
	}
}
