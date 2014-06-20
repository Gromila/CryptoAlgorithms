using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CA.CourseWork.Crypto.Interfaces;

namespace CA.CourseWork.Crypto
{
    public class AESCryptoEncoder : AESCrypto, IEncryptable
    {
        /// <summary>
        /// Encrypt data string using string key by AES-128 algorithm.
        /// </summary>
        /// <param name="data">Data to encode.</param>
        /// <param name="key">Key to encode.</param>
        /// <returns>Encoded string.</returns>
        String IEncryptable.Encode(String data, String key, bool isParallel = false)
        {
            return Encoding.Unicode.GetString(((IEncryptable)(this)).Encode(Encoding.Unicode.GetBytes(data), key, isParallel));
        }

        byte[] IEncryptable.Encode(byte[] data, String key, bool isParallel = false)
        {
            return ((IEncryptable)(this)).Encode(data, Encoding.Unicode.GetBytes(key), isParallel);
        }


        byte[] IEncryptable.Encode(byte[] data, byte[] key, bool isParallel = false)
        {
            var dataMatrix = new byte[data.Length % 16 == 0 ? data.Length / 16 : data.Length / 16 + 1][];

            for (int i = 0; i < dataMatrix.Length; i++)
            {
                dataMatrix[i] = new byte[16];
            }

            int iterator = 0;
            int row = 0;
            while (iterator < data.Length)
            {
                if (row < data.Length / 16)
                {
                    Array.Copy(data, iterator, dataMatrix[row], 0, 16);
                    iterator += 16;
                }
                else
                {
                    Array.Copy(data, iterator, dataMatrix[row], 0, data.Length % 16);
                    iterator += data.Length % 16;
                }
                row++;
            }

            var keySchedule = KeyExpansion(key);

            var encoded = new byte[data.Length];

            if (isParallel)
            {
                Parallel.For(0, dataMatrix.Length, block =>
                {
                    var state = new byte[4][];
                    for (int i = 0; i < state.Length; i++)
                    {
                        state[i] = new byte[NumberOfColumns];
                        for (int j = 0; j < state[i].Length; j++)
                        {
                            state[i][j] = dataMatrix[block][i + 4 * j];
                        }
                    }

                    Array.Copy(EncodeBlock(state, keySchedule), 0, encoded, 16 * block, 16);
                });
            }
            else
            {
                for (int block = 0; block < dataMatrix.Length; block++)
                {
                    var state = new byte[4][];
                    for (int i = 0; i < state.Length; i++)
                    {
                        state[i] = new byte[NumberOfColumns];
                        for (int j = 0; j < state[i].Length; j++)
                        {
                            state[i][j] = dataMatrix[block][i + 4 * j];
                        }
                    }

                    Array.Copy(EncodeBlock(state, keySchedule), 0, encoded, 16 * block, 16);
                }
            }
            

            return encoded;
        }

        private byte[] EncodeBlock(byte[][] state, byte[][] keySchedule)
        {
            state = AddRoundKey(state, keySchedule);

            for (int round = 1; round < NumberOfRounds; round++)
            {
                state = SubBytes(state);
                state = ShiftRows(state);
                state = MixColumns(state);
                state = AddRoundKey(state, keySchedule, round);
            }

            state = SubBytes(state);
            state = ShiftRows(state);
            state = AddRoundKey(state, keySchedule, NumberOfRounds);

            var output = new byte[state.Length * state[0].Length];
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < NumberOfColumns; j++)
                {
                    output[i + 4 * j] = state[i][j];
                }
            }

            return output;
        }
    }
}
