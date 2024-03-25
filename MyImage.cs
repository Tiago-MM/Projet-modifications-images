using System;
using System.IO;
using System.Threading;


namespace Projetinfo
{
    public class MyImage
    {
        //Constructeurs
        string type;
        int taille;
        int tailleOffset;
        int largeur;
        int hauteur;
        int nbbits;
        Pixel[,] data;
        byte[] lesautres;
        //Creation de MyImage à partir d'un fichier
        public MyImage(string myfile)
        {
            byte[] fichier = File.ReadAllBytes(myfile); // Lire les octets du fichier
            this.type += Convert.ToChar(fichier[0]); 
            this.type += Convert.ToChar(fichier[1]); // Convertir les deux octets en tant que "type"

            // Extraire les octets de la taille, largeur, hauteur, taille offset etc
            byte[] tabtaille = { fichier[2], fichier[3], fichier[4], fichier[5] };
            byte[] tabtailleoffset = { fichier[10], fichier[11], fichier[12], fichier[13] };
            byte[] tablargeur = { fichier[18], fichier[19], fichier[20], fichier[21] };
            byte[] tabhauteur = { fichier[22], fichier[23], fichier[24], fichier[25] };
            byte[] tabnbbits = { fichier[28], fichier[29] };

            // Convertir les octets de taille, largeur, hauteur,taille offset etc
            this.taille = Convertir_Endian_To_Int(tabtaille);
            this.largeur = Convertir_Endian_To_Int(tablargeur);
            this.hauteur = Convertir_Endian_To_Int(tabhauteur);
            this.tailleOffset = Convertir_Endian_To_Int(tabtailleoffset);
            this.nbbits = Convertir_Endian_To_Int(tabnbbits);

            this.data = new Pixel[this.hauteur, this.largeur]; // Créer une matrice de Pixels avec les tailles précédentes
            int ind = this.tailleOffset;

            // Parcourir les données de pixels dans le fichier et remplir la matrice de pixels
            for (int x = 0; x < this.hauteur; x++)
            {
                for (int y = 0; y < this.largeur; y++)
                {
                    byte r = fichier[ind + 2]; // Extraire la valeur du composant R 
                    byte g = fichier[ind + 1]; // Extraire la valeur du composant G
                    byte b = fichier[ind + 0]; // Extraire la valeur du composant B
                    ind += 3; 
                    this.data[x, y] = new Pixel(r, g, b); 
                }
            }

            this.lesautres = new byte[this.tailleOffset - 30]; // Créer un tableau d'octets des autres bytes
            for (int i = 30; i < this.tailleOffset; i++) // Parcourir les autres octets jusqu'à la fin du header
            {
                this.lesautres[i - 30] = fichier[i];
            }
        }
        //Création d'une instance MyImage à partir d'une largeur d'une hauteur et d'une couleur
        public MyImage(int largeur, int hauteur, Pixel a)
        {
            Byte[] tab = { 66, 77, 0, 0, 0, 0, 0, 0, 0, 0, 54, 0, 0, 0, 40, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 24, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };//Header générique
            Byte[] image = new byte[largeur * hauteur * 3 + 54];
            for (int i = 0; i < 54; i++)
            {
                image[i] = tab[i];
            }
            //Conversion, voir précédemment, même principe
            int taille = largeur * hauteur * 3 + 54;
            byte[] tableaulargeur = this.Convertir_Int_To_Endian(largeur);
            byte[] tableauhauteur = this.Convertir_Int_To_Endian(hauteur);
            byte[] tableautaille = this.Convertir_Int_To_Endian(taille);
            image[2] = tableautaille[0];
            image[3] = tableautaille[1];
            image[4] = tableautaille[2];
            image[5] = tableautaille[3];
            image[18] = tableaulargeur[0];
            image[19] = tableaulargeur[1];
            image[20] = tableaulargeur[2];
            image[21] = tableaulargeur[3];
            image[22] = tableauhauteur[0];
            image[23] = tableauhauteur[1];
            image[24] = tableauhauteur[2];
            image[25] = tableauhauteur[3];
            this.largeur = largeur;
            this.hauteur = hauteur;
            this.type = "BM";
            this.tailleOffset = 54;
            this.taille = largeur * hauteur * 3 + 54;
            this.nbbits = 24;
            this.lesautres = new byte[this.tailleOffset - 30];
            this.data = new Pixel[this.hauteur, this.largeur];
            for (int i = 30; i < this.tailleOffset; i++)
            {
                this.lesautres[i - 30] = 0;
            }
            //Matrice de pixels remplie
            for (int i = 0; i < this.hauteur; i++)
            {

                for (int j = 0; j < this.largeur; j++)
                {
                    this.data[i, j] = a;
                }
            }
        }

        public int Largeur
        {
            get { return this.largeur; }
            set { this.largeur = value; }
        }
        public Pixel[,] Data
        {
            get { return this.data; }
            set { this.data = value; }
        }
        public int Hauteur
        {
            get { return this.hauteur; }
            set { this.hauteur = value; }
        }
        public string Type
        {
            get { return this.type; }
            set { this.type = value; }
        }
        public int Taille
        {
            get { return this.taille; }
            set { this.taille = value; }
        }
        public int TailleOffset
        {
            get { return this.tailleOffset; }
            set { this.tailleOffset = value; }
        }
        public Byte[] Lesautres
        {
            get { return this.lesautres; }
            set { this.lesautres = value; }
        }
        public int Nbbits
        {
            get { return this.nbbits; }
            set { this.nbbits = value; }
        }

        //Sauvegarde de la variable MyImage dans un fichier bmp
        public void From_Image_To_File(string file)
        {
            //Réécriture dans un tableau de bytes et conversion
            byte[] imageBytes = new byte[this.taille];
            imageBytes[0] = Convert.ToByte(this.type[0]);
            imageBytes[1] = Convert.ToByte(this.type[1]);
            byte[] tableautaille = this.Convertir_Int_To_Endian(this.Taille);
            byte[] tableautailleoffset = this.Convertir_Int_To_Endian(this.TailleOffset);
            byte[] tableaulargeur = this.Convertir_Int_To_Endian(this.largeur);
            byte[] tableauhauteur = this.Convertir_Int_To_Endian(this.hauteur);
            byte[] tableaunbcouleurs = this.Convertir_Int_To_Endian(this.nbbits);
            imageBytes[2] = tableautaille[0];
            imageBytes[3] = tableautaille[1];
            imageBytes[4] = tableautaille[2];
            imageBytes[5] = tableautaille[3];
            imageBytes[10] = tableautailleoffset[0];
            imageBytes[11] = tableautailleoffset[1];
            imageBytes[12] = tableautailleoffset[2];
            imageBytes[13] = tableautailleoffset[3];
            imageBytes[14] = 40;
            imageBytes[18] = tableaulargeur[0];
            imageBytes[19] = tableaulargeur[1];
            imageBytes[20] = tableaulargeur[2];
            imageBytes[21] = tableaulargeur[3];
            imageBytes[22] = tableauhauteur[0];
            imageBytes[23] = tableauhauteur[1];
            imageBytes[24] = tableauhauteur[2];
            imageBytes[25] = tableauhauteur[3];
            imageBytes[26] = 1;
            imageBytes[28] = tableaunbcouleurs[0];
            imageBytes[29] = tableaunbcouleurs[1];
            for (int i = 30; i < this.tailleOffset; i++)
            {
                imageBytes[i] = lesautres[i - 30];
            }
            int k = this.tailleOffset;
            //parcourir les données de la matrice de pixels
            for (int j = 0; j < this.data.GetLength(0); j++)
            {
                for (int i = 0; i < this.data.GetLength(1); i++)
                {
                    imageBytes[k] = this.data[j, i].B;
                    k++;
                    imageBytes[k] = this.data[j, i].G;
                    k++;
                    imageBytes[k] = this.data[j, i].R;
                    k++;
                }
            }
            //enregistrement
            File.WriteAllBytes(file, imageBytes);
        }

        //Convertir endian en int
        public int Convertir_Endian_To_Int(byte[] tab)
        {
            int final = 0;
            for (int i = 0; i < tab.Length; i++)
            {
                final += tab[i] * (int)(Math.Pow(256, i));
            }
            return final;
        }

        //Convertir int en endian
        public byte[] Convertir_Int_To_Endian(int val)
        {
            byte[] tab = new byte[4];
            for (int i = tab.Length - 1; i >= 0; i--)
            {
                tab[i] = (byte)(val / ((int)(Math.Pow(256, i))));
                val = val % ((int)(Math.Pow(256, i)));
            }
            return tab;
        }

        //Convertir endian en byte
        public byte Convertir_EndianB_To_Byte(int[] tab)
        {
            int final = 0;
            for (int i = 0; i < tab.Length; i++)
            {
                final += tab[i] * (int)(Math.Pow(2, i));
            }
            return Convert.ToByte(final);
        }

        //Convertir byte en endian
        public int[] Convertir_Byte_To_EndianB(byte val)
        {
            int[] tab = new int[8];
            int val2 = Convert.ToInt32(val);
            for (int i = tab.Length - 1; i >= 0; i--)
            {
                tab[i] = (byte)(val2 / ((int)(Math.Pow(2, i))));
                val2 = val2 % ((int)(Math.Pow(2, i)));
            }
            return tab;
        }

        //Convertir en gris
        public void ConvertirenGris()
        {
            for (int i = 0; i < data.GetLength(0); i++)
            {
                for (int j = 0; j < data.GetLength(1); j++)
                {
                    byte gris = (byte)((data[i, j].R + data[i, j].G + data[i, j].B) / 3); //Moyenne des 3 composantes
                    data[i, j].R = gris;
                    data[i, j].G = gris;
                    data[i, j].B = gris;
                }
            }
        }

        //Convertir en Sepia
        public void Convertirensepia()
        {
            for (int i = 0; i < data.GetLength(0); i++)
            {
                for (int j = 0; j < data.GetLength(1); j++)
                {
                    // Formule de conversion en sépia
                    int nvr = (int)(data[i, j].R * 0.393 + data[i, j].G * 0.769 + data[i, j].B * 0.189);
                    int nvg = (int)(data[i, j].R * 0.349 + data[i, j].G * 0.686 + data[i, j].B * 0.168);
                    int nvb = (int)(data[i, j].R * 0.272 + data[i, j].G * 0.534 + data[i, j].B * 0.131);

                    // Vérifier les valeurs de sortie
                    nvr = (nvr > 255) ? 255 : nvr;
                    nvg = (nvg > 255) ? 255 : nvg;
                    nvb = (nvb > 255) ? 255 : nvb;

                    // Mettre à jour les composantes RGB du pixel
                    data[i, j].R = (byte)nvr;
                    data[i, j].G = (byte)nvg;
                    data[i, j].B = (byte)nvb;
                }
            }
        }

        //Convertir en noir
        public void Convertirennoir()
        {
            for (int i = 0; i < data.GetLength(0); i++)
            {
                for (int j = 0; j < data.GetLength(1); j++)
                {
                    byte noir = (byte)((data[i, j].R + data[i, j].G + data[i, j].B) / 3); //Moyenne des trois
                    if (noir > 127)
                        noir = 255;
                    else
                        noir = 0; 
                    data[i, j].R = noir;
                    data[i, j].G = noir;
                    data[i, j].B = noir;
                }

            }
        }

        //Agrandir la taille d'une image
        public void Enlarge(int coef)
        {
            Pixel[,] mat = new Pixel[this.hauteur * coef, this.largeur * coef];//Nouvelle matrice de pixels
            for (int i = 0; i < mat.GetLength(0); i++)
            {
                for (int j = 0; j < mat.GetLength(1); j++)
                {
                    mat[i, j] = data[i / coef, j / coef];//Remplissage
                }
            }
            //Mise à jour
            this.largeur = this.largeur * coef;
            this.hauteur = this.hauteur * coef;
            this.data = mat;
            this.taille = this.largeur * this.hauteur * 3 + this.tailleOffset;
        }

        //Rotation d'une image
        public void Rotation(float angle)
        {
            angle = (float)(-angle * Math.PI / 180.0);//Angle en radian
            Pixel[,] rotatedImageBytes = new Pixel[this.hauteur, this.largeur];
            int centreX = this.hauteur / 2;
            int centreY = this.largeur / 2;//Centre

            for (int x = 0; x < this.hauteur; x++) //parcourir pixels
            {
                for (int y = 0; y < this.largeur; y++)
                {
                    rotatedImageBytes[x, y] = new Pixel(0, 0, 0);//Pixel noir partout

                    int indx = (int)((x - centreX) * Math.Cos(angle) + (y - centreY) * Math.Sin(angle) + centreX); //Formules mathématique de changement de base
                    int indy = (int)((y - centreY) * Math.Cos(angle) - (x - centreX) * Math.Sin(angle) + centreY);

                    if (indx >= 0 && indx < this.hauteur && indy >= 0 && indy < this.largeur)
                    {
                        rotatedImageBytes[x, y] = this.data[indx, indy]; //Si bien compris alors dans la matrice alors écriture
                    }
                }
            }
            this.data = rotatedImageBytes;//Nouvelle matrice
        }

        // Réduire la taille de l'image
        public void Reduire(int coef)
        {
            int newWidth = this.largeur / coef; // Nouvelle taille
            int newHeight = this.hauteur / coef; 
            Pixel[,] mat = new Pixel[newHeight, newWidth]; // nouvelle matrice

            for (int i = 0; i < newHeight; i++) // Parcourir données
            {
                for (int j = 0; j < newWidth; j++) 
                {
                    int sommeR = 0; // Variable pour stocker la somme des composantes rouges
                    int sommeG = 0; // Variable pour stocker la somme des composantes vertes
                    int sommeB = 0; // Variable pour stocker la somme des composantes bleues

                    for (int k = i * coef; k < (i + 1) * coef; k++) // la zone réduite
                    {
                        for (int l = j * coef; l < (j + 1) * coef; l++) 
                        {
                            sommeR += data[k, l].R; 
                            sommeG += data[k, l].G; 
                            sommeB += data[k, l].B; 
                        }
                    }

                    // Calculer les nouvelles composantes RGB
                    mat[i, j] = new Pixel((byte)(sommeR / (coef * coef)), (byte)(sommeG / (coef * coef)), (byte)(sommeB / (coef * coef))); //Moyenne
                }
            }

            // Mettre à jour la largeur, hauteur, matrice de pixels et taille de l'image réduite
            this.largeur = newWidth;
            this.hauteur = newHeight;
            this.data = mat;
            this.taille = this.largeur * this.hauteur * 3 + this.tailleOffset;
        }

        //Methode de convolution matrice
        public void Convo(double[,] filtre)
        {
            ///Initinialise une matrice de pixels
            Pixel[,] result = new Pixel[this.hauteur, this.largeur];
            ///Multiplication des pixels autour d'un pixel avec les kernels
            for (int x = 1; x < this.hauteur - 1; x++)
            {
                for (int y = 1; y < this.largeur - 1; y++)
                {
                    double red = 0.0, green = 0.0, blue = 0.0;
                    for (int i = -1; i <= 1; i++)
                    {
                        for (int j = -1; j <= 1; j++)
                        {
                            red += this.data[x + i, y + j].R * filtre[i + 1, j + 1];//Multiplication bloc par bloc
                            green += this.data[x + i, y + j].G * filtre[i + 1, j + 1];
                            blue += this.data[x + i, y + j].B * filtre[i + 1, j + 1];
                        }
                    }
                    ///Vérification si red etc bien compris entre 0 et 255
                    int r = Math.Min(Math.Max((int)red, 0), 255);
                    int g = Math.Min(Math.Max((int)green, 0), 255);
                    int b = Math.Min(Math.Max((int)blue, 0), 255);
                    ///Initialisation du pixel
                    result[x, y] = new Pixel(Convert.ToByte(r), Convert.ToByte(g), Convert.ToByte(b));
                }
            }

            ///Pour les bords, afin de remplir
            for (int ligne = 1; ligne < hauteur - 1; ligne++)
            {
                result[ligne, 0] = result[ligne, 1];
                result[ligne, largeur - 1] = result[ligne, largeur - 2];
            }
            for (int colonne = 0; colonne < largeur; colonne++)
            {
                result[0, colonne] = result[1, colonne]; 
                result[hauteur - 1, colonne] = result[hauteur - 2, colonne]; 
            }
            //Car ça prend de base des matrices 3*3, donc les bords ne peuvent pas être pris
            this.data = result;
        }

        ///Ensemble des filtres, les commentaires à coté des filtres représentent les sources, où nous avons trouvé les filtres
        public void Flou(int nbflou = 1)///Puissance du Flou (1: faible)
        {
            double[,] flou = { { 1.0 / 9.0, 1.0 / 9.0, 1.0 / 9.0 }, { 1.0 / 9.0, 1.0 / 9.0, 1.0 / 9.0 }, { 1.0 / 9.0, 1.0 / 9.0, 1.0 / 9.0 } };//Wikipédia
            for (int i = 0; i < nbflou; i++)//puissance du flou
            {
                Convo(flou);
            }
        }
        public void Renforcement()
        {
            double[,] renforcement = { { 0, -1, 0 }, { -1, 5, -1 }, { 0, -1, 0 } };//Wikipédia
            Convo(renforcement);
        }
        public void Repoussage()
        {
            double[,] repoussage = { { -2, -1, 0 }, { -1, 1, 1 }, { 0, 1, 2 } };//Mathinfo
            Convo(repoussage);
        }
        public void Detectiondecontours1()
        {
            double[,] deteccontour1 = { { 1, 0, -1 }, { 0, 0, 0 }, { -1, 0, 1 } };//Wikipédia
            Convo(deteccontour1);
        }
        public void Detectiondecontours2()
        {
            double[,] deteccontour2 = { { 0, 1, 0 }, { 1, -4, 1 }, { 0, 1, 0 } };//Wikipédia
            Convo(deteccontour2);
        }
        public void Detectiondecontours3()
        {
            double[,] deteccontour3 = { { -1, -1, -1 }, { -1, 8, -1 }, { -1, -1, -1 } };//Wikipédia
            Convo(deteccontour3);
        }

        // Méthode pour générer une fractale de l'ensemble de Mandelbrot
        public void Fract(int recurrence, Pixel b1, Pixel b2)
        {
            int i = 0;
            Pixel[,] fractal = new Pixel[this.hauteur, this.largeur]; // Création d'un tableau de pixels pour la fractale

            // Boucle pour parcourir tous les pixels de l'image
            for (int x = 0; x < this.hauteur; x++)
            {
                for (int y = 0; y < this.largeur; y++)
                {
                    // Calcul des coordonnées complexes correspondant au pixel actuel
                    double chauteurx = (x - this.hauteur / 2.0) * 4.0 / this.hauteur;
                    double clargeury = (y - this.largeur / 2.0) * 4.0 / this.hauteur;

                    double zx = 0, zy = 0; // Initialisation des coordonnées complexes

                    // Boucle pour effectuer les récurrences 
                    for (i = 0; i < recurrence; i++)
                    {
                        double newZx = zx * zx - zy * zy + chauteurx; // Calcul des nouvelles coordonnése zx et zy
                        double newZy = 2 * zx * zy + clargeury;

                        // Si la magnitude de la coordonnée complexe dépasse 2, le point n'appartient pas à l'ensemble de Mandelbrot
                        if (newZx * newZx + newZy * newZy > 4)
                        {
                            break;
                        }

                        zx = newZx;
                        zy = newZy; // Mise à jour de la coordonnée zx et zy
                    }

                    // Si le point a atteint le nombre maximal de récurrences, il appartient à l'ensemble de Mandelbrot
                    if (i == recurrence)
                    {
                        fractal[x, y] = b1; // Attribution de la couleur b1 au pixel dans le tableau de pixels de la fractale
                    }
                    else
                    {
                        fractal[x, y] = b2; // Sinon, attribution de la couleur b2
                    }
                }

                this.data = fractal; // Mise à jour du tableau de pixels de l'image avec la fractale générée
                //Cf https://fr.wikipedia.org/wiki/Ensemble_de_Mandelbrot
            }
        }

        //Méthode miroir
        public void Miroir()
        {
            Pixel[,] miroir = new Pixel[this.hauteur, this.largeur];
            for (int i = 0; i < this.hauteur; i++)
            {
                for (int j = 0; j < this.largeur; j++)
                {
                    miroir[i, this.largeur - 1 - j] = this.data[i, j];//Inversement
                }
            }
            this.data = miroir;
        }

        //Cacher une image dans une autres
        public void Hide(MyImage imageSecrete)
        {
            Pixel[,] nv = new Pixel[this.hauteur, this.largeur];
            for (int i = 0; i < this.hauteur; i++)
            {
                for (int j = 0; j < this.largeur; j++)
                {
                    ///Tableaux de binaires de l'image de référence
                    int[] R1 = Convertir_Byte_To_EndianB(data[i, j].R);
                    int[] G1 = Convertir_Byte_To_EndianB(data[i, j].G);
                    int[] B1 = Convertir_Byte_To_EndianB(data[i, j].B);

                    ///Tableaux de binaires initialisés à 0
                    int[] R2 = { 0, 0, 0, 0, 0, 0, 0, 0 };
                    int[] G2 = R2;
                    int[] B2 = G2;

                    ///Si les (i,j) bien compris dans le format de l'image secrète => Modification
                    ///Sinon ils restent nuls
                    if (i < imageSecrete.data.GetLength(0) && j < imageSecrete.data.GetLength(1))
                    {
                        R2 = Convertir_Byte_To_EndianB(imageSecrete.data[i, j].R);
                        G2 = Convertir_Byte_To_EndianB(imageSecrete.data[i, j].G);
                        B2 = Convertir_Byte_To_EndianB(imageSecrete.data[i, j].B);
                    }
                    ///On met dans les 4 premiers bits(poids faibles) de R1,G1,B1 les 4 derniers bits(poids forts) de l'image à cacher
                    for (int k = 0; k < 4; k++)
                    {
                        R1[k] = R2[k + 4];
                        G1[k] = G2[k + 4];
                        B1[k] = B2[k + 4];
                    }
                    ///Initilaisation de chaque pixel
                    nv[i, j] = new Pixel(Convertir_EndianB_To_Byte(R1), Convertir_EndianB_To_Byte(G1), Convertir_EndianB_To_Byte(B1));
                }
            }
            this.data = nv;
        }
        /// Processus inverse
        /// Trouver l'image cachée
        public void Find()
        {
            Pixel[,] nv = new Pixel[this.hauteur, this.largeur];
            for (int i = 0; i < this.hauteur; i++)
            {
                for (int j = 0; j < this.largeur; j++)
                {
                    ///Convertir pixels de l'image en tableaux de binaires
                    int[] R1 = Convertir_Byte_To_EndianB(data[i, j].R);
                    int[] G1 = Convertir_Byte_To_EndianB(data[i, j].G);
                    int[] B1 = Convertir_Byte_To_EndianB(data[i, j].B);

                    ///Initialisation
                    int[] R2 = { 0, 0, 0, 0, 0, 0, 0, 0 };
                    int[] G2 = { 0, 0, 0, 0, 0, 0, 0, 0 };
                    int[] B2 = { 0, 0, 0, 0, 0, 0, 0, 0 };

                    ///Les 4 premiets bits(poids faibles) de R1,G1,B1 de la nouvelle image
                    ///sont placés dans les 4 derniers bits(poids forts) de R2,G2,B2
                    for (int k = 0; k < 4; k++)
                    {
                        R2[k + 4] = R1[k];
                        G2[k + 4] = G1[k];
                        B2[k + 4] = B1[k];
                    }
                    ///Initialisation de chaque pixel
                    nv[i, j] = new Pixel(Convertir_EndianB_To_Byte(R2), Convertir_EndianB_To_Byte(G2), Convertir_EndianB_To_Byte(B2));
                }
            }
            this.data = nv;
        }

        //Interverir les couleurs RGB
        public void Intervertircouleur(string a, string b)
        {
            //Bleu et rouge
            if ((a == "bleu" && b == "rouge") || (b == "bleu" && a == "rouge"))
            {
                for (int i = 0; i < this.data.GetLength(0); i++)
                {
                    for (int j = 0; j < this.data.GetLength(1); j++)
                    {
                        byte rouge = this.data[i, j].R;
                        this.data[i, j] = new Pixel(this.data[i, j].B, this.data[i, j].G, rouge); //Remplacement B par R
                    }
                }
            }
            //Bleu et vert
            else if ((a == "bleu" && b == "vert") || (b == "bleu" && a == "vert"))
            {
                for (int i = 0; i < this.data.GetLength(0); i++)
                {
                    for (int j = 0; j < this.data.GetLength(1); j++)
                    {
                        byte rouge = this.data[i, j].G;
                        this.data[i, j] = new Pixel(this.data[i, j].R, this.data[i, j].B, rouge);//Idem
                    }
                }
            }
            //Rouge et vert
            else
            {
                for (int i = 0; i < this.data.GetLength(0); i++)
                {
                    for (int j = 0; j < this.data.GetLength(1); j++)
                    {
                        byte rouge = this.data[i, j].R;
                        this.data[i, j] = new Pixel(this.data[i, j].G, rouge, this.data[i, j].B);//Idem
                    }
                }
            }
        }

        //Renvoie le complémentaire d'une image, image négative
        public void Complementaire()
        {
            for (int i = 0; i < this.data.GetLength(0); i++)
            {
                for (int j = 0; j < this.data.GetLength(1); j++)
                {
                    this.data[i, j] = new Pixel(Convert.ToByte(Convert.ToInt32(255 - this.data[i, j].R)),
                        Convert.ToByte(Convert.ToInt32(255 - this.data[i, j].G)),
                        Convert.ToByte(Convert.ToInt32(255 - this.data[i, j].B))); //Formule car complémentaire correpsond à cette formule
                }
            }
        }

        //Méthodes des dessins de chiffres
        private void dessiner1(int x)
        {
            for (int j = x; j < x + 20; j++)
            {
                for (int i = 20; i < 180; i++)
                {
                    this.Data[i, j] = new Pixel(0, 255, 0);
                }
            }
        }
        private void dessiner2(int x)
        {
            for (int j = x; j < x + 20; j++)
            {
                for (int i = 90; i < 180; i++)
                {
                    this.Data[i, j] = new Pixel(0, 255, 0);
                }
            }
            for (int j = x - 60; j < x - 60 + 20; j++)
            {
                for (int i = 20; i < 100; i++)
                {
                    this.Data[i, j] = new Pixel(0, 255, 0);
                }
            }
            //Trait haut
            for (int j = 160; j < 180; j++)
            {
                for (int i = x; i >= x - 60; i--)
                {
                    this.Data[j, i] = new Pixel(0, 255, 0);
                }
            }
            //Trait milieu
            for (int j = 90; j < 110; j++)
            {
                for (int i = x; i >= x - 60; i--)
                {
                    this.Data[j, i] = new Pixel(0, 255, 0);
                }
            }
            //Trait bas
            for (int j = 20; j < 40; j++)
            {
                for (int i = x + 20; i >= x + 20 - 60; i--)
                {
                    this.Data[j, i] = new Pixel(0, 255, 0);
                }
            }

        }
        private void dessiner3(int x)
        {
            for (int j = x; j < x + 20; j++)
            {
                for (int i = 20; i < 180; i++)
                {
                    this.Data[i, j] = new Pixel(0, 255, 0);
                }
            }
            for (int j = 160; j < 180; j++)
            {
                for (int i = x; i >= x - 60; i--)
                {
                    this.Data[j, i] = new Pixel(0, 255, 0);
                }
            }
            for (int j = 20; j < 40; j++)
            {
                for (int i = x; i >= x - 60; i--)
                {
                    this.Data[j, i] = new Pixel(0, 255, 0);
                }
            }
            for (int j = 90; j < 110; j++)
            {
                for (int i = x; i >= x - 60; i--)
                {
                    this.Data[j, i] = new Pixel(0, 255, 0);
                }
            }
        }
        private void dessiner4(int x)
        {
            for (int j = x; j < x + 20; j++)
            {
                for (int i = 20; i < 180; i++)
                {
                    this.Data[i, j] = new Pixel(0, 255, 0);
                }
            }
            for (int j = 90; j < 110; j++)
            {
                for (int i = x; i >= x - 60; i--)
                {
                    this.Data[j, i] = new Pixel(0, 255, 0);
                }
            }
            //Trait gauche
            for (int j = x - 60; j < x - 60 + 20; j++)
            {
                for (int i = 90; i < 180; i++)
                {
                    this.Data[i, j] = new Pixel(0, 255, 0);
                }
            }

        }
        private void dessiner5(int x)
        {
            for (int j = x - 60; j < x - 60 + 20; j++)
            {
                for (int i = 90; i < 180; i++)
                {
                    this.Data[i, j] = new Pixel(0, 255, 0);
                }
            }
            for (int j = x; j < x + 20; j++)
            {
                for (int i = 20; i < 100; i++)
                {
                    this.Data[i, j] = new Pixel(0, 255, 0);
                }
            }
            //Trait haut
            for (int j = 160; j < 180; j++)
            {
                for (int i = x + 20; i >= x + 20 - 60; i--)
                {
                    this.Data[j, i] = new Pixel(0, 255, 0);
                }
            }
            //Trait milieu
            for (int j = 90; j < 110; j++)
            {
                for (int i = x + 19; i >= x + 19 - 60; i--)
                {
                    this.Data[j, i] = new Pixel(0, 255, 0);
                }
            }
            //Trait bas
            for (int j = 20; j < 40; j++)
            {
                for (int i = x; i >= x - 60; i--)
                {
                    this.Data[j, i] = new Pixel(0, 255, 0);
                }
            }

        }
        private void dessiner6(int x)
        {
            for (int j = x - 60; j < x - 60 + 20; j++)
            {
                for (int i = 20; i < 180; i++)
                {
                    this.Data[i, j] = new Pixel(0, 255, 0);
                }
            }
            for (int j = x; j < x + 20; j++)
            {
                for (int i = 20; i < 100; i++)
                {
                    this.Data[i, j] = new Pixel(0, 255, 0);
                }
            }
            //Trait haut
            for (int j = 160; j < 180; j++)
            {
                for (int i = x + 20; i >= x + 20 - 60; i--)
                {
                    this.Data[j, i] = new Pixel(0, 255, 0);
                }
            }
            //Trait milieu
            for (int j = 90; j < 110; j++)
            {
                for (int i = x + 19; i >= x + 19 - 60; i--)
                {
                    this.Data[j, i] = new Pixel(0, 255, 0);
                }
            }
            //Trait bas
            for (int j = 20; j < 40; j++)
            {
                for (int i = x; i >= x - 60; i--)
                {
                    this.Data[j, i] = new Pixel(0, 255, 0);
                }
            }
            for (int j = x; j < x + 20; j++)
            {
                for (int i = 20; i < 100; i++)
                {
                    this.Data[i, j] = new Pixel(0, 255, 0);
                }
            }

        }
        private void dessiner7(int x)
        {
            for (int j = x; j < x + 20; j++)
            {
                for (int i = 20; i < 180; i++)
                {
                    this.Data[i, j] = new Pixel(0, 255, 0);
                }
            }
            for (int j = 160; j < 180; j++)
            {
                for (int i = x; i >= x - 60; i--)
                {
                    this.Data[j, i] = new Pixel(0, 255, 0);
                }
            }
        }
        private void dessiner8(int x)
        {
            for (int j = x - 60; j < x - 60 + 20; j++)
            {
                for (int i = 20; i < 180; i++)
                {
                    this.Data[i, j] = new Pixel(0, 255, 0);
                }
            }
            for (int j = x; j < x + 20; j++)
            {
                for (int i = 20; i < 180; i++)
                {
                    this.Data[i, j] = new Pixel(0, 255, 0);
                }
            }
            for (int j = 160; j < 180; j++)
            {
                for (int i = x; i >= x - 60; i--)
                {
                    this.Data[j, i] = new Pixel(0, 255, 0);
                }
            }
            for (int j = 20; j < 40; j++)
            {
                for (int i = x; i >= x - 60; i--)
                {
                    this.Data[j, i] = new Pixel(0, 255, 0);
                }
            }
            for (int j = 90; j < 110; j++)
            {
                for (int i = x; i >= x - 60; i--)
                {
                    this.Data[j, i] = new Pixel(0, 255, 0);
                }
            }

        }
        private void dessiner9(int x)
        {
            for (int j = x; j < x + 20; j++)
            {
                for (int i = 20; i < 180; i++)
                {
                    this.Data[i, j] = new Pixel(0, 255, 0);
                }
            }
            for (int j = 160; j < 180; j++)
            {
                for (int i = x; i >= x - 60; i--)
                {
                    this.Data[j, i] = new Pixel(0, 255, 0);
                }
            }
            for (int j = 20; j < 40; j++)
            {
                for (int i = x; i >= x - 60; i--)
                {
                    this.Data[j, i] = new Pixel(0, 255, 0);
                }
            }
            for (int j = 90; j < 110; j++)
            {
                for (int i = x; i >= x - 60; i--)
                {
                    this.Data[j, i] = new Pixel(0, 255, 0);
                }
            }
            for (int j = x - 60; j < x - 60 + 20; j++)
            {
                for (int i = 90; i < 180; i++)
                {
                    this.Data[i, j] = new Pixel(0, 255, 0);
                }
            }
        }
        private void dessiner0(int x)
        {
            for (int j = x; j < x + 20; j++)
            {
                for (int i = 20; i < 180; i++)
                {
                    this.Data[i, j] = new Pixel(0, 255, 0);
                }
            }
            for (int j = x - 60; j < x - 60 + 20; j++)
            {
                for (int i = 20; i < 180; i++)
                {
                    this.Data[i, j] = new Pixel(0, 255, 0);
                }
            }
            for (int j = 160; j < 180; j++)
            {
                for (int i = x; i >= x - 60; i--)
                {
                    this.Data[j, i] = new Pixel(0, 255, 0);
                }
            }
            for (int j = 20; j < 40; j++)
            {
                for (int i = x; i >= x - 60; i--)
                {
                    this.Data[j, i] = new Pixel(0, 255, 0);
                }
            }

        }
        private void deuxpoints(int x)
        {
            for (int i = x; i <= x + 19; i++)
            {
                for (int j = 55; j < 75; j++)
                {
                    this.Data[j, i] = new Pixel(0, 255, 0);
                }
            }
            for (int i = x; i <= x + 19; i++)
            {
                for (int j = 125; j < 145; j++)
                {
                    this.Data[j, i] = new Pixel(0, 255, 0);
                }
            }
        }


        //Méthode pour avoir l'heure
        public static MyImage Ilestquelleheure(string a = "HH:mm:ss")
        {
            MyImage test = new MyImage(752, 200, new Pixel(0,0,0));//MyImage avec matrice de pixels noirs
            DateTime heureActuelle = DateTime.Now;
            string heure = heureActuelle.ToString(a); //obtenir l'heure
            int position = 0;
            for (int i = 0; i < 8; i++) //Les positions
            {
                switch (i)
                {
                    case 0:
                        position = 70;
                        break;
                    case 1:
                        position = 170;
                        break;
                    case 2:
                        position = 211;
                        break;
                    case 3:
                        position = 310;
                        break;
                    case 4:
                        position = 410;
                        break;
                    case 5:
                        position = 451;
                        break;
                    case 6:
                        position = 551;
                        break;
                    case 7:
                        position = 651;
                        break;
                }
                switch (heure[i]) //Le dessin
                {
                    case '0':
                        test.dessiner0(position);
                        break;
                    case '1':
                        test.dessiner1(position);
                        break;
                    case '2':
                        test.dessiner2(position);
                        break;
                    case '3':
                        test.dessiner3(position);
                        break;
                    case '4':
                        test.dessiner4(position);
                        break;
                    case '5':
                        test.dessiner5(position);
                        break;
                    case '6':
                        test.dessiner6(position);
                        break;
                    case '7':
                        test.dessiner7(position);
                        break;
                    case '8':
                        test.dessiner8(position);
                        break;
                    case '9':
                        test.dessiner9(position);
                        break;
                    case ':':
                        test.deuxpoints(position);
                        break;
                }
            }
            return test;//renvoie l'image modifié
        }

        //Horloge qui s'affiche dans la console, tourne indéfiniment
        public static void Horloge()
        {
            MyImage hello;
            while (true)//recommencer
            {
                Console.Clear();
                hello = Ilestquelleheure();//Heure
                hello.Reduire(10);//Diminution de taille pour rentrer dans la console
                hello.Afficher();//Afficher
                Thread.Sleep(600);
                Console.Clear();//Effacer
            }
        }

        //Dessiner un cercle pour le smiley
        public static MyImage Cercle(Pixel ab)
        {
            // Création d'une matrice de pixels blanche de 40 par 40
            MyImage a = new MyImage(40, 40, new Pixel(0,0,0));
            Pixel[,] matrix = new Pixel[40, 40];
            // Dessin d'un cercle noir dans la matrice
            int centerX = 20; // Coordonnée X du centre du cercle
            int centerY = 20; // Coordonnée Y du centre du cercle
            int radius = 19; // Rayon du cercle

            for (int x = 0; x < 40; x++)
            {
                for (int y = 0; y < 40; y++)
                {
                    // Calcul de la distance entre le pixel courant et le centre du cercle
                    double distance = Math.Sqrt(Math.Pow(x - centerX, 2) + Math.Pow(y - centerY, 2));

                    // Si la distance est inférieure au rayon du cercle, alors le pixel est à l'intérieur du cercle
                    if (distance < radius)
                    {
                        // Modification de la couleur du pixel
                        matrix[x, y] = ab;
                    }
                    else
                    {
                        matrix[x, y] = new Pixel(0,0,0);//Sinon pixel noir
                    }
                }
            }
            a.data = matrix;
            return (a);
        }

        //Créer la bouche
        public void Bouche()
        {
            for (int i = 8; i < 10; i++)
            {
                for (int j = 13; j < 28; j++)
                {
                    this.data[i, j] = new Pixel(255, 255, 255);//Dessin de bouche
                }
            }
        }
        //Dessin bouche content
        public void Content()
        {
            for (int i = 8; i < 10; i++)
            {
                for (int j = 13; j < 28; j++)
                {
                    this.data[i, j] = new Pixel(255, 255, 255);
                }
            }
            for (int k = 0; k < 6; k++)
            {
                for (int j = 14+k; j < 27-k; j++)
                {
                    this.data[7-k, j] = new Pixel(255, 255, 255);
                }
            }
        }

        //Dessin bouche triste
        public void BoucheTriste()
        {
            for (int i = 8; i < 10; i++)
            {
                for (int j = 13; j < 28; j++)
                {
                    this.data[i, j] = new Pixel(255, 255, 255);
                }
            }
            this.data[8, 12] = new Pixel(255, 255, 255);
            this.data[8, 28] = new Pixel(255, 255, 255);
            // Dessin des coins de la bouche pour former une courbe

        }

        //Dessin des yeux
        public void Yeux(Pixel abc)
        {
            for (int i = 25; i < 30; i++)
            {
                for (int j = 10; j < 15; j++)
                {
                    this.data[i, j] = new Pixel(255, 255, 255);
                    this.data[i, j+16] = new Pixel(255, 255, 255);//Blanc des yeux
                }
            }
            for (int i = 26; i < 29; i++)//iris
            {
                for (int j = 11; j < 14; j++)
                {
                    this.data[i, j] = abc;
                    this.data[i, j + 16] = abc;
                }
            }
            this.data[27, 12] = new Pixel(0, 0, 0);
            this.data[27, 12 + 16] = new Pixel(0, 0, 0);//pupilles

        }

        //Goutte d'eau
        public void Gouttes(int x=25)
        {
            int k = 0;
            this.data[x, 12] = new Pixel(0, 0, 255);
            for (int i = x-1; i > x-3; i--)
            {
                for (int j = 11-k; j < 14+k; j++)
                {
                    this.data[i, j] = new Pixel(0, 0, 255);
                }
                k += 1;
            }
            k -= 1;
            for (int i = x-3; i > x-6; i--)
            {
                for (int j = 11 - k; j < 14 + k; j++)
                {
                    this.data[i, j] = new Pixel(0, 0, 255);
                }
            }
            k -= 1;
            for (int j = 11 + k; j < 14 - k; j++)
                {
                    this.data[x-6, j] = new Pixel(0, 0, 255);
                }

        }

        //Smiley qui pleure
        public static void Pleurer()
        {
            //Console.ReadLine();
            int k = 25;
            while (true)//Tourne indéfiniment
            {
                
                Console.Clear();
                MyImage cercle = Cercle(new Pixel(200,255,0));
                cercle.BoucheTriste();
                cercle.Yeux(new Pixel(0,255,255));
                cercle.Gouttes(k);
                cercle.Afficher();
                k -= 1;
                Thread.Sleep(700);
                if (k == 7) { k = 25; }//La goute remonte si dépasse
                Console.Clear();
            }
        }

        //Afficher l'image dans la console
        public void Afficher()
        {
            // Parcourir les pixels
            for (int i = this.data.GetLength(0) - 1; i >= 0; i--)
            {
                for (int j = 0; j < this.data.GetLength(1); j++)
                {
                    // Récupérer la couleur la plus proche du pixel à l'emplacement (i, j)
                    Pixel closestColor = this.data[i, j].Lepixelleplusproche();

                    // Définir la couleur de texte de la console comme étant la couleur la plus proche du pixel
                    Console.ForegroundColor = Pixel.Bonnecouleur(closestColor);

                    // Afficher "**" pour représenter le pixel à l'emplacement (i, j)
                    Console.Write("**");
                }
                Console.WriteLine();
            }
        }

        //Retouche photo
        public void Retouche(int d, int c, int zone)
        {
            int k = 1;
            int indice = zone / 2;

            // Boucle pour copier les pixels de la zone de retouche vers le haut de l'image
            for (int i = c - indice; i < c; i++)
            {
                for (int j = d - indice; j < d + indice; j++)
                {
                    this.data[i, j] = this.data[c - indice, d - indice + k]; // Copier les pixels de la position de départ 
                    k += 1;
                }
                k = 0; // Réinitialiser le compteur k
            }

            // Copier quelques pixels supplémentaires pour améliorer la transition
            this.data[c + 1, d - 1] = this.data[c - indice, d - indice + k];
            this.data[c + 1, d] = this.data[c - indice, d - indice + k]; 
            this.data[c + 1, d + 1] = this.data[c - indice, d - indice + k];
            this.data[c + 2, d] = this.data[c - indice, d - indice + k];

            // Boucle pour copier les pixels de la zone de retouche vers le bas de l'image
            for (int i = c + indice - 1; i >= c; i--)
            {
                for (int j = d - indice; j < d + indice; j++)
                {
                    this.data[i, j] = this.data[c + indice - 1, d - indice + k]; // Copier les pixels de la position de départ 
                    k += 1;
                }
                k = 0; // Réinitialiser le compteur k
            }
        }

        //Retirer la couleur d'une image
        public void RetirerCouleur(string a)
        {
            // Boucle pour parcourir tous les pixels de l'image
            for (int i = 0; i < this.data.GetLength(0); i++)
            {
                for (int j = 0; j < this.data.GetLength(1); j++)
                {
                    // Vérifier la couleur spécifiée à retirer
                    if (a == "rouge")
                    {
                        // Remplacer la composante rouge du pixel par 0
                        this.data[i, j] = new Pixel(0, this.data[i, j].G, this.data[i, j].B);
                    }
                    else if (a == "vert")
                    {
                        // Remplacer la composante verte du pixel par 0
                        this.data[i, j] = new Pixel(this.data[i, j].R, 0, this.data[i, j].B);
                    }
                    else
                    {
                        // Remplacer la composante bleue du pixel par 0
                        this.data[i, j] = new Pixel(this.data[i, j].R, this.data[i, j].G, 0);
                    }
                }
            }
        }

        //Rogner une image
        public void Rogner(int b, int a)
        {
            // Créer une nouvelle matrice de pixels pour stocker l'image rognée
            Pixel[,] nouveau = new Pixel[a, b];

            // Calculer les coordonnées du centre de l'image
            int centreX = this.hauteur / 2;
            int centreY = this.largeur / 2;

            // Calculer les coordonnées du début d'écriture de l'image
            int startX = centreX - a / 2;
            int startY = centreY - b / 2;

            // Copier les pixels de la zone rognée dans la nouvelle matrice de pixels
            for (int i = startX, c = 0; i < startX + a; i++, c++)
            {
                for (int j = startY, d = 0; j < startY + b; j++, d++)
                {
                    nouveau[c, d] = this.data[i, j];
                }
            }

            // Mettre à jour la matrice de pixels
            this.data = nouveau;

            // Mettre à jour la largeur et la hauteur
            this.largeur = b;
            this.hauteur = a;

            // Mettre à jour la taille de l'image
            this.taille = this.largeur * this.hauteur * 3 + this.tailleOffset;
        }

    }
}
