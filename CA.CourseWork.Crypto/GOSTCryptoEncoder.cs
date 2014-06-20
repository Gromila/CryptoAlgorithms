using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CA.CourseWork.Crypto.Interfaces;

namespace CA.CourseWork.Crypto
{
    public class GOSTCryptoEncoder : GOSTCrypto, IEncryptable
    {
        String IEncryptable.Encode(String data, String key, bool isParallel = false)
        {
            return Encoding.Unicode.GetString(((IEncryptable) (this)).Encode(Encoding.Unicode.GetBytes(data), key, isParallel));
        }

        byte[] IEncryptable.Encode(byte[] data, String key, bool isParallel = false)
        {
            return ((IEncryptable) (this)).Encode(data, Encoding.Unicode.GetBytes(key), isParallel);
        }

        byte[] IEncryptable.Encode(byte[] data, byte[] key, bool isParallel = false)
        {
            var subkeys = GenerateKeys(key);
            var result = new byte[data.Length];
            var block = new byte[8];

            if (isParallel)
            {
                Parallel.For(0, data.Length/8, i =>
                {
                    Array.Copy(data, 8 * i, block, 0, 8);
                    Array.Copy(EncodeBlock(block, subkeys), 0, result, 8 * i, 8);
                });
            }
            else
            {
                for (int i = 0; i < data.Length/8; i++) // N blocks 64bits length.
                {
                    Array.Copy(data, 8*i, block, 0, 8);
                    Array.Copy(EncodeBlock(block, subkeys), 0, result, 8*i, 8);
                }
            }
            return result;
        }

        private byte[] EncodeBlock(byte[] block, uint[] keys)
        {
            // separate on 2 blocks.
            uint N1 = BitConverter.ToUInt32(block, 0);
            uint N2 = BitConverter.ToUInt32(block, 4);
            
            for (int i = 0; i < 32; i++)
            {
                int keyIndex = i < 24 ? (i%8) : (7 - i%8); // to 24th cycle : 0 to 7; after - 7 to 0;
                var s = (N1 + keys[keyIndex]) % uint.MaxValue; // (N1 + X[i]) mod 2^32
                s = Substitution(s); // substitute from box
                s = (s << 11) | (s >> 21);
                s = s ^ N2; // ( s + N2 ) mod 2
                //N2 = N1;
                //N1 = s;
                if (i < 31) // last cycle : N1 don't change; N2 = s;
                {
                    N2 = N1;
                    N1 = s;
                }
                else
                {
                    N2 = s;
                }
            }

            var output = new byte[8];
            var N1buff = BitConverter.GetBytes(N1);
            var N2buff = BitConverter.GetBytes(N2);

            for (int i = 0; i < 4; i++)
            {
                output[i] = N1buff[i];
                output[4 + i] = N2buff[i];  
            } 

            return output;
        }
    }
}
