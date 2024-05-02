using System.Text;

namespace Middleware.Models
{
    public class LogginMiddleware
    {
        private readonly RequestDelegate _next;

        public LogginMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var request = context.Request;

            var schema = request.Scheme;
            var host = request.Host;
            var path = request.Path;
            var queryString = request.QueryString;

            string requestBody;

            //using (var reader = new StreamReader(request.Body, Encoding.UTF8, true, 1024, true))
            //{
            //    requestBody = await reader.ReadToEndAsync();
            //}

            var originalRequestBody = request.Body;
            using (var memStream = new MemoryStream())
            {
                await originalRequestBody.CopyToAsync(memStream);
                memStream.Seek(0, SeekOrigin.Begin);

                using (var reader = new StreamReader(memStream, Encoding.UTF8, true, 1024, true))
                {
                    requestBody = await reader.ReadToEndAsync();
                }
            }

            var logMessage = $"{DateTime.Now} - Schema: {schema}, Host: {host}, Path: {path}, QueryString: {queryString}, Request Body: {requestBody}";

            var logFilePath = "request_logs.txt";
            await File.AppendAllTextAsync(logFilePath, logMessage + Environment.NewLine);

            await _next(context);
        }
    }
}

