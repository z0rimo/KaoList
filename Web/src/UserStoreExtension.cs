// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.

using CodeRabbits.KaoList.Identity;
using Microsoft.AspNetCore.Identity;

namespace CodeRabbits.KaoList.Web
{
    public interface UserStoreExtension : IUserStore<KaoListUser>
    {
        Task<string> GetNicknameAsync(KaoListUser user, CancellationToken cancellationToken);

        Task SetNicknameAsync(KaoListUser user, string? nickname, CancellationToken cancellationToken);
    }
}
