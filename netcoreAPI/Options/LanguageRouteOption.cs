namespace netcoreAPI.Options
{
    //if we want to add support localization in route
    public class LanguageRouteOption : IRouteConstraint
    {
        public bool Match(HttpContext? httpContext, IRouter? route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {

            if (!values.ContainsKey("culture"))
                return false;

            var culture = values["culture"]?.ToString();
            return culture == "en-US" || culture == "es-ES";
        }
    }
}
