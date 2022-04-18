using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CodeRabbits.KaoList.Bookmark;

/// <summary>
/// Contains extension methods to <see cref="IServiceCollection"/> for configuring identity services.
/// </summary>
public static class BookmarkServiceCollectionExtensions
{
    /// <summary>
    /// Adds the default identity system configuration for the specified User and Role types.
    /// </summary>
    /// <typeparam name="TUser">The type representing a User in the system.</typeparam>
    /// <typeparam name="TBookmark">The type representing a Role in the system.</typeparam>
    /// <param name="services">The services available in the application.</param>
    /// <returns>An <see cref="BookmarkBuilder"/> for creating and configuring the bookmark system.</returns>
    public static BookmarkBuilder AddBookmark<TUser, TBookmark>(
        this IServiceCollection services)
        where TUser : IdentityUser, new()
        where TBookmark : UserBookmark, new()
    {
        // Services bookmark depends on
        services.AddOptions().AddLogging();

        // No interface for the error describer so we can add errors without rev'ing the interface
        services.TryAddScoped<BookmarkErrorDescriber>();
        services.TryAddScoped<BookmarkManager<TUser, TBookmark>>();

        return new BookmarkBuilder(typeof(TUser), typeof(TBookmark), services);
    }

}
