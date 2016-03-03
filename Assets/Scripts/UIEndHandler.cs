using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Game
{
    public class UIEndHandler : MonoBehaviour
    {
        public Text text;
        public Button restart;
        public Button quit;
        public GameHandler gameHandler;
        [HideInInspector]public bool pause = false;

        // Use this for initialization
        void Start()
        {
            gameHandler = GameObject.FindGameObjectWithTag("MazeCamera").GetComponent<GameHandler>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetButtonDown("R") || Input.GetButtonDown("Enter") || Input.GetButtonDown("Return"))
            {
                restart.onClick.Invoke();
            }
            if (Input.GetButtonDown("Echap"))
            {
                Debug.Log(pause);
                if (pause)
                {
                    pause = false;
                    gameHandler.pause = false;
                    gameHandler.EnableAgent(true);
                    this.gameObject.SetActive(false);
                }
                else
                {
                    GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MainCameraHandler>().ExitGame();
                }
            }
        }

        public void ShowUI(bool show)
        {

        }

    }
}