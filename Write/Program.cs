using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Write
{
    class Program
    {

        void SimpleWrite()
        {
            // Create a string array with the lines of text
            string[] lines = { "First line", "Second line", "Third line", "Fourth Line" };

            // Set a variable to the My Documents path.
            string mydocpath =
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            // Write the string array to a new file named "WriteLines.txt".
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(mydocpath, "WriteLines.txt")))
            {
                foreach (string line in lines)
                    outputFile.WriteLine(line);
            }
        }

        void PPM()
        {
            // Create a string array with the lines of text
            string[] lines = { "P6", "1024 788", "255" };

            // Set a variable to the My Documents path.
            string mydocpath =
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            // Write the string array to a new file named "WriteLines.txt".
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(mydocpath, "Img1.ppm")))
            {
                foreach (string line in lines)
                    outputFile.WriteLine(line);
            }
        }

        static void Main(string[] args)
        {
            Program prog = new Program();
            prog.PPM();
        }
    }
}
