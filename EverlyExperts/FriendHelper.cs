using EverlyExperts.Contracts;
using EverlyExperts.Models;

namespace EverlyExperts
{
    public static class FriendHelper
    {
        private static IRepositoryWrapper? _repository;

        public static IRepositoryWrapper? Repository
        {
            get { return _repository; }
        }

        public static void InitHelper(IRepositoryWrapper repository)
        {
            _repository = repository;
        }

        public static async Task<List<Stack<Friend>>> GetPathsToFriendsByTopic(IEnumerable<Friend> friends, string topic, bool scanFriends = true)
        {
            List<Stack<Friend>> friendsPaths = new List<Stack<Friend>>();
            Stack<Friend> friendPath = new Stack<Friend>();

            foreach (Friend friend in friends)
            {
                if (scanFriends && friend.FriendMember.WebsiteTopics.Contains(topic))
                {
                    friendPath.Push(friend);
                    friendsPaths.Add(friendPath);
                }

                var myFriends = await _repository.Friend.GetAllFriendsByMemberIdAsync(friend.FriendMember.Id);

                List<Stack<Friend>> myFriendsPaths = await GetPathsToFriendsByTopic(myFriends, topic);

                if (myFriendsPaths.Count > 0)
                {
                    foreach (Stack<Friend> myFriendPath in myFriendsPaths)
                    {
                        myFriendPath.Push(friend);
                        friendsPaths.Add(myFriendPath);
                    }
                }
            }

            return friendsPaths;
        }
    }
}
