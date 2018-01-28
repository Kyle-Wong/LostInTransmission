using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour {

    // Use this for initialization
    public GraphicColorLerp colorLerp;

	void Start () {
        if (GameData.playSceneIntro)
        {
            colorLerp.gameObject.GetComponent<Image>().enabled = true;
            colorLerp.setColors(Color.black, new Color(0, 0, 0, 0));
            colorLerp.startColorChange();
        } else
        {
            colorLerp.gameObject.GetComponent<Image>().enabled = false;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
    public static IEnumerator transitionThenLoad(string sceneName, float delay, bool introTransition)
    {
        GraphicColorLerp colorLerp = GameObject.Find("Overlay").GetComponent<GraphicColorLerp>();
        colorLerp.gameObject.GetComponent<Image>().enabled = true;
        colorLerp.duration = delay;
        colorLerp.setColors(new Color(0, 0, 0, 0), Color.black);
        colorLerp.startColorChange();
        
        yield return new WaitForSeconds(delay);
        GameData.playSceneIntro = introTransition;
        SceneManager.LoadScene(sceneName);
    }
}
