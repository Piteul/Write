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

            //prog.PPM();
            //Ray rayon = new Ray(new Vector3(500, 100, 0), new Vector3(0, 100, 0));
            //Sphere sphere = new Sphere(new Vector3(500, 500, 0), 100);
            //Synthese.intersection(rayon, sphere);
            
            ////Premier test vecteur
            //Vector3 v12 = rayon.p;
            //Vector3 v3 = Vector3.Add(v2, v2); 
            //float prodScalaire = Vector3.Dot(v2, v3);
            //Vector3 v4 = Vector3.Cross(v1, v2);
            //Console.WriteLine("Résultat :");
            //Console.WriteLine(res.ToString());
            //Console.WriteLine("Produit Scalaire : " + prodScalaire.ToString());

            ////Dessin classique
            //Ray rayon = new Ray(new Vector3(0, 0, -10), new Vector3(0, 0, 1));
            //Sphere sphere = new Sphere(new Vector3(20, 10, 0), 5, new Couleur(0.3,0.8,0.1));
            //Sphere sphere2 = new Sphere(new Vector3(60, 50, 0), 10, new Couleur(1,0.4,1));
            //string nomFichier = "test.ppm";
            //Image img = new Image(200, 200, new Couleur(1, 1, 1));
            //img.dessinerSphere(sphere);
            //img.dessinerSphere(sphere2);
            //img.dessinerRayon(rayon);
            //img.dessinerIntersection(rayon, sphere);
            //Image.genererPPM(nomFichier, img);


            Console.WriteLine("Démarrage...\n");

            //Camera camera = new Camera(new Vector3(0, 0, 1000), 200, 200, new Vector3(0, 0, -1));
            Camera camera = new Camera(new Vector3(0,0,-10), 1000, 1000);
            Lumiere lumiere = new Lumiere(new Vector3(500, 0, 0));

            Scene scene = new Scene(camera, lumiere);
            scene.spheres.Add(new Sphere(new Vector3(200, 200, 200), 50, new Couleur(0.3, 0.8, 0.1)));
            scene.spheres.Add(new Sphere(new Vector3(300, 250, 800), 100, new Couleur(0.6, 0.6, 0.2)));
            scene.spheres.Add(new Sphere(new Vector3(500, 350, 450), 150, new Couleur(1, 0.8, 0.2)));
            scene.spheres.Add(new Sphere(new Vector3(500, 350, 350), 100, new Couleur(1, 0.8, 1)));
            scene.spheres.Add(new Sphere(new Vector3(800, 600, 300), 100, new Couleur(1, 0.3, 0.2)));
            scene.spheres.Add(new Sphere(new Vector3(750, 750, 700), 200, new Couleur(0.2, 0.2, 1)));
            scene.spheres.Add(new Sphere(new Vector3(750, 850, 850), 200, new Couleur(0.6, 0.2, 1)));
            scene.spheres.Add(new Sphere(new Vector3(500, 1500, 2000), 800, new Couleur(0.1, 0.8, 0.8)));
            scene.spheres.Add(new Sphere(new Vector3(850, 100, 1000), 300, new Couleur(0.7, 0.5, 0.3)));

            //img = img.dessineScene(camera, scene, new Couleur(1, 1, 1));
            Image img1 = Image.dessineAll(scene);
            Image.genererPPM("Scene.ppm", img1);


            Console.WriteLine("Image générée.");
            //Console.ReadLine();



        }
    }

}
