using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Collections;
namespace Projetinfo
{
    public class Menu
    {
        public static MyImage Fonction(string affichage)
        {
            MyImage imageutilisee = null;
            string userInput = "";
            string[] tableaudemots1 = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16" };
            do
            {
                if (affichage == "affichage1")
                {
                    Console.Clear();
                    Console.WriteLine("Choisissez la fonction que vous souhaitez utiliser :");
                    Console.WriteLine(
                        "1  Mettre en noir\n" +
                        "2  Mettre en gris\n" +
                        "3  Agrandir l'image\n" +
                        "4  Rotation d'image\n" +
                        "5  Renforcement d'image\n" +
                        "6  Repoussage d'image\n" +
                        "7  Détection de contours 1\n" +
                        "8  Détection de contours 2\n" +
                        "9  Détection de contours 3\n" +
                        "10 Image flou\n" +
                        "11 Cacher une image\n" +
                        "12 Retrouver une image\n" +
                        "13 Fractale\n" +
                        "14 Huffman\n" +
                        "15 Céer une image\n" +
                        "16 Innovations\n"
                        );
                }
                else
                {
                    Console.Clear();
                    MyImage test = new MyImage("./Images/autres/Menu1.bmp");
                    test.Afficher();
                    Console.ForegroundColor = ConsoleColor.White;
                }

                userInput = Console.ReadLine();
            }
            while (!tableaudemots1.Contains(userInput));
            Console.Clear();
            switch (userInput)
            {
                case "1":
                    Console.WriteLine("Mettre en noir !");
                    Thread.Sleep(2000);
                    imageutilisee = Demander(affichage);
                    imageutilisee.Convertirennoir();
                    break;
                case "2":
                    Console.WriteLine("Mettre en gris !");
                    Thread.Sleep(2000);
                    imageutilisee = Demander(affichage);
                    imageutilisee.ConvertirenGris();
                    break;
                case "3":
                    Console.WriteLine("Agrandir l'image");
                    Thread.Sleep(2000);
                    imageutilisee = Demander(affichage);
                    int agrandissement = 0;
                    Console.WriteLine("Entrer le coefficient d'agrandissement :");
                    do
                    {
                        agrandissement = Convert.ToInt32(Console.ReadLine());
                    }
                    while (agrandissement <= 0);
                    imageutilisee.Enlarge(agrandissement);
                    break;
                case "4":
                    Console.WriteLine("Rotation d'image");
                    Thread.Sleep(2000);
                    imageutilisee = Demander(affichage);
                    int degre = 0;
                    Console.WriteLine("Entrer le degré de rotation :");
                    do
                    {
                        degre = Convert.ToInt32(Console.ReadLine());
                    }
                    while (degre < 0);
                    imageutilisee.Rotation(degre);
                    break;
                case "5":
                    Console.WriteLine("Renforcement d'image");
                    Thread.Sleep(2000);
                    imageutilisee = Demander(affichage);
                    imageutilisee.Renforcement();
                    break;
                case "6":
                    Console.WriteLine("Repoussage d'image");
                    Thread.Sleep(2000);
                    imageutilisee = Demander(affichage);
                    imageutilisee.Repoussage();
                    break;
                case "7":
                    Console.WriteLine("Détection de contours 1");
                    Thread.Sleep(2000);
                    imageutilisee = Demander(affichage);
                    imageutilisee.Detectiondecontours1();
                    break;
                case "8":
                    Console.Clear();
                    Console.WriteLine("Détection de contours 2");
                    Thread.Sleep(2000);
                    imageutilisee = Demander(affichage);
                    imageutilisee.Detectiondecontours2();
                    break;
                case "9":
                    Console.Clear();
                    Console.WriteLine("Détection de contours 3");
                    Thread.Sleep(2000);
                    imageutilisee = Demander(affichage);
                    imageutilisee.Detectiondecontours3();
                    break;
                case "10":
                    Console.Clear();
                    Console.WriteLine("Image Flou");
                    Thread.Sleep(2000);
                    imageutilisee = Demander(affichage);
                    int flou = 0;
                    Console.WriteLine("Entrer la puissance du flou :");
                    do
                    {
                        flou = Convert.ToInt32(Console.ReadLine());
                    }
                    while (flou <= 0);
                    imageutilisee.Flou(flou);
                    break;
                case "11":
                    Console.Clear();
                    Console.WriteLine("Cacher une image");
                    Thread.Sleep(2000);
                    MyImage acacher = null;
                    Console.WriteLine("Quelle image souhaitez vous cacher ?");
                    Thread.Sleep(2000);
                    acacher = Demander(affichage);
                    Console.WriteLine("Dans quelle image souhaitez vous la cacher ?");
                    Thread.Sleep(2000);
                    imageutilisee = Demander(affichage);
                    imageutilisee.Hide(acacher);
                    break;
                case "12":
                    Console.Clear();
                    Console.WriteLine("Retrouver une image");
                    Thread.Sleep(2000);
                    imageutilisee = Demander(affichage);
                    imageutilisee.Find();
                    break;
                case "13":
                    Console.Clear();
                    Console.WriteLine("Fractale");
                    Console.WriteLine("Entrer la taille en hauteur de l'image : (modulo 4)");
                    int largeur = Convert.ToInt32(Console.ReadLine());
                    while (largeur % 4 != 0)
                    {
                        Console.WriteLine("Entrer la taille en hauteur de l'image : (modulo 4)");
                        largeur = Convert.ToInt32(Console.ReadLine());
                    }
                    Console.WriteLine("Entrer la taille en largeur de l'image : (modulo 4)");
                    int hauteur = Convert.ToInt32(Console.ReadLine());
                    while (hauteur % 4 != 0)
                    {
                        Console.WriteLine("Entrer la taille en largeur de l'image : (modulo 4)");
                        hauteur = Convert.ToInt32(Console.ReadLine());
                    }
                    imageutilisee = new MyImage(hauteur, largeur, new Pixel(0, 0, 0));
                    int coef = 0;
                    Console.Clear();
                    Console.WriteLine("Entrer le coefficient de répétition :");
                    do
                    {
                        coef = Convert.ToInt32(Console.ReadLine());
                    }
                    while (coef <= 0);
                    Console.Clear();
                    imageutilisee.Fract(coef, Pixel.GetPixel(Pixel.Choixcouleur()), Pixel.GetPixel(Pixel.Choixcouleur()));
                    break;
                case "14":
                    Console.Clear();
                    Console.WriteLine("Huffman");
                    Thread.Sleep(2000);
                    string[] tablenc = { "encoder", "decoder" };
                    imageutilisee = Demander(affichage);
                    ArbreHuffman huff = new ArbreHuffman(null, null);
                    huff.CreerArbre(imageutilisee.Data);

                    BitArray encoded = huff.Encoder(imageutilisee.Data);
                    Console.WriteLine("Voici l'image encodé");
                    foreach (bool bit in encoded)               //avec boucle foreach
                    {
                        Console.Write((bit ? 1 : 0) + "");
                    }
                    Console.WriteLine();
                    Console.WriteLine("Cliquer sur une touche pour continuer");
                    Console.ReadLine();
                    Console.Clear();

                    Pixel[,] decoded = huff.TabEnMat(huff.Decoder(encoded), imageutilisee.Hauteur, imageutilisee.Largeur);//ça decode à parir de l'image encodé
                    MyImage nouvelle = new MyImage(imageutilisee.Hauteur, imageutilisee.Largeur, new Pixel(0, 0, 0));
                    Console.WriteLine("L'image a été décodé");
                    Console.WriteLine("Cliquer sur une touche pour continuer");
                    Console.ReadLine();
                    nouvelle.Data = decoded; //Ici c'est la matrice décodé qui est enregistrée
                    imageutilisee = nouvelle;
                    break;
                case "15":
                    Console.WriteLine("Créer une image");
                    Console.WriteLine("Entrer la taille en hauteur de l'image : (modulo 4)");
                    int largeurv = Convert.ToInt32(Console.ReadLine());
                    while (largeurv % 4 != 0)
                    {
                        Console.WriteLine("Entrer la taille en hauteur de l'image : (modulo 4)");
                        largeurv = Convert.ToInt32(Console.ReadLine());
                    }
                    Console.WriteLine("Entrer la taille en largeur de l'image : (modulo 4)");
                    int hauteurv = Convert.ToInt32(Console.ReadLine());
                    while (hauteurv % 4 != 0)
                    {
                        Console.WriteLine("Entrer la taille en largeur de l'image : (modulo 4)");
                        hauteurv = Convert.ToInt32(Console.ReadLine());
                    }
                    imageutilisee = new MyImage(hauteurv, largeurv, Pixel.GetPixel(Pixel.Choixcouleur()));//Faire la couleur
                    break;
                case "16":
                    Console.WriteLine("Innovations");
                    imageutilisee = Inno(affichage);
                    break;
            }
            return (imageutilisee);
        }

        public static MyImage Inno(string affichage)
        {
            MyImage inno = null;
            string userInput = "";
            string[] tableaudemots1 = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13" };
            do
            {
                Console.Clear();
                if (affichage == "affichage1")
                {
                    Console.WriteLine("Choisissez la fonction que vous souhaitez utiliser :");
                    Console.WriteLine(
                        "1  Retrécir l'image\n" +
                        "2  Intervertir les couleurs(rouge,bleu,vert)\n" +
                        "3  Image négative\n" +
                        "4  Image sépia\n" +
                        "5  Rogner l'image\n" +
                        "6  Retirer couleur de l'image(bleu,vert,rouge)\n" +
                        "7  Image miroir\n" +
                        "8 Obtenir l'heure\n" +
                        "9 Création de smiley personalisé\n" +
                        "10 Afficher dans la console\n" +
                        "11 Horloge dans la console\n" +
                        "12 Si vous nous mettez une mauvaise note\n" +
                        "13 Retouche photo\n"
                        );
                }
                else
                {
                    Console.Clear();
                    MyImage test = new MyImage("./Images/autres/Menu2.bmp");
                    test.Afficher();
                    Console.ForegroundColor = ConsoleColor.White;
                }
                userInput = Console.ReadLine();
            }
            while (!tableaudemots1.Contains(userInput));
            switch (userInput)
            {
                case "1":
                    Console.Clear();
                    Console.WriteLine("Rétrecir l'image !");
                    Thread.Sleep(2000);
                    int agrandissement = 0;
                    Console.WriteLine("Entrer le coefficient (préférence : 2,4,8,16) :");
                    do
                    {
                        agrandissement = Convert.ToInt32(Console.ReadLine());
                    }
                    while (agrandissement <= 0);
                    inno = Demander(affichage);
                    inno.Reduire(agrandissement);
                    break;
                case "2":
                    Console.Clear();
                    string a = "";
                    string b = "";
                    string[] liste = { "bleu", "vert", "rouge" };
                    Console.WriteLine("Intervertir les couleurs !");
                    Thread.Sleep(2000);
                    Console.WriteLine("Entrer les couleurs à intervertir (bleu, rouge, vert) :");
                    Console.WriteLine("Couleur 1 :");
                    do
                    {
                        a = Console.ReadLine().ToLower();
                    }
                    while (!liste.Contains(a));
                    Console.WriteLine("Couleur 2 :");
                    do
                    {
                        b = Console.ReadLine().ToLower();
                    }
                    while (!liste.Contains(a));
                    inno = Demander(affichage);
                    inno.Intervertircouleur(a, b);
                    break;
                case "3":
                    Console.Clear();
                    Console.WriteLine("Image négative");
                    Thread.Sleep(2000);
                    inno = Demander(affichage);
                    inno.Complementaire();
                    break;
                case "4":
                    Console.Clear();
                    Console.WriteLine("Image sépian");
                    Thread.Sleep(2000);
                    inno = Demander(affichage);
                    inno.Convertirensepia();
                    break;
                case "5":
                    Console.Clear();
                    Console.WriteLine("Rogner l'image");
                    Thread.Sleep(2000);
                    inno = Demander(affichage);
                    Console.WriteLine("Entrer la taille en largeur de l'image : (modulo 4)");
                    int nvlargeur = Convert.ToInt32(Console.ReadLine());
                    while (nvlargeur % 4 != 0 || nvlargeur >= inno.Largeur)
                    {
                        Console.WriteLine("Entrer la taille en largeur de l'image : (modulo 4)");
                        nvlargeur = Convert.ToInt32(Console.ReadLine());
                    }
                    Console.WriteLine("Entrer la taille en hauteur de l'image : (modulo 4)");
                    int nvhauteur = Convert.ToInt32(Console.ReadLine());
                    while (nvhauteur % 4 != 0 || nvhauteur >= inno.Hauteur)
                    {
                        Console.WriteLine("Entrer la taille en hauteur de l'image : (modulo 4)");
                        nvhauteur = Convert.ToInt32(Console.ReadLine());
                    }
                    inno.Rogner(nvlargeur, nvhauteur);
                    break;
                case "6":
                    Console.Clear();
                    Console.WriteLine("Retirer la couleur de l'image(rouge, bleu, vert)");
                    Thread.Sleep(2000);
                    inno = Demander(affichage);
                    string ab = "";
                    string[] listea = { "bleu", "vert", "rouge" };
                    Console.WriteLine("Couleur à enlever:");
                    do
                    {
                        ab = Console.ReadLine().ToLower();
                    }
                    while (!listea.Contains(ab));
                    inno.RetirerCouleur(ab);
                    break;
                case "7":
                    Console.Clear();
                    Console.WriteLine("Image Miroir");
                    Thread.Sleep(2000);
                    inno = Demander(affichage);
                    inno.Miroir();
                    break;
                case "8":
                    Console.Clear();
                    Console.WriteLine("Obtenir l'heure");
                    Thread.Sleep(2000);
                    inno = MyImage.Ilestquelleheure();
                    break;
                case "9":
                    Console.Clear();
                    Console.WriteLine("Création de smiley personalisé");
                    Thread.Sleep(2000);
                    Console.WriteLine("Couleur du smiley");
                    Pixel couleur = Pixel.GetPixel(Pixel.Choixcouleur());
                    inno = MyImage.Cercle(couleur);
                    string humeur = "";
                    Console.Clear();
                    string[] tableauhumeur = { "triste", "content", "normale" };
                    do
                    {
                        Console.WriteLine("L'humeur du smiley ? (Content, normale, triste)");
                        humeur = Console.ReadLine();
                    }
                    while (!tableauhumeur.Contains(humeur));
                    if (humeur == "triste")
                    {
                        inno.BoucheTriste();
                    }
                    else if (humeur == "content")
                    {
                        inno.Content();
                    }
                    else
                    {
                        inno.Bouche();
                    }
                    Console.Clear();
                    Console.WriteLine("Couleur des yeux");
                    inno.Yeux(Pixel.GetPixel(Pixel.Choixcouleur()));
                    break;
                case "10":
                    Console.Clear();
                    Console.WriteLine("Afficher image dans la console");
                    Thread.Sleep(2000);
                    Thread.Sleep(1000);
                    Console.WriteLine("Pensez à ouvrir la console en grand écran");
                    Thread.Sleep(2000);
                    inno = Menu.Demander(affichage);
                    int i = 1;
                    do
                    {
                        i += 1;
                        Console.WriteLine(inno.Largeur / i);
                    }
                    while ((inno.Largeur / i) > 100);
                    inno.Reduire(i);
                    inno.Afficher();
                    Thread.Sleep(1000);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Cliquer sur le clavier pour quitter");
                    Console.ReadLine();
                    break;
                case "11":
                    Console.Clear();
                    Console.WriteLine("Horloge dans la console");
                    Thread.Sleep(2000);
                    Console.WriteLine("Pensez à ouvrir la console en grand écran");
                    inno = MyImage.Ilestquelleheure();
                    MyImage.Horloge();
                    break;
                case "12":
                    Console.Clear();
                    Console.WriteLine("Si vous nous mettez une mauvaise note");
                    Thread.Sleep(2000);
                    Console.WriteLine("Pensez à ouvrir la console en grand écran");
                    MyImage.Pleurer();
                    break;
                case "13":
                    Console.Clear();
                    Console.WriteLine("Retouche photo");
                    Thread.Sleep(2000);
                    inno = Demander(affichage);
                    Console.Clear();
                    MyImage point = inno;
                    for (int ij = 0; ij < point.Data.GetLength(0); ij++)
                    {
                        for (int j = 0; j < point.Data.GetLength(1); j++)
                        {
                            if (ij % 10 == 0 || j % 10 == 0)
                            {
                                point.Data[ij, j] = new Pixel(0, 0, 0);
                            }
                            if (ij % 100 == 0 || j % 100 == 0)
                            {
                                point.Data[ij, j] = new Pixel(255, 255, 255);
                            }
                        }
                    }
                    point.From_Image_To_File("./Images/Images/aide.bmp");
                    Console.WriteLine("Une image(aide.bmp) a été enregistré afin de vous aider à trouver les bonnes coordonnées\n" +
                        "traits blancs : 100 pixels|| traits noirs : 10 pixels");
                    Thread.Sleep(5000);
                    int largeur = -1; ;
                    while (largeur < 0)
                    {
                        try
                        {
                            Console.WriteLine("Entrer la position en largeur à retoucher :");
                            largeur = Convert.ToInt32(Console.ReadLine());
                        }
                        catch (FormatException)
                        {
                            largeur = 5;
                        }
                    }
                    Console.WriteLine("Entrer la position en hauteur à retoucher :");
                    int hauteur = Convert.ToInt32(Console.ReadLine());
                    while (hauteur < 0)
                    {
                        Console.WriteLine("Entrer la position en hauteur à retoucher :");
                        hauteur = Convert.ToInt32(Console.ReadLine());
                    }
                    int coef = 0;
                    Console.Clear();
                    Console.WriteLine("Entrer la zone de retouche(surface):");
                    do
                    {
                        coef = Convert.ToInt32(Console.ReadLine());
                    }
                    while (coef <= 0);
                    Console.WriteLine("Entrer à nouveau l'image :");
                    Thread.Sleep(500);
                    inno = Demander(affichage);
                    inno.Retouche(largeur, hauteur, coef);
                    File.Delete("./Images/Images/aide.bmp");
                    break;

            }
            return inno;
        }


        public static MyImage Demander(string affichage)
        {
            string userInput = "";
            do
            {
                Console.Clear();
                Console.WriteLine("Choisissez l'image que vous souhaitez modifier (entrer le nom ex : \"coco.bmp\") :");
                string dossier = "./Images/Images";
                string[] fichiers = Directory.GetFiles(dossier);
                foreach (string fichier in fichiers)
                {
                    // Afficher le nom du fichier
                    if (Path.GetFileName(fichier) != ".DS_Store")
                    {
                        if (affichage=="affichage1")
                        {
                            Console.WriteLine(Path.GetFileName(fichier));
                        }
                        else
                        {
                            Console.WriteLine(Path.GetFileName(fichier));
                            MyImage nouveau = new MyImage(fichier);
                            int i = 1;
                            do
                            {
                                i += 1;
                            }
                            while ((nouveau.Largeur / i) >= 100);
                            nouveau.Reduire(i);
                            nouveau.Afficher();
                            Thread.Sleep(1000);
                            Console.WriteLine();
                            Console.WriteLine();
                            Console.ForegroundColor = ConsoleColor.White;
                        }

                    }
                }
                Console.WriteLine("(Si vous souhaitez choisir une autre image, mettez là dans le dossier :Debug<Images<Images)");
                userInput = Console.ReadLine();
            }
            while (!File.Exists("./Images/Images" + "/" + userInput));
            string chemin = "./Images/Images" + "/" + userInput;
            MyImage imageutilisee = new MyImage(chemin);
            return imageutilisee;
        }

        public static void Jouer()
        {
            Console.WriteLine("Merci d'ouvrir la console en grand écran");
            Console.WriteLine("Ce code est plus adapté pour un macbook");
            //Thread.Sleep(5000);
            string dossier = "./Images/Images";
            string[] fichiers = Directory.GetFiles(dossier);
            MyImage test = new MyImage("./Images/autres/copyright.bmp");
            test.Afficher();
            Console.WriteLine("copyright tous droits reserves");
            Console.WriteLine();
            Console.WriteLine();
            Thread.Sleep(2000);
            string[] affich = { "affichage1", "affichage2" };
            string affichage = "";
            do
            {
                Console.WriteLine("Merci d'ouvrir la console en grand écran(si affichage2)");
                Console.WriteLine("Choisir votre affichage : (\"affichage1\" ou \"affichage2\")");
                affichage = Console.ReadLine();
            }
            while (!affich.Contains(affichage));
            MyImage imageutilisee = Fonction(affichage);

            string cheminfinal = "./Images/Images";
            imageutilisee.From_Image_To_File(cheminfinal + "/imageobtenue.bmp");

            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //Process.Start(cheminfinal + "/imageobtenue.bmp");     ///Permet d'afficher l'image mais ne fonctionne pas sur windows (on est sur mac)
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            string reponse = "";
            do
            {
                Console.Clear();
                Console.WriteLine("Souhaitez vous enregistrer l'image ?");
                Console.WriteLine("oui\nnon");
                reponse = Console.ReadLine();
            }
            while (reponse.ToLower() != "oui" && reponse.ToLower() != "non");
            char[] tab = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', '-', '_', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };
            string nomfichier = "";
            File.Delete(cheminfinal + "/imageobtenue.bmp");
            if (reponse.ToLower() == "oui")
            {
                int a = 0;
                Console.WriteLine("Sous quel nom souhaitez vous l'enregistrer ?");
                do
                {
                    nomfichier = Console.ReadLine();
                    a = 0;
                    for (int i = 0; i < nomfichier.Length; i++)
                    {
                        if (tab.Contains(nomfichier[i]))
                        {
                            a += 0;
                        }
                        else { a += 1; }
                    }
                    if (File.Exists(cheminfinal + "/" + nomfichier + ".bmp"))
                    {
                        a = 1;
                    }
                }
                while (a != 0);
                imageutilisee.From_Image_To_File(cheminfinal + "/" + nomfichier + ".bmp");
            }
            Thread.Sleep(2);
        }
    }
}