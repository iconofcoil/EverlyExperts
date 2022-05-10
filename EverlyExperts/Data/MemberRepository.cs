using EverlyExperts.Contracts;
using EverlyExperts.Models;
using Microsoft.EntityFrameworkCore;

namespace EverlyExperts.Data
{
    public class MemberRepository : RepositoryBase<Member>, IMemberRepository
    {
        public MemberRepository(ApplicationDbContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<Member>> GetAllMembersAsync()
        {
            return await FindAll().OrderBy(m => m.Name).ToListAsync();
        }

        public async Task<Member> GetMemberByIdAsync(int memberId)
        {
            return await FindByCondition(m => m.Id.Equals(memberId)).FirstOrDefaultAsync();
        }

        public async Task<Member> GetMemberWithFriendsByIdAsync(int memberId)
        {
            return await FindByCondition(m => m.Id.Equals(memberId))
                         .Include(f => f.Friends)
                         .FirstOrDefaultAsync();
        }

        public void CreateMember(Member member)
        {
            Create(member);
        }
    }
}
