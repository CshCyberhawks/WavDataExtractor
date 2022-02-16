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
        
        private static string ReadString(int length)
        {
            return Encoding.UTF8.GetString(Read(length));
        }

        private static string ReadName()
        {
            return ReadString(4);
        }

        public static Output Parse(byte[] fileData)
        {
            // https://wavefilegem.com/how_wave_files_work.html

            stream = new MemoryStream(fileData);

            Output output = new Output()
            {
                FileType = ReadName(), // File Type (assuming Riff)
                FileSize = ReadUInt32(), // File Size

                AudioType = ReadName(), // Audio File Type (assuming WAVE)

                FormatChunkName = ReadName(), // Format Chunk
                FormatChunkLength = ReadUInt32(), // Format Chunk Length (unsigned 32 bit int)
                FormatCode = ReadUInt16(),
                ChannelCount = ReadUInt16(),
                SamplesPerSecond = ReadUInt32(),
                BytesPerSecond = ReadUInt32(),
                BytesPerSampleFrame = ReadUInt16(),
                BitsPerSample = ReadUInt16(),

                DataChunkName = ReadName(), // Data Chunk
                DataChunkSize = ReadUInt32() // Data Chunk Length (the length of the rest of the file)
            };

            int dataLength = output.DataChunkSize;

            while (dataLength > 1)
            {
                output.Data.Add(ReadInt16());
                dataLength -= 2;
            }

            return output;
        }
    }
}
