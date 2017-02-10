using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Assets
{

    class PracticeController : MonoBehaviour
    {
       
        //  public Button StartButton;
        public Text combo;
        int comboCounter = 1;
        float timer = 1;
        bool up = true;

        public Text good;
        public Text notQuite;
        public Text faster;

        public void loadMainMenu()
        {
            SceneManager.LoadScene("Main");
        }

        public void Update()
        {
            if (comboCounter % 10 == 0)
            {
                enableGood();
                Invoke("disableGood", 1);
            }
            combo.text = comboCounter.ToString();
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                comboCounter = 1;
                timer = 1;
                enableFaster();
                Invoke("disableFaster", 1);
            }

            if (Input.GetKeyDown(KeyCode.UpArrow) && !up)
            {
                var pos = transform.position;
                pos.y += .2f;
                transform.position = pos;
                up = true;
                comboCounter++;
                timer = 1;
            }

            else if (Input.GetKeyDown(KeyCode.DownArrow) && up)
            {
                var pos = transform.position;
                pos.y -= .2f;
                transform.position = pos;
                up = false;
                comboCounter++;
                timer = 1;
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) && !up)
            {
                comboCounter = 1;
                timer = 1;
                enableNotQuite();
                Invoke("disableNotQuite", 1);

            }
            else if (Input.GetKeyDown(KeyCode.UpArrow) && up)
            {
                comboCounter = 1;
                timer = 1;
                enableNotQuite();
                Invoke("disableNotQuite", 1);
            }
        }

        public void enableGood()
        {
            good.gameObject.SetActive(true);
            notQuite.gameObject.SetActive(false);
            faster.gameObject.SetActive(false);
        }
        public void disableGood()
        {
            good.gameObject.SetActive(false);
        }

        public void enableFaster()
        {
            faster.gameObject.SetActive(true);
            notQuite.gameObject.SetActive(false);
            good.gameObject.SetActive(false);
        }
        public void disableFaster()
        {
            faster.gameObject.SetActive(false);
        }

        public void enableNotQuite()
        {
            notQuite.gameObject.SetActive(true);
            good.gameObject.SetActive(false);
            faster.gameObject.SetActive(false);

        }
        public void disableNotQuite()
        {
            notQuite.gameObject.SetActive(false);
        }
    }
}