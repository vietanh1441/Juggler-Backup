using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

    public float maxStretch = 3.0f;
   // public LineRenderer catapultLineFront;
  //  public LineRenderer catapultLineBack;

    private SpringJoint2D spring;
    private Transform catapult;
    private Ray rayToMouse;
    private Ray leftCatapultToProjectile;
    private float maxStretchSqr;
    private float circleRadius;
    private bool clickedOn;
    private Vector2 prevVelocity;

    // Use this for initialization
    void Start () {
        rayToMouse = new Ray(catapult.position, Vector3.zero);
      //  leftCatapultToProjectile = new Ray(catapultLineFront.transform.position, Vector3.zero);
        maxStretchSqr = maxStretch * maxStretch;
        CircleCollider2D circle = GetComponent<Collider2D>() as CircleCollider2D;
        circleRadius = circle.radius;
    }
	
	// Update is called once per frame
	void Update () {
        if (clickedOn)
            Dragging();
    }

    void Dragging()
    {
        Vector3 mouseWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 catapultToMouse = mouseWorldPoint - catapult.position;

        if (catapultToMouse.sqrMagnitude > maxStretchSqr)
        {
            rayToMouse.direction = catapultToMouse;
            mouseWorldPoint = rayToMouse.GetPoint(maxStretch);
        }

        mouseWorldPoint.z = 0f;
        transform.position = mouseWorldPoint;
    }

    void OnMouseDown()
    {
        clickedOn = true;
    }

    void OnMouseUp()
    {
        GetComponent<Rigidbody2D>().isKinematic = false;
        clickedOn = false;
    }
}
