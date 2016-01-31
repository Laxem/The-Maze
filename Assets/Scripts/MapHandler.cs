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
            boardHolder = new GameObject("Board").transform;
            
            Map maze = Map.creerLabyrinthe(sizeMap, sizeMap);

            GameObject instance;

            Coordonnée coorDepart = new Coordonnée();
            Coordonnée coorExit = new Coordonnée();

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
                        coorDepart.setVal(x + 1, y + 1);
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

            instance = Instantiate(player, new Vector3(coorDepart.getX(), coorDepart.getY(), 0f), Quaternion.identity) as GameObject;
            instance.transform.SetParent(boardHolder);
            PlaceKey(boardHolder, maze, coorDepart, coorExit);
        }

        public void PlaceKey(Transform boardHolder, Map carte, Coordonnée depart, Coordonnée exit)
        {
            List<Coordonnée> listCoor = new List<Coordonnée>();

            int sizeMap = Mathf.Min(carte.getSize().getX(), carte.getSize().getY());
            int distance = (sizeMap - (sizeMap % 1)) / 2; 
            
            for (int x = 1; x < carte.getSize().getX()-1; x++) // ajout de mur invisible autour du labyrinthe
            {
                for (int y = 1; y < carte.getSize().getY()-1; y++)
                {
                    int contactWall = 0;
                    if (carte.getVal(x, y) == 0)
                    {
                        for (int i = x - 1; i < x + 2; i++)
                        {
                            for (int j = y - 1; j < y + 2; j++)
                            {
                                if (i == x || j == y)
                                {
                                    if (carte.getVal(i, j) == 1) contactWall++;
                                }
                            }
                        }
                        if (contactWall == 3 && CheckDistance(new Coordonnée(x, y), depart, exit, distance)) listCoor.Add(new Coordonnée(x, y));
                    }
                    
                }
            }
            Coordonnée coorKey = listCoor[Random.Range(0, listCoor.Count)];
            GameObject instance = Instantiate(key, new Vector3(coorKey.getX() + 1, coorKey.getY()+ 1, 0f), Quaternion.identity) as GameObject;
            instance.transform.SetParent(boardHolder);
        }

        public bool CheckDistance(Coordonnée key, Coordonnée depart, Coordonnée exit, int distance)
        {
            if ((Mathf.Abs(key.getX() - depart.getX()) + Mathf.Abs(key.getY() - depart.getY())) < distance) return false;
            else if ((Mathf.Abs(key.getX() - exit.getX()) + Mathf.Abs(key.getY() - exit.getY())) < distance) return false;
            else return true;
        }

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