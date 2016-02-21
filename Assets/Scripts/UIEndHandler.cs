using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Game
{
    public class UIEndHandler : MonoBehaviour
    {
        public Button restart;
        public Button quit;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetButtonDown("R"))
            {
                restart.onClick.Invoke();
            }
            if (Input.GetButtonDown("Echap"))
            {
                quit.onClick.Invoke();
            }
        }
    }
}