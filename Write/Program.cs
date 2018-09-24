using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Write {
    class Program {


        struct Ray {
            //point
            public Vector3 p { get; set; }
            //direction
            public Vector3 d { get; set; }

            public Ray(Vector3 v1, Vector3 v2) {
                p = v1;
                d = v2;
            }

        }

        struct Sphere {
            //centre
            public Vector3 c { get; set; }
            //longueur rayon
            public float r { get; set; }

            public Sphere(Vector3 v1, float f) {
                c = v1;
                r = f;
            }
        }

        void SimpleWrite() {
            // Create a string array with the lines of text
            string[] lines = { "First line", "Second line", "Third line", "Fourth Line" };

            // Set a variable to the My Documents path.
            string mydocpath =
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            // Write the string array to a new file named "WriteLines.txt".
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(mydocpath, "WriteLines.txt"))) {
                foreach (string line in lines)
                    outputFile.WriteLine(line);
            }
        }

        void PPM() {
            // Create a string array with the lines of text
            string[] lines =
                { "P3"
                , "3 2"
                , "255"
                , "255  0   0     0  255  0       0   0  255"
                , "255 255  0    255 255 255      0   0   0"  };

            // Set a variable to the My Documents path.
            string mydocpath =
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            // Write the string array to a new file named "WriteLines.txt".
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(mydocpath, "Img1.ppm"))) {
                foreach (string line in lines)
                    outputFile.WriteLine(line);
            }
        }


        void intersection(Ray ray, Sphere sphere) {

            //c-p
            Vector3 vecSub = Vector3.Subtract(sphere.c, ray.p);
            double A = Vector3.Dot(ray.d, ray.d);
            double B = 2 * (Vector3.Dot(ray.p, ray.d) - Vector3.Dot(sphere.c, ray.d));
            double C = Vector3.Dot(vecSub, vecSub) - Math.Pow(sphere.r, 2);

            //affichage
            Console.WriteLine("A : " + A.ToString());
            Console.WriteLine("B : " + B.ToString());
            Console.WriteLine("C : " + C.ToString());


            double delta = Math.Pow(B, 2) - (4 * (A * C));

            if (delta < 0) {
                Console.WriteLine("Aucune intersection");
            }
            else if (delta >= 0) {

                double res1 = (((-B) + Math.Sqrt(delta)) / (2 * A));
                double res2 = (((-B) - Math.Sqrt(delta)) / (2 * A));



                Console.WriteLine("Intersection !");
                Console.WriteLine("Res delta: " + Math.Min(res1, res2));
            }

        }



        static void Main(string[] args) {
            Program prog = new Program();
            //prog.PPM();

            //Ray rayon = new Ray(new Vector3(2, 2, 0), new Vector3(1, 0, 0));
            //Sphere sphere = new Sphere(new Vector3(5, 2, 0), 1);

            Ray rayon = new Ray(new Vector3(10, 10, 0), new Vector3(1, 0, 0));
            Sphere sphere = new Sphere(new Vector3(20, 10, 0), 5);

            prog.intersection(rayon, sphere);
           

            //Vector3 v12 = rayon.p;

            //Vector3 v3 = Vector3.Add(v2, v2); 
            //float prodScalaire = Vector3.Dot(v2, v3);
            //Vector3 v4 = Vector3.Cross(v1, v2);


            //Console.WriteLine("Résultat :");
            //Console.WriteLine(res.ToString());
            //Console.WriteLine("Produit Scalaire : " + prodScalaire.ToString());
            Console.ReadLine();
        }
    }

}
