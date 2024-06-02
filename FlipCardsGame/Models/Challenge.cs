using System;
using System.Collections.Generic;

namespace FlipCardsGame.Models
{
    public partial class Challenge
    {
        public int ChallengeId { get; set; }
        public string Content { get; set; } = null!;
        public string ChallengeType { get; set; } = null!;
        public int ChallengeValue { get; set; }
    }
}
