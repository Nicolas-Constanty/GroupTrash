using UnityEngine;
using System.Collections;

public class Translate : MonoBehaviour {

    public float speed = 5;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
           GetComponent<Rigidbody2D>().velocity = new Vector2(speed * Input.GetAxis("Horizontal") * Time.deltaTime, 0);
    }
}
