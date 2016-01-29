using UnityEngine;
using System.Collections;


public class Coordonnée : MonoBehaviour
{
    //Attributs

    public int x;
    public int y;

};

public class Map
{
    //Attributs
    public int[,] map;
    public int sizeX;
    public int sizeY;

    //Méthodes

    public Map(Coordonnée taille)
    {
        map = new int[taille.x, taille.y];
        sizeX = taille.x;
        sizeY = taille.y;
    }

    public int getVal(Coordonnée coor)
    {
        return map[coor.x, coor.y];
    }



};
