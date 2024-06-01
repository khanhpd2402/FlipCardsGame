using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlipCardsGame.Models
{
    public static class InitialData
    {
        public static List<GroupPlay> Groups { get; private set; }
        public static List<Challenge> Challenges { get; private set; }

        static InitialData()
        {
            // Khởi tạo danh sách nhóm
            Groups = new List<GroupPlay>
            {
                new GroupPlay { GroupId = 1, GroupName = "Nhóm 1", Score = 100 },
                new GroupPlay { GroupId = 2, GroupName = "Nhóm 2", Score = 100 },
                new GroupPlay { GroupId = 3, GroupName = "Nhóm 3", Score = 100 },
                new GroupPlay { GroupId = 4, GroupName = "Nhóm 4", Score = 100 },
                new GroupPlay { GroupId = 5, GroupName = "Nhóm 5", Score = 100 }
            };

            // Initializing the challenge data with corresponding item images
            Challenges = new List<Challenge>
{
    new Challenge { ChallengeId = 1, Content = "You are protected from the next attack.", ChallengeType = "shield", ChallengeValue = null },
    new Challenge { ChallengeId = 2, Content = "Receive a random reward.", ChallengeType = "box", ChallengeValue = null },
    new Challenge { ChallengeId = 3, Content = "Receive 50 bonus points.", ChallengeType = "crown", ChallengeValue = 50 },
    new Challenge { ChallengeId = 4, Content = "Gain an additional 20 points.", ChallengeType = "star", ChallengeValue = 20 },
    new Challenge { ChallengeId = 5, Content = "Lose 10 points.", ChallengeType = "spear", ChallengeValue = -10 },
    new Challenge { ChallengeId = 6, Content = "Gain an extra turn.", ChallengeType = "clock", ChallengeValue = null },
    new Challenge { ChallengeId = 7, Content = "Lose a turn.", ChallengeType = "bomb", ChallengeValue = null },
    new Challenge { ChallengeId = 8, Content = "Swap points with another team.", ChallengeType = "card", ChallengeValue = null },
    new Challenge { ChallengeId = 9, Content = "Receive 30 bonus points.", ChallengeType = "coins", ChallengeValue = 30 },
    new Challenge { ChallengeId = 10, Content = "Lose 15 points.", ChallengeType = "money", ChallengeValue = -15 },
    new Challenge { ChallengeId = 11, Content = "Protected from a penalty.", ChallengeType = "potion", ChallengeValue = null },
    new Challenge { ChallengeId = 12, Content = "Receive a special item.", ChallengeType = "diamond", ChallengeValue = null },
    new Challenge { ChallengeId = 13, Content = "Gain 5 points per turn.", ChallengeType = "coin", ChallengeValue = null },
    new Challenge { ChallengeId = 14, Content = "Lose 5 points per turn.", ChallengeType = "magnet", ChallengeValue = null },
    new Challenge { ChallengeId = 15, Content = "Receive 100 bonus points.", ChallengeType = "crown", ChallengeValue = 100 }
};
        }
    }
}

