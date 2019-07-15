using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

public class StartScene : MonoBehaviour
{
    private string name;
    private Text description;
    private Objective level;
    
    void Awake()
    {
        name = gameObject.name;
    }

    void Start()
    {
        level = gameObject.GetComponent<Objective>();
        description = GameObject.Find("LevelDescription").GetComponent<Text>();
    }

    void OnMouseOver()
    {
        description.text = "Make " + (!level.hasTargetSize ? "the largest possible" : "a " + level.targetSizeMM + "mm") +
            " Ball " + (!level.hasTargetTime ? "as fast as possible" : "in " + level.targetTimeSec + " seconds");
        description.enabled = true;
        gameObject.transform.Rotate(0f, 0f, -1f);
    }

    void OnMouseExit()
    {
        description.enabled = false;
        //gameObject.transform.rotation = Quaternion.identity;
    }

    void OnMouseDown() {
        Object.DontDestroyOnLoad(gameObject);
        gameObject.GetComponent<Renderer>().enabled = false;
        Renderer[] rs = GetComponentsInChildren<Renderer>();
        foreach(Renderer r in rs) {
            r.enabled = false;
        }
        Destroy(this);
        SceneManager.LoadScene("Test");
    }
}
