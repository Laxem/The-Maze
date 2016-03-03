using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace Game
{

    public class GameHandler : MonoBehaviour
    {
        [HideInInspector]public bool keyTaken;
        [HideInInspector]public float time;
        [HideInInspector]public int level;
        [HideInInspector]public bool pause;
        [HideInInspector]public bool win;
        [HideInInspector]public bool endGame;
        public static GameHandler instance = null;
        public GameObject endCanvas;
        public float ghostSpeed = 3f;
        public float ghostAngularSpeed = 80f;
        private int sizeMap = 21;
        private int foeNumber = 1;        
        private MapHandler MapScript;
        private PlayerMovement player;
        private GhostMovement[] foes;
        private Key key;



        void Awake()
        {
            if (instance == null) instance = this;
            else if (instance != this) Destroy(gameObject);

            DontDestroyOnLoad(gameObject);
            
            MapScript = GetComponent<MapHandler>();
            
            InitLevel();

            setAgents();
        }

        void Update()
        {
            if (Input.GetButtonDown("Echap") || Input.GetButtonDown("Pause"))
            {
                if (!pause && !endGame)
                {
                    pause = true;
                    endCanvas.GetComponentInChildren<UIEndHandler>().pause = true;
                    EnableAgent(false);
                    OpenEndWindow();
                }                
            }
        }


        void InitLevel()
        {
            pause = false;
            endGame = false;

            MapScript.CreatMap(sizeMap);

            keyTaken = false;

            time = Time.time;

        }

        public void Restart()
        {
            

            ClearGame();

            InitLevel();

            setAgents();
        }

        void ClearGame()
        {
            GameObject board = GameObject.Find("Board");
            DestroyImmediate(board);
        }

        void setAgents()
        {
            
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
            GameObject[] foesList = GameObject.FindGameObjectsWithTag("Foe");
            foes = new GhostMovement[foeNumber];
            for (int i = 0; i < foeNumber; i++)
            {
                foes[i] = foesList[i].GetComponent<GhostMovement>();
            }
            key = GameObject.FindGameObjectWithTag("Key").GetComponent<Key>();
        }
        
        void OpenEndWindow()
        {
            Text textFin = endCanvas.GetComponentInChildren<Text>();
            if (pause)
            {
                textFin.text = "Pause";
            }
            else
            {
                if (win)
                {
                    textFin.text = "Bravo !";
                }
                else
                {
                    textFin.text = "Perdu ...";
                }
            }            
            endCanvas.SetActive(true);
        }

        public void setDifficulty(int difLevel)
        {
            level = difLevel;
            sizeMap = 9 + difLevel * 6;
            ghostSpeed = 2 + difLevel;
            ghostAngularSpeed = 60 + difLevel * 20;
            switch (difLevel)
            {
                case 1:
                    sizeMap = 15;
                    ghostSpeed = 2;
                    ghostAngularSpeed = 60;
                    break;
                case 2:
                    sizeMap = 21;
                    ghostSpeed = 3;
                    ghostAngularSpeed = 80;
                    break;
                case 3:
                    sizeMap = 27;
                    ghostSpeed = 4;
                    ghostAngularSpeed = 100;
                    break;
            }
        }

        public void EnableAgent(bool enable)
        {
            player.enabled = enable;
            player.GetComponent<Animator>().SetBool("move", enable);
            foreach (GhostMovement foe in foes)
            {
                foe.enabled = enable;
                foe.GetComponent<Animator>().SetBool("move", enable);
            }
            key.GetComponent<Animator>().SetBool("turn", enable);
        }

        public void GameOver()
        {
            win = false;
            endGame = true;
            EnableAgent(false);

            OpenEndWindow();
        }
        
        public void Finish()
        {            
            if (CanExit())
            {
                win = true;
                endGame = true;
                EnableAgent(false);
                OpenEndWindow();
            }
            
        }

        public bool CanExit()
        {
            return keyTaken;
            
        }
    }
}