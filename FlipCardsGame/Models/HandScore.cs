using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using FlipCardsGame.Models;
namespace FlipCardsGame.Models
{
    public partial class HandScore
    {
        public static void AddPoints(string groupName, int points)
        {
            var group = GroupPlayManager.Instance.GetGroupByName(groupName);
            if (group != null)
            {
                group.Score += points;
            }
        }

        public static void SubtractPoints(string groupName, int points)
        {
            var group = GroupPlayManager.Instance.GetGroupByName(groupName);
            if (group != null)
            {
                group.Score -= points;
            }
        }

        public static void TransferPointsBetweenTeams(string fromGroupName, string toGroupName, int points)
        {
            var fromGroup = GroupPlayManager.Instance.GetGroupByName(fromGroupName);
            var toGroup = GroupPlayManager.Instance.GetGroupByName(toGroupName);
            if (fromGroup != null && toGroup != null)
            {
                fromGroup.Score -= points;
                toGroup.Score += points;
            }
        }
    }
}
