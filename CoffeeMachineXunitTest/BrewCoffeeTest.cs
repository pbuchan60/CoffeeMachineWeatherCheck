using CoffeeMachine.Controllers;
using CoffeeMachine.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NuGet.Frameworks;
using System.Text.Json;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace CoffeeMachineXunitTest
{
    public class BrewCoffeeTest
    {
        [Fact]
        public async void CoffeeTest1()
        {
            var coffeeController = new CoffeeController();
            ActionResult result = await coffeeController.Index();

            if (result is JsonResult jsonResult)
            {
                try
                {
                    BrewDto brewResult = jsonResult.Value as BrewDto;
                    Assert.Equal("Your piping hot coffee is ready", brewResult.Message);
                    Assert.InRange(brewResult.Prepared, DateTime.Now.AddSeconds(-1), DateTime.Now);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }
    }
}

