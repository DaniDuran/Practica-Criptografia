using ms_practica_criptografia.domain.helper;
using ms_practica_criptografia.domain.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace ms_practica_criptografia.unittes
{
    public class GcmEncryptShould
    {

        private readonly ITestOutputHelper _outputHelper;

        public GcmEncryptShould(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }

        [Fact]
        public void Encrypt() 
        {
            //Args
            var key = ToolKit.GetKey();
            GcmEncrypt gcmEncrypt = new GcmEncrypt("server=localhost,1433; user id= admin; password=admin1234*; database= system-lab;timeout=300;TrustServerCertificate=True;", key);

            //Act
            var result_1 = gcmEncrypt.Encrypt();
            var result_2 = gcmEncrypt.Encrypt();

            //Assert
            Assert.NotEmpty(result_1);
            Assert.True(result_1 != result_2);
            _outputHelper.WriteLine($"{Convert.ToBase64String(result_1)}");
            _outputHelper.WriteLine($"{Convert.ToBase64String(result_2)}");

        }

        [Fact]
        public void Decrypt()
        {
            //Args
            var key = ToolKit.GetKey();

            GcmEncrypt gcmEncrypt = new GcmEncrypt("server=localhost,1433; user id= admin; password=admin1234*; database= system-lab;timeout=300;TrustServerCertificate=True;", key);
            
            var encrypt_1 = gcmEncrypt.Encrypt();
            var encrypt_2 = gcmEncrypt.Encrypt();

            //Act

            GcmDecrypt gcmDecrypt_1 = new GcmDecrypt(encrypt_1, key);
            GcmDecrypt gcmDecrypt_2 = new GcmDecrypt(encrypt_2, key);

            var result_1 = gcmDecrypt_1.Decript();
            var result_2 = gcmDecrypt_2.Decript();
            //Assert
            Assert.NotEmpty(result_1);
            Assert.True(result_1.SequenceEqual(result_2));
            _outputHelper.WriteLine($"{Convert.ToBase64String(encrypt_1)}");
            _outputHelper.WriteLine($"{Encoding.UTF8.GetString(result_1)}");

            _outputHelper.WriteLine($"{Convert.ToBase64String(encrypt_2)}");
            _outputHelper.WriteLine($"{Encoding.UTF8.GetString(result_2)}");

        }

    }
}
