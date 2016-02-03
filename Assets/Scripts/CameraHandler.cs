using UnityEngine;
using System.Collections;

namespace Game
{
    public class CameraHandler : MonoBehaviour
    {

        private Vector3 playerPosition;

        // Use this for initialization
        void Start()
        {
            playerPosition = new Vector3(0, 0, 0);
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            Move();
        }

        void Move()
        {
            Vector3 CamPos = new Vector3(playerPosition.x, playerPosition.y, -1f);
            transform.position = CamPos;
        }

        public void SetPlayerPosition(Vector3 player)
        {
            playerPosition = player;
        }

    }
}