using Microsoft.Extensions.DependencyInjection;

namespace CodeRabbits.KaoList.Bookmark;

/// <summary>
/// Helper functions for configuring bookmark services.
/// </summary>
public class BookmarkBuilder
{
    public BookmarkBuilder(Type user, Type bookmark, IServiceCollection services)
    {
        UserType = user;
        BookmarkType = bookmark;
        Services = services;
    }

    /// <summary>
    /// Gets the <see cref="Type"/> used for users.
    /// </summary>
    /// <value>
    /// The <see cref="Type"/> used for users.
    /// </value>
    public Type UserType { get; }

    /// <summary>
    /// Gets the <see cref="Type"/> used for bookmarks.
    /// </summary>
    /// <value>
    /// The <see cref="Type"/> used for bookmarks.
    /// </value>
    public Type BookmarkType { get; private set; }

    /// <summary>
    /// Gets the <see cref="IServiceCollection"/> services are attached to.
    /// </summary>
    /// <value>
    /// The <see cref="IServiceCollection"/> services are attached to.
    /// </value>
    public IServiceCollection Services { get; }
}
