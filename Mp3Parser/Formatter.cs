﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mp3Parser
{
    internal class Formatter
    {
        public static string Format(Output data)
        {
            return JsonConvert.SerializeObject(data);
        }
    }
}
