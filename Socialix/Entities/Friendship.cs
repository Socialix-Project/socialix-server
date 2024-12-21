using System;
using System.Collections.Generic;

namespace Socialix.Entities;

public partial class Friendship
{
    public string FriendshipId { get; set; }

    public string UserId1 { get; set; }

    public string UserId2 { get; set; }

    public string Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual User UserId1Navigation { get; set; }

    public virtual User UserId2Navigation { get; set; }
}
