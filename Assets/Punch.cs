using UnityEngine;
using System.Collections;

public class Punch : MonoBehaviour {

    [Range(1, 2)]
    public int player = 1;
    public string control;
    public float strenght = 10;
    private Transform target;
    public Material mat;
	// Use this for initialization
    private float   _cooldown;
    private bool    _canAttack = true;

    LineRenderer line;
	void Start () {
        _cooldown = 1 - (0.2f * transform.parent.GetComponent<Item>().Part.speed);
        gameObject.AddComponent<Rigidbody2D>();
        line = gameObject.AddComponent<LineRenderer>();
        line.material = mat;
        line.SetWidth(0.02f, 0.02f);
        line.SetPosition(0, transform.position);
        line.SetPosition(1, new Vector2(0, 25));
        FixedJoint2D join = gameObject.AddComponent<FixedJoint2D>();
        join.connectedBody = transform.parent.GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player" + player).transform.FindChild("Head");
    }
	
	// Update is called once per frame
	void Update () {
        line.SetPosition(0, transform.position);
        line.SetPosition(1, transform.position + new Vector3(0, 25, 0));
        if (Input.GetAxis(control) != 0 && _canAttack)
        {
            if (target != null)
                GetComponent<Rigidbody2D>().AddForce((target.transform.position - transform.position).normalized * strenght * 5, ForceMode2D.Impulse);
            StartCoroutine(waitCD());
        }
	}

    IEnumerator waitCD()
    {
        _canAttack = false;
        yield return new WaitForSeconds(_cooldown);
        _canAttack = true;
    }
}
