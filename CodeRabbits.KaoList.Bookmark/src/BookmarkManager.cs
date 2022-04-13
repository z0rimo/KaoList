// (c) 2022 CodeRabbits
// This code is licensed under MIT license (see LICENSE.txt for details).

namespace CodeRabbits.KaoList.Bookmark;

public class BookmarkManager<TUser, TBookmark> : IDisposable where TUser : class
                                                             where TBookmark : class
{
    private bool _disposed;

    /// <summary>
    /// The cancellation token used to cancel operations.
    /// </summary>
    protected virtual CancellationToken CancellationToken => CancellationToken.None;

    public BookmarkManager(IBookmarkStore<TUser, TBookmark> store)
    {
        Store = store ?? throw new ArgumentNullException(nameof(store));
    }

    /// <summary>
    /// Gets or sets the persistence store the manager operates over.
    /// </summary>
    /// <value>The persistence store the manager operates over.</value>
    protected internal IBookmarkStore<TUser, TBookmark> Store { get; set; }


    /// <summary>
    ///     Returns an IQueryable of bookmarks if the store is an IQueryableBookmarkStore
    /// </summary>
    public virtual IQueryable<TBookmark> Bookmarks
    {
        get
        {
            if (Store is not IQueryableBookmarkStore<TUser, TBookmark> queryableStore)
            {
                throw new NotSupportedException(Resources.StoreNotIQueryableBookmarkStore);
            }

            return queryableStore.Bookmarks;
        }
    }

    /// <summary>
    /// Returns a list of bookmarks from the bookmark store who have the specified <paramref name="user"/>.
    /// </summary>
    /// <param name="user">The user to look for.</param>
    /// <returns>
    /// A <see cref="Task{TResult}"/> that represents the result of the asynchronous query, a list of <typeparamref name="TBookmark"/>s who
    /// have the specified user.
    /// </returns>
    public virtual Task<IList<TBookmark>> GetBookmarksAsync(TUser? user)
    {
        ThrowIfDisposed();
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user));
        }

        return Store.GetBookmarksAsync(user, CancellationToken); ;
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing && !_disposed)
        {
            Store.Dispose();
            _disposed = true;
        }
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Throws if this class has been disposed.
    /// </summary>
    protected void ThrowIfDisposed()
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(GetType().Name);
        }
    }
}
