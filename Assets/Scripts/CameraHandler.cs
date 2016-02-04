using UnityEngine;
using System.Collections;

namespace Game
{
    public class CameraHandler : MonoBehaviour
    {

        private Vector3 playerPosition;
        private int mapSize;
        private float camSize;
        private float caseSize = 1f;

        // Use this for initialization
        void Start()
        {
            playerPosition = new Vector3(0, 0, 0);

            Camera cam = GetComponent<Camera>();
            camSize = cam.orthographicSize;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            Move();
        }

        void Move()
        {
            if (2*camSize < mapSize) //handle the camera when the map don't fit in the picture frame
            {                
                if (playerPosition.x-1 < (camSize - caseSize/2)) playerPosition.x = camSize - caseSize / 2+1;
                else if (playerPosition.x > (mapSize - camSize + caseSize / 2)) playerPosition.x = mapSize - camSize + caseSize / 2;
                if (playerPosition.y-1 < (camSize - caseSize/2)) playerPosition.y = camSize - caseSize / 2+1;
                else if (playerPosition.y > (mapSize - camSize + caseSize / 2)) playerPosition.y = mapSize - camSize + caseSize / 2;
                Vector3 CamPos = new Vector3(playerPosition.x, playerPosition.y, -1f);
                transform.position = CamPos;
                
            }
            else
            {
                Vector3 CamPos = new Vector3(mapSize/2 + caseSize, mapSize/2 + caseSize, -1f);
                transform.position = CamPos;
            }
            Debug.Log(camSize + " " + mapSize);
        }

        public void SetPlayerPosition(Vector3 player)
        {
            playerPosition = player;
        }
        public void setMapSize(int mapS)
        {
            mapSize = mapS;
        }

    }
}