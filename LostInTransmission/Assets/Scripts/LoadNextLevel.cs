using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadNextLevel : MonoBehaviour {

    // Use this for initialization
    private List<GameObject> playerList;
    public int levelNumber;
    public bool forceOverride = false;
    public string overrideLevelName;
	void Start () {
        playerList = new List<GameObject>();
        string sceneName = SceneManager.GetActiveScene().name;
        int.TryParse(sceneName.Substring(sceneName.Length - 1), out levelNumber);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Player") return;
        if (!playerList.Contains(collision.gameObject))
        {
            playerList.Add(collision.gameObject);
        }
        if (playerList.Count >= 2)
        {
            if (forceOverride)
            {
                StartCoroutine(SceneController.transitionThenLoad(overrideLevelName, 0.3f, true));
            }
            else
            {
                StartCoroutine(SceneController.transitionThenLoad("level" + (levelNumber + 1), 0.3f, true));
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        playerList.Remove(collision.gameObject);
    }
}
