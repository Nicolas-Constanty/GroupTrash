using UnityEngine;
using System.Collections;

public class TutoManager : MonoBehaviour {

    public HitMeLight hitPart1;
    public HitMeLight hitPart2;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (hitPart1.enoughtHitted() && hitPart2.enoughtHitted())
            Application.LoadLevel("Scene_Nicolas");
	}
}
