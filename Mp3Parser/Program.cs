using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Mp3Parser
{
    internal class Program
    {
        static void Main(string[] args)
        {
            byte[] fileData = File.ReadAllBytes("example.wav");

            Output audioData = Parser.Parse(fileData);

            string formattedData = Formatter.Format(audioData);

            File.WriteAllText("output.json", formattedData);
        }
    }
}
