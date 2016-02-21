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
        private GameHandler gameHandler;
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

        public void changeDifficulty(int newLevel)
        {
            level = newLevel;
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