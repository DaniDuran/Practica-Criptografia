using ms_practica_criptografia.domain.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ms_practica_criptografia.domain.services
{
    public abstract class GcmBase
    {
        protected byte[]? _data;
        protected byte[]? _key;

        private GcmBase()
        {
                
        }

        protected GcmBase(byte[] data,string keyBase64)
        {
            if (data == null || keyBase64 == null)
                throw new ArgumentException("No se permiten valores nulos");


            _key = Convert.FromBase64String(keyBase64);

            if (_key.Length != Constantes.KeySizeBytes)
                throw new ArgumentException("Tamaño de llave no valido");

            _data = data;
        }
    }
}
