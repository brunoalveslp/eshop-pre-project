namespace API.Errors;

public class ApiResponse
{
    public int StatusCode { get; set; }
    public string Message { get; set; }

    public ApiResponse(int statusCode, string message = null)
    {
        StatusCode = statusCode;
        Message = message ?? GetDefaultMessageForStatusCode(StatusCode);
    }

    private string GetDefaultMessageForStatusCode(int statusCode)
    {
        return StatusCode switch
        {
            400 => "A bad request, you have made",
            401 => "Authorized, you are not",
            404 => "Resource not found, it was not",
            500 => "Errors are the path to the dark side. Errors leads to Anger. Anger Leads to hate. Hate leads to carrer change",
            _ => null
        };
    }

}
