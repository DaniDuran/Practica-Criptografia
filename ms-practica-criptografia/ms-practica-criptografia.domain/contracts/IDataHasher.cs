using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ms_practica_criptografia.domain.contracts
{
    public interface IDataHasher
    {
        string Hash(byte[] data);
        bool Verify(byte[] data, string hashData);

    }
}
