using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA.CourseWork.Crypto.Interfaces
{
    public interface IDecryptable
    {
        String Decode(String data, String key, bool isParallel = false);

        byte[] Decode(byte[] data, String key, bool isParallel = false);

        byte[] Decode(byte[] data, byte[] key, bool isParallel = false);
    }
}
