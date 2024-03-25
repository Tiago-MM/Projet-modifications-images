using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Projetinfo
{
    public class ArbreHuffman
    {
        public Dictionary<Pixel, int> DicoFrequence;
        public Noeud racine;
        public ArbreHuffman(Noeud racine, Dictionary<Pixel, int> DicoFrequence)
        {
            this.racine = racine;
            this.DicoFrequence = new Dictionary<Pixel, int>();
        }

        public Noeud Racine
        {
            get { return racine; }
            set { racine = value; }
        }

        public void CreerArbre(Pixel[,] mat)        //On cree l'abre d'Huffman pour un tableau donne
        {
            Pixel[] tab = MatEnTab(mat);
            List<Noeud> listeNoeud = new List<Noeud>();
            //On remplit le dico avec le nombre et sa frequence depuis le tableau
            for (int i = 0; i < tab.Length; i++)
            {
                //si la valeur n'est pas renseigne, on le fait en mettant sa frequence a zero
                if (!DicoFrequence.ContainsKey(tab[i]))
                {
                    DicoFrequence.Add(tab[i], 0);
                }

                //et a chaque fois que l'on retrouve la valeur tab[i], on augmente la frequence de 1 
                DicoFrequence[tab[i]]++;
            }
            //Pour chaque couple du dico, on cree un noeud avec sa valeur et frequence respective 
            foreach (Pixel unPixel in DicoFrequence.Keys)
            {
                listeNoeud.Add(new Noeud(null, null, unPixel, DicoFrequence[unPixel])); //On met ces noeuds dans une liste
            }

            //On range les noeuds selon leur frequence
            listeNoeud = listeNoeud.OrderBy(noeud => noeud.Frequence).ToList<Noeud>();
            //On cree la dependance des noeuds entre eux et on trouve le noeud racine
            while (listeNoeud.Count >= 2)  //On veut une liste compose d'un seul noeud : le noeud racine
            {
                //On range les noeuds selon leur frequence a chaque fois pour toujours avoir celui de plus faible frequence en 1er
                listeNoeud = listeNoeud.OrderBy(noeud => noeud.Frequence).ToList<Noeud>();

                //On prend les deux premiers elements 
                Noeud noeud1 = listeNoeud[0];
                Noeud noeud2 = listeNoeud[1];

                //Creer un element parent des deux noeuds qui a pour noeud gauche le 1er et noeud droite le 2eme
                //Frequence prend la somme pour pouvoir l'ordonner dans la liste après
                Noeud parent = new Noeud(noeud1, noeud2, null, noeud1.Frequence + noeud2.Frequence);

                //On enleve ces deux noeuds a la liste 
                listeNoeud.Remove(noeud1);
                listeNoeud.Remove(noeud2);
                //et on ajoute le noeud parent, qui va servir a cree un nouveau parent 
                listeNoeud.Add(parent);

            }
            if (listeNoeud == null)       // Securise
            {
                Racine = null;
            }
            else
            {
                Racine = listeNoeud[0];  //Logiquement il ne devrait rester qu'un seul noeud
            }
        }

        public BitArray Encoder(Pixel[,] mat)
        {
            Pixel[] tab = MatEnTab(mat);
            List<bool> tabEncode = new List<bool>();
            for (int i = 0; i < tab.Length; i++)
            {
                //pour chaque valeur du tableau de Pixel, on note le chemin parcouru (en boolean) 
                List<bool> listeBoolTemp = Racine.CheminParcouru(tab[i], new List<bool>());

                //et on ajoute les chemins parcourus a la suite dans une liste
                tabEncode.AddRange(listeBoolTemp);

            }

            //on transforme le tableau de boolean en instance de la classe BitArray (false=0/true=1)
            BitArray tabBin = new BitArray(tabEncode.ToArray());
            return tabBin;
        }

        public Pixel[] Decoder(BitArray tabBin)
        {

            List<Pixel> listeDecode = new List<Pixel>();
            Noeud noeudActuel = Racine;          //le noeud va changer pour suivre le chemin indique par le tableau tabBit, il commence a la racine

            foreach (bool binary in tabBin)
            {
                if (!binary)                   //si on trouve un zero (soit un false) on empreinte le chemin de gauche
                {
                    if (noeudActuel.NoeudGauche != null)         //Secu
                        noeudActuel = noeudActuel.NoeudGauche;   //donc le noeud actuel devient celui de gauche pour continuer le chemin avec le reste des valeurs binaires
                }
                else       //si on trouve un 1 soit un true on empreinte celui de droite
                {
                    if (noeudActuel.NoeudDroite != null)         //Secu
                        noeudActuel = noeudActuel.NoeudDroite;   //idem que pour la gauche 
                }
                //Si le noeud actuel est le dernier noeud, la "feuille", on peut reporter la valeur obtenue dans le tableau 
                if (noeudActuel.NoeudGauche == null && noeudActuel.NoeudDroite == null)
                {
                    listeDecode.Add(noeudActuel.PixelNoeud);
                    noeudActuel = Racine;
                }
            }

            Pixel[] tabDecode = listeDecode.ToArray();
            return tabDecode;
        }
        public Pixel[] MatEnTab(Pixel[,] mat)
        {
            Pixel[] tab = new Pixel[mat.GetLength(0) * mat.GetLength(1)];
            for (int i = 0; i < mat.GetLength(0); i++)
            {
                for (int j = 0; j < mat.GetLength(1); j++)
                {
                    tab[i + j * mat.GetLength(0)] = mat[i, j];
                }
            }
            return tab;
        }
        public Pixel[,] TabEnMat(Pixel[] tab, int hauteur, int largeur)
        {
            Pixel[,] mat = new Pixel[hauteur, largeur];
            for (int i = 0; i < hauteur; i++)
            {
                for (int j = 0; j < largeur; j++)
                {
                    mat[i, j] = tab[i + j * hauteur];
                }
            }
            return mat;
        }

    }
}