using UnityEngine;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace Game
{
    public class MapHandler : MonoBehaviour
    {
        public GameObject depart;
        public GameObject exit;                                        
        public GameObject floor;                           
        public GameObject wall;
        public GameObject invisibleWall;
        public GameObject player;
        public GameObject key;

        private Transform boardHolder;
        //private List <Vector3> map = new List <Vector3> ();

        private void InitMap()
        {/*
            map.Clear();
            
            for (int x = 0; x < sizeMap; x++){
                for (int y = 0; y < sizeMaze; y++){
                    map.Add(new Vector3(x, y, 0f));
                }
            }*/
        }

        private void BoardSetup(int sizeMap)
        {
            int coorXDepart = 0;
            int coorYDepart = 0;
            boardHolder = new GameObject("Board").transform;

            
            Map maze = Map.creerLabyrinthe(sizeMap, sizeMap);


            GameObject instance;


            for (int x = 0; x < maze.getSize().getX(); x++)
            {
                for (int y = 0; y < maze.getSize().getY(); y++)
                {
                    GameObject toInstantiate;

                    if (maze.getVal(x, y) == 1)
                        toInstantiate = wall;
                    else if (maze.getVal(x, y) == 2)
                    {
                        toInstantiate = depart;
                        coorXDepart = x+1;
                        coorYDepart = y+1;
                    }
                    else if (maze.getVal(x, y) == 3)
                    {
                        toInstantiate = exit;
                    }
                    else toInstantiate = floor;

                    instance = Instantiate(toInstantiate, new Vector3(x+1, y+1, 0f), Quaternion.identity) as GameObject;

                    instance.transform.SetParent(boardHolder);

                }
            }

            for (int x = 0; x < maze.getSize().getX() + 2; x++) // ajout de mur invisible autour du labyrinthe
            {
                for (int y = 0; y < maze.getSize().getY() + 2; y++)
                {
                    if (x == 0 || x == (maze.getSize().getX() + 1) || y == 0 || y == (maze.getSize().getY() + 1))
                    {
                        instance = Instantiate(invisibleWall, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;
                        instance.transform.SetParent(boardHolder);
                    }
                }
            }

            instance = Instantiate(player, new Vector3(coorXDepart, coorYDepart, 0f), Quaternion.identity) as GameObject;
            instance.transform.SetParent(boardHolder);
            instance = Instantiate(key, new Vector3(1f+1, 4f+1, 0f), Quaternion.identity) as GameObject;
            instance.transform.SetParent(boardHolder);
        }

        /*public void PlaceKey(Transform boardHolder, )
        {

        }*/

        // Use this for initialization
        public void CreatMap(int sizeMap)
        {            
            InitMap();

            BoardSetup(sizeMap);

        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}