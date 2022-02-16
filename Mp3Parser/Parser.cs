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

        private static int ReadUInt16()
        {
            return (int)BitConverter.ToUInt16(Read(2), 0);
        }

        private static int ReadInt16()
        {
            return BitConverter.ToInt16(Read(2), 0);
        }

        private static int ReadUInt32()
        {
            return (int)BitConverter.ToUInt32(Read(4), 0);
        }

        private static string ReadName()
        {
            return Encoding.UTF8.GetString(Read(4));
        }

        public static Output Parse(byte[] fileData)
        {
            // https://wavefilegem.com/how_wave_files_work.html

            stream = new MemoryStream(fileData);

            Output output = new Output();

            output.FileType = ReadName(); // File Type (assuming Riff)
            output.FileSize = ReadUInt32(); // File Size

            output.AudioType = ReadName(); // Audio File Type (assuming WAVE)

            Read(4); // Format Chunk
            int formatLength = ReadUInt32(); // Format Chunk Length (unsigned 32 bit int)
            output.FormatCode = ReadUInt16();
            output.ChannelCount = ReadUInt16();
            output.SamplesPerSecond = ReadUInt32();
            output.BytesPerSecond = ReadUInt32();
            output.BytesPerSampleFrame = ReadUInt16();
            output.BitsPerSample = ReadUInt16();

            Read(4); // Data Chunk
            int dataLength = ReadUInt32(); // Data Chunk Length (the length of the rest of the file)
            output.DateChunkSize = dataLength;

            while (dataLength > 1)
            {
                output.Data.Add(ReadInt16());
                dataLength -= 2;
            }

            return output;
        }
    }
}
