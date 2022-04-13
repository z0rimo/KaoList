// (c) 2022 CodeRabbits
// This code is licensed under MIT license (see LICENSE.txt for details).

namespace CodeRabbits.KaoList.Bookmark;

public interface IBookmarkStore<TUser, TBookmark> : IDisposable where TUser : class
                                                                where TBookmark : class
{
    /// <summary>
    /// Gets a list of <see cref="TBookmark"/>s to be belonging to the specified <paramref name="user"/> as an asynchronous operation.
    /// </summary>
    /// <param name="user">The bookmark to retrieve.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
    /// <returns>
    /// A <see cref="Task{TResult}"/> that represents the result of the asynchronous query, a list of <see cref="TBookmark"/>s.
    Task<IList<TBookmark>> GetBookmarksAsync(TUser? user, CancellationToken cancellationToken);
}
