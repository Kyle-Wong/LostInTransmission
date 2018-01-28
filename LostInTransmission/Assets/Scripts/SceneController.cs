using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour {

    // Use this for initialization
    public GraphicColorLerp colorLerp;

	void Awake () {
        if (true || GameData.playSceneIntro)
        {
            colorLerp.GetComponent<Image>().enabled = true;
            colorLerp.setColors(Color.black, new Color(0, 0, 0, 0));
            colorLerp.startColorChange();
        } else
        {
            colorLerp.GetComponent<Image>().enabled = false;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
    public static IEnumerator transitionThenLoad(string sceneName, float delay, bool introTransition)
    {
        GraphicColorLerp colorLerp = GameObject.Find("Overlay").GetComponent<GraphicColorLerp>();
        colorLerp.GetComponent<Image>().enabled = true;
        colorLerp.duration = delay;
        colorLerp.setColors(new Color(0, 0, 0, 0), Color.black);
        colorLerp.startColorChange();
        
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }
}
