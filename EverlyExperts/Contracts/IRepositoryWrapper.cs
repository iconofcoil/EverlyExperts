namespace EverlyExperts.Contracts
{
    public interface IRepositoryWrapper
    {
        IMemberRepository Member { get; }
        IFriendRepository Friend { get; }
        Task SaveAsync();
    }
}
