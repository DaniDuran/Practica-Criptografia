using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ms_practica_criptografia.domain.contracts;
using ms_practica_criptografia.domain.services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace ms_practica_criptografia.unittes
{
    public class DataHasherShould
    {
        private readonly ITestOutputHelper _outputHelper;

        public DataHasherShould(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }
        private void BuildRequiredComponents(out DataHasher dataHasher)
        {
            var host = Host.CreateDefaultBuilder()
                           .ConfigureServices((services) => 
                           {
                               services.AddTransient<IDataHasher,DataHasher>();
                           }).Build();

            dataHasher = ActivatorUtilities.CreateInstance<DataHasher>(host.Services);
        }

        [Fact]
        public void generatedHash_OK() 
        {
            //args

            //Act
            BuildRequiredComponents(out DataHasher dataHasher);
            var result = dataHasher.Hash(Encoding.UTF8.GetBytes("Clave123"));
            var result_1 = dataHasher.Hash(Encoding.UTF8.GetBytes("Clave123"));
            
            //Assert
            Assert.NotEmpty(result);
            Assert.NotEmpty(result_1);
            _outputHelper.WriteLine(result);
            _outputHelper.WriteLine(result_1);
        }

        [Fact]
        public void verify() 
        {
            //args
            var hash = "AC353C31FE02CE5C027060A5E81A63FE343ABAC40948394DFF46CE69B887EBA20327452F4C152BF54800EE6B39ED683D92FB4360A6F5963A7F421B528DC6B180:41C23CD0074D2BC88C42EA5593E4C1E3:50000:SHA512";
            //Act
            BuildRequiredComponents(out DataHasher dataHasher);
            var result = dataHasher.Verify(Encoding.UTF8.GetBytes("Clave1234"), hash);
            var result_1 = dataHasher.Verify(Encoding.UTF8.GetBytes("Clave123"),hash);

            //Assert
            Assert.False(result);
            Assert.True(result_1);
            _outputHelper.WriteLine(result.ToString());
            _outputHelper.WriteLine(result_1.ToString());
        }
    }
}
