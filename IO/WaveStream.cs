using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WaveLibrary.IO
{
    class FmtHeader
    {
        public UInt16 format = 0x0001;
        public UInt16 channels = 1;
        public UInt32 sampleRate = 44100;
        public UInt32 bytePerSec = 88200; // (sampleRate * (bit / 8) * channels)
        public UInt16 blockSize = 2; // (channels * (bit / 8))
        public UInt16 bit = 16;
        public UInt16 extendSize = 0;
        public byte[] extend = new byte[0];
    }

    public static class WaveStream
    {

        const UInt32 RiffHeader = 0x46464952;
        const UInt32 WaveHeader = 0x45564157;
        const UInt32 FmtTag = 0x20746d66;
        const UInt32 DataTag = 0x61746164;

        public static Wave Read(Stream stream)
        {
            FmtHeader fmt = new FmtHeader();
            byte[] sampleData = null;

            using (BinaryReader br = new BinaryReader(stream))
            {
                if (br.ReadUInt32() != RiffHeader) throw new FileLoadException();
                UInt32 dataSize = br.ReadUInt32();
                if (br.ReadUInt32() != WaveHeader) throw new FileLoadException();
                for (; ; )
                {
                    UInt32 tag = br.ReadUInt32();
                    UInt32 size = br.ReadUInt32();

                    if (tag == FmtTag)
                    {
                        fmt.format = br.ReadUInt16();
                        fmt.channels = br.ReadUInt16();
                        fmt.sampleRate = br.ReadUInt32();
                        fmt.bytePerSec = br.ReadUInt32();
                        fmt.blockSize = br.ReadUInt16();
                        fmt.bit = br.ReadUInt16();
                        if (size > 16)
                        {
                            fmt.extendSize = br.ReadUInt16();
                            fmt.extend = br.ReadBytes(fmt.extendSize);
                        }
                    }
                    else if (tag == DataTag)
                    {
                        sampleData = br.ReadBytes((int)size);
                    }
                    else
                    {
                        br.ReadBytes((int)size); //unknown chunk
                    }

                    dataSize -= (size + 8);
                    if(dataSize < 8) break;
                    if (dataSize < 0) throw new FileLoadException();
                }
            }

            Wave wave = CreateWave(fmt, sampleData);
            if (wave == null) throw new FileLoadException();
            return wave;
        }

        /// <summary>
        /// 現状はChannel1しか取らない
        /// </summary>
        /// <param name="fmt"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        private static Wave CreateWave(FmtHeader fmt, byte[] data)
        {
            if (fmt.format != 1) return null;
            int length = data.Length / ((fmt.bit / 8) * fmt.channels);
            double[] d = new double[length];
            switch (fmt.bit)
            {
                case 8:
                    for (int i = 0; i < length; i++)
                        d[i] = (((int)data[i * fmt.channels]) - 128) / 128.0;
                    break;
                case 16:
                    for (int i = 0; i < length; i++)
                        d[i] = BitConverter.ToInt16(new byte[] { data[i * fmt.channels * 2], data[i * fmt.channels * 2 + 1] }, 0) / (double)Int16.MaxValue;
                    break;
                default:
                    return null;
            }
            return new Wave(d, (int)fmt.sampleRate);
        }

        public static void Write(Stream stream, IWave wave)
        {
            using (var bw = new BinaryWriter(stream))
            {
                FmtHeader fmt = new FmtHeader();
                fmt.sampleRate = (UInt32)wave.SamplingRate;
                bw.Write(RiffHeader);
                bw.Write((UInt32)(36 + wave.Length * 2));
                bw.Write(WaveHeader);
                bw.Write(FmtTag);
                bw.Write((UInt32)16);
                bw.Write((UInt16)fmt.format);
                bw.Write((UInt16)fmt.channels);
                bw.Write((UInt32)fmt.sampleRate);
                bw.Write((UInt32)fmt.bytePerSec);
                bw.Write((UInt16)fmt.blockSize);
                bw.Write((UInt16)fmt.bit);
                bw.Write(DataTag);
                bw.Write((UInt32)(wave.Length * 2));
                foreach (var a in wave.Reads())
                {
                    bw.Write((Int16)((int)Int16.MaxValue * a));
                }
            }
        }

    }
}
