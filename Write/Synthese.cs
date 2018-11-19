using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Write {
    public class Synthese {


        public struct Boite {

            public Vector3 maxX;
            public Vector3 maxY;
            public Couleur couleur;


            public Boite(Vector3 _v1, Vector3 _v2, Couleur _couleur) {

                maxX = _v1;
                maxY = _v2;
                couleur = _couleur;
            }
        }


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

            public Surface surface;

            public Sphere(Vector3 v1, int f, Couleur _couleur) {
                c = v1;
                r = f;
                couleur = _couleur;
                surface = new Surface(0.2);
            }
        }

        /// <summary>
        /// Camera
        /// </summary>
        public struct Camera {

            public Vector3 position { get; set; }
            public int hauteur;
            public int largeur;
            public Focus focus;

            public Camera(Vector3 p, int _hauteur, int _largeur) {
                position = p;
                hauteur = _hauteur;
                largeur = _largeur;

                focus.distance = 1000;
                focus.position = new Vector3(position.X + (hauteur / 2), position.Y + (largeur / 2), position.Z - focus.distance);
                Console.WriteLine("Focus pos : " + focus.position.ToString());
            }


            public Vector3 VecteurDirecteurFocus(float x, float y) {
                Vector3 v = Vector3.Subtract(new Vector3(x, y, position.Z), focus.position);
                //Console.WriteLine("Vector angleFocus : " + vec.ToString());
                return Vector3.Normalize(v);
            }

        }

        /// <summary>
        /// Focus
        /// </summary>
        public struct Focus {
            public Vector3 position;
            public float distance; //distance par rapport à l'écran

            public Focus(Vector3 pos, float dist) {
                position = pos;
                distance = dist;
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
            public int puissance;
            public int intensite;


            public Lumiere(Vector3 _origine, int _puissance, int _intensite) {
                origine = _origine;
                puissance = _puissance;
                intensite = _intensite;
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

        public class Surface {
            //(Albedo* cosTheta) / Pi
            public double albedo;

            public Surface(double _albedo) {
                albedo = _albedo;
            }
        }

        public static double DiffusionLumineuse(double albedo, Vector3 rayonVersLumiere, Vector3 normaleSphere) {
            // formule : (Albedo*cosTheta) / Pi
            // rayonVersLumiere est le rayon de départ pointSphere, en direction de la lumière
            // Dot entre rayoutVersLumiere et normal de la sphere, vérifier >= 0 et <= 1

            var res = (albedo * Vector3.Dot(rayonVersLumiere, normaleSphere)) / Math.PI;
            // Return quantité de lumière transmise par ma surface
            return res;
        }

        public static double DistanceLumineuse(int puissanceLumineuse, Vector3 pointSphere, Vector3 origineLumineuse) {
            // distance entre point d'origine de ma lumière et le point dans ma sphere actuel
            float distance = Vector3.Distance(origineLumineuse, pointSphere);

            double distanceLumineuse = (puissanceLumineuse * 1) / (distance * distance);

            return distanceLumineuse;

        }

        /// <summary>
        /// Détermine si on est sur le rayon ou non
        /// </summary>
        /// <param name="r"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        public static bool SurRayon(Ray r, Vector3 v) {
            if (((v.X - r.p.X) / (r.complet.X - r.p.X)) == ((v.Y - r.p.Y) / (r.complet.Y - r.p.Y))
                && ((v.X - r.p.X) / (r.complet.X - r.p.X)) == ((v.Z - r.p.Z) / (r.complet.Z - r.p.Z)))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Calcule l'Intersection entre le rayon et la sphère
        /// </summary>
        /// <param name="ray"></param>
        /// <param name="sphere"></param>
        /// <returns></returns>
        public static double Intersection(Ray ray, Sphere sphere) {

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
                //Console.WriteLine("Aucune Intersection");
                return -1;
            }
            else if (delta >= 0) {
                //on determine les deux Intersections et retourne la première
                double res1 = (((-B) + Math.Sqrt(delta)) / (2 * A));
                double res2 = (((-B) - Math.Sqrt(delta)) / (2 * A));

                if (res2 > 0) {
                    return res2;
                }
                else if (res1 > 0) {
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
