using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ms_practica_criptografia.domain.services
{
    public class GcmDecrypt : GcmBase
    {
        public GcmDecrypt(byte[] data, string keyBase64) : base(data, keyBase64) { }

        public GcmDecrypt(string dataBase64, string keyBase64) : base(Convert.FromBase64String(dataBase64), keyBase64) { }

        public byte[] Decript() 
        {
            using AesGcm aesGcm = new AesGcm(_key!);
            Span <byte> encryptedData = _data.AsSpan();

            // extraer tamaños de parámetros
            int nonceSize = BinaryPrimitives.ReadInt32LittleEndian(encryptedData.Slice(0, 4));
            int tagSize = BinaryPrimitives.ReadInt32LittleEndian(encryptedData.Slice(4 + nonceSize, 4));
            int cipherSize = encryptedData.Length - 4 - nonceSize - 4 - tagSize;

            // extraer parámetros
            var nonce = encryptedData.Slice(4, nonceSize);
            var tag = encryptedData.Slice(4 + nonceSize + 4, tagSize);
            var cipherBytes = encryptedData.Slice(4 + nonceSize + 4 + tagSize, cipherSize);

            // decriptar
            Span<byte> plainBytes = cipherSize < 1024 ? stackalloc byte[cipherSize] : new byte[cipherSize];
            aesGcm.Decrypt(nonce, cipherBytes, tag, plainBytes);

            return plainBytes.ToArray();
        }
    }
}
