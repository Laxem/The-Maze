using UnityEngine;
using System.Collections;

namespace Game
{

    public class UICameraHandler : MonoBehaviour
    {
        CameraHandler camerahandler;

        // Use this for initialization
        void Start()
        {
            camerahandler = GameObject.FindGameObjectWithTag("MazeCamera").GetComponent<CameraHandler>();
            
        }

        // Update is called once per frame
        void Update()
        {
            if (camerahandler != null)
            {
                transform.position = camerahandler.transform.position;
            }
        }
    }

}
