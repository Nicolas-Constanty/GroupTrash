using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Character;
using UnityEngine.UI;

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

	[SerializeField]
	private Text timer;
	[SerializeField]
	private int waitTime = 30;
	private int remainingTime;
	private float decreaseTime = 0;

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

    void Awake()
    {
        player1.items.Add((int)PART.HEAD, new List<Part>());
        player1.items.Add((int)PART.BODY, new List<Part>());
        player1.items.Add((int)PART.LEFTARM, new List<Part>());
        player1.items.Add((int)PART.RIGHTARM, new List<Part>());
        player1.items.Add((int)PART.LEFTLEG, new List<Part>());
        player1.items.Add((int)PART.RIGHTLEG, new List<Part>());

        player2.items.Add((int)PART.HEAD, new List<Part>());
        player2.items.Add((int)PART.BODY, new List<Part>());
        player2.items.Add((int)PART.LEFTARM, new List<Part>());
        player2.items.Add((int)PART.RIGHTARM, new List<Part>());
        player2.items.Add((int)PART.LEFTLEG, new List<Part>());
        player2.items.Add((int)PART.RIGHTLEG, new List<Part>());
    }

	void Start ()
	{
		state = GameState.SELECTION;
		lastState = state;

		remainingTime = waitTime;

		player1.playerObj = GameObject.FindGameObjectWithTag ("Player1");
		player2.playerObj = GameObject.FindGameObjectWithTag ("Player2");

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

		if (state == GameState.SELECTION)
		{
			if (remainingTime > 0)
			{
				remainingTime = waitTime - (int)decreaseTime;
				decreaseTime += Time.deltaTime;
				timer.text = remainingTime.ToString ();
			}
			else
			{
				decreaseTime = 0;
				timer.text = "Go !";
				StartGame ();
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

		if (Input.GetKeyDown (KeyCode.A))
		{
			SetWin (player1.playerObj);
		}
	}

	public void StartGame()
	{
		canvasSelection.SetActive (false);
		canvasHUD.SetActive (true);
		player1.playerObj = (GameObject)Instantiate (player1Prefab);
		player2.playerObj = (GameObject)Instantiate (player2Prefab);
		state = GameState.PLAYING;
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

	public void SetWin(GameObject player)
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
		state = GameState.SELECTION;
		remainingTime = waitTime;
		decreaseTime = 0;
		canvasHUD.SetActive (false);
		canvasPause.SetActive (false);
		canvasWin.SetActive (false);		
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

	public int GetNbCandies(int playerID)
	{
		if (playerID == 1)
		{
			return player1.nbCandies;
		}
		else if (playerID == 2)
		{
			return player2.nbCandies;
		}
		return 0;
	}

	class PlayerStats
	{
		GameObject _playerObj = null;
		int _nbVictories = 0;
		int _nbCandies = 0;

		Dictionary<int, List<Part>> _items = new Dictionary<int, List<Part>>();

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
