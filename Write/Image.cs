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

        /// <summary>
        /// Image
        /// </summary>
        /// <param name="_hauteur"></param>
        /// <param name="_largeur"></param>
        /// <param name="_couleur"></param>
        public Image(int _hauteur, int _largeur, Couleur _couleur) {
            hauteur = _hauteur;
            largeur = _largeur;
            r = new int[hauteur, largeur];
            g = new int[hauteur, largeur];
            b = new int[hauteur, largeur];
            Couleur couleur = _couleur;

            for (int i = 0; i < hauteur; i++)
                for (int j = 0; j < largeur; j++) {
                    dessinerPixel(i, j, couleur.rgb, 255);
                    //r[i, j] = 255;
                    //g[i, j] = 255;
                    //b[i, j] = 255;
                }
        }

        /// <summary>
        /// Dessine un pixel (x,y)
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="_rgb"></param>
        public void dessinerPixel(int x, int y, double[] _rgb, int val) {
            this.r[x, y] = (int)Clamp((_rgb[0] * val));
            this.g[x, y] = (int)Clamp((_rgb[1] * val));
            this.b[x, y] = (int)Clamp((_rgb[2] * val));
            //Console.WriteLine(this.r[x, y].ToString());

        }

        public void dessinerPixel(int x, int y, double[] _rgb, double val) {
            this.r[x, y] = (int)Clamp((_rgb[0] * val));
            this.g[x, y] = (int)Clamp((_rgb[1] * val));
            this.b[x, y] = (int)Clamp((_rgb[2] * val));
            //Console.WriteLine(this.r[x, y].ToString());

        }

        public void dessinerPixel(int x, int y, int r, int g, int b, int val) {
            this.r[x, y] = r * val;
            this.g[x, y] = g * val;
            this.b[x, y] = b * val;
            //Console.WriteLine(this.r[x, y].ToString());

        }

        public double Clamp(double i) {
            if (i < 0) {
                return 0;
            }
            else if (i > 255) {
                return 255;
            }
            else return i;

        }

        /// <summary>
        /// Dessine une sphère
        /// </summary>
        /// <param name="sphere"></param>
        public void dessinerSphere(Sphere sphere) {

            for (int i = 0; i < hauteur; i++)
                for (int j = 0; j < largeur; j++) {

                    if (Math.Pow(i - sphere.c.X, 2) + Math.Pow(j - sphere.c.Y, 2) < Math.Pow(sphere.r, 2)) {

                        dessinerPixel(i, j, sphere.couleur.rgb, 255);
                        //r[i, j] = 0;
                        //g[i, j] = 172;
                        //b[i, j] = 230;
                    }
                }
        }

        /// <summary>
        /// Dessine un rayon
        /// </summary>
        /// <param name="rayon"></param>
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

        /// <summary>
        /// Dessine l'intersection d'un rayon et d'une sphère
        /// </summary>
        /// <param name="rayon"></param>
        /// <param name="sphere"></param>
        public void dessinerIntersection(Ray rayon, Sphere sphere) {
            float x = (float)intersection(rayon, sphere);
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
        /// Dessine l'integralité de la scène et la transforme en image
        /// </summary>
        /// <param name="scen"></param>
        /// <returns></returns>
        public static Image dessineAll(Scene scen) {
            Camera cam = scen.cam;
            Image img = new Image(cam.hauteur, cam.largeur, new Couleur(0, 0, 0));

            for (int x = 0; x < cam.hauteur; x++) {
                for (int y = 0; y < cam.largeur; y++) {

                    double tmp = double.MaxValue; //contiendra la val de l'intersection
                    Vector3 pixelActuel = new Vector3(x, y, cam.position.Z);
                    Ray r = new Ray(pixelActuel, cam.VecteurDirecteurFocus(x, y));
                    //Ray r = new Ray(new Vector3(x, y, cam.position.Z), new Vector3(0,0,1));

                    //Stockera la sphère la plus proche et qui sera éclairé
                    Sphere sphereTemp = new Sphere(new Vector3(0, 0, 0), 0, new Couleur(0, 0, 0));


                    foreach (Sphere s in scen.spheres) {
                        double resIntersection = intersection(r, s);
                        if (resIntersection != -1 && resIntersection < tmp) {

                            tmp = resIntersection;
                            sphereTemp = s;
                            //img.dessinerPixel(x, y, s.couleur.rgb, 255);
                        }
                    }

                    if (tmp != double.MaxValue) {

                        Vector3 pointSphere = pixelActuel + Vector3.Multiply((float)(tmp * 0.99999), cam.VecteurDirecteurFocus(x, y));

                        //Rayon de départ pointSphere et en direction de la lumière
                        Ray r2 = new Ray(pointSphere, scen.lumiere.origine - pointSphere);
                        tmp = double.MaxValue;

                        foreach (Sphere s2 in scen.spheres) {
                            double resIntersection = intersection(r2, s2);
                            if (resIntersection != -1 && resIntersection < tmp) {

                                tmp = resIntersection;
                                //img.dessinerPixel(x, y, s.couleur.rgb, 255);
                            }
                        }
                        ////
                        double albedo = 0.2;
                        Surface surface1 = new Surface(albedo);
                        // Résultat de  formule est (Albedo*cosTheta) / Pi
                        double albedoOnSphere = surface1.DifuseLight(surface1.albedo, Vector3.Normalize(r2.d), cam.VecteurDirecteurFocus(x, y)); //ou pointSphere
                        // Facteur 1/D²
                        float epsilon = 0;
                        //pointOnSphere = Vector3.Add(pointOnSphere, sphereToLight.d * epsilon);
                        int powerOfLightValue = 500;
                        Vector3 powerOfLight = new Vector3(powerOfLightValue, powerOfLightValue, powerOfLightValue);
                        Vector3 distanceLight = surface1.DistanceLight(powerOfLight, pointSphere, scen.lumiere.origine);
                        int intensity = 200;
                        double powerOfShadow = 0.2;


                        ////

                        if (tmp < 1) {

                            //img.dessinerPixel(x, y, sphereTemp.couleur.rgb, 50);
                            double flux = (powerOfShadow * distanceLight.X * intensity * albedoOnSphere * intensity);
                            flux = flux * -100;
                            //if(flux > 0) Console.WriteLine("Flux : " + flux.ToString());

                            img.dessinerPixel(x, y, sphereTemp.couleur.rgb, flux);
                            
                            //img.SetPixel(x, y, 
                            //    (int)(sphereTemp.couleur.r * distanceLight.X * intensity * albedoOnSphere * intensity), 
                            //    (int)(sphereTemp.couleur.g * distanceLight.Y * intensity * albedoOnSphere * intensity),
                            //    (int)(sphereTemp.couleur.b * distanceLight.Z * intensity * albedoOnSphere * intensity));


                        }
                        else {

                            //img.dessinerPixel(x, y, sphereTemp.couleur.rgb, 255);
                            double flux =(distanceLight.X * intensity * albedoOnSphere * intensity);
                            flux = flux * -100;
                            //if (flux > 0) Console.WriteLine("Flux : " + flux.ToString());

                            img.dessinerPixel(x, y, sphereTemp.couleur.rgb, flux);

                            //img.SetPixel(x, y,
                            //(sphereTemp.couleur.r * powerOfShadow * distanceLight.X * intensity * albedoOnSphere * intensity),
                            //(sphereTemp.couleur.g * powerOfShadow * distanceLight.Y * intensity * albedoOnSphere * intensity),
                            //(sphereTemp.couleur.b * powerOfShadow * distanceLight.Z * intensity * albedoOnSphere * intensity));


                        }
                    }

                }
            }
            return img;
        }

        ///
        public void SetPixel(int x, int y, double _r, double _g, double _b) {
            this.r[x, y] = (int) Clamp(_r);
            this.g[x, y] = (int) Clamp(_g);
            this.b[x, y] = (int) Clamp(_b);
        }



        //public double Clamp(double color) {
        //    if (color <= 0) color = 0;
        //    if (color >= 255) color = 255;
        //    color = color / 255;
        //    color = Math.Pow(color, 1 / 2.2);
        //    color = color * 255;
        //    return color;
        //}


        ////


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
//public Image dessineScene(Camera cam, Scene scen, Couleur _couleur) {
//    Image img = new Image(cam.largeur, cam.longueur, _couleur);
//    for (int x = (int)cam.o.X; x < cam.o.X + cam.largeur; x++) {
//        for (int y = (int)cam.o.Y; y < cam.o.Y + cam.longueur; y++) {
//            Ray r = new Ray(new Vector3(x, y, cam.o.Z), cam.GetFocusAngle(x, y));
//            double temp = double.MaxValue;
//            Sphere s2 = new Sphere(new Vector3(0, 0, 0), 0, new Couleur(0, 0, 0)); //défaut
//            foreach (Sphere s in scen.spheres) {
//                if (intersection(r, s) != -1 && intersection(r, s) < temp) {
//                    temp = intersection(r, s);
//                    s2 = s;
//                }
//            }
//            if (temp != double.MaxValue) {
//                Vector3 pointOnSphere = Vector3.Add(new Vector3(x, y, cam.o.Z), Vector3.Multiply((float)temp, cam.d));

//                //On décale i un tout petit peu vers l'extérieur de la sphère pour être sur de pas être dans la sphère.
//                //On calcule le vecteur pointSphere->centreSphere, on le normalise et on l'inverse
//                Vector3 directionTemp = Vector3.Negate(Vector3.Normalize(Vector3.Subtract(s2.c, pointOnSphere)));

//                pointOnSphere = Vector3.Add(pointOnSphere, directionTemp);
//                Ray r2 = new Ray(pointOnSphere, Vector3.Subtract(scen.lumiere.origine, pointOnSphere));
//                bool seeTheLight = true;

//                foreach (Sphere s in scen.spheres) {
//                    if (intersection(r, s) != -1) {
//                        seeTheLight = false;
//                        break;
//                    }

//                }
//                if (seeTheLight) {
//                    Console.WriteLine("Lumiere");
//                    //dessinerPixel(x, y, s2.couleur.rgb, 255);
//                    dessinerPixel(x, y, (int) s2.couleur.r, (int) s2.couleur.g, (int) s2.couleur.b, 255);
//                }
//                else {
//                    //dessinerPixel(x, y, s2.couleur.rgb, 100);
//                    dessinerPixel(x, y, (int) s2.couleur.r, (int) s2.couleur.g, (int) s2.couleur.b, 100);

//                }
//            }
//        }
//    }

//    return img;
//}
