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
        public GameObject lockExit;
        public GameObject floor;                           
        public GameObject wall;
        public GameObject invisibleWall;
        public GameObject player;
        public GameObject key;
        public GameObject ghost;

        private Transform boardHolder;

        public void CreatMap(int sizeMap)
        {
            BoardSetup(sizeMap);

            SetCamSize(sizeMap);

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
                        toInstantiate = lockExit;
                        instance = Instantiate(toInstantiate, new Vector3(x + 1, y + 1, 0f), Quaternion.identity) as GameObject;
                        instance.transform.SetParent(boardHolder);
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

            PlaceKeyAndGhost(boardHolder, maze, coorDepart, coorExit);
        }

        public void PlaceKeyAndGhost(Transform boardHolder, Map carte, Coordonnée depart, Coordonnée exit)
        {
            List<Coordonnée> listCoorKey = new List<Coordonnée>();
            List<Coordonnée> listCoorGhost = new List<Coordonnée>();

            int sizeMap = Mathf.Min(carte.getSize().getX(), carte.getSize().getY());
            int distanceKey = (sizeMap - (sizeMap % 1)) / 2;
            int distanceGhost = (sizeMap + 1) / 2;
            
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
                        if (contactWall == 3 && CheckDistance(new Coordonnée(x, y), depart, distanceKey) && CheckDistance(new Coordonnée(x, y), exit, distanceKey)) listCoorKey.Add(new Coordonnée(x, y));
                        else if (CheckDistance(new Coordonnée(x, y), depart, distanceGhost)) listCoorGhost.Add(new Coordonnée(x, y));
                    }
                    
                }
            }
            Coordonnée coorKey = listCoorKey[Random.Range(0, listCoorKey.Count)];
            GameObject instance = Instantiate(key, new Vector3(coorKey.getX() + 1, coorKey.getY()+ 1, 0f), Quaternion.identity) as GameObject;
            instance.transform.SetParent(boardHolder);

            Coordonnée coorGhost = listCoorGhost[Random.Range(0, listCoorGhost.Count)];
            instance = Instantiate(ghost, new Vector3(coorGhost.getX() + 1, coorGhost.getY() + 1, 0f), Quaternion.identity) as GameObject;
            instance.transform.SetParent(boardHolder);
        }

        public bool CheckDistance(Coordonnée a, Coordonnée b, int distance)
        {
            if ((Mathf.Abs(a.getX() - b.getX()) + Mathf.Abs(a.getY() - b.getY())) < distance) return false;
            else return true;
        }
        
        

        void SetCamSize(int sizeMap)
        {
            CameraHandler cam = GameObject.FindGameObjectWithTag("MazeCamera").GetComponent<CameraHandler>();
            cam.setMapSize(sizeMap);

        }
        
    }

}