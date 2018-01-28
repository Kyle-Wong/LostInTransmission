using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetText : MonoBehaviour {

    // Use this for initialization
    public string[] lines;
    public int poemNumber;
    public int lineNumber;
    private TextMesh text;
	void Start () {
        text = GetComponent<TextMesh>();
        text.text = "";
        text.text += lines[poemNumber * 3 + lineNumber];


    }

    // Update is called once per frame
    void Update () {
		
	}
}
