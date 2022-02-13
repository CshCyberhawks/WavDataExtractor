using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mp3Parser
{
    internal class Formatter
    {
        public static string Format(int[] freqs)
        {
            return JsonConvert.SerializeObject(freqs);
        }
    }
}
