using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {

	public void LoadLevel()
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
