using System.Net;

namespace PavelNikolaevKt_31_23.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                // Логируем ошибку
                _logger.LogError(exception, "Произошло необработанное исключение");

                var httpResponse = context.Response;
                httpResponse.ContentType = "application/json";

                var responseModel = new ResponseModel<object>
                {
                    Succeeded = false,
                    Message = exception.Message
                };

                // Определяем код ответа (по умолчанию 500)
                httpResponse.StatusCode = (int)HttpStatusCode.InternalServerError;

                // Записываем внутреннюю ошибку, если она есть
                responseModel.Errors = new List<string>
                {
                    exception.InnerException?.Message ?? exception.Message
                };

                await httpResponse.WriteAsJsonAsync(responseModel);
            }
        }

        // Модель ответа, которая будет возвращаться клиенту при ошибке
        public class ResponseModel<T>
        {
            public bool Succeeded { get; set; }
            public string? Message { get; set; }
            public List<string>? Errors { get; set; }
            public T? Data { get; set; }

            public ResponseModel() { }

            public ResponseModel(T data, string? message = null)
            {
                Succeeded = true;
                Message = message;
                Data = data;
            }

            public ResponseModel(string message)
            {
                Succeeded = true;
                Message = message;
            }
        }
    }
}
