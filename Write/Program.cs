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

            Ray rayon = new Ray(new Vector3(500, 100, 0), new Vector3(0, 100, 0));
            Sphere sphere = new Sphere(new Vector3(500, 500, 0), 100);

            //Ray rayon = new Ray(new Vector3(10, 10, 0), new Vector3(1, 0, 0));
            //Sphere sphere = new Sphere(new Vector3(20, 10, 0), 5);
            Camera camera = new Camera(new Vector3(0, 0, -10), 800, 600);


             

            Synthese.intersection(rayon, sphere);


            //Vector3 v12 = rayon.p;

            //Vector3 v3 = Vector3.Add(v2, v2); 
            //float prodScalaire = Vector3.Dot(v2, v3);
            //Vector3 v4 = Vector3.Cross(v1, v2);


            //Console.WriteLine("Résultat :");
            //Console.WriteLine(res.ToString());
            //Console.WriteLine("Produit Scalaire : " + prodScalaire.ToString());
            Console.ReadLine();


            string nomFichier = "test.ppm";
            Image img = new Image(1000, 1000);
            img.drawASphere(sphere);
            img.drawARayon(rayon);
            img.drawIntersection(rayon, sphere);

            Image.writePPM(nomFichier, img);

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();


            

     
        }
    }

}
