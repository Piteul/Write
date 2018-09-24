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

        public int height;
        public int width;
        public int[,] r;
        public int[,] g;
        public int[,] b;


        public Image(int _height, int _width) {
            height = _height;
            width = _width;
            r = new int[height, width];
            g = new int[height, width];
            b = new int[height, width];

            for (int i = 0; i < height; i++)
                for (int j = 0; j < width; j++) {
                    r[i, j] = 255;
                    g[i, j] = 255;
                    b[i, j] = 255;
                }
        }

        public void drawASphere(Sphere sphere) {

            for (int i = 0; i < height; i++)
                for (int j = 0; j < width; j++) {

                    if (Math.Pow(i - sphere.c.X, 2) + Math.Pow(j - sphere.c.Y, 2) < Math.Pow(sphere.r, 2)) {
                        r[i, j] = 0;
                        g[i, j] = 172;
                        b[i, j] = 230;
                    }
                }
        }

        public void drawARayon(Ray rayon) {
            r[(int)rayon.p.X, (int)rayon.p.Y] = 255;
            g[(int)rayon.p.X, (int)rayon.p.Y] = 153;
            b[(int)rayon.p.X, (int)rayon.p.Y] = 0;

            r[(int)rayon.complet.X, (int)rayon.complet.Y] = 204;
            g[(int)rayon.complet.X, (int)rayon.complet.Y] = 102;
            b[(int)rayon.complet.X, (int)rayon.complet.Y] = 0;


            for (int i = 0; i < height; i++)
                for (int j = 0; j < width; j++) {
                    if (surRayon(rayon, new Vector3(i, j, 0))) {
                        Console.WriteLine("oui");
                        r[i, j] = 255;
                        g[i, j] = 13;
                        b[i, j] = 13;
                    }

                }
        }

        public void drawIntersection(Ray rayon, Sphere sphere) {
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


        //Simple function to write a file
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
        //Simple function to write a PPM
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

        public static void writePPM(string file, Image img) {
            //Use a streamwriter to write the text part of the encoding.
            var width = img.width;
            var height = img.height;

        // Set a variable to the My Documents path.
        string mydocpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        //string mydocpath = "C:\\Users\\atetart\\Documents";
        

         StreamWriter outputFile = new StreamWriter(Path.Combine(mydocpath, file));
            outputFile.Write("P3" + "\n");
            outputFile.Write(width + " " + height + "\n");
            outputFile.Write("255" + "\n");


            for (int x = 0; x < height; x++)
                for (int y = 0; y < width; y++) {
                    outputFile.WriteLine(img.r[x, y]);
                    outputFile.WriteLine(img.g[x, y]);
                    outputFile.WriteLine(img.b[x, y]);
                }
            outputFile.Close();
        }
    }
}
