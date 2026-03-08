namespace PracticaProgramada2.Middleware
{
    public class MiddlewareGlobalExceptionHandler
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<MiddlewareGlobalExceptionHandler> _logger;


        public MiddlewareGlobalExceptionHandler(RequestDelegate next, ILogger<MiddlewareGlobalExceptionHandler> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context) 
        {

            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
      
        }


        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            _logger.LogError(ex, "Ocurrió una excepción no controlada.");

            ExceptionResponse response = ex switch
            {
                NotImplementedException => new ExceptionResponse(System.Net.HttpStatusCode.BadRequest, "Funcionalidad no implementada."),
                ApplicationException => new ExceptionResponse(System.Net.HttpStatusCode.BadRequest, "Error en la aplicación"),
                _ => new ExceptionResponse(System.Net.HttpStatusCode.InternalServerError, "Ocurrió un error inesperado.")   
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)response.statusCode;
            await context.Response.WriteAsJsonAsync(response);

        }
                
    }
}
