using System;
using FlipCardsGame.Models;
namespace FlipCardsGame.Models
{
    public class GroupPlayManager
    {
        // Biến static giữ phiên bản duy nhất của GroupPlayManager
        private static GroupPlayManager _instance;

        // Đối tượng GroupPlay duy nhất
        public GroupPlay CurrentGroup { get; private set; }
        public GroupPlay NewtGroup { get; private set; }

        // Constructor private để ngăn không cho tạo instance từ bên ngoài
        private GroupPlayManager()
        {
            CurrentGroup = new GroupPlay();
            NewtGroup = new GroupPlay();
        }

        // Phương thức để lấy instance duy nhất của GroupPlayManager
        public static GroupPlayManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GroupPlayManager();
                }
                return _instance;
            }
        }

        // Phương thức để thiết lập thông tin nhóm chơi
        public void SetCurrentGroup(GroupPlay groupPlay)
        {
            CurrentGroup = groupPlay;
        } public void SetNewGroup(GroupPlay groupPlay)
        {
            NewtGroup = groupPlay;
        }

        // Phương thức để cập nhật điểm số
        public void UpdateScoreCurrentGroup(int points)
        {
            CurrentGroup.Score += points;
        }
        public void UpdateScoreNewGroup(int points)
        {
            CurrentGroup.Score += points;
        }

        // Phương thức để xóa nhóm chơi cũ
        public void ResetGroup()
        {
            CurrentGroup = new GroupPlay();
            NewtGroup = new GroupPlay();
        }
    }
}
