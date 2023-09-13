// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.

namespace CodeRabbits.KaoList.Web
{
    public static class HttpContextExtenstion
    {
        public static string GenerateToken()
        {
            return Guid.NewGuid().ToString();
        }

        public static string GetIdentityToken(this HttpContext context)
        {
            var token = context.Request.Cookies["IdentityToken"];
            if (token == null)
            {
                token = GenerateToken();
                var cookieOptions = new CookieOptions
                {
                    // Set the secure flag, which Chrome's changes will require for SameSite none.
                    // Note this will also require you to be running on HTTPS.
                    Secure = true,

                    // Set the cookie to HTTP only which is good practice unless you really do need
                    // to access it client side in scripts.
                    HttpOnly = true,

                    // Add the SameSite attribute, this will emit the attribute with a value of none.
                    SameSite = SameSiteMode.None

                    // The client should follow its default cookie policy.
                    // SameSite = SameSiteMode.Unspecified
                };

                context.Response.Cookies.Append("IdentityToken", token, cookieOptions);

            }

            return token;
        }
    }
}
