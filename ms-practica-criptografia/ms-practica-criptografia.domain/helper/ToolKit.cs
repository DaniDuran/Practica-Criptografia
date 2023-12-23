using ms_practica_criptografia.domain.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ms_practica_criptografia.domain.helper
{
    public static class ToolKit
    {

        public static string GetKey() 
        {
            using Aes aes = Aes.Create();
            aes.KeySize = Constantes.KeySizeBytes * 8;//Tamaño en bits
            return Convert.ToBase64String(aes.Key);
        }


    }
}
