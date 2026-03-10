using System.Net;

namespace newZealandWalksAPI.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        #region Fields
        private readonly ILogger _logger;
        private readonly RequestDelegate _next;
        #endregion

        #region Constructors
        public ExceptionHandlerMiddleware(
            ILogger<ExceptionHandlerMiddleware> logger,
            RequestDelegate next
            )
        {
            _logger = logger;
            _next = next;
        }
        #endregion

        #region Methods
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid();

                // Log this exception
                _logger.LogError(exception: ex, message: $"{errorId} : {ex.Message}");

                // Return a custom error response
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var error = new
                {
                    Id = errorId,
                    ErrorMessage = "Something went Wrong. We are currently looking into resolving this."
                };

                await context.Response.WriteAsJsonAsync(error);
            }
        }
        #endregion
    }
}
