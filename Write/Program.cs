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
            //Synthese.Intersection(rayon, sphere);
            
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
            //img.DessinerSphere(sphere);
            //img.DessinerSphere(sphere2);
            //img.DessinerRayon(rayon);
            //img.DessinerIntersection(rayon, sphere);
            //Image.GenererPPM(nomFichier, img);


            Console.WriteLine("Démarrage...\n");

            ////Premiere Compo
            //Camera camera = new Camera(new Vector3(0,0,-10), 1000, 1000);
            //Lumiere lumiere = new Lumiere(new Vector3(500, 0, 0), 500, 250);

            //Scene scene = new Scene(camera, lumiere);
            //scene.spheres.Add(new Sphere(new Vector3(200, 200, 200), 50, new Couleur(0.3, 0.8, 0.1)));
            //scene.spheres.Add(new Sphere(new Vector3(300, 250, 800), 100, new Couleur(0.6, 0.6, 0.2)));
            //scene.spheres.Add(new Sphere(new Vector3(500, 350, 450), 150, new Couleur(1, 0.8, 0.2)));
            //scene.spheres.Add(new Sphere(new Vector3(500, 350, 350), 100, new Couleur(1, 0.8, 1)));
            //scene.spheres.Add(new Sphere(new Vector3(800, 600, 300), 100, new Couleur(1, 0.3, 0.2)));
            //scene.spheres.Add(new Sphere(new Vector3(750, 750, 700), 200, new Couleur(0.2, 0.2, 1)));
            //scene.spheres.Add(new Sphere(new Vector3(750, 850, 850), 200, new Couleur(0.6, 0.2, 1)));
            //scene.spheres.Add(new Sphere(new Vector3(500, 1500, 2000), 800, new Couleur(0.1, 0.8, 0.8)));
            //scene.spheres.Add(new Sphere(new Vector3(850, 100, 1000), 300, new Couleur(0.7, 0, 0.3)));


            //Deuxième Compo
            Camera camera = new Camera(new Vector3(0, 0, 0), 720, 1280);
            Lumiere lumiere = new Lumiere(new Vector3(0, 640, 200), 3000, 850);

            Scene scene = new Scene(camera, lumiere);
            Sphere s1 = new Sphere(new Vector3(720/2, (1280/4)*1, 600), 200, new Couleur(1, 0.6, 0.6));
            Sphere s2 = new Sphere(new Vector3(720/2, (1280/4)*3, 600), 200, new Couleur(0.6, 1, 0.6));
            Sphere leftWall = new Sphere(new Vector3(360, (float)(-1e5 - 100),500), (int)1e5, new Couleur(1, 0.8, 0.2));
            Sphere rightWall = new Sphere(new Vector3(360, (float)1e5 + 1380, 500), (int)1e5, new Couleur(1, 0.8, 1));
            Sphere topWall = new Sphere(new Vector3((float)-1e5 - 100, 640, 500), (int)1e5, new Couleur(1, 0.3, 0.2));
            Sphere bottomWall = new Sphere(new Vector3((float)1e5 + 820, 640, 500), (int)1e5, new Couleur(0.2, 0.7, 1));
            Sphere backWall = new Sphere(new Vector3(360, 640,  (float)1e5 + 1000), (int)1e5, new Couleur(0.6, 0.2, 1));
            Sphere frontWall = new Sphere(new Vector3(360, 640,  (float)-1e5 - 1), (int)1e5, new Couleur(0.7, 0, 0.3));

            Sphere[] spheres = { frontWall, backWall, rightWall, leftWall, bottomWall, topWall, s1, s2 };
            scene.spheres.AddRange(spheres);


            //img = img.dessineScene(camera, scene, new Couleur(1, 1, 1));
            Image img1 = Image.DessineAll(scene);
            Image.GenererPPM("Scene.ppm", img1);


            Console.WriteLine("Image générée.");
            //Console.ReadLine();

            System.Diagnostics.Process.Start(@"C:\Users\atetart\Documents\Scene.ppm");



        }
    }

}
