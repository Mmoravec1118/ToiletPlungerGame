using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialController : MonoBehaviour {

    public Text tutorialText;
    public Text comboText;
    int combo = 1;
    int count = 0;
    public GameObject plunger;
    public GameObject toilet;
    bool canPlunge = false;
    Queue<string> tutorialInfo;
    public Image downArrow;
    public Image upArrow;
    bool isUp = true;
    float timer = 1;

    enum Stage { intro, down, up, practice, combo }
    Stage stage = Stage.intro;
	// Use this for initialization
	void Start () {
        tutorialInfo = new Queue<string>();
        tutorialInfo.Enqueue("Welcome to the Toilet Plunger Simulator!");
        tutorialInfo.Enqueue("Here you will learn how to properly plunge a toilet");
        tutorialInfo.Enqueue("It is a relatively simple task!");
        tutorialInfo.Enqueue("To Start off you need a clogged toilet");
        tutorialInfo.Enqueue("Ah! There it is");
        tutorialInfo.Enqueue("And a plunger...");
        tutorialInfo.Enqueue("There we go.");
        tutorialInfo.Enqueue("To start off, simply place the plunger in the toilet");
        tutorialInfo.Enqueue("Then pull up on the plunger to release the pressure!");
        tutorialInfo.Enqueue("Repeat that process until the toilet is no longer clogged!");
        tutorialInfo.Enqueue("Great! Doing it quickly is the key, to push the clog through!");
        tutorialInfo.Enqueue("Try to get a 10x combo!");
        tutorialInfo.Enqueue("Excelent! That's all there is to it!");

        tutorialText.text = tutorialInfo.Dequeue();
        Invoke("UpdateText", 5);
        Invoke("UpdateText", 10);
        Invoke("UpdateText", 15);
        Invoke("SpawnToilet", 17);
        Invoke("UpdateText", 18);
        Invoke("UpdateText", 21);
        Invoke("SpawnPlunger", 23);
        Invoke("UpdateText", 25);
        Invoke("UpdateText", 28);
        Invoke("SetStageToDown", 28);

    }

    // Update is called once per frame
    void Update () {
        switch (stage)
        {
            case Stage.intro:
                break;
            case Stage.down:
                canPlunge = true;
                downArrow.gameObject.SetActive(true);
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    tutorialText.text = tutorialInfo.Dequeue();
                    stage = Stage.up;
                    var pos = plunger.transform.position;
                    pos.y -= .2f;
                    plunger.transform.position = pos;
                    downArrow.gameObject.SetActive(false);

                }
                break;
            case Stage.up:
                upArrow.gameObject.SetActive(true);
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    var pos = plunger.transform.position;
                    pos.y += .2f;
                    plunger.transform.position = pos;
                    tutorialText.text = tutorialInfo.Dequeue();
                    stage = Stage.practice;
                    upArrow.gameObject.SetActive(false);
                }
                break;
            case Stage.practice:

                if (Input.GetKeyDown(KeyCode.UpArrow) && !isUp)
                {
                    var pos = plunger.transform.position;
                    pos.y += .2f;
                    plunger.transform.position = pos;
                    isUp = true;
                    count++;
                }

                else if (Input.GetKeyDown(KeyCode.DownArrow) && isUp)
                {
                    var pos = plunger.transform.position;
                    pos.y -= .2f;
                    plunger.transform.position = pos;
                    isUp = false;
                    count++;
                }

                if (count > 10)
                {
                    stage = Stage.combo;
                    tutorialText.text = tutorialInfo.Dequeue();
                    Invoke("UpdateText", 5);
                    comboText.gameObject.SetActive(true);
                }
                break;
            case Stage.combo:

                comboText.text = combo.ToString();
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    combo = 1;
                    timer = 1;

                }

                if (Input.GetKeyDown(KeyCode.UpArrow) && !isUp)
                {
                    var pos = plunger.transform.position;
                    pos.y += .2f;
                    plunger.transform.position = pos;
                    isUp = true;
                    combo++;
                    timer = 1;
                }

                else if (Input.GetKeyDown(KeyCode.DownArrow) && isUp)
                {
                    var pos = plunger.transform.position;
                    pos.y -= .2f;
                    plunger.transform.position = pos;
                    isUp = false;
                    combo++;
                    timer = 1;
                }
                else if (Input.GetKeyDown(KeyCode.DownArrow) && !isUp)
                {
                    combo = 1;
                    timer = 1;


                }
                else if (Input.GetKeyDown(KeyCode.UpArrow) && isUp)
                {
                    combo = 1;
                    timer = 1;

                }

                if (combo >= 10)
                {
                    tutorialText.text = tutorialInfo.Dequeue();
                    plunger.gameObject.SetActive(false);
                    Invoke("goToMain", 3);
                    stage = Stage.intro;
                }

                break;
            default:
                break;
        }
    }

    void UpdateText()
    {
        tutorialText.text = tutorialInfo.Dequeue();
    }

    void SpawnToilet()
    {
        toilet.SetActive(true);
    }

    void SpawnPlunger()
    {
        plunger.SetActive(true);
    }
    void CanPlunge()
    {
        canPlunge = true;
    }
    void SetStageToDown()
    {
        stage = Stage.down;
    }
    void goToMain()
    {
        SceneManager.LoadScene("Main");
    }

}
