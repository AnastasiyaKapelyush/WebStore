using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace WebStore.Infrastucture.Middleware
{
    public class TestMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<TestMiddleware> _logger;

        public TestMiddleware(RequestDelegate next, ILogger<TestMiddleware> Logger)
        {
            _next = next;
            _logger = Logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            //Предобработка

            var processing = _next(context); //Запуск следующего слоя пром ПО

            //Паралелльная обработка

            await processing; // Ожидание завершения обработки следующей частью конвейера

            //Постобработка данных
        }
    }
}
