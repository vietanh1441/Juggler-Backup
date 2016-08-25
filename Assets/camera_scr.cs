using UnityEngine;
using System.Collections;

public class camera_scr : MonoBehaviour {

    bool go_to = false;
    public Vector3 camera_o = new Vector3(0.3f, 3.9f, -10);
    public Vector3 camera_d = new Vector3(0.3f, 20, -10);
    private float smooth = 1.5f;
    private Vector3 newPos;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (go_to == false)
            return;
        transform.position = Vector3.Lerp(transform.position, newPos, smooth * Time.deltaTime);
        if(Mathf.Abs (Vector3.Distance(transform.position,newPos))<0.1f)
        {
            go_to = false;
        }

    }


    //Go to origin with smooth slowly speed, change 
    void GoToOrigin()
    {
        go_to = true;
        newPos = camera_o;
        smooth = 1.5f;
    }

    void GoToPause()
    {
        go_to = true;
        newPos = camera_d;
        smooth = 4f;
    }
}
