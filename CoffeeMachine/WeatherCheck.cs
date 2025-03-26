using CoffeeMachine.Models;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System.Buffers.Text;

namespace CoffeeMachine
{
    public class WeatherCheck
    {
        public static async Task<double> Temperature()
        {
            using (var client = new HttpClient())
            {
                //Using Melbourne longitude and latitude
                var url = $"https://api.openweathermap.org/data/2.5/weather?lat=37.48&lon=144.57&appid=8847f557a047c26f1236a4bca765efa8&units=metric"; // "units=metric" to get temperature in Celsius
                try
                {
                    var response = await client.GetFromJsonAsync<WeatherData>(url);
                    return response.main.temp;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    return 0;
                }
            }
        }
    }
}
