using Microsoft.AspNetCore.Mvc;

namespace OnSugarAPI.Helpers;

public class ResponseHelper
{
	public static JsonResult Success(object content)
	{
        var response = new Dictionary<string, object>
        {
            { "status", "success" },
            { "content", content }
        };

        return new JsonResult(response);
	}

	public static JsonResult Error(int errorCode, string? description)
	{
        var response = new Dictionary<string, object>
        {
            { "status", "error" },
            { "content", new Dictionary<string, object>
                {
                    { "code", errorCode },
                    { "description", description ?? "Unknown error." }
                }
            },
        };

        return new JsonResult(response);
	}
}