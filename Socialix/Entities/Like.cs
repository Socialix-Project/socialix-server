using System;
using System.Collections.Generic;

namespace Socialix.Entities;

public partial class Like
{
    public string LikeId { get; set; }

    public string UserId { get; set; }

    public string PostId { get; set; }

    public string CommentId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Comment Comment { get; set; }

    public virtual Post Post { get; set; }

    public virtual User User { get; set; }
}
