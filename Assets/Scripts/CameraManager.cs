using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour {

	public GameObject[] tiles;
	public int distanceThreeshold = 2;

	private Vector3 startPos;

	private GameObject p1;
	private GameObject p2;

	private Vector3 vecRef;

	// Use this for initialization
	void Start ()
	{
		startPos = this.transform.position;
		p1 = GameObject.FindGameObjectWithTag ("Player1");
		p2 = GameObject.FindGameObjectWithTag ("Player2");
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (p1 == null || p2 == null)
		{
			p1 = GameObject.FindGameObjectWithTag ("Player1");
			p2 = GameObject.FindGameObjectWithTag ("Player2");
			return;
		}

		for (int i = 0; i < tiles.Length; i++)
		{
			tiles [i].transform.position = (transform.position - startPos) / (/*tiles.Length -*/ i + 2);
		}
		transform.position = Vector3.SmoothDamp(this.transform.position,
			startPos + new Vector3(Mathf.Clamp(p1.transform.position.x - p2.transform.position.x, -distanceThreeshold, distanceThreeshold), 0, 0),
			ref vecRef, 0.5f);
	}

	public void TiltCamera(float magnitude)
	{
		// O.3f
		StartCoroutine (cameraTilt(magnitude));
	}

	IEnumerator cameraTilt(float magnitude)
	{
		Vector3 currPos = transform.position;
		for (int i = 0; i < 15; i++)
		{
			transform.position = currPos + new Vector3 (Random.Range(-magnitude, magnitude), Random.Range(-magnitude, magnitude), 0);
			yield return new WaitForSeconds (0.01f);
		}
	}
}
