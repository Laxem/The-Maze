using UnityEngine;
using System.Collections;

namespace Game
{

    public class MainCameraHandler : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {
            foreach(GameObject gamaObject in GameObject.FindGameObjectsWithTag("Player"))
            {
                //Destroy(gameObject);
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void ExitGame()
        {
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }
    }

}
