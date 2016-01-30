using UnityEngine;


public class Player : MonoBehaviour {
    
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

        if (Input.GetButtonDown("LeftPunch"))
        {
            Debug.Log("LeftPunch");
            leftHand.GetComponent<Rigidbody2D>().AddForce(leftHand.transform.parent.transform.up * force * 1 / cooldownLH, ForceMode2D.Impulse);
            cooldownLH = force;
        }
        else if (Input.GetButtonDown("RightPunch"))
        {
            Debug.Log("RightPunch");
            rightHand.GetComponent<Rigidbody2D>().AddForce(rightHand.transform.parent.transform.up * force * 1 / cooldownRH, ForceMode2D.Impulse);
            cooldownRH = force;
        }
        else if (Input.GetButtonDown("LeftKick"))
        {
            Debug.Log("LeftKick");
            leftFoot.GetComponent<Rigidbody2D>().AddForce(-leftFoot.transform.parent.transform.right * force * 1 / cooldownLF, ForceMode2D.Impulse);
            cooldownLF = force;
        }
        else if (Input.GetButtonDown("RightKick"))
        {
            Debug.Log("RightKick");
            rightFoot.GetComponent<Rigidbody2D>().AddForce(-rightFoot.transform.parent.transform.right * force * 1 / cooldownRF, ForceMode2D.Impulse);
            cooldownRF = force;
        }




    }
}
