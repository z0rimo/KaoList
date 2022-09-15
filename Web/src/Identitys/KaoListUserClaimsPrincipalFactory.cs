// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.

using CodeRabbits.KaoList.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace CodeRabbits.KaoList.Web.Identitys
{
    public class KaoListUserClaimsPrincipalFactory<T> : UserClaimsPrincipalFactory<T> where T : KaoListUser
    {
        public KaoListUserClaimsPrincipalFactory(UserManager<T> userManager, IOptions<IdentityOptions> optionsAccessor) : base(userManager, optionsAccessor)
        {
        }

        public override async Task<ClaimsPrincipal> CreateAsync(T user)
        {
            var claimsPrincipal = await base.CreateAsync(user);
            if (user.NickName is not null)
            {
                claimsPrincipal.AddIdentity(new ClaimsIdentity(new[] { new Claim("nickname", user.NickName) }));
            }

            return claimsPrincipal;
        }
    }
}
