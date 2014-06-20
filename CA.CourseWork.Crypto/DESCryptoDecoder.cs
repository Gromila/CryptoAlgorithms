using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CA.CourseWork.Crypto.Interfaces;

namespace CA.CourseWork.Crypto
{
    public class DESCryptoDecoder : DESCrypto, IDecryptable
    {
        String IDecryptable.Decode(String data, String key, bool isParallel = false)
        {
            return Encoding.Unicode.GetString(((IDecryptable)(this)).Decode(Encoding.Unicode.GetBytes(data), key, isParallel));
        }

        byte[] IDecryptable.Decode(byte[] data, String key, bool isParallel = false)
        {
            return ((IDecryptable)(this)).Decode(data, Encoding.Unicode.GetBytes(key), isParallel);
        }

        byte[] IDecryptable.Decode(byte[] data, byte[] key, bool isParallel = false)
        {
            var subkeys = GenerateSubkeys(key);

            var result = new byte[data.Length];
            var block = new byte[8];

            if (isParallel)
            {
                Parallel.For(0, data.Length/8, i =>
                {
                    Array.Copy(data, 8*i, block, 0, 8);
                    Array.Copy(DecodeBlock(block, subkeys), 0, result, 8*i, 8);
                });
            }
            else
            {
                for (int i = 0; i < data.Length/8; i++) // N blocks 64bits length.
                {
                    Array.Copy(data, 8*i, block, 0, 8);
                    Array.Copy(DecodeBlock(block, subkeys), 0, result, 8*i, 8);
                }
            }

            return result;
        }

        private byte[] DecodeBlock(byte[] data, byte[][] subkeys)
        {
            var message = SelectBits(data, InputPermutation);

            int blockSize = InputPermutation.Length;

            var left = SelectBits(message, 0, blockSize / 2);
            var right = SelectBits(message, blockSize / 2, blockSize / 2);
            
            for (int i = 0; i < subkeys.Length; i++)
            {
                byte[] rightTemp = right;

                right = SelectBits(right, ExpansionPermutation);

                right = XORBytes(right, subkeys[subkeys.Length - i - 1]);

                right = Substitution6x4(right);

                right = SelectBits(right, Permutation);

                right = XORBytes(left, right);

                left = rightTemp;
            }

            var result = ConcatenateBits(right, blockSize / 2, left, blockSize / 2);
            return SelectBits(result, FinalPermutation);
        }
    }
}
