using UnityEngine;
using System.Collections;
using Character;

public class Player : MonoBehaviour {

    //public Vector4 displacementArea = Vector4.zero;

    [Range(1,2)]
    public int player = 1;

    public AdvanceBody abody;

    private Vector3 myPlayerPos, enemyPos;

    private GameManager gM;

    public float speed = 200;
    public float gogopowerjump = 300;

    private bool jumping = false;

    private float oldPositiony;

    // Use this for initialization
    void Start()
    {
        oldPositiony = transform.position.y;
        gM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        abody.Coffee(player, gM);
    }

    // Update is called once per frame
    void Update()
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


    //void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.yellow;

    //    Gizmos.DrawLine(new Vector3(displacementArea.x, displacementArea.y, 0), new Vector3(displacementArea.z, displacementArea.y, 0));
    //    Gizmos.DrawLine(new Vector3(displacementArea.x, displacementArea.y, 0), new Vector3(displacementArea.x, displacementArea.w, 0));
    //    Gizmos.DrawLine(new Vector3(displacementArea.x, displacementArea.w, 0), new Vector3(displacementArea.z, displacementArea.w, 0));
    //    Gizmos.DrawLine(new Vector3(displacementArea.z, displacementArea.y, 0), new Vector3(displacementArea.z, displacementArea.w, 0));
    //}
}
