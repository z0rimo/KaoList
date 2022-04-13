// (c) 2022 CodeRabbits
// This code is licensed under MIT license (see LICENSE.txt for details).

namespace CodeRabbits.KaoList.Bookmark;

/// <summary>
/// Provides an abstraction for querying users in a Bookmark store.
/// </summary>
/// <typeparam name="TBookmark">The type encapsulating a bookmark.</typeparam>
public interface IQueryableBookmarkStore<TUser, TBookmark> : IBookmarkStore<TUser, TBookmark> where TUser : class
                                                                                              where TBookmark : class
{
    /// <summary>
    /// Returns an <see cref="IQueryable{T}"/> collection of bookmarks.
    /// </summary>
    /// <value>An <see cref="IQueryable{T}"/> collection of bookmarks.</value>
    IQueryable<TBookmark> Bookmarks { get; }
}
