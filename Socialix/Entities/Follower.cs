using System;
using System.Collections.Generic;

namespace Socialix.Entities;

public partial class Follower
{
    public string FollowerId { get; set; }

    public string UserId { get; set; }

    public string FollowerUserId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual User FollowerUser { get; set; }

    public virtual User User { get; set; }
}
