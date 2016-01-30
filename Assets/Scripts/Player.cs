using UnityEngine;


public class Player : MonoBehaviour {
    public Vector4 displacementArea = Vector4.zero;


    public GameObject leftHand;
    public GameObject rightHand;
    public GameObject leftFoot;
    public GameObject rightFoot;

    public int force = 10;

    public float cooldownLH = 1;
    public float cooldownRH = 1;
    public float cooldownLF = 1;
    public float cooldownRF = 1; 

	void Start () {
        if (displacementArea == Vector4.zero)
            Debug.LogError("Stp met des valeurs à Displacement Area dans " + gameObject.name, gameObject);

        if(leftHand == null)
            Debug.LogError("Stp met la main gauche du perso dans " + gameObject.name, gameObject);
        if (rightHand == null)
            Debug.LogError("Stp met la main droite du perso dans " + gameObject.name, gameObject);
        if (leftFoot == null)
            Debug.LogError("Stp met le pied gauche du perso dans " + gameObject.name, gameObject);
        if (rightFoot == null)
            Debug.LogError("Stp met le pied droit du perso dans " + gameObject.name, gameObject);
    }
	

	void Update () {
        if (cooldownLH > 1)
            cooldownLH = Mathf.Max(1, cooldownLH - Time.deltaTime * 50);
        if (cooldownRH > 1)
            cooldownRH = Mathf.Max(1, cooldownRH - Time.deltaTime * 50);
        if (cooldownLF > 1)
            cooldownLF = Mathf.Max(1, cooldownLF - Time.deltaTime * 50);
        if (cooldownRF > 1)
            cooldownRF = Mathf.Max(1, cooldownRF - Time.deltaTime * 50);

        if (Input.GetButtonDown("LeftPunch") || Input.GetKeyDown(KeyCode.JoystickButton3))
        {
            Debug.Log("LeftPunch");
            leftHand.GetComponent<Rigidbody2D>().AddForce(-leftHand.transform.parent.transform.right * force * 1 / cooldownLH, ForceMode2D.Impulse);
            cooldownLH = force;
        }
        else if (Input.GetButtonDown("RightPunch") || Input.GetKeyDown(KeyCode.JoystickButton2))
        {
            Debug.Log("RightPunch");
            rightHand.GetComponent<Rigidbody2D>().AddForce(-rightHand.transform.parent.transform.right * force * 1 / cooldownRH, ForceMode2D.Impulse);
            cooldownRH = force;
        }
        else if (Input.GetButtonDown("LeftKick") || Input.GetKeyDown(KeyCode.JoystickButton1))
        {
            Debug.Log("LeftKick");
            leftFoot.GetComponent<Rigidbody2D>().AddForce(-leftFoot.transform.parent.transform.right * force * 1 / cooldownLF, ForceMode2D.Impulse);
            cooldownLF = force;
        }
        else if (Input.GetButtonDown("RightKick") || Input.GetKeyDown(KeyCode.JoystickButton0))
        {
            Debug.Log("RightKick");
            rightFoot.GetComponent<Rigidbody2D>().AddForce(-rightFoot.transform.parent.transform.right * force * 1 / cooldownRF, ForceMode2D.Impulse);
            cooldownRF = force;
        }

        if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            Vector2 newPos = new Vector2(Mathf.Min(Mathf.Max(displacementArea.x, transform.position.x + Input.GetAxis("Horizontal")/20 ), displacementArea.z),
                Mathf.Min(Mathf.Max(displacementArea.y, transform.position.y + Input.GetAxis("Vertical") / 20), displacementArea.w));
            transform.position = newPos;
        }

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
