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
            GcmEncrypt gcmEncrypt = new GcmEncrypt("\"server=localhost,1433; user id= admin; password=admin1234*; database= system-lab;timeout=300;TrustServerCertificate=True;\"", key);

            //Act
            var result_1 = gcmEncrypt.Encrypt();
            var result_2 = gcmEncrypt.Encrypt();


            //Assert
            Assert.NotEmpty(result_1);
            Assert.True(result_1 != result_2);
            _outputHelper.WriteLine($"{Convert.ToBase64String(result_1)}");
            _outputHelper.WriteLine($"{Convert.ToBase64String(result_2)}");

        }
    }
}
