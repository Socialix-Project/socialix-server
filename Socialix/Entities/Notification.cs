using System;
using System.Collections.Generic;

namespace Socialix.Entities;

public partial class Notification
{
    public string NotificationId { get; set; }

    public string UserId { get; set; }

    public string Message { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool? IsRead { get; set; }

    public virtual User User { get; set; }
}
