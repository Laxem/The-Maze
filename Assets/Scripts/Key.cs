using UnityEngine;
using System.Collections;

namespace Game
{
    public class Key : MonoBehaviour
    {
        private GameHandler gameHandler;
        
        void Start()
        {
            gameHandler = GameObject.FindGameObjectWithTag("MazeCamera")
                .GetComponent<GameHandler>();
        }

        public void TouchPlayer()
        {
            gameHandler.keyTaken = true;
            GameObject Lock = GameObject.FindGameObjectWithTag("Lock");
            Lock.SetActive(false);
            gameObject.SetActive(false);            
        }
    }
}