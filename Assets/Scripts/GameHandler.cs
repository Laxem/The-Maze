using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Game
{

    public class GameHandler : MonoBehaviour
    {

        public static GameHandler instance = null;
        private MapHandler MapScript;
        private int sizeMap = 7;
        [HideInInspector]public bool keyTaken;

        void Awake()
        {
            if (instance == null) instance = this;
            else if (instance != this) Destroy(gameObject);

            DontDestroyOnLoad(gameObject);

            MapScript = GetComponent<MapHandler>();

            InitGame();
        }


        void InitGame()
        {
            //Call the SetupScene function of the BoardManager script, pass it current level number.
            MapScript.CreatMap(sizeMap);

            keyTaken = false;

        }


        void Update()
        {

        }

        public void CheckIfGameOver()
        {
            //MovementHandler.pause = true;
        }
    }
}