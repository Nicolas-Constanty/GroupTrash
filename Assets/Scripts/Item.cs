using UnityEngine;
using Character;

public class Item : MonoBehaviour {

    private float timer = 0f;
    private Part _part = new Part();
    public GameObject player;
    public Part Part
    {
        get { return _part; }
        set {   _part.damage = value.damage;
                _part.hp = value.hp;
                _part.candies = value.candies;
                _part.special = value.special;
                _part.speed = value.speed;
                _part.sprite = value.sprite;
                _part.spriteParts = value.spriteParts;
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (_part.hp < 0)
            return;

        if ((coll.gameObject.layer == LayerMask.NameToLayer("Player1") || coll.gameObject.layer == LayerMask.NameToLayer("Player2")) && timer < 0.05)
        {
            Item hitted = coll.gameObject.GetComponentInChildren<Item>();
            float magnitude = transform.parent.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude;

            if (coll.transform.parent.GetChild(0).GetComponentInChildren<Rigidbody2D>().velocity.magnitude < magnitude)
                hitted.Part.hp -= (int)magnitude + Part.damage;

            if (hitted.Part.hp < 0)
            {
                Transform parent = hitted.transform.parent;
                foreach (HingeJoint2D joint in parent.GetComponentsInChildren<HingeJoint2D>())
                {
                    if (joint.gameObject.name.Equals("neck") || joint.gameObject.name.Equals("hip"))
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
