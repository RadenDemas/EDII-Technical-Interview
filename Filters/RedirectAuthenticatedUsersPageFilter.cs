using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace EDIITechincalInterview.Filters
{
    public class RedirectAuthenticatedUsersPageFilter : IPageFilter
    {
        public void OnPageHandlerSelected(PageHandlerSelectedContext context)
        {
        }

        public void OnPageHandlerExecuting(PageHandlerExecutingContext context)
        {
            if (context.HttpContext.User.Identity != null && context.HttpContext.User.Identity.IsAuthenticated)
            {
                var path = context.ActionDescriptor.ViewEnginePath;
                var area = context.RouteData.Values["area"]?.ToString();
                
                if (area == "Identity" && path != null && path.StartsWith("/Account/", StringComparison.OrdinalIgnoreCase))
                {
                    if (!path.StartsWith("/Account/Logout", StringComparison.OrdinalIgnoreCase) &&
                        !path.StartsWith("/Account/Manage", StringComparison.OrdinalIgnoreCase))
                    {
                        if (context.HttpContext.User.IsInRole("Admin"))
                        {
                            context.Result = new RedirectToActionResult("Index", "Dashboard", new { area = "Admin" });
                        }
                        else if (context.HttpContext.User.IsInRole("User"))
                        {
                            context.Result = new RedirectToActionResult("Index", "Dashboard", new { area = "User" });
                        }
                        else
                        {
                            context.Result = new RedirectResult("/");
                        }
                    }
                }
            }
        }

        public void OnPageHandlerExecuted(PageHandlerExecutedContext context)
        {
        }
    }
}
