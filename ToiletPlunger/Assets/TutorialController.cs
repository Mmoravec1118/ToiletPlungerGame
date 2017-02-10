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
   public CameraMover cam;

    public Slider horizontal, vertical;

    enum Stage { intro,horizontal, vertical, down, up, practice, combo }
    Stage stage = Stage.intro;
    private float sliderSpeed = .025f;

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
        tutorialInfo.Enqueue("To start off we have to position the plunger over the drain.");
        tutorialInfo.Enqueue("Press the space bar to stop the plunger when it's in the middle.");
        tutorialInfo.Enqueue("For our purposes we will do it horizontally first then vertically");
        tutorialInfo.Enqueue("Like this, make sure it's in the middle or you won't have a good seal.");
        tutorialInfo.Enqueue("Now you try");

        tutorialInfo.Enqueue("Now you, simply place the plunger in the toilet");
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
        Invoke("UpdateText", 32);
        Invoke("UpdateText", 35);
        Invoke("UpdateText", 38);
        Invoke("HorizontalShowWait", 40);
        
        //Invoke("SetStageToDown", 28);

    }

    // Update is called once per frame
    void Update () {
        switch (stage)
        {
            case Stage.intro:
                break;

            case Stage.horizontal:
                cam.above = true;
                horizontal.value += sliderSpeed;
                Vector3 pos = plunger.transform.position;
                pos.x += sliderSpeed * .1f;
                plunger.transform.position = pos;
                if (horizontal.value == -.5f || horizontal.value == .5f)
                {
                    sliderSpeed *= -1;
                }
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (horizontal.value > .1f || horizontal.value < -.1f )
                    {
                        tutorialText.text = "Not qute the middle...";
                    }
                    else
                    {
                        tutorialText.text = "Good, now the vertical...";
                        stage = Stage.vertical;
                    }
                }

                break;

            case Stage.vertical:

                cam.above = true;
                vertical.value += sliderSpeed;
                pos = plunger.transform.position;
                pos.z += sliderSpeed * .1f;
                plunger.transform.position = pos;
                if (vertical.value == -.5f || vertical.value == .5f)
                {
                    sliderSpeed *= -1;
                }
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (vertical.value > .1f || vertical.value < -.1f)
                    {
                        tutorialText.text = "Not qute the middle...";
                    }
                    else
                    {
                        tutorialText.text = "Great Now you have to depress the plunger like this!";
                        stage = Stage.down;
                        StartCoroutine("ExamplePlunger");
                        cam.above = false;
                    }
                }
                break;

            case Stage.down:
                if (canPlunge)
                {
                    downArrow.gameObject.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.DownArrow))
                    {
                        tutorialText.text = tutorialInfo.Dequeue();
                        stage = Stage.up;
                        pos = plunger.transform.position;
                        pos.y -= .2f;
                        plunger.transform.position = pos;
                        downArrow.gameObject.SetActive(false);

                    }
                }
                break;
            case Stage.up:
                upArrow.gameObject.SetActive(true);
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    pos = plunger.transform.position;
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
                    pos = plunger.transform.position;
                    pos.y += .2f;
                    plunger.transform.position = pos;
                    isUp = true;
                    count++;
                }

                else if (Input.GetKeyDown(KeyCode.DownArrow) && isUp)
                {
                    pos = plunger.transform.position;
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
                    pos = plunger.transform.position;
                    pos.y += .2f;
                    plunger.transform.position = pos;
                    isUp = true;
                    combo++;
                    timer = 1;
                }

                else if (Input.GetKeyDown(KeyCode.DownArrow) && isUp)
                {
                    pos = plunger.transform.position;
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

    void ShowSliders()
    {
        vertical.gameObject.SetActive(true);
        horizontal.gameObject.SetActive(true);
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

    void HorizontalShowWait()
    {
        StartCoroutine("HorizontalShow");
    }
    IEnumerator HorizontalShow()
    {
        for (float i = 0; i < 140; i++)
        {
            horizontal.value += sliderSpeed;
            if (horizontal.value == -.5f || horizontal.value == .5f)
            {
                sliderSpeed *= -1;
            }
            Vector3 pos = plunger.transform.position;
            pos.x += sliderSpeed * .1f;
            plunger.transform.position = pos;
            yield return null;
        }
        StartCoroutine("VerticalShow");
    }

    IEnumerator VerticalShow()
    {
        for (float i = 0; i < 140; i++)
        {
            vertical.value += sliderSpeed;
            if (vertical.value == -.5f || vertical.value == .5f)
            {
                sliderSpeed *= -1;
            }
            Vector3 pos = plunger.transform.position;
            pos.z += sliderSpeed * .1f;
            plunger.transform.position = pos;
            yield return null;
        }
        UpdateText();
        stage = Stage.horizontal;
    }

    IEnumerator ExamplePlunger()
    {

        for (int i = 0; i < 10; i++)
        {
            if (isUp)
            {
                var pos = plunger.transform.position;
                pos.y -= .2f;
                plunger.transform.position = pos;
                isUp = false;
            }
            else
            {
                var pos = plunger.transform.position;
                pos.y += .2f;
                plunger.transform.position = pos;
                isUp = true;
            }
            yield return new WaitForSeconds(.25f);

        }
        canPlunge = true;
        Invoke("UpdateText", 1);
    }

}
