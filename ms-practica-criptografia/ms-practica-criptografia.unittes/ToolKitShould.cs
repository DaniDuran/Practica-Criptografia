using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ms_practica_criptografia.domain.helper;
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
    public class ToolKitShould
    {

        private readonly ITestOutputHelper _outputHelper;

        public ToolKitShould(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }

        [Fact]
        public void GetKey()
        {
            //Args

            //Act
            var result = ToolKit.GetKey();
            var result2 = ToolKit.GetKey();

            //Assert
            Assert.NotEmpty(result);
            Assert.True(result!=result2);
            _outputHelper.WriteLine(result);
            _outputHelper.WriteLine(result2);
        }
    }
}
