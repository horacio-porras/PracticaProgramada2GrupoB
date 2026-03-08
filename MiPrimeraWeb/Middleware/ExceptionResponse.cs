using System.Net;

namespace PracticaProgramada2.Middleware
{
    public record ExceptionResponse(HttpStatusCode statusCode, string description);

}
