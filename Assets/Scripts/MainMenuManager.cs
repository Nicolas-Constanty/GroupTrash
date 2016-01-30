using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour {

	[SerializeField]
	private Image loadScreen;

	public Sprite[] loadingScreens;

	public void Start()
	{
		loadScreen.enabled = false;
	}

	public void LoadLevel()
	{
		if (loadingScreens.Length > 0)
		{
			loadScreen.enabled = true;
			loadScreen.sprite = loadingScreens [Random.Range (0, loadingScreens.Length - 1)];
			Invoke ("LoadScene", 2);
		}
		else
		{
			LoadScene ();
		}
	}

	void LoadScene()
	{
		SceneManager.LoadScene (1);
	}

	public void Quit()
	{
		#if UNITY_STANDALONE
		Application.Quit ();
		#elif UNITY_WEBPLAYER
		Application.ExternalEval ("window.close");
		#endif
	}
}
