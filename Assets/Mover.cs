using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour {

    public bool left = true;
    private bool go_left= false;
    private bool go_right = false;
    public GameObject hand;
   

	// Use this for initialization
	void Start () {
       // hand = GameObject.FindGameObjectWithTag("Hand");
    }
	
	// Update is called once per frame
	void Update () {
        if(go_left)
        {
            if (hand.transform.position.x > -13.5)
            {
                hand.transform.position = new Vector3(hand.transform.position.x - 0.1f, hand.transform.position.y, hand.transform.position.z);
            }
        }
        if (go_right)
        {
            if (hand.transform.position.x < -3)
            {
                hand.transform.position = new Vector3(hand.transform.position.x + 0.1f, hand.transform.position.y, hand.transform.position.z);

            }
        }
    }

    void OnMouseDown()
    {
        if (left)
        {
            Debug.Log("Left");
            go_left = true;
        }
        else
        {
            Debug.Log("Right");
            go_right = true;
        }
    }

    void OnMouseUp()
    {
        if(left)
        {
            go_left = false;
        }
        else
        {
            go_right = false;
        }
    }
}
