using System.Net;
using System.Text.Json;

namespace ContactGate.WebAPI.Middleware;

//
// Глобальный обработчик исключений. 
// Перехватывает ошибки и возвращает клиенту понятный JSON ответ.
//

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }
// 
// Попытка выполнения запроса и перехват возникающих исключений
// 
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
// 
// Формирование HTTP-ответа в зависимости от типа ошибки
// 
    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        // Маппинг типов исключений на стандартные HTTP статус-коды
        var code = exception switch
        {
            KeyNotFoundException => HttpStatusCode.NotFound,
            InvalidOperationException => HttpStatusCode.BadRequest,
            FluentValidation.ValidationException => HttpStatusCode.BadRequest, 
            _ => HttpStatusCode.InternalServerError
        };
// 
// Сериализация сообщения об ошибке в единый формат
// 
        var result = JsonSerializer.Serialize(new { error = exception.Message });
        
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;

        return context.Response.WriteAsync(result);
    }
}