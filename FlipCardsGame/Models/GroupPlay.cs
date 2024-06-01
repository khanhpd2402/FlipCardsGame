using System;
using System.Collections.Generic;

namespace FlipCardsGame.Models
{
    public partial class GroupPlay
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; } = null!;
        public int Score { get; set; }
    }
}
