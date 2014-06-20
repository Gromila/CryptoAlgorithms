using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA.CourseWork.Crypto
{
    public abstract class DESCrypto
    {
        protected readonly byte[] InputPermutation =  { 
            58, 50, 42, 34, 26, 18, 10, 2,
            60, 52, 44, 36, 28, 20, 12, 4,
            62, 54, 46, 38, 30, 22, 14, 6,
            64, 56, 48, 40, 32, 24, 16, 8,
            57, 49, 41, 33, 25, 17, 9,  1,
            59, 51, 43, 35, 27, 19, 11, 3,
            61, 53, 45, 37, 29, 21, 13, 5,
            63, 55, 47, 39, 31, 23, 15, 7
        };

        protected readonly byte[] FinalPermutation = {
            40, 8, 48, 16, 56, 24, 64, 32,
            39, 7, 47, 15, 55, 23, 63, 31,
            38, 6, 46, 14, 54, 22, 62, 30,
            37, 5, 45, 13, 53, 21, 61, 29,
            36, 4, 44, 12, 52, 20, 60, 28,
            35, 3, 43, 11, 51, 19, 59, 27,
            34, 2, 42, 10, 50, 18, 58, 26,
            33, 1, 41, 9, 49, 17, 57, 25
        };

        protected readonly byte[] ExpansionPermutation = {
            32, 1,  2,  3,  4,  5,
            4,  5,  6,  7,  8,  9,
            8,  9,  10, 11, 12, 13,
            12, 13, 14, 15, 16, 17,
            16, 17, 18, 19, 20, 21,
            20, 21, 22, 23, 24, 25,
            24, 25, 26, 27, 28, 29,
            28, 29, 30, 31, 32, 1
        };

        protected readonly byte[][] SubstitutionBoxes = { 
            new byte[] {
                14, 4,  13, 1,  2,  15, 11, 8,  3,  10, 6,  12, 5,  9,  0,  7,
                0,  15, 7,  4,  14, 2,  13, 1,  10, 6,  12, 11, 9,  5,  3,  8,
                4,  1,  14, 8,  13, 6,  2,  11, 15, 12, 9,  7,  3,  10, 5,  0,
                15, 12, 8,  2,  4,  9,  1,  7,  5,  11, 3,  14, 10, 0,  6,  13
            }, 
            new byte[] {
                15, 1,  8,  14, 6,  11, 3,  4,  9,  7,  2,  13, 12, 0,  5,  10,
                3,  13, 4,  7,  15, 2,  8,  14, 12, 0,  1,  10, 6,  9,  11, 5,
                0,  14, 7,  11, 10, 4,  13, 1,  5,  8,  12, 6,  9,  3,  2,  15,
                13, 8,  10, 1,  3,  15, 4,  2,  11, 6,  7,  12, 0,  5,  14, 9
            }, 
            new byte[] {
                10, 0,  9,  14, 6,  3,  15, 5,  1,  13, 12, 7,  11, 4,  2,  8,
                13, 7,  0,  9,  3,  4,  6,  10, 2,  8,  5,  14, 12, 11, 15, 1,
                13, 6,  4,  9,  8,  15, 3,  0,  11, 1,  2,  12, 5,  10, 14, 7,
                1,  10, 13, 0,  6,  9,  8,  7,  4,  15, 14, 3,  11, 5,  2,  12
            },
            new byte[] {
                7,  13, 14, 3,  0,  6,  9,  10, 1,  2,  8,  5,  11, 12, 4,  15,
                13, 8,  11, 5,  6,  15, 0,  3,  4,  7,  2,  12, 1,  10, 14, 9,
                10, 6,  9,  0,  12, 11, 7,  13, 15, 1,  3,  14, 5,  2,  8,  4,
                3,  15, 0,  6,  10, 1,  13, 8,  9,  4,  5,  11, 12, 7,  2,  14
            },
            new byte[] {
                2,  12, 4,  1,  7,  10, 11, 6,  8,  5,  3,  15, 13, 0,  14, 9,
                14, 11, 2,  12, 4,  7,  13, 1,  5,  0,  15, 10, 3,  9,  8,  6,
                4,  2,  1,  11, 10, 13, 7,  8,  15, 9,  12, 5,  6,  3,  0,  14,
                11, 8,  12, 7,  1,  14, 2,  13, 6,  15, 0,  9,  10, 4,  5,  3
            }, 
            new byte[] {
                12, 1,  10, 15, 9,  2,  6,  8,  0,  13, 3,  4,  14, 7,  5,  11,
                10, 15, 4,  2,  7,  12, 9,  5,  6,  1,  13, 14, 0,  11, 3,  8,
                9,  14, 15, 5,  2,  8,  12, 3,  7,  0,  4,  10, 1,  13, 11, 6,
                4,  3,  2,  12, 9,  5,  15, 10, 11, 14, 1,  7,  6,  0,  8,  13
            }, 
            new byte[] {
                4,  11, 2,  14, 15, 0,  8,  13, 3,  12, 9,  7,  5,  10, 6,  1,
                13, 0,  11, 7,  4,  9,  1,  10, 14, 3,  5,  12, 2,  15, 8,  6,
                1,  4,  11, 13, 12, 3,  7,  14, 10, 15, 6,  8,  0,  5,  9,  2,
                6,  11, 13, 8,  1,  4,  10, 7,  9,  5,  0,  15, 14, 2,  3,  12
            }, 
            new byte[] {
                13, 2,  8,  4,  6,  15, 11, 1,  10, 9,  3,  14, 5,  0,  12, 7,
                1,  15, 13, 8,  10, 3,  7,  4,  12, 5,  6,  11, 0,  14, 9,  2,
                7,  11, 4,  1,  9,  12, 14, 2,  0,  6,  10, 13, 15, 3,  5,  8,
                2,  1,  14, 7,  4,  10, 8,  13, 15, 12, 9,  0,  3,  5,  6,  11
            } 
        };

        protected readonly byte[] Permutation = {
            16, 7,  20, 21, 29, 12, 28, 17,
            1,  15, 23, 26, 5,  18, 31, 10,
            2,  8,  24, 14, 32, 27, 3,  9,
            19, 13, 30, 6, 22, 11, 4,  25
        };

        protected readonly byte[] PC1Permutation = {
            57, 49, 41, 33, 25, 17, 9, 1,  58, 50, 42, 34, 26, 18,
            10, 2,  59, 51, 43, 35, 27, 19, 11, 3,  60, 52, 44, 36,
            63, 55, 47, 39, 31, 23, 15, 7,  62, 54, 46, 38, 30, 22,
            14, 6,  61, 53, 45, 37, 29, 21, 13, 5,  28, 20, 12, 4
        };

        protected readonly byte[] PC2Permutation = {
            14, 17, 11, 24, 1,  5, 3,  28, 15, 6,  21, 10,
            23, 19, 12, 4,  26, 8, 16, 7,  27, 20, 13, 2,
            41, 52, 31, 37, 47, 55, 30, 40, 51, 45, 33, 48,
            44, 49, 39, 56, 34, 53, 46, 42, 50, 36, 29, 32
        };

        protected readonly byte[] Rotations = {
            1, 1, 2, 2, 2, 2, 2, 2, 1, 2, 2, 2, 2, 2, 2, 1
        };

        protected byte[] Substitution6x4(byte[] input)
        {
            input = SplitBytes(input, 6); // 6-bit blocks.
            var output = new byte[input.Length/2];
            int leftHalfByte = 0;
            for (int i = 0; i < input.Length; i++)
            {
                byte value = input[i];
                int borders = 2*(value >> 7 & 0x0001) + (value >> 2 & 0x0001); // 1 and 6 bits
                int middle = value >> 3 & 0x000F; // 4 middle bits
                int halfByte = SubstitutionBoxes[i][16*borders + middle];
                if (i%2 == 0)
                {
                    leftHalfByte = halfByte;
                }
                else
                {
                    output[i/2] = (byte) (16*leftHalfByte + halfByte);
                }
            }
            return output;
        }

        protected byte[] SplitBytes(byte[] input, int length)
        {
            int numberOfBytes = (8*input.Length - 1)/length + 1;
            var output = new byte[numberOfBytes];
            for (int i = 0; i < numberOfBytes; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    int value = GetBit(input, length*i + j);
                    SetBit(output, 8*i + j, value);
                }
            }

            return output;
        }

        protected byte[][] GenerateSubkeys(byte[] key)
        {
            int activeKeySize = PC1Permutation.Length;
            int numberOfSubkeys = Rotations.Length;

            byte[] activeKey = SelectBits(key, PC1Permutation);
            int halfKeySize = activeKeySize/2;

            byte[] left = SelectBits(activeKey, 0, halfKeySize);
            byte[] right = SelectBits(activeKey, halfKeySize, halfKeySize);

            var subkeys = new byte[numberOfSubkeys][];
            for (int i = 0; i < numberOfSubkeys; i++)
            {
                left = RotateLeft(left, halfKeySize, Rotations[i]);
                right = RotateLeft(right, halfKeySize, Rotations[i]);
                byte[] value = ConcatenateBits(left, halfKeySize, right, halfKeySize);
                subkeys[i] = SelectBits(value, PC2Permutation);
            }

            return subkeys;
        }

        protected byte[] RotateLeft(byte[] input, int length, int step)
        {
            int numberOfBytes = (length - 1)/8 + 1;
            var output = new byte[numberOfBytes];
            for (int i = 0; i < length; i++)
            {
                int value = GetBit(input, (i + step)%length);
                SetBit(output, i, value);
            }

            return output;
        }

        protected byte[] ConcatenateBits(byte[] a, int aLength, byte[] b, int bLength)
        {
            int numberOfBytes = (aLength + bLength - 1)/8 + 1;
            var output = new byte[numberOfBytes];
            int j = 0;
            for (int i = 0; i < aLength; i++)
            {
                int value = GetBit(a, i);
                SetBit(output, j, value);
                j++;
            }

            for (int i = 0; i < bLength; i++)
            {
                int value = GetBit(b, i);
                SetBit(output, j, value);
                j++;
            }

            return output;
        }

        protected byte[] XORBytes(byte[] a, byte[] b)
        {
            var output = new byte[a.Length];
            for (int i = 0; i < a.Length; i++)
            {
                output[i] = (byte) (a[i] ^ b[i]);
            }

            return output;
        }

        protected byte[] SelectBits(byte[] input, int position, int length)
        {
            int numberOfBytes = (length - 1)/8 + 1;
            var output = new byte[numberOfBytes];
            for (int i = 0; i < length; i++)
            {
                int value = GetBit(input, position + i);
                SetBit(output, i, value);
            }

            return output;
        }

        protected byte[] SelectBits(byte[] input, byte[] map)
        {
            int numberOfBytes = (map.Length - 1)/8 + 1;
            var output = new byte[numberOfBytes];
            for (int i = 0; i < map.Length; i++)
            {
                int value = GetBit(input, map[i] - 1);
                SetBit(output, i, value);
            }

            return output;
        }

        protected int GetBit(byte[] data, int position)
        {
            int positionByte = position/8;
            int positionBit = position%8;
            byte value = data[positionByte];
            return value >> (8 - (positionBit + 1)) & 0x0001;
        }

        protected void SetBit(byte[] data, int position, int value)
        {
            int positionByte = position/8;
            int positionBit = position%8;
            byte old = data[positionByte];
            old = (byte) (((0xFF7F >> positionBit) & old) & 0x00FF);
            data[positionByte] = (byte)((value << (8 - (positionBit + 1))) | old);
        }
    }
}
