using EverlyExperts.Contracts;
using EverlyExperts.Models;
using Microsoft.EntityFrameworkCore;

namespace EverlyExperts.Data
{
    public class FriendRepository : RepositoryBase<Friend>, IFriendRepository
    {
        public FriendRepository(ApplicationDbContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<Friend>> GetAllFriendsByMemberIdAsync(int memberId)
        {
            return await FindByCondition(f => f.MemberId.Equals(memberId)).Include(f => f.FriendMember).ToListAsync();
        }

        public void CreateFriend(Friend friend)
        {
            Create(friend);
        }
    }
}
