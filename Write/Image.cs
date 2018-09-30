using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static Write.Synthese;

namespace Write {
    public class Image {

        public int hauteur;
        public int largeur;
        public int[,] r;
        public int[,] g;
        public int[,] b;
        public Couleur couleur;


        public Image(int _hauteur, int _largeur, Couleur _couleur) {
            hauteur = _hauteur;
            largeur = _largeur;
            r = new int[hauteur, largeur];
            g = new int[hauteur, largeur];
            b = new int[hauteur, largeur];
            Couleur couleur = _couleur;

            for (int i = 0; i < hauteur; i++)
                for (int j = 0; j < largeur; j++) {
                    dessinerPixel(i, j, couleur.rgb);
                    //r[i, j] = 255;
                    //g[i, j] = 255;
                    //b[i, j] = 255;
                }
        }


        public void dessinerPixel(int x, int y, double[] _rgb) {
            this.r[x, y] = (int)(_rgb[0]*255);
            this.g[x, y] = (int)(_rgb[1]*255);
            this.b[x, y] = (int)(_rgb[2]*255);

        }
        public void dessinerSphere(Sphere sphere) {

            for (int i = 0; i < hauteur; i++)
                for (int j = 0; j < largeur; j++) {

                    if (Math.Pow(i - sphere.c.X, 2) + Math.Pow(j - sphere.c.Y, 2) < Math.Pow(sphere.r, 2)) {

                        dessinerPixel(i, j, sphere.couleur.rgb);
                        //r[i, j] = 0;
                        //g[i, j] = 172;
                        //b[i, j] = 230;
                    }
                }
        }


        public void dessinerRayon(Ray rayon) {
            r[(int)rayon.p.X, (int)rayon.p.Y] = 255;
            g[(int)rayon.p.X, (int)rayon.p.Y] = 153;
            b[(int)rayon.p.X, (int)rayon.p.Y] = 0;

            r[(int)rayon.complet.X, (int)rayon.complet.Y] = 204;
            g[(int)rayon.complet.X, (int)rayon.complet.Y] = 102;
            b[(int)rayon.complet.X, (int)rayon.complet.Y] = 0;


            for (int i = 0; i < hauteur; i++)
                for (int j = 0; j < largeur; j++) {
                    if (surRayon(rayon, new Vector3(i, j, 0))) {
                        r[i, j] = 255;
                        g[i, j] = 13;
                        b[i, j] = 13;
                    }

                }
        }

        public void dessinerIntersection(Ray rayon, Sphere sphere) {
            float x = (float) intersection(rayon, sphere);
            if (x != -1) {
                Vector3 i = Vector3.Add(rayon.p, Vector3.Multiply(x, rayon.d));
                //Console.WriteLine(i.X);
                //Console.WriteLine(i.Y);

                r[(int)i.X, (int)i.Y] = 0;
                g[(int)i.X, (int)i.Y] = 0;
                b[(int)i.X, (int)i.Y] = 0;
            }

        }

        /// <summary>
        ///Simple function to write a file
        /// </summary>
        public void SimpleWrite() {
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

        /// <summary>
        ///Simple function to write a PPM
        /// </summary>
        public void simplePPM() {
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

        /// <summary>
        /// Create the PPM file of the Image
        /// </summary>
        /// <param name="file"></param>
        /// <param name="img"></param>
        public static void genererPPM(string file, Image img) {
            //Use a streamwriter to write the text part of the encoding.
            var largeur = img.largeur;
            var hauteur = img.hauteur;

        // Set a variable to the My Documents path.
        string mydocpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        //string mydocpath = "C:\\Users\\atetart\\Documents";
        

         StreamWriter outputFile = new StreamWriter(Path.Combine(mydocpath, file));
            outputFile.Write("P3" + "\n");
            outputFile.Write(largeur + " " + hauteur + "\n");
            outputFile.Write("255" + "\n");


            for (int x = 0; x < hauteur; x++)
                for (int y = 0; y < largeur; y++) {
                    outputFile.WriteLine(img.r[x, y]);
                    outputFile.WriteLine(img.g[x, y]);
                    outputFile.WriteLine(img.b[x, y]);
                }
            outputFile.Close();
        }
    }
}
