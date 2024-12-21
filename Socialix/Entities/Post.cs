using System;
using System.Collections.Generic;

namespace Socialix.Entities;

public partial class Post
{
    public string PostId { get; set; }

    public string UserId { get; set; }

    public string Content { get; set; }

    public string MediaUrl { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<Like> Likes { get; set; } = new List<Like>();

    public virtual User User { get; set; }
}
