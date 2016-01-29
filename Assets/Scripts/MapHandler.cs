using UnityEngine;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace Game
{
    public class MapHandler : MonoBehaviour
    {
        public int sizeMaze;

        public GameObject depart;
        public GameObject exit;                                        
        public GameObject floorTiles;                           
        public GameObject wallTiles;
        public GameObject player;
        public GameObject key;

        private Transform boardHolder;
        private List <Vector3> Map = new List <Vector3> ();

        private void InitMap()
        {
            Map.Clear();
            
            for (int x = 0; x < sizeMaze; x++){
                for (int y = 0; y < sizeMaze; y++){
                    Map.Add(new Vector3(x, y, 0f));
                }
            }
        }

        private void BoardSetup()
        {
            int coorXDepart = 0;
            int coorYDepart = 0;
            boardHolder = new GameObject("Board").transform;

            for (int x = 0; x < sizeMaze; x++)
            {
                for (int y = 0; y < sizeMaze; y++)
                {
                    GameObject toInstantiate = floorTiles;

                    if (x == 0 || x == sizeMaze-1 || y == 0 || y == sizeMaze-1)
                        toInstantiate = wallTiles;
                    else if(x == 1 && y == 1)
                    {
                        toInstantiate = depart;
                        coorXDepart = 1;
                        coorYDepart = 1;
                    }
                    else if (x == sizeMaze-2 && y == sizeMaze-2)
                    {
                        toInstantiate = exit;
                    }

                    GameObject instance =
                        Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;

                    instance.transform.SetParent(boardHolder);

                }
            }
             
            Instantiate(player, new Vector3(coorXDepart, coorYDepart, 0f), Quaternion.identity);

            Instantiate(key, new Vector3(1f, 4f, 0f), Quaternion.identity);

        }

        // Use this for initialization
        public void CreatMap(int sizeMap)
        {
            sizeMaze = sizeMap;
            
            
            InitMap();

            BoardSetup();

        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}