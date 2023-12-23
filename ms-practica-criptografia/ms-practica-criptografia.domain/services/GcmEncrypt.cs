using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ms_practica_criptografia.domain.services
{
    public class GcmEncrypt : GcmBase
    {
        public GcmEncrypt(byte[] data, string keyBase64) : base(data, keyBase64) { }

        public GcmEncrypt(string plainData, string keyBase64) : base(Encoding.UTF8.GetBytes(plainData), keyBase64) { }

        public byte[] Encrypt() 
        {
            using AesGcm aesGcm = new AesGcm(_key!);
            int nonceSize = AesGcm.NonceByteSizes.MaxSize;
            int tagSize = AesGcm.TagByteSizes.MaxSize;
            int cipherSize = _data!.Length;

            // copiar los datos a un solo array, para facilitar operación
            int encryptedDataLength = 4 + nonceSize + 4 + tagSize + cipherSize;
            Span<byte> encryptedData = encryptedDataLength < 1024 ? stackalloc byte[encryptedDataLength] : new byte[encryptedDataLength].AsSpan();
            // copiar parámetros
            BinaryPrimitives.WriteInt32LittleEndian(encryptedData.Slice(0, 4), nonceSize);
            BinaryPrimitives.WriteInt32LittleEndian(encryptedData.Slice(4 + nonceSize, 4), tagSize);
            var nonce = encryptedData.Slice(4, nonceSize);
            var tag = encryptedData.Slice(4 + nonceSize + 4, tagSize);
            var cipherBytes = encryptedData.Slice(4 + nonceSize + 4 + tagSize, cipherSize);

            // generar secuencia aleatoria segura
            RandomNumberGenerator.Fill(nonce);

            // encriptar
            aesGcm.Encrypt(nonce, _data.AsSpan(), cipherBytes, tag);

            return encryptedData.ToArray();
        }
        

    }
}
