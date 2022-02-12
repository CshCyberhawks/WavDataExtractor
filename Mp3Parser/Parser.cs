using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mp3Parser
{
    internal class Parser
    {
        private static MemoryStream stream;

        private static byte[] Read(int length, int offset = 0)
        {
            byte[] buffer = new byte[length];
            stream.Read(buffer, offset, length);

            return buffer;
        }

        private static int ReadInt16()
        {
            return BitConverter.ToInt16(Read(2), 0);
        }

        private static int ReadUInt32()
        {
            return (int)BitConverter.ToUInt32(Read(4), 0);
        }

        public static int[] Parse(byte[] fileData)
        {
            // https://wavefilegem.com/how_wave_files_work.html

            stream = new MemoryStream(fileData);

            Read(4); // File Type (assuming Riff)
            Read(4); // File Size

            Read(4); // Audio File Type (assuming WAVE)

            Read(4); // Format Chunk
            int formatLength = ReadUInt32(); // Format Chunk Length (unsigned 32 bit int)
            Read(formatLength); // Ignore the format chunk data

            Read(4); // Data Chunk
            int dataLength = ReadUInt32(); // Data Chunk Length (the length of the rest of the file)

            List<int> audioData = new List<int>();

            while (dataLength > 1)
            {
                audioData.Add(ReadInt16());
                dataLength -= 2;
            }

            return audioData.ToArray();
        }
    }
}
