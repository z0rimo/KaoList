// (c) 2022 CodeRabbits
// This code is licensed under MIT license (see LICENSE.txt for details).

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CodeRabbits.KaoList.Bookmark;

public class BookmarkStore<TUser, TBookmark, TContext> : IBookmarkStore<TUser, TBookmark>
    where TUser : IdentityUser<string>, new()
    where TBookmark : UserBookmark<string, int>, new()
    where TContext : DbContext
{
    private bool _disposed;

    /// <summary>
    /// Creates a new instance of the store.
    /// </summary>
    /// <param name="context">The context used to access the store.</param>
    /// <param name="describer">The <see cref="BookmarkErrorDescriber"/> used to describe store errors.</param>
    public BookmarkStore(TContext context, BookmarkErrorDescriber? describer = null)
    {
        ErrorDescriber = describer ?? new BookmarkErrorDescriber();
        Context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <summary>
    /// Gets or sets the <see cref="BookmarkErrorDescriber"/> for any error that occurred with the current operation.
    /// </summary>
    public BookmarkErrorDescriber ErrorDescriber { get; set; }

    /// <summary>
    /// Gets the database context for this store.
    /// </summary>
    public virtual TContext Context { get; private set; }

    private DbSet<TBookmark> Bookmarks { get { return Context.Set<TBookmark>(); } }


    public virtual async Task<IList<TBookmark>> GetBookmarksAsync(TUser user, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user));
        }
        var userId = user.Id;
        return await Bookmarks.Where(l => l.UserId.Equals(userId)).ToListAsync(cancellationToken);
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

    /// <summary>
    /// Dispose the store
    /// </summary>
    public void Dispose()
    {
        _disposed = true;
    }
}
