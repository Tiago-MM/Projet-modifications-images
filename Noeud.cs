using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projetinfo
{
    public class Noeud
    {
        public Noeud noeudGauche;
        public Noeud noeudDroite;
        public Pixel pixelNoeud;
        public int frequence;

        public Noeud(Noeud noeudGauche, Noeud noeudDroite, Pixel pixelNoeud, int frequence)
        {
            this.noeudGauche = noeudGauche;
            this.noeudDroite = noeudDroite;
            this.pixelNoeud = pixelNoeud;
            this.frequence = frequence;
        }

        public Noeud NoeudGauche
        {
            get { return noeudGauche; }
            set { noeudGauche = value; }
        }
        public Noeud NoeudDroite
        {
            get { return noeudDroite; }
            set { noeudDroite = value; }
        }
        public Pixel PixelNoeud
        {
            get { return pixelNoeud; }
            set { pixelNoeud = value; }
        }
        public int Frequence
        {
            get { return frequence; }
            set { frequence = value; }
        }


        public List<bool> CheminParcouru(Pixel pixelComp, List<bool> chemin)
        {
            //Condition d'arret : si on est en présence d'une feuille, cad il n'y a plus de noeud gauche et droite
            if (NoeudDroite == null && NoeudGauche == null)
            {
                // Une fois le noeud feuille/final atteint si on retrouve la bonne valeur on retourne le chemin, sinon un chemin null
                if (pixelComp.Equals(PixelNoeud)) return chemin;
                else return null;
            }
            else
            {
                //On cree deux chemin gauche et droite, un seul arrivera jusqu'a la valeur et sera different de null
                List<bool> cheminGauche = null;
                List<bool> cheminDroite = null;
                if (NoeudGauche != null) //secu
                {
                    List<bool> cheminGaucheTemp = new List<bool>();

                    cheminGaucheTemp.AddRange(chemin);  //on ajoute le chemin empreinte jusqu'a present 
                    cheminGaucheTemp.Add(false);   //on ajoute faux (0 en binaire) car on empreinte le chemin de gauche
                    cheminGauche = NoeudGauche.CheminParcouru(pixelComp, cheminGaucheTemp);// et on refait la methode en partant du noeud de gauche
                }

                if (NoeudDroite != null) //secu
                {
                    List<bool> cheminDroiteTemp = new List<bool>();
                    cheminDroiteTemp.AddRange(chemin);  //idem,on ajoute le chemin empreinte jusqu'a present 
                    cheminDroiteTemp.Add(true); //on ajoute vrai (1 en binaire) car on empreinte le chemin de droite
                    cheminDroite = NoeudDroite.CheminParcouru(pixelComp, cheminDroiteTemp);//et on refait la methode en partant du noeud de gauche
                }
                //l'un des deux chemins est le bon, si le gauche est null, on prend celui de droite, qui est lui meme une selection entre deux chemins et ainsi de suite
                //etant donne que l'arbre d'huffman est cree avec un tableau qui est le meme utilise pour l'encodage, un noeud de l'arbre a forcement une bonne valeur (equivalente a val2) donc un des chemins retourne la bonne sequence de bool, l'autre un null
                if (cheminGauche != null) return cheminGauche;
                else return cheminDroite;
            }
        }
    }
}