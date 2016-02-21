using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Game
{
    public class DifficultyHandler : MonoBehaviour
    {
        public Button easy;
        public Button normal;
        public Button hard;
        public GameObject mazeCamera;
        private static int level = 2;

        // Use this for initialization
        void Start()
        {
            switch (level)
            {
                case 1:
                    easy.interactable = false;
                    break;
                case 2:
                    normal.interactable = false;
                    break;
                case 3:
                    hard.interactable = false;
                    break;
            }
        }

        void Update()
        {
            int oldLevel = level;
            if (Input.GetButtonDown("Up")) level -= 1;
            else if (Input.GetButtonDown("Down")) level += 1;
            if (level == 0) level = 3;
            else if (level == 4) level = 1;
            if (level != oldLevel)
            {
                changeDifficulty(level);
                mazeCamera.GetComponent<GameHandler>().setDifficulty(level);
            }
        }

        public void changeDifficulty(int newLevel)
        {
            level = newLevel;
            Debug.Log(level);
            easy.interactable = true;
            normal.interactable = true;
            hard.interactable = true;
            switch (level)
            {
                case 1:
                    easy.interactable = false;
                    break;
                case 2:
                    normal.interactable = false;
                    break;
                case 3:
                    hard.interactable = false;
                    break;
            }
        }
    }
}