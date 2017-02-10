using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FollowMouse : MonoBehaviour
{
    int comboCounter = 1;
    int score;
    float timer = 1;
    float gameTimer = 10;
    bool up = true;
    public Text scoreText;
    public Text combo;
    public Text epicWin;
    public Text win;
    public Text loss;
    public Text epicLoss;
    public Text gameTime;
    public Text Advice;
    public CameraMover cam;
    public Slider horizontal;
    public Slider vertical;

    float multiplier;
    enum Phase { horizontal, vertical, plunge }
    Phase phase = Phase.horizontal;

    float sliderSpeed = .025f;
    // Use this for initialization
    void Start()
    {

    }
    
    // Update is called once per frame
    void Update()
    {
        switch (phase)
        {
            case Phase.horizontal:
                cam.above = true;
                horizontal.value += sliderSpeed;
                Vector3 pos = transform.position;
                pos.x += sliderSpeed * .1f;
                transform.position = pos;
                if (horizontal.value == -.5f || horizontal.value == .5f)
                {
                    sliderSpeed *= -1;
                }
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    phase = Phase.vertical;
                }
                break;
            case Phase.vertical:
                vertical.value += sliderSpeed;
                pos = transform.position;
                pos.z += sliderSpeed * .1f;
                transform.position = pos;
                if (vertical.value == -.5f || vertical.value == .5f)
                {
                    sliderSpeed *= -1;
                }
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    phase = Phase.plunge;
                    multiplier = Mathf.Abs(horizontal.value) + Mathf.Abs(vertical.value);
                    multiplier = 1 - multiplier;
                }
                break;

            case Phase.plunge:

                cam.above = false;
                combo.text = "Current Combo: " + comboCounter;
                scoreText.text = "score: " + score;
                gameTime.text = gameTimer.ToString();


                if (gameTimer <= 0)
                {
                    gameTimer = 0.00f;
                    if (score > 1000)
                    {
                        epicWin.gameObject.SetActive(true);
                    }
                    else if (score > 500)
                    {
                        if (multiplier > .5f)
                        {
                            Advice.gameObject.SetActive(true);
                            Advice.text = "You could be more precise with your placement...";
                        }
                        win.gameObject.SetActive(true);
                    }
                    else if (score > 0)
                    {
                        loss.gameObject.SetActive(true);
                        if (multiplier > .5f)
                        {
                            Advice.gameObject.SetActive(true);
                            Advice.text = "You could be more precise with your placement...";
                        }

                        else
                        {
                            Advice.gameObject.SetActive(true);
                            Advice.text = "You need to plunge faseter...";
                        }
                    }
                
                else if (score <= 0)
                    {
                        epicLoss.gameObject.SetActive(true);
                    }
                    Invoke("loadMainMenu", 5);
                }

                else
                {
                    timer -= Time.deltaTime;
                    gameTimer -= Time.deltaTime;
                    if (timer <= 0)
                    {
                        comboCounter = 1;
                        timer = 1;
                    }

                    if (Input.GetKeyDown(KeyCode.UpArrow) && !up)
                    {
                        pos = transform.position;
                        pos.y += .1f;
                        transform.position = pos;
                        up = true;
                        score += (int)(comboCounter * multiplier);
                        comboCounter++;
                        timer = 1;
                    }

                    else if (Input.GetKeyDown(KeyCode.DownArrow) && up)
                    {
                        pos = transform.position;
                        pos.y -= .1f;
                        transform.position = pos;
                        up = false;
                        score += (int)(comboCounter * multiplier);
                        comboCounter++;
                        timer = 1;
                    }
                    else if (Input.GetKeyDown(KeyCode.DownArrow) && !up)
                    {
                        comboCounter = 1;
                        timer = 1;

                    }
                    else if (Input.GetKeyDown(KeyCode.UpArrow) && up)
                    {
                        comboCounter = 1;
                        timer = 1;
                    }
                }
                break;
            default:
                break;
        }
    }

    void loadMainMenu()
    {
        SceneManager.LoadScene("Main");
    }

  
}
