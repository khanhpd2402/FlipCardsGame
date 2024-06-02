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
    new Challenge { ChallengeId = 1, Content = "Nhóm bạn bị mất lượt chơi tiếp theo.", ChallengeType = "shield", ChallengeValue = 0 },
    new Challenge { ChallengeId = 2, Content = "Nhóm bạn là một nhà hảo tâm, hãy bỏ 10 xu vào hộp và quyên góp cho một nhóm bất kì.", ChallengeType = "box", ChallengeValue = 10 },
    new Challenge { ChallengeId = 3, Content = "Vương miện nhà vua sẽ giúp bạn thu thuế của một nhóm bất kì 20 xu.", ChallengeType = "crown", ChallengeValue = 20 },
    new Challenge { ChallengeId = 4, Content = "Bạn nhận được ngôi sao hy vọng, bạn được kiểm tra một đáp án trong lượt chơi tới. Hãy dùng nó hợp lý nhé!~~", ChallengeType = "star", ChallengeValue = 0 },
    new Challenge { ChallengeId = 5, Content = "Hãy dùng nó và tấn công một nhóm bạn ghét. Nhóm đó sẽ bị mất 10 xu :))", ChallengeType = "spear", ChallengeValue = 10 },
    new Challenge { ChallengeId = 6, Content = "Nhóm bạn được thêm 1 lượt chơi.", ChallengeType = "clock", ChallengeValue = 0 },
    new Challenge { ChallengeId = 7, Content = "Hãy dùng nó và ném vào 1 nhóm bạn ghét, nhóm đó sẽ bị mất 1 lượt chơi.", ChallengeType = "bomb", ChallengeValue = 0 },
    new Challenge { ChallengeId = 8, Content = "Thần bài sẽ giúp bạn tráo đổi trở thành người giàu nhất.", ChallengeType = "card", ChallengeValue = 0 },
    new Challenge { ChallengeId = 9, Content = "Nhóm bạn nhận được 10 xu.", ChallengeType = "coins", ChallengeValue = 10 },
    new Challenge { ChallengeId = 10, Content = "Nhóm bạn nhận được 1 phần quà rất giá trị từ chúng tớ ~~.", ChallengeType = "money", ChallengeValue = 0 },
    new Challenge { ChallengeId = 11, Content = "Nó sẽ giúp bạn hồi phục sau một đợt tấn công vật lý(Bomb, Hồi sinh, Kiếm).", ChallengeType = "potion", ChallengeValue = 0 },
    new Challenge { ChallengeId = 12, Content = "Nhóm bạn nhận được 20 xu.", ChallengeType = "diamond", ChallengeValue = 20 },
    new Challenge { ChallengeId = 13, Content = "Nhóm bạn nhận được 5 xu.", ChallengeType = "coin", ChallengeValue = 5 },
    new Challenge { ChallengeId = 14, Content = "Nam châm sẽ hút 10 xu của một nhóm bất kì về cho bạn", ChallengeType = "magnet", ChallengeValue = 10 },
    new Challenge { ChallengeId = 15, Content = "Nó sẽ bảo vệ bạn khỏi một đợt tấn công, nhưng bạn sẽ không thể tránh được bomb.", ChallengeType = "sword", ChallengeValue = 0 }
};
        }
        public static GroupPlay? GetHighestScoringGroup()
        {
            return Groups.OrderByDescending(g => g.Score).FirstOrDefault();
        }
    }
}

