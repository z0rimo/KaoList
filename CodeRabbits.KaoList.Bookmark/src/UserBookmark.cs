// (c) 2022 CodeRabbits
// This code is licensed under MIT license (see LICENSE.txt for details).

namespace CodeRabbits.KaoList.Bookmark
{
    public class UserBookmark : UserBookmark<string, int> { }

    public class UserBookmark<TUserKey, TSongKey> where TUserKey : IEquatable<TUserKey>
        where TSongKey : IEquatable<TSongKey>
    {
        /// <summary>
        /// Gets or sets the primary key of the user that is linked to a song.
        /// </summary>
        public virtual TUserKey UserId { get; set; } = default!;

        /// <summary>
        /// Gets or sets the primary key of the role that is linked to the user.
        /// </summary>
        public virtual TSongKey SongId { get; set; } = default!;
    }
}
