using EverlyExperts.Contracts;

namespace EverlyExperts.Data
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private ApplicationDbContext repoContext;
        private IMemberRepository memberRepository;
        private IFriendRepository friendRepository;

        public IMemberRepository Member
        {
            get
            {
                if (memberRepository == null)
                    memberRepository = new MemberRepository(repoContext);

                return memberRepository;
            }
        }

        public IFriendRepository Friend
        {
            get
            {
                if (friendRepository == null)
                    friendRepository = new FriendRepository(repoContext);

                return friendRepository;
            }
        }

        public RepositoryWrapper(ApplicationDbContext repositoryContext)
        {
            repoContext = repositoryContext;
        }

        public async Task SaveAsync()
        {
            await repoContext.SaveChangesAsync();
        }
    }
}
