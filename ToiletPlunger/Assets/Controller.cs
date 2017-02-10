using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour {
    public GameObject plunger;
    public Text ready;
    public Text set;
    public Text plunge;
    public Button StartButton;

    public Button LO;
    public Button Instructions;
	// Use this for initialization
	void Start () {
		
	}

    public void loadPlunge()
    {
        SceneManager.LoadScene("plunge");
    }
    public void loadPractice()
    {
        SceneManager.LoadScene("Practice");
    }
    public void loadTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }
    // Update is called once per frame
    void Update () {
		
	}
    public void ActiveGame()
    {
        StartButton.gameObject.SetActive(false);
        enableReady();
        Invoke("enableSet", 2);
        Invoke("enablePlunge", 4);
        Invoke("disableSet", 4);
        Invoke("disableReady", 4);
        Invoke("disablePlunge", 5);
        Invoke("enablePlunger", 5);

    }

    public void enableReady()
    {
        ready.gameObject.SetActive(true);
    }
    public void enableSet()
    {
        set.gameObject.SetActive(true);
    }
    public void enablePlunge()
    {
        plunge.gameObject.SetActive(true);
    }

    public void disableReady()
    {
        ready.gameObject.SetActive(false);
    }
    public void disableSet()
    {
        set.gameObject.SetActive(false);
    }
    public void disablePlunge()
    {
        plunge.gameObject.SetActive(false);
    }

    public void enablePlunger()
    {
        plunger.SetActive(true);
    }
    public void disablePlunger()
    {
        plunger.SetActive(false);
    }

    public void enableInstructions()
    {
        Instructions.gameObject.SetActive(true);
    }

    public void enableLO()
    {
        LO.gameObject.SetActive(true);
    }

    public void disableInstructions()
    {
        Instructions.gameObject.SetActive(false);
    }

    public void disableLO()
    {
        LO.gameObject.SetActive(false);
    }
}
