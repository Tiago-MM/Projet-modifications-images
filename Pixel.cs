using System;
using System.Linq;
using System.Collections.Generic;


namespace Projetinfo
{
    //Constructeurs
	public class Pixel
	{
        byte red;
        byte green;
        byte blue;

        public Pixel(byte red, byte green, byte blue)
        {
            this.red = red;
            this.green = green;
            this.blue = blue;
        }
        public byte R
        {
            get { return this.red; }
            set { this.red = value; }
        }
        public byte G
        {
            get { return this.green; }
            set { this.green = value; }
        }
        public byte B
        {
            get { return this.blue; }
            set { this.blue = value; }
        }

        // Méthode pour trouver la couleur la plus proche dans la liste de couleurs prédéfinies
        public Pixel Lepixelleplusproche()
        {
            // Créer une liste de pixels pour représenter les couleurs prédéfinies
            List<Pixel> colors = new List<Pixel>();

            // Ajouter les couleurs prédéfinies à la liste
            colors.Add(new Pixel(0, 0, 0));           // Noir
            colors.Add(new Pixel(0, 0, 128));         // Bleu foncé
            colors.Add(new Pixel(0, 128, 0));         // Vert foncé
            colors.Add(new Pixel(0, 128, 128));       // Cyan foncé
            colors.Add(new Pixel(128, 0, 0));         // Rouge foncé
            colors.Add(new Pixel(128, 0, 128));       // Magenta foncé
            colors.Add(new Pixel(128, 128, 0));       // Jaune foncé
            colors.Add(new Pixel(192, 192, 192));     // Gris
            colors.Add(new Pixel(128, 128, 128));     // Gris foncé
            colors.Add(new Pixel(0, 0, 255));         // Bleu
            colors.Add(new Pixel(0, 255, 0));         // Vert
            colors.Add(new Pixel(0, 255, 255));       // Cyan
            colors.Add(new Pixel(255, 0, 0));         // Rouge
            colors.Add(new Pixel(255, 0, 255));       // Magenta
            colors.Add(new Pixel(255, 255, 0));       // Jaune
            colors.Add(new Pixel(255, 255, 255));     // Blanc

            // Initialiser la couleur la plus proche et la distance la plus proche à des valeurs de référence
            Pixel pixelproche = colors[0];
            double distanceproche = double.MaxValue;

            // Parcourir la liste des couleurs prédéfinies
            foreach (Pixel color in colors)
            {
                // Calculer la distance entre la couleur actuelle de la liste et la couleur de l'objet courant (this)
                double distance = Calculercouleur(this, color);

                // Mettre à jour la couleur la plus proche et la distance la plus proche si la distance actuelle est plus petite
                if (distance < distanceproche)
                {
                    distanceproche = distance;
                    pixelproche = color;
                }
            }

            // Retourner la couleur la plus proche
            return pixelproche;
        }

        // Méthode pour calculer la distance entre deux couleurs dans l'espace RVB
        private static double Calculercouleur(Pixel color1, Pixel color2)
        {
            double diffR = color1.R - color2.R;
            double diffG = color1.G - color2.G;
            double diffB = color1.B - color2.B;
            return Math.Sqrt(diffR * diffR + diffG * diffG + diffB * diffB);//Formule mathématique x^2+y^2+z^2
        }

        //Méthode d'égalité entre deux pixels
        public bool Equals(Pixel otherPixel)
        {
            if (otherPixel == null)
            {
                return false;
            }

            return R == otherPixel.R && G == otherPixel.G && B == otherPixel.B;//True or False
        }

        //Méthode pour attribuer une couleur à l'écriture dans la console
        public static ConsoleColor Bonnecouleur(Pixel closestColor)
        {
            if (closestColor.Equals(new Pixel(0, 0, 0)))        // Noir
            {
                return ConsoleColor.Black;
            }
            else if (closestColor.Equals(new Pixel(0, 0, 128)))  // Bleu foncé
            {
                return ConsoleColor.DarkBlue;
            }
            else if (closestColor.Equals(new Pixel(0, 128, 0)))  // Vert foncé
            {
                return ConsoleColor.DarkGreen;
            }
            else if (closestColor.Equals(new Pixel(0, 128, 128)))  // Cyan foncé
            {
                return ConsoleColor.DarkCyan;
            }
            else if (closestColor.Equals(new Pixel(128, 0, 0)))  // Rouge foncé
            {
                return ConsoleColor.DarkRed;
            }
            else if (closestColor.Equals(new Pixel(128, 0, 128)))  // Magenta foncé
            {
                return ConsoleColor.DarkMagenta;
            }
            else if (closestColor.Equals(new Pixel(128, 128, 0)))  // Jaune foncé
            {
                return ConsoleColor.DarkYellow;
            }
            else if (closestColor.Equals(new Pixel(192, 192, 192)))  // Gris
            {
                return ConsoleColor.Gray;
            }
            else if (closestColor.Equals(new Pixel(128, 128, 128)))  // Gris foncé
            {
                return ConsoleColor.DarkGray;
            }
            else if (closestColor.Equals(new Pixel(0, 0, 255)))  // Bleu
            {
                return ConsoleColor.Blue;
            }
            else if (closestColor.Equals(new Pixel(0, 255, 0)))  // Vert
            {
                return ConsoleColor.Green;
            }
            else if (closestColor.Equals(new Pixel(0, 255, 255)))  // Cyan
            {
                return ConsoleColor.Cyan;
            }
            else if (closestColor.Equals(new Pixel(255, 0, 0)))  // Rouge
            {
                return ConsoleColor.Red;
            }
            else if (closestColor.Equals(new Pixel(255, 0, 255)))  // Magenta
            {
                return ConsoleColor.Magenta;
            }
            else if (closestColor.Equals(new Pixel(255, 255, 0)))  // Jaune
            {
                return ConsoleColor.Yellow;
            }
            else if (closestColor.Equals(new Pixel(255, 255, 255)))  // Blanc
            {
                return ConsoleColor.White;
            }
            else
            {
                // Si aucune couleur prédéfinie ne correspond, retourner la couleur blanche
                return ConsoleColor.Gray;
            }
        }

        //Permet d'obtenir le pixel correspondant à une couleur
        public static Pixel GetPixel(string couleur)
        {
            List<Pixel> colors = new List<Pixel>();
            colors.Add(new Pixel(0, 0, 0));           // Noir
            colors.Add(new Pixel(0, 0, 128));         // Bleu foncé
            colors.Add(new Pixel(0, 128, 0));         // Vert foncé
            colors.Add(new Pixel(0, 128, 128));       // Cyan foncé
            colors.Add(new Pixel(128, 0, 0));         // Rouge foncé
            colors.Add(new Pixel(128, 0, 128));       // Magenta foncé
            colors.Add(new Pixel(128, 128, 0));       // Jaune foncé
            colors.Add(new Pixel(192, 192, 192));     // Gris
            colors.Add(new Pixel(128, 128, 128));     // Gris foncé
            colors.Add(new Pixel(0, 0, 255));         // Bleu
            colors.Add(new Pixel(0, 255, 0));         // Vert
            colors.Add(new Pixel(0, 255, 255));       // Cyan
            colors.Add(new Pixel(255, 0, 0));         // Rouge
            colors.Add(new Pixel(255, 0, 255));       // Magenta
            colors.Add(new Pixel(255, 255, 0));       // Jaune
            colors.Add(new Pixel(255, 255, 255));     // Blanc

            Pixel selectedPixel = null;

            // Renvoie le pixel de la couleur donnée
            if (couleur == "noir")
            {
                selectedPixel = colors[0];
            }
            else if (couleur == "bleu foncé")
            {
                selectedPixel = colors[1];
            }
            else if (couleur == "vert foncé")
            {
                selectedPixel = colors[2];
            }
            else if (couleur == "cyan foncé")
            {
                selectedPixel = colors[3];
            }
            else if (couleur == "rouge foncé")
            {
                selectedPixel = colors[4];
            }
            else if (couleur == "magenta foncé")
            {
                selectedPixel = colors[5];
            }
            else if (couleur == "jaune foncé")
            {
                selectedPixel = colors[6];
            }
            else if (couleur == "gris")
            {
                selectedPixel = colors[7];
            }
            else if (couleur == "gris foncé")
            {
                selectedPixel = colors[8];
            }
            else if (couleur == "bleu")
            {
                selectedPixel = colors[9];
            }
            else if (couleur == "vert")
            {
                selectedPixel = colors[10];
            }
            else if (couleur == "cyan")
            {
                selectedPixel = colors[11];
            }
            else if (couleur == "rouge")
            {
                selectedPixel = colors[12];
            }
            else if (couleur == "magenta")
            {
                selectedPixel = colors[13];
            }
            else if (couleur == "jaune")
            {
                selectedPixel = colors[14];
            }
            else if (couleur == "blanc")
            {
                selectedPixel = colors[15];
            }
            return selectedPixel;
        }

        //Déterminer la couleur que l'utilisateur souhaite choisir
        public static string Choixcouleur()
        {
            string couleur = "";
            string[] tableaucouleurs = { "noir", "bleu foncé", "vert foncé", "cyan foncé", "rouge foncé",
        "magenta foncé", "jaune foncé", "gris", "gris foncé", "bleu",
        "vert", "cyan", "rouge", "magenta", "jaune", "blanc"};//Ensemble des couleurs possible
            do
            {
                Console.WriteLine("Choisissez la couleur parmi ce choix : ");
                foreach (string a in tableaucouleurs)
                {
                    Console.WriteLine(a);
                }
                couleur = Console.ReadLine();
            }
            while (!tableaucouleurs.Contains(couleur));//Saisie sécurisée
            return (couleur);//Renvoie la couleur
        }
    }
}

