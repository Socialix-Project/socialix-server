using System;
using System.Collections.Generic;

namespace Socialix.Entities;

public partial class Comment
{
    public string CommentId { get; set; }

    public string PostId { get; set; }

    public string UserId { get; set; }

    public string Content { get; set; }

    public decimal? CreatedAt { get; set; }

    public decimal? UpdatedAt { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual ICollection<Like> Likes { get; set; } = new List<Like>();

    public virtual Post Post { get; set; }

    public virtual User User { get; set; }
}
