using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Game
{
    public class Coordonnée : System.Object
    {
        //Attributs

        public int x;
        public int y;

        #region Contructeurs
        public Coordonnée()
        {
            x = 0;
            y = 0;
        }

        public Coordonnée(int newX, int newY)
        {
            x = newX;
            y = newY;
        }
        #endregion

        public int getX() { return x; }
        public int getY() { return y; }

        public void setX(int newX) { x = newX; }
        public void setY(int newY) { y = newY; }
        public void setVal(int newX, int newY)
        {
            x = newX;
            y = newY;
        }

        public static bool operator !=(Coordonnée coor1, Coordonnée coor2)
        {
            return coor1.getX() != coor2.getX() || coor1.getY() != coor2.getY();
        }
        public static bool operator ==(Coordonnée coor1, Coordonnée coor2)
        {
            return coor1.getX() == coor2.getX() && coor1.getY() == coor2.getY();
        }
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Coordonnée))
                return false;
            else
                return (getX() == ((Coordonnée)obj).getX()) && (getY() == ((Coordonnée)obj).getY());
        }
        public bool Equals(Coordonnée coor)
        {
            if ((object)coor == null)
            {
                return false;
            }
            
            return (getX() == (coor.getX())) && (getY() == (coor.getY()));
        }
        public override int GetHashCode()
        {
            return getX()^getY();
        }
        public static bool operator >(Coordonnée coor1, Coordonnée coor2)
        {
            return coor1.getX() > coor2.getX() || (coor1.getX() == coor2.getX() && coor1.getY() > coor2.getY());
        }
        public static bool operator <(Coordonnée coor1, Coordonnée coor2)
        {
            return coor1.getX() > coor2.getX() || (coor1.getX() == coor2.getX() && coor1.getY() > coor2.getY());
        }
        
    };

    public class Map
    {
        //Attributs
        private int[] map;
        private int sizeX;
        private int sizeY;

        //Méthodes
        public Map(int x, int y)
        {
            map = new int[x * y];
            sizeX = x;
            sizeY = y;
        }
        public Map(Coordonnée taille)
        {
            map = new int[taille.x * taille.y];
            sizeX = taille.x;
            sizeY = taille.y;
        }

        public void setVal(Coordonnée coor, int valeur)
        {
            map[coor.getY() * sizeX + coor.getX()] = valeur;
        }
        public void setVal(int x, int y, int valeur)
        {
            map[y * sizeX + x] = valeur;
        }
        public int getVal(Coordonnée coor)
        {
            return map[coor.getY() * sizeX + coor.getX()];
        }
        public int getVal(int x, int y)
        {
            return map[y * sizeX + x];
        }

        public Coordonnée getSize()
        {
            Coordonnée result = new Coordonnée(sizeX, sizeY);
            return result;
        }
        
        public static void caseDA(Coordonnée taille, ref Coordonnée depart, ref Coordonnée arrivee)
        {
            int cote = Random.Range(1, 5);            
            int a;
            int d;
            switch (cote)
            {
                case 1:
                    d = (Random.Range(0, (taille.getX() - 1) / 2)) * 2 + 1;
                    a = (Random.Range(0, (taille.getX() - 1) / 2)) * 2 + 1;
                    depart.setX(d);
                    depart.setY(taille.getY() - 1);

                    arrivee.setX(a);
                    arrivee.setY(0);
                    break;

                case 2:
                    d = (Random.Range(0, (taille.getY() - 1) / 2)) * 2 + 1;
                    a = (Random.Range(0, (taille.getY() - 1) / 2)) * 2 + 1;
                    depart.setX(taille.getX() - 1);
                    depart.setY(d);

                    arrivee.setX(0);
                    arrivee.setY(a);
                    break;

                case 3:
                    d = (Random.Range(0, (taille.getX() - 1) / 2)) * 2 + 1;
                    a = (Random.Range(0, (taille.getX() - 1) / 2)) * 2 + 1;
                    depart.setX(d);
                    depart.setY(0);

                    arrivee.setX(a);
                    arrivee.setY(taille.getY() - 1);
                    break;

                case 4:
                    d = (Random.Range(0, (taille.getY() - 1) / 2)) * 2 + 1;
                    a = (Random.Range(0, (taille.getY() - 1) / 2)) * 2 + 1;
                    depart.setX(0);
                    depart.setY(d);

                    arrivee.setX(taille.getX() - 1);
                    arrivee.setY(a);
                    break;
            }
        }

        public static bool quickContact(Map carte, Coordonnée newBloc)
        {
            
            int x = newBloc.getX();
            int y = newBloc.getY();
            for (int i = x-1; i < x+2; i++)
            {
                for(int j = y-1; j < y+2; j++)
                {
                    if(i != x || j != y)
                    {
                        if (carte.getVal(i, j) == 1) return false;
                    }
                }
            }
            return true;
        }

        public static bool contact(Map carte, Coordonnée newBloc, List<Coordonnée> listeB, int bloc) //détermine si tout les cases vides (sans mur) sont en contact
        {
            List<Coordonnée> listeBloc = new List<Coordonnée>();
            for(int i = 0; i < listeB.Count; i++)
            {
                listeBloc.Add(new Coordonnée(listeB[i].getX(), listeB[i].getY()));
            }

            List<Coordonnée> listeContact = new List<Coordonnée>();

            Coordonnée baseCoor = new Coordonnée(1, 1);
            listeContact.Add(baseCoor);     //on peut commencer a chercher les contacts à partir de n'importe quel case
            listeBloc.Remove(baseCoor);     //on commence donc toujours par une case que l'on sait toujours vide

            int maxi = listeBloc.Count;

            listeBloc.Remove(newBloc);

            int a = 0;
            while ((a < listeContact.Count) && (maxi != listeContact.Count))
            {
                int b = 0;
                while (b < listeBloc.Count)
                {
                    if ( ((listeBloc[b].getX() == listeContact[a].getX()) && ((listeBloc[b].getY() == ((listeContact[a].getY()) - 1)) || (listeBloc[b].getY() == (listeContact[a].getY() + 1))) ) || ( (listeBloc[b].getY() == listeContact[a].getY()) && ((listeBloc[b].getX() == (listeContact[a].getX() - 1)) || (listeBloc[b].getX() == (listeContact[a].getX() + 1)))) )
                    {
                        listeContact.Add(new Coordonnée(listeBloc[b].getX(), listeBloc[b].getY() ) );
                        
                        listeBloc.RemoveAt(b);
                        b--;


                    }
                    b++;
                }
                a++;
            }
            return maxi == listeContact.Count;
        }

        public static Map creerLabyrinthe(int largeur, int hauteur)
        {
            //on rend les dimensions impaire si elles ne le sont pas déjà
            int x = largeur - (largeur % 2) + 1;
            int y = hauteur - (hauteur % 2) + 1;
            Coordonnée taille = new Coordonnée(x, y);

            Map carte = new Map(taille);

            for (int i = 0; i < x; i++)
            {
                carte.setVal(i, 0, 4);
                carte.setVal(i, y - 1, 4);
            }

            for (int j = 0; j < y; j++)
            {
                carte.setVal(0, j, 4);
                carte.setVal(x - 1, j, 4);
            }

            //on calcule le nombre de mur à créer avec une formule magique
            int arret = (x - 3) * (y - 3) / 2;

            //on crée la liste des blocs dans lesquels il est possible de faire un mur
            List<Coordonnée> listeLibre = new List<Coordonnée>();
            for (int i1 = 1; i1 < x - 1; i1++)
            {
                for (int j1 = 1; j1 < y - 1; j1++)
                {
                    if (i1 % 2 == 0 || j1 % 2 == 0)
                    {
                        listeLibre.Add(new Coordonnée(i1, j1));
                    }
                }
            }

            List<Coordonnée> listeBloc = new List<Coordonnée>();
            for (int i = 1; i < carte.getSize().getX() - 1; i++)
            {
                for (int j = 1; j < carte.getSize().getY() - 1; j++)
                {
                    if (carte.getVal(i, j) == 0)
                    {
                        listeBloc.Add(new Coordonnée(i, j));
                    }
                }
            }

            int mur = 0;

            int iteration = 0;
            
            while (mur < arret && listeLibre.Count != 0)   //boucle de création des murs
            {
                int nbRand = Random.Range(0, listeLibre.Count);
                Coordonnée bloc = listeLibre[nbRand];

                carte.setVal(bloc.getX(), bloc.getY(), 1);
                mur++;
                if(!quickContact(carte, bloc))
                {
                    if (!contact(carte, bloc, listeBloc, 0))
                    {
                        carte.setVal(bloc.getX(), bloc.getY(), 0);
                        mur--;
                    }
                    else listeBloc.Remove(bloc);
                }
                else listeBloc.Remove(bloc);
                
                listeLibre.RemoveAt(nbRand);
                iteration++;
            }

            //on change les murs extérieurs en murs normaux
            for (int i = 0; i < x; i++)
            {
                carte.setVal(i, 0, 1);
                carte.setVal(i, y - 1, 1);
            }

            for (int j = 0; j < x; j++)
            {
                carte.setVal(0, j, 1);
                carte.setVal(x - 1, j, 1);
            }

            Coordonnée depart = new Coordonnée();
            Coordonnée arrivee = new Coordonnée();
            caseDA(taille, ref depart, ref arrivee);
            carte.setVal(depart.getX(), depart.getY(), 2);
            carte.setVal(arrivee.getX(), arrivee.getY(), 3);
            
            return carte;
        }

    };
}