using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Write {
    public class Synthese {


        /// <summary>
        /// Rayon de base P et direction D
        /// </summary>
        public struct Ray {
            //point
            public Vector3 p { get; set; }
            //direction
            public Vector3 d { get; set; }

            public Vector3 complet { get; set; }


            public Ray(Vector3 v1, Vector3 v2) {
                p = v1;
                d = v2;
                complet = Vector3.Add(p, d);
            }

            public Ray(Camera c, Vector3 v2) {
                p = c.position;
                d = v2;
                complet = Vector3.Add(p, d);
            }


        }

        /// <summary>
        /// Sphère, centre C et rayon R
        /// </summary>
        public struct Sphere {
            //centre
            public Vector3 c { get; set; }
            //hauteur rayon
            public float r { get; set; }

            public Couleur couleur;

            public Sphere(Vector3 v1, int f, Couleur _couleur) {
                c = v1;
                r = f;
                couleur = _couleur;
            }
        }

        /// <summary>
        /// Camera
        /// </summary>
        public struct Camera {
            public Vector3 position { get; set; }
            public int hauteur;
            public int largeur;

            public Camera(Vector3 p, int _hauteur, int _largeur) {
                position = p;
                hauteur = _hauteur;
                largeur = _largeur;
            }

        }

        /// <summary>
        /// Lumiere
        /// </summary>
        public struct Lumiere {
            public Vector3 origine;

            public Lumiere(Vector3 _origine) {
                origine = _origine;
            }

        }

        /// <summary>
        /// Couleur
        /// </summary>
        public struct Couleur {
            public double r, g, b;
            public double[] rgb;

            public Couleur(double red, double green, double blue) {
                r = red;
                g = green;
                b = blue;
                rgb = new double[3] { red, green, blue };
            }

        }

        /// <summary>
        /// Détermine si on est sur le rayon ou non
        /// </summary>
        /// <param name="r"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        public static bool surRayon(Ray r, Vector3 v) {
            if (((v.X - r.p.X) / (r.complet.X - r.p.X)) == ((v.Y - r.p.Y) / (r.complet.Y - r.p.Y))
                && ((v.X - r.p.X) / (r.complet.X - r.p.X)) == ((v.Z - r.p.Z) / (r.complet.Z - r.p.Z)))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Calcule l'intersection entre le rayon et la sphère
        /// </summary>
        /// <param name="ray"></param>
        /// <param name="sphere"></param>
        /// <returns></returns>
        public static double intersection(Ray ray, Sphere sphere) {

            //c-p
            Vector3 vecSub = Vector3.Subtract(sphere.c, ray.p);
            double A = Vector3.Dot(ray.d, ray.d);
            double B = 2 * (Vector3.Dot(ray.p, ray.d) - Vector3.Dot(sphere.c, ray.d));
            double C = Vector3.Dot(vecSub, vecSub) - Math.Pow(sphere.r, 2);

            //affichage
            //Console.WriteLine("A : " + A.ToString());
            //Console.WriteLine("B : " + B.ToString());
            //Console.WriteLine("C : " + C.ToString());

            //Calcul du delta
            double delta = Math.Pow(B, 2) - (4 * (A * C));

            if (delta < 0) {
                Console.WriteLine("Aucune intersection");

                return -1;
            }
            else if (delta >= 0) {
                //on determine les deux intersections et retourne la première
                double res1 = (((-B) + Math.Sqrt(delta)) / (2 * A));
                double res2 = (((-B) - Math.Sqrt(delta)) / (2 * A));

                double finalRes = Math.Min(res1, res2);

                Console.WriteLine("Intersection !");
                Console.WriteLine("Res delta: " + finalRes.ToString());

                return finalRes;
            }

            return -1;

        }

        public static void trouverSphere(Image img, Camera camera, Sphere sphere) {

            Ray ray;

            for (int x = 0; x < camera.hauteur; x++) {
                for (int y = 0; y < camera.largeur; y++) {
                    Vector3 vec = Vector3.Add(new Vector3(x, y, 0), camera.position);
                    ray = new Ray(vec, new Vector3(0, 0, 1));

                    if (intersection(ray, sphere) == -1) {
                        //Console.WriteLine("No intersection");

                    }
                    else {


                    }

                }
            }

        }

    }
}
