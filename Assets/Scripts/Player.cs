using UnityEngine;
using System.Collections;
using Character;

public class Player : MonoBehaviour {

    public Vector4 displacementArea = Vector4.zero;

    [Range(1,2)]
    public int player = 1;

    public AdvanceBody abody;

    private Vector3 myPlayerPos, enemyPos;

    private GameManager gM;

    void Start()
    {
        //if (displacementArea == Vector4.zero)
        //    Debug.LogError("Stp, place des valeurs à Displacement Area dans " + gameObject.name, gameObject);

        //if (player == 1)
        //{
        //    myPlayerPos = GameObject.Find("RagdollP1").transform.GetChild(0).transform.position;
        //    enemyPos = GameObject.Find("RagdollP2").transform.GetChild(0).transform.position;
        //}
        //else
        //{
        //    myPlayerPos = GameObject.Find("RagdollP2").transform.GetChild(0).transform.position;
        //    enemyPos = GameObject.Find("RagdollP1").transform.GetChild(0).transform.position;
        //}

        //gM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        //abody.Coffee(player, gM);
    }

    void Update()
    {
        //foreach (LineRenderer line in gameObject.GetComponentsInChildren<LineRenderer>())
        //{
        //    line.SetPosition(0, line.gameObject.transform.position);
        //    line.SetPosition(1, new Vector3(line.gameObject.transform.position.x, 25, 0));
        //}

        //if ((Input.GetButtonDown("LeftPunch1") && player == 1) ||
        //    (Input.GetButtonDown("LeftPunch2") && player == 2) ||
        //    (Input.GetKeyDown(KeyCode.Joystick1Button3) && player == 1) ||
        //    (Input.GetKeyDown(KeyCode.Joystick2Button3) && player == 2) )
        //{
        //    Debug.Log("LeftPunch");
        //    //leftHand.GetComponent<Rigidbody2D>().AddForce(-leftHand.transform.parent.transform.right * force * 1 / cooldownLH, ForceMode2D.Impulse);
        //    leftHand.GetComponent<Rigidbody2D>().AddForce( (enemyPos-myPlayerPos).normalized * force * 1 / cooldownLH, ForceMode2D.Impulse);
        //    cooldownLH = force;
        //}
        //else if ((Input.GetButtonDown("RightPunch1") && player == 1) ||
        //    (Input.GetButtonDown("RightPunch2") && player == 2) ||
        //    (Input.GetKeyDown(KeyCode.Joystick1Button2) && player == 1) ||
        //    (Input.GetKeyDown(KeyCode.Joystick2Button2) && player == 2))
        //{
        //    Debug.Log("RightPunch");
        //    //rightHand.GetComponent<Rigidbody2D>().AddForce(-rightHand.transform.parent.transform.right * force * 1 / cooldownRH, ForceMode2D.Impulse);
        //    rightHand.GetComponent<Rigidbody2D>().AddForce((enemyPos - myPlayerPos).normalized * force * 1 / cooldownRH, ForceMode2D.Impulse);
        //    cooldownRH = force;
        //}
        //else if ((Input.GetButtonDown("LeftKick1") && player == 1) ||
        //    (Input.GetButtonDown("LeftKick2") && player == 2) ||
        //    (Input.GetKeyDown(KeyCode.Joystick1Button1) && player == 1) ||
        //    (Input.GetKeyDown(KeyCode.Joystick2Button1) && player == 2))
        //{
        //    Debug.Log("LeftKick");
        //    //leftFoot.GetComponent<Rigidbody2D>().AddForce(-leftFoot.transform.parent.transform.right * force * 1 / cooldownLF, ForceMode2D.Impulse);
        //    leftFoot.GetComponent<Rigidbody2D>().AddForce((enemyPos - myPlayerPos).normalized * force * 1 / cooldownLF, ForceMode2D.Impulse);
        //    cooldownLF = force;
        //}
        //else if ((Input.GetButtonDown("RightKick1") && player == 1) ||
        //    (Input.GetButtonDown("RightKick2") && player == 2) ||
        //    (Input.GetKeyDown(KeyCode.Joystick1Button0) && player == 1) ||
        //    (Input.GetKeyDown(KeyCode.Joystick2Button0) && player == 2))
        //{
        //    Debug.Log("RightKick");
        //    //rightFoot.GetComponent<Rigidbody2D>().AddForce(-rightFoot.transform.parent.transform.right * force * 1 / cooldownRF, ForceMode2D.Impulse);
        //    rightFoot.GetComponent<Rigidbody2D>().AddForce((enemyPos - myPlayerPos).normalized * force * 1 / cooldownRF, ForceMode2D.Impulse);
        //    cooldownRF = force;
        //}

        //Vector2 newPos = Vector2.zero;

        //if ((Input.GetAxis("Horizontal1") != 0 && player == 1)||
        //    (Input.GetAxis("Vertical1") != 0 && player == 1))
        //{
        //    newPos = new Vector2(Mathf.Min(Mathf.Max(displacementArea.x, puppeteer.transform.position.x + Input.GetAxis("Horizontal1") / 20), displacementArea.z),
        //        Mathf.Min(Mathf.Max(displacementArea.y, puppeteer.transform.position.y + Input.GetAxis("Vertical1") / 20), displacementArea.w));
        //}
        //if ((Input.GetAxis("Horizontal2") != 0 && player == 2) ||
        //    (Input.GetAxis("Vertical2") != 0 && player == 2))
        //{
        //    newPos = new Vector2(Mathf.Min(Mathf.Max(displacementArea.x, puppeteer.transform.position.x + Input.GetAxis("Horizontal2") / 20), displacementArea.z),
        //        Mathf.Min(Mathf.Max(displacementArea.y, puppeteer.transform.position.y + Input.GetAxis("Vertical2") / 20), displacementArea.w));
        //}
        //if(newPos != Vector2.zero)
        //    puppeteer.transform.position = newPos;
    }
    

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawLine(new Vector3(displacementArea.x, displacementArea.y, 0), new Vector3(displacementArea.z, displacementArea.y, 0));
        Gizmos.DrawLine(new Vector3(displacementArea.x, displacementArea.y, 0), new Vector3(displacementArea.x, displacementArea.w, 0));
        Gizmos.DrawLine(new Vector3(displacementArea.x, displacementArea.w, 0), new Vector3(displacementArea.z, displacementArea.w, 0));
        Gizmos.DrawLine(new Vector3(displacementArea.z, displacementArea.y, 0), new Vector3(displacementArea.z, displacementArea.w, 0));
    }
}
