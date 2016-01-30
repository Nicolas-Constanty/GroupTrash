using UnityEngine;
using System.Collections;
using Character;

public class Player : MonoBehaviour {

    public Vector4 displacementArea = Vector4.zero;

    [Range(1,2)]
    public int player = 1;

    public GameObject leftHand;
    public GameObject rightHand;
    public GameObject leftFoot;
    public GameObject rightFoot;

    public int force = 100;

    private float cooldownLH = 1;
    private float cooldownRH = 1;
    private float cooldownLF = 1;
    private float cooldownRF = 1;

    public Body body;

    public GameObject puppeteer;

    private Vector3 myPlayerPos, enemyPos;

    private GameManager gM;

    void Start()
    {
        if (displacementArea == Vector4.zero)
            Debug.LogError("Stp, place des valeurs à Displacement Area dans " + gameObject.name, gameObject);

        if (leftHand == null)
            Debug.LogError("Stp, place la main gauche du perso dans " + gameObject.name, gameObject);
        if (rightHand == null)
            Debug.LogError("Stp, place la main droite du perso dans " + gameObject.name, gameObject);
        if (leftFoot == null)
            Debug.LogError("Stp, place le pied gauche du perso dans " + gameObject.name, gameObject);
        if (rightFoot == null)
            Debug.LogError("Stp, place le pied droit du perso dans " + gameObject.name, gameObject);

        if(puppeteer == null)
            Debug.LogError("Stp, place P"+player + " dans puppeteer dans l'objet : " + gameObject.name, gameObject);

        //body = new Body(new Part(), new Part(), new Part(), new Part(), new Part(), new Part());


        if (player == 1)
        {
            myPlayerPos = GameObject.Find("RagdollP1").transform.GetChild(0).transform.position;
            enemyPos = GameObject.Find("RagdollP2").transform.GetChild(0).transform.position;
        }
        else
        {
            myPlayerPos = GameObject.Find("RagdollP2").transform.GetChild(0).transform.position;
            enemyPos = GameObject.Find("RagdollP1").transform.GetChild(0).transform.position;
        }

        gM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        SetMember();

        Debug.Log("found hip at : ", gameObject.transform.Find("kneeL").gameObject);
    }

    private void SetMember()
    {
        body.body = gM.GetPlayerPart(player, (int)PART.BODY);
        body.head = gM.GetPlayerPart(player, (int)PART.HEAD);
        body.leftArm = gM.GetPlayerPart(player, (int)PART.LEFTARM);
        body.rightArm = gM.GetPlayerPart(player, (int)PART.RIGHTARM);
        body.leftArm = gM.GetPlayerPart(player, (int)PART.LEFTLEG);
        body.rightArm = gM.GetPlayerPart(player, (int)PART.RIGHTLEG);


    }

    void Update()
    {
        if (cooldownLH > 1)
            cooldownLH = Mathf.Max(1, cooldownLH - Time.deltaTime * (force/2));
        if (cooldownRH > 1)
            cooldownRH = Mathf.Max(1, cooldownRH - Time.deltaTime * (force / 2));
        if (cooldownLF > 1)
            cooldownLF = Mathf.Max(1, cooldownLF - Time.deltaTime * (force / 2));
        if (cooldownRF > 1)
            cooldownRF = Mathf.Max(1, cooldownRF - Time.deltaTime * (force / 2));



        if ((Input.GetButtonDown("LeftPunch1") && player == 1) ||
            (Input.GetButtonDown("LeftPunch2") && player == 2) ||
            (Input.GetKeyDown(KeyCode.Joystick1Button3) && player == 1) ||
            (Input.GetKeyDown(KeyCode.Joystick2Button3) && player == 2) )
        {
            Debug.Log("LeftPunch");
            //leftHand.GetComponent<Rigidbody2D>().AddForce(-leftHand.transform.parent.transform.right * force * 1 / cooldownLH, ForceMode2D.Impulse);
            leftHand.GetComponent<Rigidbody2D>().AddForce( (enemyPos-myPlayerPos).normalized * force * 1 / cooldownLH, ForceMode2D.Impulse);
            cooldownLH = force;
        }
        else if ((Input.GetButtonDown("RightPunch1") && player == 1) ||
            (Input.GetButtonDown("RightPunch2") && player == 2) ||
            (Input.GetKeyDown(KeyCode.Joystick1Button2) && player == 1) ||
            (Input.GetKeyDown(KeyCode.Joystick2Button2) && player == 2))
        {
            Debug.Log("RightPunch");
            //rightHand.GetComponent<Rigidbody2D>().AddForce(-rightHand.transform.parent.transform.right * force * 1 / cooldownRH, ForceMode2D.Impulse);
            rightHand.GetComponent<Rigidbody2D>().AddForce((enemyPos - myPlayerPos).normalized * force * 1 / cooldownRH, ForceMode2D.Impulse);
            cooldownRH = force;
        }
        else if ((Input.GetButtonDown("LeftKick1") && player == 1) ||
            (Input.GetButtonDown("LeftKick2") && player == 2) ||
            (Input.GetKeyDown(KeyCode.Joystick1Button1) && player == 1) ||
            (Input.GetKeyDown(KeyCode.Joystick2Button1) && player == 2))
        {
            Debug.Log("LeftKick");
            //leftFoot.GetComponent<Rigidbody2D>().AddForce(-leftFoot.transform.parent.transform.right * force * 1 / cooldownLF, ForceMode2D.Impulse);
            leftFoot.GetComponent<Rigidbody2D>().AddForce((enemyPos - myPlayerPos).normalized * force * 1 / cooldownLF, ForceMode2D.Impulse);
            cooldownLF = force;
        }
        else if ((Input.GetButtonDown("RightKick1") && player == 1) ||
            (Input.GetButtonDown("RightKick2") && player == 2) ||
            (Input.GetKeyDown(KeyCode.Joystick1Button0) && player == 1) ||
            (Input.GetKeyDown(KeyCode.Joystick2Button0) && player == 2))
        {
            Debug.Log("RightKick");
            //rightFoot.GetComponent<Rigidbody2D>().AddForce(-rightFoot.transform.parent.transform.right * force * 1 / cooldownRF, ForceMode2D.Impulse);
            rightFoot.GetComponent<Rigidbody2D>().AddForce((enemyPos - myPlayerPos).normalized * force * 1 / cooldownRF, ForceMode2D.Impulse);
            cooldownRF = force;
        }

        Vector2 newPos = Vector2.zero;

        if ((Input.GetAxis("Horizontal1") != 0 && player == 1)||
            (Input.GetAxis("Vertical1") != 0 && player == 1))
        {
            newPos = new Vector2(Mathf.Min(Mathf.Max(displacementArea.x, puppeteer.transform.position.x + Input.GetAxis("Horizontal1") / 20), displacementArea.z),
                Mathf.Min(Mathf.Max(displacementArea.y, puppeteer.transform.position.y + Input.GetAxis("Vertical1") / 20), displacementArea.w));
        }
        if ((Input.GetAxis("Horizontal2") != 0 && player == 2) ||
            (Input.GetAxis("Vertical2") != 0 && player == 2))
        {
            newPos = new Vector2(Mathf.Min(Mathf.Max(displacementArea.x, puppeteer.transform.position.x + Input.GetAxis("Horizontal2") / 20), displacementArea.z),
                Mathf.Min(Mathf.Max(displacementArea.y, puppeteer.transform.position.y + Input.GetAxis("Vertical2") / 20), displacementArea.w));
        }
        if(newPos != Vector2.zero)
            puppeteer.transform.position = newPos;
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
