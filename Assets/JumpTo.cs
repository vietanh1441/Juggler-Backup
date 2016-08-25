using UnityEngine;
using System.Collections;

public class JumpTo : MonoBehaviour {

    public Vector2 pos;

    void Awake()
    {
        pos.y = 10;
        pos.x = Random.Range(-7, 7);
    }

	// Use this for initialization
	void Start () {
        transform.position = pos;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
