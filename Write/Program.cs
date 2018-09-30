using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using static Write.Synthese;

namespace Write {
    public class Program {

        static void Main(string[] args) {
            Program prog = new Program();
            //prog.PPM();

            //Ray rayon = new Ray(new Vector3(500, 100, 0), new Vector3(0, 100, 0));
            //Sphere sphere = new Sphere(new Vector3(500, 500, 0), 100);
            //Synthese.intersection(rayon, sphere);

            Ray rayon = new Ray(new Vector3(10, 10, 0), new Vector3(1, 0, 0));
            Sphere sphere = new Sphere(new Vector3(20, 10, 0), 5, new Couleur(0.3,0.8,0.1));
            Camera camera = new Camera(new Vector3(0, 0, -10), 20,20);


           // trouverSphere(camera, sphere);




            //Dessin classique
            string nomFichier = "test.ppm";
            Image img = new Image(200, 200, new Couleur(1,1,1));
            img.dessinerSphere(sphere);
            img.dessinerRayon(rayon);
            img.dessinerIntersection(rayon, sphere);
            Image.genererPPM(nomFichier, img);


            //Vector3 v12 = rayon.p;

            //Vector3 v3 = Vector3.Add(v2, v2); 
            //float prodScalaire = Vector3.Dot(v2, v3);
            //Vector3 v4 = Vector3.Cross(v1, v2);

            //Console.WriteLine("Résultat :");
            //Console.WriteLine(res.ToString());
            //Console.WriteLine("Produit Scalaire : " + prodScalaire.ToString());

            Console.WriteLine("\nPress any key to exit.");
            Console.ReadLine();




        }
    }

}
