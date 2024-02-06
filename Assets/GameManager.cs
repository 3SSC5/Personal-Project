using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PP
{
    public class GameManager : MonoBehaviour
    {
        public GameObject GameOverUI;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if(Input.GetKeyDown(KeyCode.V)) 
            {
                GameOver();
            }
        }

        public void StartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        public void GameOver()
        {
            GameOverUI.SetActive(true);
        }

        public void Reset()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void Menu()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }

            private void OnCollisionEnter2D(Collision2D collision)
            {
                if (collision.gameObject.tag == "Player")
                {
                    GameOver();
                }
            }
        }
    }
