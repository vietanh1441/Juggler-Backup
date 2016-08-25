using UnityEngine;
using System.Collections;

public class Hand : MonoBehaviour {

    public bool left;
    public GameObject otherHand;
    public int heat = 0;
    Collider2D col;
    public GameObject ball;
    public GameObject helper3D;
    public HandHelper handHelper;
    private int fingerIndex;
    private GameObject gameController;
    private Controller controller_scr;
    private bool move = false;
    private Vector3 moveTarget;

    void OnEnable()
    {
        EasyTouch.On_Drag += On_Drag;
        EasyTouch.On_DragStart += On_DragStart;
        EasyTouch.On_DragEnd += On_DragEnd;
    }

    void OnDisable()
    {
        UnsubscribeEvent();
    }

    void OnDestroy()
    {
        UnsubscribeEvent();
    }

    void UnsubscribeEvent()
    {
        EasyTouch.On_Drag -= On_Drag;
        EasyTouch.On_DragStart -= On_DragStart;
        EasyTouch.On_DragEnd -= On_DragEnd;
    }


    // Use this for initialization
    void Start () {
        handHelper = gameObject.transform.GetChild(0).gameObject.GetComponent<HandHelper>();
        col = GetComponent<Collider2D>();
        gameController = GameObject.FindGameObjectWithTag("GameController");
        controller_scr = gameController.GetComponent<Controller>();
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.position.x < -8)
        {
            transform.position = new Vector2(-8f, transform.position.y);
        }
        if (transform.position.x > 9)
        {
            transform.position = new Vector2(9f, transform.position.y);
        }
        if(move)
        {
            float step = 1 * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, moveTarget, step);
            if (Vector3.Distance(transform.position, moveTarget)<0.1f)
            {
                move = false;
            }
        }
    }

    public void GoLeft()
    {
        transform.position = new Vector3(transform.position.x - 0.1f, transform.position.y, transform.position.z);
    }

    public void GoRight()
    {
        transform.position = new Vector3(transform.position.x + 0.1f, transform.position.y, transform.position.z);
    }

    public void Throw()
    {
        Debug.Log("Throw");
        col.enabled = true;
        ball = null;
        if (left)
            controller_scr.LeftHandThrow();
        else
            controller_scr.RightHandThrow();
    }

    public void Panic()
    {
        ball.SendMessage("Panic");
    }

    public void ThrowOrder(Vector2 minus)
    {
        if (ball == null)
            return;

        ball.GetComponent<ProjectileDragging>().Pull(minus);
    }

    //How does it catch?
    public void Catch(GameObject g)
    {
       
            ball = g;
            col.enabled = false;
            handHelper.following = true;
            handHelper.tr = g.transform;
            handHelper.PlayClose();
            if (left)
                controller_scr.LeftHandCatch();
            else
                controller_scr.RightHandCatch();
            heat++;
            if(heat == 3)
        {
            transform.tag = "Tired";
        }
        otherHand.SendMessage("ResetHeat");
        
    }

    public void ResetHeat()
    {
        heat = 0;
        transform.tag = "Hand";
    }

    void On_DragStart(Gesture gesture)
    {
        if(gesture.pickedObject == helper3D)
        {
            fingerIndex = gesture.fingerIndex;
            Vector3 mouseWorldPoint = gesture.GetTouchToWorldPoint(gesture.pickedObject.transform.position);
            mouseWorldPoint.z = 0f;
            mouseWorldPoint.y = -5.6f;
            // transform.position = mouseWorldPoint;
            transform.position = mouseWorldPoint;
        }
    }

    void On_Drag(Gesture gesture)
    {
        if (gesture.pickedObject == helper3D && fingerIndex == gesture.fingerIndex)
        {
            Vector3 mouseWorldPoint = gesture.GetTouchToWorldPoint(gesture.pickedObject.transform.position);
            mouseWorldPoint.z = 0f;
            mouseWorldPoint.y = -5.6f;
            // transform.position = mouseWorldPoint;
            transform.position = mouseWorldPoint;
        }
    }

    void On_DragEnd(Gesture gesture)
    {
        if (gesture.pickedObject == helper3D && fingerIndex == gesture.fingerIndex)
        {
            Vector3 mouseWorldPoint = gesture.GetTouchToWorldPoint(gesture.pickedObject.transform.position);
            mouseWorldPoint.z = 0f;
            mouseWorldPoint.y = -5.6f;
            // transform.position = mouseWorldPoint;
            transform.position = mouseWorldPoint;
        }
    }

    public void MoveDistance(Vector2 dist)
    {
        moveTarget = new Vector3(transform.position.x - dist.x, transform.position.y, transform.position.z);
        move = true;
    }

}
