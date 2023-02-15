using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace TodoAssistant.API.Extensions;

public static class ObjectExtensions
{
    public static IActionResult ToActionResult(this object result)
    {
        return new ObjectResult(result)
        {
            StatusCode = (int)HttpStatusCode.OK
        };
    }
}
