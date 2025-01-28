using Microsoft.AspNetCore.Mvc;

namespace HelpDeskSystem.Helpers;

public static class MenuHelper
{
    public static string IsActive(this IUrlHelper urlHelper, string controller, string action)
    {
        var routeData = urlHelper.ActionContext.RouteData;
        var routeController = routeData.Values["controller"]?.ToString();
        var routeAction = routeData.Values["action"]?.ToString();

        return (controller == routeController && action == routeAction) ? "active" : "";
    }
}