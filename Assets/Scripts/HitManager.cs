using UnityEngine;
using System.Collections;

public class HitManager : MonoBehaviour
{
    public Player player;

    public float life = 100;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.layer == 8 || coll.gameObject.layer == 9)
        {
            HitManager hitted = coll.gameObject.GetComponentInChildren<HitManager>();
            Debug.Log(hitted.life);
            hitted.life -= coll.transform.parent.GetChild(0).GetComponentInChildren<Rigidbody2D>().velocity.magnitude;

            //TODO timer ?
            if(hitted.life < 0)
            {
                foreach(HingeJoint2D joint in hitted.transform.parent.GetComponentsInChildren<HingeJoint2D>())
                {
                    Destroy(joint);
                }
            }
        }
    }

}
