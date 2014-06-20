using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA.CourseWork.Crypto.Interfaces
{
    public interface IEncryptable
    {
        String Encode(String data, String key, bool isParallel = false);

        byte[] Encode(byte[] data, String key, bool isParallel = false);

        byte[] Encode(byte[] data, byte[] key, bool isParallel = false);
    }
}
