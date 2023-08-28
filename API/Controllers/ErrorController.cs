using API.Errors;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

/* you need this controller so you can send errors in program class pipeline configuration
// Configure the HTTP request pipeline errors handling.
            app.UseMiddleware<ExceptionMiddleware>();

            // error handling needs this
            app.UseStatusCodePagesWithReExecute("error/{0}");
*/

[Route("errors/{code}")]
[ApiExplorerSettings(IgnoreApi = true)]
public class ErrorController : BaseApiController
{
    public IActionResult Error(int code)
    {
        return new ObjectResult(new ApiResponse(code));
    }
}