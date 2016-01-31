using UnityEngine;
using System.Collections;

public class Translate : MonoBehaviour {

    public float speed = 200;
    public float gogopowerjump = 300;
    [Range(1, 2)]
    public int player = 1;

    private bool jumping = false;

    private float oldPositiony;

	// Use this for initialization
	void Start () {
        oldPositiony = transform.position.y;
    }
	
	// Update is called once per frame
	void Update ()
    {
        Rigidbody2D rigid = GetComponent<Rigidbody2D>();
        float jmp = Input.GetAxis("Jump" + player);

        rigid.velocity = new Vector2(speed * Input.GetAxis("Horizontal" + player) * Time.deltaTime, Mathf.SmoothStep(rigid.velocity.y, 0, 0.1f));

        if (jmp > 0.1f && !jumping)
        {
            rigid.velocity += new Vector2(0, gogopowerjump);
            StartCoroutine(jumpCorr());
        }

        transform.position = new Vector3(transform.position.x, Mathf.SmoothStep(transform.position.y, oldPositiony, 0.1f));

        

    }

    public IEnumerator jumpCorr()
    {
        jumping = true;
        yield return new WaitForSeconds(1);
        jumping = false;
    }
}
