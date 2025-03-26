using CoffeeMachine.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CoffeeMachineXunitTest.IntegrationTests
{
    public class CoffeeMachineIntegrationTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private WebApplicationFactory<Program> _factory;

        public CoffeeMachineIntegrationTest(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Test1()
        {
            for (int i = 0; i < 5; i++)
            {
                var client = _factory.CreateClient();
                var result = await client.GetAsync("/api/brew-coffee");
                if (i == 4) // 5th request
                {
                    Assert.Equal("ServiceUnavailable", result.StatusCode.ToString());
                }
                else
                {
                    Assert.NotNull(result);
                    var statusCode = result.StatusCode;
                    var content = await result.Content.ReadAsStringAsync();
                    Assert.NotEmpty(content);
                    JObject resultJson = JObject.Parse(content);
                    var message = resultJson["message"].Value<string>();
                    var prepared = resultJson["prepared"].Value<string>();
                    Assert.Equal("Your piping hot coffee is ready", message);
                }
            }
        }
    }
}
