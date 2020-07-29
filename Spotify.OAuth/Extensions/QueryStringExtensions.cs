using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;

namespace Spotify.OAuth.Extensions
{
    public static class QueryStringExtensions
    {
        public static void AddQueryString<T>(this IDictionary<string, string> querystring, T defaultValue,
            AuthenticationProperties properties, string keyName, Func<T, string> stringFormatter = null)
        {
            var value = string.Empty;
            T getParameter = default;

            //Difference between this and going to the actual collection items through
            //properties.Items
            var parameterValue = properties.GetParameter<T>(keyName);

            if (parameterValue != null)
            {
                getParameter = parameterValue;
            }

            value = stringFormatter != null ?
                stringFormatter(getParameter) :
                getParameter?.ToString();

            if (!string.IsNullOrWhiteSpace(value))
            {
                querystring[keyName] = value;
            }
        }

        public static void AddQueryString<T>(this IDictionary<string, string> queryStrings,
            AuthenticationProperties properties,
            string keyName,
            T defaultValue = default)
        {
            queryStrings.AddQueryString(properties, keyName, defaultValue);
        }
    }
}
