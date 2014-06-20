using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CA.CourseWork.Crypto
{
    public abstract class AESCrypto
    {
        protected const int NumberOfColumns = 4; // Nb in specification.

        protected const int NumberOfRounds = 10; // Nr in spec.

        protected const int KeyLength = 4; // 32-bit word.

        protected byte[] SubstitutionBox = 
        {
            0x63, 0x7c, 0x77, 0x7b, 0xf2, 0x6b, 0x6f, 0xc5, 0x30, 0x01, 0x67, 0x2b, 0xfe, 0xd7, 0xab, 0x76,
            0xca, 0x82, 0xc9, 0x7d, 0xfa, 0x59, 0x47, 0xf0, 0xad, 0xd4, 0xa2, 0xaf, 0x9c, 0xa4, 0x72, 0xc0,
            0xb7, 0xfd, 0x93, 0x26, 0x36, 0x3f, 0xf7, 0xcc, 0x34, 0xa5, 0xe5, 0xf1, 0x71, 0xd8, 0x31, 0x15,
            0x04, 0xc7, 0x23, 0xc3, 0x18, 0x96, 0x05, 0x9a, 0x07, 0x12, 0x80, 0xe2, 0xeb, 0x27, 0xb2, 0x75,
            0x09, 0x83, 0x2c, 0x1a, 0x1b, 0x6e, 0x5a, 0xa0, 0x52, 0x3b, 0xd6, 0xb3, 0x29, 0xe3, 0x2f, 0x84,
            0x53, 0xd1, 0x00, 0xed, 0x20, 0xfc, 0xb1, 0x5b, 0x6a, 0xcb, 0xbe, 0x39, 0x4a, 0x4c, 0x58, 0xcf,
            0xd0, 0xef, 0xaa, 0xfb, 0x43, 0x4d, 0x33, 0x85, 0x45, 0xf9, 0x02, 0x7f, 0x50, 0x3c, 0x9f, 0xa8,
            0x51, 0xa3, 0x40, 0x8f, 0x92, 0x9d, 0x38, 0xf5, 0xbc, 0xb6, 0xda, 0x21, 0x10, 0xff, 0xf3, 0xd2,
            0xcd, 0x0c, 0x13, 0xec, 0x5f, 0x97, 0x44, 0x17, 0xc4, 0xa7, 0x7e, 0x3d, 0x64, 0x5d, 0x19, 0x73,
            0x60, 0x81, 0x4f, 0xdc, 0x22, 0x2a, 0x90, 0x88, 0x46, 0xee, 0xb8, 0x14, 0xde, 0x5e, 0x0b, 0xdb,
            0xe0, 0x32, 0x3a, 0x0a, 0x49, 0x06, 0x24, 0x5c, 0xc2, 0xd3, 0xac, 0x62, 0x91, 0x95, 0xe4, 0x79,
            0xe7, 0xc8, 0x37, 0x6d, 0x8d, 0xd5, 0x4e, 0xa9, 0x6c, 0x56, 0xf4, 0xea, 0x65, 0x7a, 0xae, 0x08,
            0xba, 0x78, 0x25, 0x2e, 0x1c, 0xa6, 0xb4, 0xc6, 0xe8, 0xdd, 0x74, 0x1f, 0x4b, 0xbd, 0x8b, 0x8a,
            0x70, 0x3e, 0xb5, 0x66, 0x48, 0x03, 0xf6, 0x0e, 0x61, 0x35, 0x57, 0xb9, 0x86, 0xc1, 0x1d, 0x9e,
            0xe1, 0xf8, 0x98, 0x11, 0x69, 0xd9, 0x8e, 0x94, 0x9b, 0x1e, 0x87, 0xe9, 0xce, 0x55, 0x28, 0xdf,
            0x8c, 0xa1, 0x89, 0x0d, 0xbf, 0xe6, 0x42, 0x68, 0x41, 0x99, 0x2d, 0x0f, 0xb0, 0x54, 0xbb, 0x16
        };

        protected byte[] InverseSubstitutionBox =
        {
            0x52, 0x09, 0x6a, 0xd5, 0x30, 0x36, 0xa5, 0x38, 0xbf, 0x40, 0xa3, 0x9e, 0x81, 0xf3, 0xd7, 0xfb,
            0x7c, 0xe3, 0x39, 0x82, 0x9b, 0x2f, 0xff, 0x87, 0x34, 0x8e, 0x43, 0x44, 0xc4, 0xde, 0xe9, 0xcb,
            0x54, 0x7b, 0x94, 0x32, 0xa6, 0xc2, 0x23, 0x3d, 0xee, 0x4c, 0x95, 0x0b, 0x42, 0xfa, 0xc3, 0x4e,
            0x08, 0x2e, 0xa1, 0x66, 0x28, 0xd9, 0x24, 0xb2, 0x76, 0x5b, 0xa2, 0x49, 0x6d, 0x8b, 0xd1, 0x25,
            0x72, 0xf8, 0xf6, 0x64, 0x86, 0x68, 0x98, 0x16, 0xd4, 0xa4, 0x5c, 0xcc, 0x5d, 0x65, 0xb6, 0x92,
            0x6c, 0x70, 0x48, 0x50, 0xfd, 0xed, 0xb9, 0xda, 0x5e, 0x15, 0x46, 0x57, 0xa7, 0x8d, 0x9d, 0x84,
            0x90, 0xd8, 0xab, 0x00, 0x8c, 0xbc, 0xd3, 0x0a, 0xf7, 0xe4, 0x58, 0x05, 0xb8, 0xb3, 0x45, 0x06,
            0xd0, 0x2c, 0x1e, 0x8f, 0xca, 0x3f, 0x0f, 0x02, 0xc1, 0xaf, 0xbd, 0x03, 0x01, 0x13, 0x8a, 0x6b,
            0x3a, 0x91, 0x11, 0x41, 0x4f, 0x67, 0xdc, 0xea, 0x97, 0xf2, 0xcf, 0xce, 0xf0, 0xb4, 0xe6, 0x73,
            0x96, 0xac, 0x74, 0x22, 0xe7, 0xad, 0x35, 0x85, 0xe2, 0xf9, 0x37, 0xe8, 0x1c, 0x75, 0xdf, 0x6e,
            0x47, 0xf1, 0x1a, 0x71, 0x1d, 0x29, 0xc5, 0x89, 0x6f, 0xb7, 0x62, 0x0e, 0xaa, 0x18, 0xbe, 0x1b,
            0xfc, 0x56, 0x3e, 0x4b, 0xc6, 0xd2, 0x79, 0x20, 0x9a, 0xdb, 0xc0, 0xfe, 0x78, 0xcd, 0x5a, 0xf4,
            0x1f, 0xdd, 0xa8, 0x33, 0x88, 0x07, 0xc7, 0x31, 0xb1, 0x12, 0x10, 0x59, 0x27, 0x80, 0xec, 0x5f,
            0x60, 0x51, 0x7f, 0xa9, 0x19, 0xb5, 0x4a, 0x0d, 0x2d, 0xe5, 0x7a, 0x9f, 0x93, 0xc9, 0x9c, 0xef,
            0xa0, 0xe0, 0x3b, 0x4d, 0xae, 0x2a, 0xf5, 0xb0, 0xc8, 0xeb, 0xbb, 0x3c, 0x83, 0x53, 0x99, 0x61,
            0x17, 0x2b, 0x04, 0x7e, 0xba, 0x77, 0xd6, 0x26, 0xe1, 0x69, 0x14, 0x63, 0x55, 0x21, 0x0c, 0x7d
        };

        protected byte[][] RCon =
        {
            new byte[] {0x01, 0x02, 0x04, 0x08, 0x10, 0x20, 0x40, 0x80, 0x1b, 0x36},
            new byte[] {0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00},
            new byte[] {0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00},
            new byte[] {0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00}
        };

        /// <summary>
        /// Substitute bytes part of algorithm.
        /// </summary>
        /// <param name="state">Source array.</param>
        /// <param name="invert">Direction: encoding or decoding.</param>
        /// <returns>Matrix with substituted bytes.</returns>
        protected byte[][] SubBytes(byte[][] state, bool invert = false)
        {
            var box = !invert ? SubstitutionBox : InverseSubstitutionBox;

            for (int i = 0; i < state.Length; i++)
            {
                for (int j = 0; j < state[i].Length; j++)
                {
                    var row = state[i][j]/0x10;
                    var column = state[i][j]%0x10;

                    var boxItem = box[16*row + column];

                    state[i][j] = boxItem;
                }
            }

            return state;
        }

        /// <summary>
        /// Mix column part of algorithm.
        /// </summary>
        /// <param name="state">Source matrix.</param>
        /// <param name="invert">Direction: encoding or decoding.</param>
        /// <returns>Matrix with mixed columns.</returns>
        protected byte[][] MixColumns(byte[][] state, bool invert = false)
        {
            for (int i = 0; i < NumberOfColumns; i++)
            {
                byte s0, s1, s2, s3;
                if (!invert)
                {
                    s0 = (byte) (MultiplyInGFBy02(state[0][i]) ^ MultiplyInGFBy03(state[1][i]) ^ state[2][i] ^ state[3][i]);
                    s1 = (byte) (state[0][i] ^ MultiplyInGFBy02(state[1][i]) ^ MultiplyInGFBy03(state[2][i]) ^ state[3][i]);
                    s2 = (byte) (state[0][i] ^ state[1][i] ^ MultiplyInGFBy02(state[2][i]) ^ MultiplyInGFBy03(state[3][i]));
                    s3 = (byte) (MultiplyInGFBy03(state[0][i]) ^ state[1][i] ^ state[2][i] ^ MultiplyInGFBy02(state[3][i]));
                }
                else
                {
                    s0 = (byte) (MultiplyInGFBy0E(state[0][i]) ^ MultiplyInGFBy0B(state[1][i]) ^ MultiplyInGFBy0D(state[2][i]) ^ MultiplyInGFBy09(state[3][i]));
                    s1 = (byte) (MultiplyInGFBy09(state[0][i]) ^ MultiplyInGFBy0E(state[1][i]) ^ MultiplyInGFBy0B(state[2][i]) ^ MultiplyInGFBy0D(state[3][i]));
                    s2 = (byte) (MultiplyInGFBy0D(state[0][i]) ^ MultiplyInGFBy09(state[1][i]) ^ MultiplyInGFBy0E(state[2][i]) ^ MultiplyInGFBy0B(state[3][i]));
                    s3 = (byte) (MultiplyInGFBy0B(state[0][i]) ^ MultiplyInGFBy0D(state[1][i]) ^ MultiplyInGFBy09(state[2][i]) ^ MultiplyInGFBy0E(state[3][i]));
                }

                state[0][i] = s0;
                state[1][i] = s1;
                state[2][i] = s2;
                state[3][i] = s3;
            }
            return state;
        }

        /// <summary>
        /// Shift rows part of algorith.
        /// </summary>
        /// <param name="state">State matrix.</param>
        /// <param name="invert">Direction: encoding or decoding.</param>
        /// <returns>Matrix with shifted rows.</returns>
        protected byte[][] ShiftRows(byte[][] state, bool invert = false)
        {
            int count = 1;

            if (!invert) // encryption
            {
                // from 1 because of 0 row of matrix doesn't shift!
                for (int i = 1; i < state.Length; i++)
                {
                    state[i] = LeftShift(state[i], count);
                    count++;
                }
            }
            else    // decryption
            {
                for (int i = 1; i < state.Length; i++)
                {
                    state[i] = RightShift(state[i], count);
                    count++;
                }
            }
            return state;
        }

        /// <summary>
        /// Generates key schedule table.
        /// </summary>
        /// <param name="key">Input 128 bit key.</param>
        /// <returns>Key Schedule.</returns>
        protected byte[][] KeyExpansion(byte[] key)
        {
            // Generating matrix.
            const int columnsNumber = (NumberOfColumns*(NumberOfRounds + 1));
            var keySchedule = new byte[4][];
            for (int i = 0; i < 4; i++)
            {
                keySchedule[i] = new byte[columnsNumber];
            }

            // first block is our key.
            for (int i = 0; i < keySchedule.Length; i++)
            {
                for (int j = 0; j < KeyLength; j++)
                {
                    keySchedule[i][j] = key[i + 4*j];
                }
            }

            byte s;

            for (int column = KeyLength; column < columnsNumber; column++)
            {
                if (column%KeyLength == 0)
                {
                    var temp = new byte[4];
                    for (int i = 1; i < 4; i++)
                    {
                        temp[i - 1] = keySchedule[i][column - 1];
                    }
                    temp[3] = keySchedule[0][column - 1];

                    for (int i = 0; i < temp.Length; i++)
                    {
                        var row = (byte) (temp[i]/0x10);
                        var col = (byte) (temp[i]%0x10);
                        byte sboxItem = SubstitutionBox[16*row + col];
                        temp[i] = sboxItem;
                    }

                    for (int i = 0; i < 4; i++)
                    {
                        s = (byte) (keySchedule[i][column - 4] ^ temp[i] ^ RCon[i][column/KeyLength - 1]);
                        keySchedule[i][column] = s;
                    }
                }
                else
                {
                    for (int i = 0; i < 4; i++)
                    {
                        s = (byte) (keySchedule[i][column - 4] ^ keySchedule[i][column - 1]);
                        keySchedule[i][column] = s;
                    }
                }
            }

            return keySchedule;
        }

        /// <summary>
        /// Xor operation of State and Key Schedule.
        /// </summary>
        /// <param name="state">Current state of data.</param>
        /// <param name="keySchedule">Key schedule.</param>
        /// <param name="round">Round number.</param>
        /// <returns>XOR(State, KeySchedule).</returns>
        protected byte[][] AddRoundKey(byte[][] state, byte[][] keySchedule, int round = 0)
        {
            for (int i = 0; i < KeyLength; i++)
            {
                for (int j = 0; j < NumberOfColumns; j++)
                {
                    state[j][i] ^= keySchedule[j][NumberOfColumns*round + i];
                }
            }
            
            return state;
        }

        /// <summary>
        /// Left shift of array on shiftLength bytes.
        /// </summary>
        /// <param name="array">Source array.</param>
        /// <param name="shiftLength">Shift length.</param>
        /// <returns>Shifted array.</returns>
        private byte[] LeftShift(byte[] array, int shiftLength)
        {
            var shiftedArray = new byte[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                if (i < shiftLength)
                {
                    shiftedArray[array.Length - (shiftLength - i)] = array[i];
                }
                else
                {
                    shiftedArray[i - shiftLength] = array[i];
                }
            }
            return shiftedArray;
        }

        /// <summary>
        /// Right shift of array on shiftLength bytes.
        /// </summary>
        /// <param name="array">Source array.</param>
        /// <param name="shiftLength">Shift length.</param>
        /// <returns>Shifted array.</returns>
        private byte[] RightShift(byte[] array, int shiftLength)
        {
            var shiftedArray = new byte[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                if (i + shiftLength < array.Length)
                {
                    shiftedArray[i + shiftLength] = array[i];
                }
                else
                {
                    shiftedArray[(i + shiftLength)%array.Length] = array[i];
                }
            }
            return shiftedArray;
        }

        /// <summary>
        /// Muliply by 02 in Galua space.
        /// </summary>
        /// <param name="number">Input byte.</param>
        /// <returns>Output byte.</returns>
        private byte MultiplyInGFBy02(byte number)
        {
            byte res = (number < 0x80) ? (byte)(number << 1) : (byte)((number << 1) ^ 0x1b);
            return (byte)(res % 0x100);
        }

        /// <summary>
        /// Multiply by 03 in Galua space.
        /// 0x03 * byte = (0x02 + 0x01) * byte = 0x02 * byte + byte.
        /// + is XOR.
        /// </summary>
        /// <param name="number">Input byte.</param>
        /// <returns>Output byte.</returns>
        private byte MultiplyInGFBy03(byte number)
        {
            return (byte) (MultiplyInGFBy02(number) ^ number);
        }

        private byte MultiplyInGFBy09(byte number)
        {
            return (byte) (MultiplyInGFBy02(MultiplyInGFBy02(MultiplyInGFBy02(number))) ^ number);
        }

        private byte MultiplyInGFBy0B(byte number)
        {
            return (byte)(MultiplyInGFBy02(MultiplyInGFBy02(MultiplyInGFBy02(number))) ^ MultiplyInGFBy02(number) ^ number);
        }

        private byte MultiplyInGFBy0D(byte number)
        {
            return (byte)(MultiplyInGFBy02(MultiplyInGFBy02(MultiplyInGFBy02(number))) ^ MultiplyInGFBy02(MultiplyInGFBy02(number)) ^ number);
        }

        private byte MultiplyInGFBy0E(byte number)
        {
            return (byte)(MultiplyInGFBy02(MultiplyInGFBy02(MultiplyInGFBy02(number))) ^ MultiplyInGFBy02(MultiplyInGFBy02(number)) ^ MultiplyInGFBy02(number));
        }
    }
}
