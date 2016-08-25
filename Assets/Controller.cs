using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {

    public GameObject left_hand;
    public GameObject right_hand;
    private Hand leftHandScr;
    private Hand rightHandScr;
    private bool left_available = false;
    private bool right_available = false;


	// Use this for initialization
	void Start () {
        leftHandScr = left_hand.GetComponent<Hand>();
        rightHandScr = right_hand.GetComponent<Hand>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void LeftHandCatch()
    {
        left_available = true;
        //Invoke("Control", 0.5f);
    }

    public void Control()
    {
        leftHandScr.ThrowOrder(new Vector2(0, 2));
    }

    public void RightHandCatch()
    {
        right_available = true;
    }

    public void RightHandThrow()
    {
        right_available = false;
    }

    public void LeftHandThrow()
    {
        left_available = false;

    }
}
