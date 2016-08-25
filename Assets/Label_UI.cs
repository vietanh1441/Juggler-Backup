using UnityEngine;
using System.Collections;

public class Label_UI : MonoBehaviour {
    private UILabel label;
    Central central_scr;
	// Use this for initialization
	void Start () {
        label = gameObject.GetComponent<UILabel>();
        central_scr = GameObject.Find("Central").GetComponent<Central>();
	}
	
	// Update is called once per frame
	void Update () {

        label.text = " X  " + central_scr.coin;
	}
}
