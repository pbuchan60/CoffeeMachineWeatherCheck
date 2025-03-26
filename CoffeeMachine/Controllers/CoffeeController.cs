using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices.JavaScript;
using CoffeeMachine.Middleware;
using CoffeeMachine.Models;

namespace CoffeeMachine.Controllers
{
    [ApiController]
    [Route("api/brew-coffee")]
    public class CoffeeController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            Task<double> task = WeatherCheck.Temperature();
            double temperature = await task;
            int count = RequestCountMiddleware.GetEndpointRequestCount("/api/brew-coffee");
            if (count == 5)
            {
                return StatusCode(503, "Service Unavailable");
            }
            else
            {
                if (DateTime.Now.Month == 4 && DateTime.Now.Day == 1)
                {
                    return StatusCode(418, "I'm a teapot");
                }
                else
                {
                    BrewDto brew = new BrewDto();
                    if (temperature>30)
                    {
                        brew.Message = "Your refreshing iced coffee is ready";
                    }
                    else
                    {
                        brew.Message = "Your piping hot coffee is ready";
                    }                        
                    brew.Prepared = DateTime.Now;
                    return new JsonResult(brew);
                }
            } 
        }
    }
}
