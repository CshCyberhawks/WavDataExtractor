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

        public string FormatChunkName;
        public int FormatChunkLength;
        public int FormatCode;
        public int ChannelCount;
        public int SamplesPerSecond;
        public int BytesPerSecond;
        public int BytesPerSampleFrame;
        public int BitsPerSample;

        public string DataChunkName;
        public int DataChunkSize;
        public List<int> Data = new List<int>();

        public Output()
        {

        }
    }
}
