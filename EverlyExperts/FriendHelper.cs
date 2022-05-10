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

        public static async Task<List<Stack<Friend>>> GetPathsToFriendsByTopic(IEnumerable<Friend> friends, string topic, int originalMemberId, bool scanFriends = true)
        {
            List<Stack<Friend>> friendsPaths = new List<Stack<Friend>>();
            Stack<Friend> friendPath = new Stack<Friend>();

            foreach (Friend friend in friends)
            {
                // Avoid friend of original member
                if (friend.FriendMember.Id == originalMemberId)
                {
                    continue;
                }

                // Scan friend website topics
                if (scanFriends && friend.FriendMember.WebsiteTopics.Contains(topic))
                {
                    friendPath.Push(friend);
                    friendsPaths.Add(friendPath);
                }

                // Now process friends of friend
                var myFriends = await _repository.Friend.GetAllFriendsByMemberIdAsync(friend.FriendMember.Id);

                List<Stack<Friend>> myFriendsPaths = await GetPathsToFriendsByTopic(myFriends, topic, originalMemberId);

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
