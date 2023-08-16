// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.

using CodeRabbits.KaoList.Data;
using Microsoft.AspNetCore.Identity;

namespace CodeRabbits.KaoList.Web.Datas
{
    public static class SeedData
    {
        public static void Initialize(KaoListDataContext context)
        {
            var adminRole = context.Roles.FirstOrDefault(r => r.Name == "Administrator");

            if (adminRole == null)
            {
                adminRole = new IdentityRole("Administrator");
                context.Roles.Add(adminRole);
                context.SaveChanges();
            }

            var user = context.Users.FirstOrDefault(u => u.Email == "q@q.q");
            if (user != null && !context.UserRoles.Any(ur => ur.UserId == user.Id && ur.RoleId == adminRole.Id))
            {
                var adminUserRole = new IdentityUserRole<string> { RoleId = adminRole.Id, UserId = user.Id };
                context.UserRoles.Add(adminUserRole);
                context.SaveChanges();
            }
        }
    }
}
