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

    public BookmarkManager(IBookmarkStore<TBookmark, TUser> store)
    {
        Store = store ?? throw new ArgumentNullException(nameof(store));
    }

    /// <summary>
    /// Gets or sets the persistence store the manager operates over.
    /// </summary>
    /// <value>The persistence store the manager operates over.</value>
    protected internal IBookmarkStore<TBookmark, TUser> Store { get; set; }


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
    /// Add the <paramref name="bookmark"/> in <see cref="IBookmarkStore">.
    /// </summary>
    /// <param name="bookmark">The bookmark to add.</param>
    /// <returns>
    /// The <see cref="Task"/> that represents the asynchronous operation, containing the <see cref="BookmarkResult"/>
    /// of the operation.
    /// </returns>
    public virtual Task<BookmarkResult> AddBookmark(TBookmark? bookmark)
    {
        ThrowIfDisposed();
        if (bookmark == null)
        {
            throw new ArgumentNullException(nameof(bookmark));
        }

        return AddBookmarks(new TBookmark[] { bookmark });
    }

    /// <summary>
    /// Adds the <paramref name="bookmarks"/> in <see cref="IBookmarkStore">.
    /// </summary>
    /// <param name="bookmarks">The bookmarks to add.</param>
    /// <returns>
    /// The <see cref="Task"/> that represents the asynchronous operation, containing the <see cref="BookmarkResult"/>
    /// of the operation.
    /// </returns>
    public virtual async Task<BookmarkResult> AddBookmarks(IEnumerable<TBookmark> bookmarks)
    {
        ThrowIfDisposed();
        if (bookmarks == null)
        {
            throw new ArgumentNullException(nameof(bookmarks));
        }

        return await Store.AddBookmarks(bookmarks, CancellationToken).ConfigureAwait(false);
    }


    /// <summary>
    /// Remove the <paramref name="bookmark"/> in <see cref="IBookmarkStore">.
    /// </summary>
    /// <param name="bookmark">The bookmark to remove.</param>
    /// <returns>
    /// The <see cref="Task"/> that represents the asynchronous operation, containing the <see cref="BookmarkResult"/>
    /// of the operation.
    /// </returns>
    public virtual Task<BookmarkResult> RemoveBookmark(TBookmark? bookmark)
    {
        ThrowIfDisposed();
        if (bookmark == null)
        {
            throw new ArgumentNullException(nameof(bookmark));
        }

        return AddBookmarks(new TBookmark[] { bookmark });
    }


    /// <summary>
    /// Remove the <paramref name="bookmark"/> in <see cref="IBookmarkStore">.
    /// </summary>
    /// <param name="bookmark">The bookmarks to remove.</param>
    /// <returns>
    /// The <see cref="Task"/> that represents the asynchronous operation, containing the <see cref="BookmarkResult"/>
    /// of the operation.
    /// </returns>
    public virtual async Task<BookmarkResult> RemovesBookmark(IEnumerable<TBookmark>? bookmarks)
    {
        ThrowIfDisposed();
        if (bookmarks == null)
        {
            throw new ArgumentNullException(nameof(bookmarks));
        }

        return await Store.RemoveBookmarks(bookmarks, CancellationToken).ConfigureAwait(false);
    }


    /// <summary>
    /// Returns a list of bookmarks from the bookmark store who have the specified <paramref name="user"/>.
    /// </summary>
    /// <param name="user">The user to look for.</param>
    /// <returns>
    /// A <see cref="Task{TResult}"/> that represents the result of the asynchronous query, a list of <typeparamref name="TBookmark"/>s who
    /// have the specified user.
    /// </returns>
    public virtual Task<IList<TBookmark>> GetBookmarksForUserAsync(TUser? user)
    {
        ThrowIfDisposed();
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user));
        }

        return Store.GetBookmarksAsync(user, CancellationToken);
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
