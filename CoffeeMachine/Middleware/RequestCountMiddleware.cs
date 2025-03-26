namespace CoffeeMachine.Middleware
{
    public class RequestCountMiddleware
    {
        private static Dictionary<string, int> _endpointCounts = new Dictionary<string, int>();

        private readonly RequestDelegate _next;

        public RequestCountMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var endpoint = context.Request.Path.ToString();

            if (_endpointCounts.ContainsKey(endpoint) && _endpointCounts[endpoint] != 5)
            {
                _endpointCounts[endpoint]++;
            }
            else
            {
                _endpointCounts[endpoint] = 1;
            }

            await _next(context);
        }

        // Static method to get the request count for an endpoint
        public static int GetEndpointRequestCount(string endpoint) =>
            _endpointCounts.ContainsKey(endpoint) ? _endpointCounts[endpoint] : 0;
    }
}
