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
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       
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
                win.gameObject.SetActive(true);
            }
            else if (score > 0)
            {
                loss.gameObject.SetActive(true);
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
                var pos = transform.position;
                pos.y += .2f;
                transform.position = pos;
                up = true;
                score += comboCounter;
                comboCounter++;
                timer = 1;
            }

            else if (Input.GetKeyDown(KeyCode.DownArrow) && up)
            {
                var pos = transform.position;
                pos.y -= .2f;
                transform.position = pos;
                up = false;
                score += comboCounter;
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
    }

    void loadMainMenu()
    {
        SceneManager.LoadScene("Main");
    }
}
