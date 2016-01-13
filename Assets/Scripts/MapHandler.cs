using UnityEngine;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace Completed
{

    public class MapHandler : MonoBehaviour
    {
        public int sizeMaze = 5;

        public GameObject depart;
        public GameObject exit;                                        
        public GameObject floorTiles;                           
        public GameObject wallTiles;

        private Transform boardHolder;
        private List <Vector3> Map = new List <Vector3> ();

        void InitMap()
        {
            Map.Clear();
            
            for (int x = 0; x < sizeMaze; x++){
                for (int y = 0; y < sizeMaze; y++){
                    Map.Add(new Vector3(x, y, 0f));
                }
            }
        }

        void BoardSetup()
        {
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
        }

        // Use this for initialization
        void Start()
        {
            BoardSetup();
            
            InitMap();

            /*GameObject instance = Instantiate(depart, new Vector3(1, 1, 0f), Quaternion.identity) as GameObject;
            instance.transform.SetParent(boardHolder);
            instance = Instantiate(exit, new Vector3(sizeMaze - 2, sizeMaze - 2, 0f), Quaternion.identity) as GameObject;
            instance.transform.SetParent(boardHolder);
            */
        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}