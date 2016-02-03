using UnityEngine;
using System.Collections;

namespace Game
{
    public class Key : MonoBehaviour
    {
        private GameHandler gameHandler;
        
        void Start()
        {
            gameHandler = GameObject.FindGameObjectWithTag("MainCamera")
                .GetComponent<GameHandler>();
        }

        public void TouchPlayer()
        {
            gameHandler.keyTaken = true;
            gameObject.SetActive(false);
                
        }
    }
}