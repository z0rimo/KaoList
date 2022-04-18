using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeRabbits.KaoList.Bookmark
{
    public static class BookmarkEntityFrameworkBuilderExtensions
    {
        public static BookmarkBuilder AddEntityFrameworkStores<TContext>(this BookmarkBuilder builder)
            where TContext : DbContext
        {
            AddStores(builder.Services, builder.UserType, builder.BookmarkType, typeof(TContext));
            return builder;
        }

        private static void AddStores(IServiceCollection services, Type userType, Type bookmakrType, Type contextType)
        {
            var identityUserType = FindGenericBaseType(userType, typeof(IdentityUser<>));
            if (identityUserType == null)
            {
                throw new InvalidOperationException(Resources.NotIdentityUser);
            }

            var bookmarkType = FindGenericBaseType(bookmakrType, typeof(UserBookmark<,>));
            if (bookmarkType == null)
            {
                throw new InvalidOperationException(Resources.NotIdentityUser);
            }

            var userKeyType = bookmarkType.GenericTypeArguments[0];
            var songKeyType = bookmarkType.GenericTypeArguments[1];
            Type bookmarkStoreType = typeof(BookmarkStore<,,,,>).MakeGenericType(
                userType, bookmakrType, contextType, userKeyType, songKeyType);

            services.TryAddScoped(contextType);
            services.TryAddScoped(typeof(IBookmarkStore<,>).MakeGenericType(userType, bookmakrType), bookmarkStoreType);
        }

        private static Type? FindGenericBaseType(Type currentType, Type genericBaseType)
        {
            Type? type = currentType;
            while (type != null)
            {
                var genericType = type.IsGenericType ? type.GetGenericTypeDefinition() : null;
                if (genericType != null && genericType == genericBaseType)
                {
                    return type;
                }
                type = type.BaseType;
            }

            return null;
        }
    }
}
