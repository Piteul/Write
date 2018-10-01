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

            //public Ray(Camera c, Vector3 v2) {
            //    p = c.position;
            //    d = v2;
            //    complet = Vector3.Add(p, d);
            //}


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

            //public Vector3 o;
            //public int longueur, largeur;
            //public Vector3 d;
            //public Vector3 focus;
            //public float distanceFocus = 500;

            //public Camera(Vector3 _origine, int _longueur, int _largeur, Vector3 _direction) {
            //    o = _origine;
            //    longueur = _longueur;
            //    largeur = _largeur;
            //    d = Vector3.Normalize(_direction);
            //    focus = new Vector3(o.X + (longueur / 2), o.Y + (largeur / 2), o.Z);
            //    focus = Vector3.Add(focus, Vector3.Multiply(Vector3.Negate(d), 500));
            //}

            //public Vector3 GetFocusAngle(float x, float y) {
            //    Vector3 res = Vector3.Subtract(new Vector3(x, y, o.Z), focus);
            //    return Vector3.Normalize(res);
            //}


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
        /// Scene
        /// </summary>
        public struct Scene {
            public List<Sphere> spheres;
            public Camera cam { get; set; }
            public Lumiere lumiere { get; set; }


            public Scene(Camera _cam, Lumiere _lumiere) {
                cam = _cam;
                lumiere = _lumiere;
                spheres = new List<Sphere>();

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
                //Console.WriteLine("Aucune intersection");
                return -1;
            }
            else if (delta >= 0) {
                //on determine les deux intersections et retourne la première
                double res1 = (((-B) + Math.Sqrt(delta)) / (2 * A));
                double res2 = (((-B) - Math.Sqrt(delta)) / (2 * A));

                if(res2 > 0) {
                    return res2;
                }
                else if(res1 > 0) {
                    return res1;
                }
                else {
                    return -1;
                }

                //double finalRes = Math.Min(res1, res2);

                //Console.WriteLine("Intersection !");
                //Console.WriteLine("Res delta: " + finalRes.ToString());

                //return finalRes;
            }

            return -1;

        }

     

    }
}
