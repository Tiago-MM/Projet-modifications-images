# Projet-modifications-images

Dans le cadre du projet d’informatique scientifique, nous avons créé un programme POO en C# permettant de lire et écrire une image au format .bmp, ainsi qu'à effectuer divers traitements d'image, création d'images, et une implémentation d'algorithme de compression(Huffman). Pour ce faire nous avons utilisé différentes classes :la classe MyImage, la classe Pixel, la classe Menu pour l’interface et enfin la classe Nœud et ArbreHuffman pour la compression Huffman. Le but étant de recréer la classe Bitmap, une classe très utilisée en C#.


LA STRUCTURE :

• La classe Pixel :
La classe Pixel est utilisée dans le projet afin de représenter les pixels d'une image au format bmp. Chaque pixel dans une image est composé de valeurs de couleur pour les composantes rouge, verte et bleue (RGB), qui déterminent la couleur du pixel. Ainsi cette classe est essentiel pour représenter l’image à l’aide d’une matrice de pixels. De nombreuses méthodes sont présentes, notamment GetClosestColor() qui, à l’aide de la méthode CalculateColorDistance(), trouve la couleur la plus proche d’un pixel.

• La classe MyImage :
C’est la classe essentielle à ce projet, elle permet de manipuler des images au format bmp. Cette classe comprend des méthodes pour lire une image à partir d'un fichier .bmp donné, effectue un traitement simple d'image comme le passage d'une photo couleur à une photo en nuances de gris et en noir et blanc, et pour sauvegarder l'image toujours au format .bmp. La classe MyImage utilise une classe Pixel pour représenter les pixels de l'image. Ensuite, nous avons mis en place des méthodes pour effectuer des traitements d'image plus avancés, tels que la rotation d'une image avec un angle quelconque et l'agrandissement d'une image avec un coefficient quelconque. Ces traitements sont réalisés en manipulant les pixels de l'image à l'aide des méthodes de la classe MyImage et de la classe Pixel. Nous avons également implémenté des filtres basés sur une méthode de convolution, qui permet de réaliser une détection de contour, de renforcement des bords, de flou et de repoussage sur une image. Nous avons mis en place la création d'une image décrivant une fractale en utilisant des algorithmes mathématiques appropriés, ainsi que cacher et trouver une image dans une autre image en utilisant des techniques de stéganographie.

• La classe Menu :
C’est là où l’utilisateur pourra choisir l’image à modifier, les fonctions à utiliser etc, il y a deux affichages, un affichage classique (affichage1) et un affichage avec des dessins (affichage2).

• La classe Noeud :
Un noeud est défini par un pixel, la fréquence de ce dernier dans l'image et le noeud à droite et à gauche de ce dernier. La méthode CheminParcouru permet de trouver le chemin en partant d'un noeud donné (on appelle toujours cette méthode à partir du noeud racine pour avoir le chemin complet), c'est à dire en partant de ce noeud, vers quelle noeud je dois me diriger pour continuer le chemin et atteintre le noeud correspondant au pixel recherché, si on prend le noeud de gauche, on renseigne un false (0 binaire) et à droite un true. Ainsi, on a une succession de bool (ou binaire) qui décrit ou aller à partir du noeud donné pour aller jusqu'à celui qui possède la valeur de pixel recherché en attribut.
Projet scientifique informatique

 • ArbreHuffman :
Un arbre est défini par un noeud racine et un dictionnaire de <Pixel,Frequence>. La methode CreerArbre fait exactement cela : elle commence par les deux noeuds de plus basse frequence et en fait un nouveau noeud qui garde en mémoire ces derniers en attribut noeudDroite noeudGauche et ainsi de suite avec ce nouveau noeud jusqu'à ce qu'il n'en reste plus qu'un qui possède en mémoire tout les autres. La methode encoder : prends les pixels un à un et les transforme en chemin binaire (CheminParcouru) puis les concaténe dans une liste pour avoir la matrice de pixels entière coder sous forme de liste de binaire. La methode decoder : récupère le code binaire et déroule le chemin à partir de la racine, une fois qu'il n'y a plus de noeudDroite noeudGauche, on a atteint une "feuille" de l'arbre, le chemin est terminé et on peut récupérer la valeur du pixel dans ce noeud. On repart de la racine en continuant la liste binaire là où on l'avait laissé.




NOS INNOVATIONS PRINCIPALES :

Image négative/sepia : Obtenir une image ayant les couleurs complémentaires/ avoir une teinte sépia, donnant à l'image un aspect vieilli et rétro.

Rogner l'image : La fonctionnalité "Rogner l'image" permet à l'utilisateur de garder une partie du centre de l’image selon les dimensions qu’il a choisies.

Retirer couleur/intervertir de l'image (bleu, vert, rouge) : Cette innovation permet à l'utilisateur de supprimer sélectivement les composantes bleue, verte ou rouge d'une image ou de changer les couleurs.

Création de smiley personnalisé : Cette innovation permet à l’utilisateur de créer des smileys personnalisés en combinant différentes humeurs et couleurs selon son envie.

Horloge dans la console : L'innovation permet d'afficher avec la méthode afficher() une horloge en en temps réel utilisant la méthode Ilestquelleheure() qui enregistre une image avec l’heure.

Si vous nous mettez une mauvaise note : Cette méthode est à destination des personnes qui vous nous corriger fonctionnant tel qu’un gif. Un. Smiley en train de pleurer s’affichera reflétant ainsi notre humeur à la suite de cette mauvaise nouvelle.

Retouche photo : L'innovation "Retouche photo" est une fonctionnalité de traitement d'image offrant une possibilité de retoucher les photos(boutons sur le visage par exemple) en donnant les coordonnées de la surface que nous souhaitons retoucher.


Nous souhaitons souligner que la mise en place de fonctionnalités avancées telles que la compression JPEG et Huffman dans notre projet n'a pas été sans défis. La compression d'image est un processus complexe et malheureusement dû aux contraintes de temps et à d'autres soucis nous n’avons pas pu aboutir à un code fournissant la compression jpeg. Cependant, nous sommes fiers de ce que nous avons accompli en réussissant la mise en place de la compression Huffman, ce qui représente un travail significatif en soi et bien évidemment de toutes les innovations que nous avons créées.
