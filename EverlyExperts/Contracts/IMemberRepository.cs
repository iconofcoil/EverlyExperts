using EverlyExperts.Models;

namespace EverlyExperts.Contracts
{
    public interface IMemberRepository : IRepositoryBase<Member>
    {
        Task<IEnumerable<Member>> GetAllMembersAsync();
        Task<Member> GetMemberByIdAsync(int memberId);
        Task<Member> GetMemberWithFriendsByIdAsync(int memberId);
        void CreateMember(Member member);
    }
}
