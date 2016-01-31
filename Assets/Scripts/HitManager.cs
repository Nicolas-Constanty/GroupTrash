using UnityEngine;
using System.Collections;

public class HitManager : MonoBehaviour
{
    public Player player;

    public float life = 100f;

    //private bool detach = false;


    private float timer = 0f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //if (life < 0f && !detach)
        //{
        //    Destroy(transform.parent.gameObject.GetComponent<HingeJoint2D>());
        //    Destroy(transform.gameObject.GetComponent<HingeJoint2D>());
        //    detach = true;
        //}
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (life < 0f)
            return;

        if ((coll.gameObject.layer == LayerMask.NameToLayer("Player1") || coll.gameObject.layer == LayerMask.NameToLayer("Player2")) && timer < 0.05 )
        {
            HitManager hitted = coll.gameObject.GetComponentInChildren<HitManager>();
            float magnitude = coll.transform.parent.GetChild(0).GetComponentInChildren<Rigidbody2D>().velocity.magnitude;

            if (transform.parent.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude > magnitude)
                hitted.life -= magnitude;

            if (hitted.life < 0f)
            {
                Transform parent = hitted.transform.parent;
                foreach (HingeJoint2D joint in parent.GetComponentsInChildren<HingeJoint2D>())
                {
                    if(joint.gameObject.name.Equals("neck") || joint.gameObject.name.Equals("hip"))
                    {
                        GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().SetWin(player.gameObject);
                    }

                    Destroy(joint);
                }
                foreach (FixedJoint2D joint in parent.GetComponentsInChildren<FixedJoint2D>())
                {
                    if (joint.gameObject.name.Equals("neck") || joint.gameObject.name.Equals("hip"))
                    {
                        GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().SetWin(player.gameObject);
                    }
                    Destroy(joint);
                }
            }

            timer = 0f;
        }

        timer += Time.deltaTime;
    }
}
