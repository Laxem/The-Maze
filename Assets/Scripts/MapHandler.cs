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
        public Sprite wall1;
        public Sprite wall2;
        public Sprite wall22;
        public Sprite wall3;
        public Sprite wall4;
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
                    if (maze.getVal(x, y) == 1)
                        PlaceWall(maze, x, y);
                    else if (maze.getVal(x, y) == 2)
                    {
                        coorDepart.setVal(x + 1, y + 1);
                        PlaceEntryExit(maze, x, y, depart);
                    }
                    else if (maze.getVal(x, y) == 3)
                    {
                        instance = Instantiate(lockExit, new Vector3(x + 1, y + 1, 0f), Quaternion.identity) as GameObject;
                        instance.transform.SetParent(boardHolder);

                        PlaceEntryExit(maze, x, y, exit);
                    }
                    else
                    {
                        instance = Instantiate(floor, new Vector3(x + 1, y + 1, 0f), Quaternion.identity) as GameObject;
                        instance.transform.SetParent(boardHolder);
                    }
                    
                }
            }

            for (int x = 0; x < maze.getSize().getX() + 2; x++) // add of invisible wall around the maze
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

        void PlaceEntryExit(Map maze, int x, int y, GameObject tile)
        {
            GameObject instance;
            instance = Instantiate(tile, new Vector3(x + 1, y + 1, 0f), Quaternion.identity) as GameObject;
            instance.transform.SetParent(boardHolder);
            if (maze.getVal(x, y + 1) == -1) instance.transform.rotation = Quaternion.Euler(0f, 0f, 180);
            else if (maze.getVal(x + 1, y) == -1) instance.transform.rotation = Quaternion.Euler(0f, 0f, 90);
            else if (maze.getVal(x - 1, y) == -1) instance.transform.rotation = Quaternion.Euler(0f, 0f, 270);
        }

        public void PlaceKeyAndGhost(Transform boardHolder, Map carte, Coordonnée depart, Coordonnée exit)
        {
            List<Coordonnée> listCoorKey = new List<Coordonnée>();
            List<Coordonnée> listCoorGhost = new List<Coordonnée>();

            int sizeMap = Mathf.Min(carte.getSize().getX(), carte.getSize().getY());
            int distanceKey = (sizeMap - (sizeMap % 1)) / 2;
            int distanceGhost = (sizeMap + 1) * 3 / 4;
            
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

        void PlaceWall(Map maze, int x, int y)
        {
            int wallAround = 0;  // each power of 10 represent a block around the wall placed
            if (maze.getVal(x, y + 1) == 1) wallAround += 1000; //top
            if (maze.getVal(x + 1, y) == 1) wallAround += 100;  //right
            if (maze.getVal(x, y - 1) == 1) wallAround += 10;   //bottom
            if (maze.getVal(x - 1, y) == 1) wallAround += 1;    //left

            GameObject instance;

            switch (wallAround)
            {
                case 1000 :
                    instance = Instantiate(wall, new Vector3(x + 1, y + 1, 0f), Quaternion.identity) as GameObject;
                    instance.transform.SetParent(boardHolder);
                    instance.GetComponent<SpriteRenderer>().sprite = wall1;
                    instance.transform.rotation = Quaternion.Euler(0f, 0f, 180);
                    break;
                case 100:
                    instance = Instantiate(wall, new Vector3(x + 1, y + 1, 0f), Quaternion.identity) as GameObject;
                    instance.transform.SetParent(boardHolder);
                    instance.GetComponent<SpriteRenderer>().sprite = wall1;
                    instance.transform.rotation = Quaternion.Euler(0f, 0f, 90);
                    break;
                case 10:
                    instance = Instantiate(wall, new Vector3(x + 1, y + 1, 0f), Quaternion.identity) as GameObject;
                    instance.transform.SetParent(boardHolder);
                    instance.GetComponent<SpriteRenderer>().sprite = wall1;
                    break;
                case 1:
                    instance = Instantiate(wall, new Vector3(x + 1, y + 1, 0f), Quaternion.identity) as GameObject;
                    instance.transform.SetParent(boardHolder);
                    instance.GetComponent<SpriteRenderer>().sprite = wall1;
                    instance.transform.rotation = Quaternion.Euler(0f, 0f, 270);
                    break;
                case 1100:
                    instance = Instantiate(wall, new Vector3(x + 1, y + 1, 0f), Quaternion.identity) as GameObject;
                    instance.transform.SetParent(boardHolder);
                    instance.GetComponent<SpriteRenderer>().sprite = wall22;
                    instance.transform.rotation = Quaternion.Euler(0f, 0f, 270);
                    break;
                case 1010:
                    instance = Instantiate(wall, new Vector3(x + 1, y + 1, 0f), Quaternion.identity) as GameObject;
                    instance.transform.SetParent(boardHolder);
                    instance.GetComponent<SpriteRenderer>().sprite = wall2;
                    break;
                case 1001:
                    instance = Instantiate(wall, new Vector3(x + 1, y + 1, 0f), Quaternion.identity) as GameObject;
                    instance.transform.SetParent(boardHolder);
                    instance.GetComponent<SpriteRenderer>().sprite = wall22;
                    break;
                case 0110:
                    instance = Instantiate(wall, new Vector3(x + 1, y + 1, 0f), Quaternion.identity) as GameObject;
                    instance.transform.SetParent(boardHolder);
                    instance.GetComponent<SpriteRenderer>().sprite = wall22;
                    instance.transform.rotation = Quaternion.Euler(0f, 0f, 180);
                    break;
                case 0101:
                    instance = Instantiate(wall, new Vector3(x + 1, y + 1, 0f), Quaternion.identity) as GameObject;
                    instance.transform.SetParent(boardHolder);
                    instance.GetComponent<SpriteRenderer>().sprite = wall2;
                    instance.transform.rotation = Quaternion.Euler(0f, 0f, 270);
                    break;
                case 0011:
                    instance = Instantiate(wall, new Vector3(x + 1, y + 1, 0f), Quaternion.identity) as GameObject;
                    instance.transform.SetParent(boardHolder);
                    instance.GetComponent<SpriteRenderer>().sprite = wall22;
                    instance.transform.rotation = Quaternion.Euler(0f, 0f, 90);
                    break;
                case 1110:
                    instance = Instantiate(wall, new Vector3(x + 1, y + 1, 0f), Quaternion.identity) as GameObject;
                    instance.transform.SetParent(boardHolder);
                    instance.GetComponent<SpriteRenderer>().sprite = wall3;
                    instance.transform.rotation = Quaternion.Euler(0f, 0f, 270);
                    break;
                case 1101:
                    instance = Instantiate(wall, new Vector3(x + 1, y + 1, 0f), Quaternion.identity) as GameObject;
                    instance.transform.SetParent(boardHolder);
                    instance.GetComponent<SpriteRenderer>().sprite = wall3;
                    break;
                case 1011:
                    instance = Instantiate(wall, new Vector3(x + 1, y + 1, 0f), Quaternion.identity) as GameObject;
                    instance.transform.SetParent(boardHolder);
                    instance.GetComponent<SpriteRenderer>().sprite = wall3;
                    instance.transform.rotation = Quaternion.Euler(0f, 0f, 90);
                    break;
                case 0111:
                    instance = Instantiate(wall, new Vector3(x + 1, y + 1, 0f), Quaternion.identity) as GameObject;
                    instance.transform.SetParent(boardHolder);
                    instance.GetComponent<SpriteRenderer>().sprite = wall3;
                    instance.transform.rotation = Quaternion.Euler(0f, 0f, 180);
                    break;
                case 1111:
                    instance = Instantiate(wall, new Vector3(x + 1, y + 1, 0f), Quaternion.identity) as GameObject;
                    instance.transform.SetParent(boardHolder);
                    instance.GetComponent<SpriteRenderer>().sprite = wall4;
                    break;
            }
            
        }

        void SetCamSize(int sizeMap)
        {
            CameraHandler cam = GameObject.FindGameObjectWithTag("MazeCamera").GetComponent<CameraHandler>();
            cam.setMapSize(sizeMap);

        }
        
    }

}