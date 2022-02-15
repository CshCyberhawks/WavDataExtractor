using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mp3Parser
{
    internal class Output
    {
        public string FileType;
        public int FileSize;

        public string AudioType;

        public int DateChunkSize;
        public List<int> Data = new List<int>();

        public Output()
        {

        }
    }
}
