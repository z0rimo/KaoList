using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

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

            var bookmarkBaseType = FindGenericBaseType(bookmakrType, typeof(UserBookmark<,>));
            if (bookmarkBaseType == null)
            {
                throw new InvalidOperationException(Resources.NotUserBookmak);
            }

            var userKeyType = bookmarkBaseType.GenericTypeArguments[0];
            if (userKeyType != bookmarkBaseType.GenericTypeArguments[0])
            {
                throw new InvalidOperationException(Resources.NotMatchUserKeyType);
            }

            var songKeyType = bookmarkBaseType.GenericTypeArguments[1];
            Type bookmarkStoreType = typeof(BookmarkStore<,,,,>).MakeGenericType(
                userType, bookmakrType, contextType, userKeyType, songKeyType);

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
