using EverlyExperts.Models;

namespace EverlyExperts.Contracts
{
    public interface IFriendRepository : IRepositoryBase<Friend>
    {
        Task<IEnumerable<Friend>> GetAllFriendsByMemberIdAsync(int memberId);
        void CreateFriend(Friend friend);
    }
}
