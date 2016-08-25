using UnityEngine;
using System.Collections;

public class HandHelper : MonoBehaviour {
    private Vector3 home;
    public bool right = true;
    public bool following = false;
    public GameObject fingers;
    public Sprite[] sprite;
    // 0 = open
    // 1 = half
    // 2 = close
    private SpriteRenderer self;
    private LineRenderer ln;

    public Transform tr;
	// Use this for initialization
	void Start () {
        self = gameObject.GetComponent<SpriteRenderer>();
        fingers = transform.GetChild(0).gameObject;
        ln = gameObject.GetComponent<LineRenderer>();
        
	}
	
	// Update is called once per frame
	void Update () {
	    if(following)
        {
            if(right)
              transform.position = new Vector3(tr.position.x - 0.3f, tr.position.y - 1, 0); 
            else
                transform.position = new Vector3(tr.position.x + 0.3f, tr.position.y - 1, 0);
        }
        ln.SetPosition(0, transform.parent.position);
        ln.SetPosition(1, transform.position);

    }

    void PlayAnimationAndReturn(Transform t)
    {
        tr = t;
        following = true;
        //Play animation
        PlayOpen();

        //Return to position at the end 
        Invoke("ReturnPosition", 0.5f);
    }

    public void PlayOpen()
    {
        Invoke("Half", 0.1f);
        Invoke("Open", 0.2f);
        
    }

    public void PlayClose()
    {
        Invoke("Half", 0.1f);
        Invoke("Close", 0.2f);
    }

    void Open()
    {
        self.sprite = sprite[0];
        fingers.SetActive(false);
    }

    void Close()
    {
        self.sprite = sprite[2];
        fingers.SetActive(true);
    }

    void Half()
    {
        self.sprite = sprite[1];

    }

    void ReturnPosition()
    {
        transform.localPosition = new Vector3(-0.3f,-1,0);
        following = false;
    }
}
