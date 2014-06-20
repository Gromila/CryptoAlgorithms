using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CA.CourseWork.Crypto.Interfaces;

namespace CA.CourseWork.Crypto
{
    public class AESCryptoDecoder : AESCrypto, IDecryptable
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

            var decoded = new byte[data.Length];
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

                    Array.Copy(DecodeBlock(state, keySchedule), 0, decoded, 16 * block, 16);
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
                            state[i][j] = dataMatrix[block][i + 4*j];
                        }
                    }

                    Array.Copy(DecodeBlock(state, keySchedule), 0, decoded, 16*block, 16);
                }
            }

            return decoded;
        }

        private byte[] DecodeBlock(byte[][] state, byte[][] keySchedule)
        {
            state = AddRoundKey(state, keySchedule, NumberOfRounds);

            var round = NumberOfRounds - 1;

            while (round >= 1)
            {
                state = ShiftRows(state, true);
                state = SubBytes(state, true);
                state = AddRoundKey(state, keySchedule, round);
                state = MixColumns(state, true);
                round -= 1;
            }

            state = ShiftRows(state, true);
            state = SubBytes(state, true);
            state = AddRoundKey(state, keySchedule, round);

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
