using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ProjectileDragging : MonoBehaviour {
	public float maxStretch = 3.0f;
    private int fingerIndex;
    private Vector3 deltaPosition;
    private bool panic = false;
    public GameObject helper;
    public GameObject handHelper;
    private bool panicable;
    private Collider col;
    private bool pull = false;

    public LineRenderer catapultLineFront;
	public LineRenderer catapultLineBack;
    

    private Vector3 originHandPos;

    private SpringJoint2D spring;
	private Transform catapult;
	private Ray rayToMouse;
	private Ray leftCatapultToProjectile;
	private float maxStretchSqr;
	private float circleRadius;
	private bool clickedOn;
	private Vector2 prevVelocity;

    private GameObject hand;
    private GameObject central;
    private Central central_script;
    private CircleCollider2D circle;

    // Subscribe to events
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

   

    void Awake () {

		spring = GetComponent <SpringJoint2D> ();
        
        central = GameObject.FindGameObjectWithTag("Central");
        central_script = central.GetComponent<Central>();
        //hand = GameObject.FindGameObjectWithTag("Hand");
        
        //catapult = hand.transform;
        spring.enabled = false;
    }
	
	void Start () {
		//LineRendererSetup ();
		//rayToMouse = new Ray(catapult.position, Vector3.zero);
	//	leftCatapultToProjectile = new Ray(catapultLineFront.transform.position, Vector3.zero);
		maxStretchSqr = maxStretch * maxStretch;
		circle = GetComponent<Collider2D>() as CircleCollider2D;
		circleRadius = circle.radius;
        
        GetComponent<Rigidbody2D>().isKinematic = false;
        clickedOn = false;
        col = helper.GetComponent<Collider>();

       
    }
	
	void Update () {

        
      /*  if (clickedOn)
			Dragging ();*/
		
       

		if (spring.enabled != false) {
			if (!GetComponent<Rigidbody2D>().isKinematic && prevVelocity.sqrMagnitude > GetComponent<Rigidbody2D>().velocity.sqrMagnitude) {
                //Destroy (spring);
                spring.enabled = false;
				GetComponent<Rigidbody2D>().velocity =1.5f* prevVelocity;
			}

            if (!clickedOn)
            {
                prevVelocity = GetComponent<Rigidbody2D>().velocity;
                if (GetComponent<Rigidbody2D>().isKinematic)
                {
                    transform.position = hand.transform.position;
                }
            }

                //	LineRendererUpdate ();

            } else {
			//catapultLineFront.enabled = false;
			//catapultLineBack.enabled = false;
		}
	}
	
	void LineRendererSetup () {
		catapultLineFront.SetPosition(0, catapultLineFront.transform.position);
		catapultLineBack.SetPosition(0, catapultLineBack.transform.position);
		
		catapultLineFront.sortingLayerName = "Foreground";
		catapultLineBack.sortingLayerName = "Foreground";
		
		catapultLineFront.sortingOrder = 3;
		catapultLineBack.sortingOrder = 1;
	}
	
    /*
	void OnMouseDown () {
        
       // Debug.Log(Input.touchCount);
        
		spring.enabled = false;
        rayToMouse = new Ray(hand.transform.position, Vector3.zero);
        clickedOn = true;
        //Vector3 mouseWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        originHandPos = hand.transform.position;
    }
	
	void OnMouseUp () {
        hand.transform.position = originHandPos;
		spring.enabled = true;
		GetComponent<Rigidbody2D>().isKinematic = false;
		clickedOn = false;
        hand.SendMessage("Throw");
        
        //circle.enabled = false;
        //Invoke("TurnOn", 1);
	}
    */
    public void Panic()
    {
        if (panicable == false)
            return;

        spring.enabled = false;
        rayToMouse = new Ray(hand.transform.position, Vector3.zero);
        clickedOn = true;
        //Vector3 mouseWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        originHandPos = hand.transform.position;

        panic = true;

        Dragging();

        hand.transform.position = originHandPos;
        spring.enabled = true;
        GetComponent<Rigidbody2D>().isKinematic = false;
        clickedOn = false;
        handHelper.SendMessage("PlayAnimationAndReturn", transform);
        hand.SendMessage("Throw");
        circle.enabled = true;
        panicable = false;
        col.enabled = false;
    }
    
    void TurnOn()
    {
        circle.enabled = true;
    }

    void On_Drag(Gesture gesture)
    {
        if (gesture.pickedObject == helper && fingerIndex == gesture.fingerIndex)
        {

            // the world coordinate from touch
            Vector3 mouseWorldPoint = gesture.GetTouchToWorldPoint(gesture.pickedObject.transform.position);
            Vector2 catapultToMouse = mouseWorldPoint - originHandPos;
          //  transform.position = position - deltaPosition;


            if (catapultToMouse.sqrMagnitude > maxStretchSqr)
            {
                rayToMouse.direction = catapultToMouse;
                mouseWorldPoint = rayToMouse.GetPoint(maxStretch);
            }

            mouseWorldPoint.z = 0f;
            transform.position = mouseWorldPoint;
            // handHelper.transform.position = new Vector3(transform.position.x - 0.3f, transform.position.y - 1, 0);

           
        }

        /* if (panic == false)

         {
             Vector3 mouseWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

             Vector2 catapultToMouse = mouseWorldPoint - originHandPos;

             if (catapultToMouse.sqrMagnitude > maxStretchSqr)
             {
                 rayToMouse.direction = catapultToMouse;
                 mouseWorldPoint = rayToMouse.GetPoint(maxStretch);
             }

             mouseWorldPoint.z = 0f;
             transform.position = mouseWorldPoint;
             hand.transform.position = transform.position;
         }
         else
         {
             transform.position = new Vector2(transform.position.x - Random.Range(-1f, 1f), transform.position.y - Random.Range(2f, 4f));
             hand.transform.position = transform.position;
         }*/
    }

    void On_DragStart(Gesture gesture)
    {
       
        if (gesture.pickedObject == helper)
        {

            spring.enabled = false;
            rayToMouse = new Ray(hand.transform.position, Vector3.zero);
            fingerIndex = gesture.fingerIndex;
            Vector3 position = gesture.GetTouchToWorldPoint(gesture.pickedObject.transform.position);
            deltaPosition = position - transform.position;
            originHandPos = hand.transform.position;
        }


        /* 
        spring.enabled = false;
          rayToMouse = new Ray(hand.transform.position, Vector3.zero);
          clickedOn = true;
          //Vector3 mouseWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
          originHandPos = hand.transform.position;
          */
    }

    void On_DragEnd(Gesture gesture)
    {
        if (gesture.pickedObject == helper && fingerIndex == gesture.fingerIndex)
        {
            Debug.Log("DragEnd");
            handHelper.SendMessage("PlayAnimationAndReturn", transform);
            spring.enabled = true;
            GetComponent<Rigidbody2D>().isKinematic = false;
            hand.SendMessage("Throw");
            circle.enabled = true;
            panicable = false;
            col.enabled = false;
            
        }

        /*hand.transform.position = originHandPos;
        spring.enabled = true;
        GetComponent<Rigidbody2D>().isKinematic = false;
        clickedOn = false;
        hand.SendMessage("Throw");*/
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.transform.tag == "Hand" && GetComponent<Rigidbody2D>().velocity.y < 0)
        {
            circle.enabled = false;
            other.gameObject.SendMessage("Catch", gameObject);
            hand = other.gameObject;
            handHelper = hand.transform.GetChild(0).gameObject;
            Catching(other);
            col.enabled = true;
        }
        if(other.transform.tag == "Coin")
        {
            GetCoin(other);
        }
    }

    void GetCoin(Collider2D other)
    {
        Destroy(other.gameObject);
        central_script.IncreaseCoin();
    }

    public void Catching(Collider2D other)
    {
        Invoke("Down", 0.1f);
        StartCoroutine(Up(other));
    }

    public void Down()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - 0.01f, transform.position.z);
    }

    IEnumerator Up (Collider2D other)
    {
        yield return new WaitForSeconds(0.2f);
        transform.position = new Vector3(transform.position.x, transform.position.y + 0.01f, transform.position.z);
        
        spring.connectedBody = other.gameObject.GetComponent<Rigidbody2D>();
        transform.position = other.transform.position;
        GetComponent<Rigidbody2D>().isKinematic = true;
        spring.enabled = true;
        rayToMouse = new Ray(other.transform.position, Vector3.zero);
        
        central_script.Shoot();
        Debug.Log("Catch");
        panicable = true;
        // Now do your thing here
    }

   /* public void Up()
    {
        
    }*/

	void Dragging () {
        if (panic == false)

        {
            Vector3 mouseWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector2 catapultToMouse = mouseWorldPoint - originHandPos;

            if (catapultToMouse.sqrMagnitude > maxStretchSqr)
            {
                rayToMouse.direction = catapultToMouse;
                mouseWorldPoint = rayToMouse.GetPoint(maxStretch);
            }

            mouseWorldPoint.z = 0f;
            transform.position = mouseWorldPoint;
            hand.transform.position = transform.position;
        }
       

        else
        {
            float rand1 = Random.Range(-1.5f, 1.5f);
            float rand2 = Random.Range(1.5f, 3f);
            Debug.Log(rand1);
            Debug.Log(rand2);
            transform.position = new Vector2(transform.position.x - rand1, transform.position.y - rand2);
            hand.transform.position = transform.position;
        }
	}

    public void Pull(Vector2 minus)
    {
        spring.enabled = false;
        rayToMouse = new Ray(hand.transform.position, Vector3.zero);
        clickedOn = true;
        //Vector3 mouseWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        originHandPos = hand.transform.position;

        panic = true;

        PullFrom(minus);

        hand.transform.position = originHandPos;
        spring.enabled = true;
        GetComponent<Rigidbody2D>().isKinematic = false;
        clickedOn = false;
        handHelper.SendMessage("PlayAnimationAndReturn", transform);
        hand.SendMessage("Throw");
        circle.enabled = true;
        panicable = false;
        col.enabled = false;
    }

    void PullFrom(Vector2 minus)
    {
        transform.position = new Vector2(transform.position.x - minus.x, transform.position.y - minus.y);
        hand.transform.position = transform.position;
    }

	void LineRendererUpdate () {
		Vector2 catapultToProjectile = transform.position - catapultLineFront.transform.position;
		leftCatapultToProjectile.direction = catapultToProjectile;
		Vector3 holdPoint = leftCatapultToProjectile.GetPoint(catapultToProjectile.magnitude + circleRadius);
		catapultLineFront.SetPosition(1, holdPoint);
		catapultLineBack.SetPosition(1, holdPoint);
	}
}
