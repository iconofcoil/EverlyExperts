using Duende.IdentityServer.EntityFramework.Options;
using EverlyExperts;
using EverlyExperts.Contracts;
using EverlyExperts.Data;
using EverlyExperts.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EverlyExpertsTest
{
    [TestClass]
    public class FriendHelperUnitTests
    {
        public IRepositoryWrapper MockRepositoryWrapper;

        public FriendHelperUnitTests()
        {
            IList<Friend> friends = new List<Friend>();

            friends.Add(new Friend() { FriendId = 2, MemberId = 1, FriendMember = new Member() { Id = 2, Name = "Maria", WebsiteTopics = "Climate Change" } });
            friends.Add(new Friend() { FriendId = 3, MemberId = 1, FriendMember = new Member() { Id = 3, Name = "Gustavo", WebsiteTopics = "Unity" } });

            friends.Add(new Friend() { FriendId = 4, MemberId = 2, FriendMember = new Member() { Id = 4, Name = "Monserrat", WebsiteTopics = "Star Wars" } });
            friends.Add(new Friend() { FriendId = 5, MemberId = 2, FriendMember = new Member() { Id = 5, Name = "Carolina", WebsiteTopics = "Harry Potter" } });
            friends.Add(new Friend() { FriendId = 6, MemberId = 2, FriendMember = new Member() { Id = 6, Name = "Rita", WebsiteTopics = "Climate Change" } });

            friends.Add(new Friend() { FriendId = 7, MemberId = 3, FriendMember = new Member() { Id = 7, Name = "Diana", WebsiteTopics = "Baby Sitter" } });
            friends.Add(new Friend() { FriendId = 8, MemberId = 3, FriendMember = new Member() { Id = 8, Name = "Alfredo", WebsiteTopics = "Star Wars" } });
            friends.Add(new Friend() { FriendId = 1, MemberId = 3, FriendMember = new Member() { Id = 1, Name = "Luis Pablo", WebsiteTopics = "Star Wars" } });

            Mock<IRepositoryWrapper> repoWrapperMock = new Mock<IRepositoryWrapper>();
            Mock<IFriendRepository> friendRepositoryMock = new Mock<IFriendRepository>();

            repoWrapperMock.Setup(r => r.Friend).Returns(friendRepositoryMock.Object);
            friendRepositoryMock.Setup(r => r.GetAllFriendsByMemberIdAsync(It.IsAny<int>())).ReturnsAsync((int memberId) => friends.Where(f => f.MemberId == memberId));

            this.MockRepositoryWrapper = repoWrapperMock.Object;
        }

        [TestMethod]
        public async Task TestGetPathsToFriendsByTopicMethod()
        {
            // Arrange
            var memberId = 1;

            IList<Friend> friends = new List<Friend>();
            friends.Add(new Friend() { FriendId = 2, MemberId = memberId, FriendMember = new Member() { Id = 2, Name = "Maria", WebsiteTopics = "Climate Change" } });
            friends.Add(new Friend() { FriendId = 3, MemberId = memberId, FriendMember = new Member() { Id = 3, Name = "Gustavo", WebsiteTopics = "Unity" } });

            List<Stack<Friend>> expectedPaths = new List<Stack<Friend>>();
            
            Stack<Friend> path1 = new Stack<Friend>();
            path1.Push(new Friend() { FriendId = 4, MemberId = 2, FriendMember = new Member() { Id = 4, Name = "Monserrat", WebsiteTopics = "Star Wars" } });
            path1.Push(new Friend() { FriendId = 2, MemberId = 1, FriendMember = new Member() { Id = 2, Name = "Maria", WebsiteTopics = "Climate Change" } });
            expectedPaths.Add(path1);
            
            Stack<Friend> path2 = new Stack<Friend>();
            path2.Push(new Friend() { FriendId = 8, MemberId = 3, FriendMember = new Member() { Id = 8, Name = "Alfredo", WebsiteTopics = "Star Wars" } });
            path2.Push(new Friend() { FriendId = 3, MemberId = 1, FriendMember = new Member() { Id = 3, Name = "Gustavo", WebsiteTopics = "Unity" } });
            expectedPaths.Add(path2);

            // Act
            FriendHelper.InitHelper(this.MockRepositoryWrapper);
            List<Stack<Friend>> actualPaths = await FriendHelper.GetPathsToFriendsByTopic(friends, "Star Wars", memberId, false);

            // Assert

            // Same number of elements in collections
            Assert.AreEqual(expectedPaths.Count(), actualPaths.Count());
            Assert.AreEqual(expectedPaths[0].Count(), actualPaths[0].Count());
            Assert.AreEqual(expectedPaths[1].Count(), actualPaths[1].Count());

            // Evaluates names on first elements of each stack on actual
            Assert.AreEqual(((Friend)actualPaths[0].Peek()).FriendMember.Name, "Maria");
            Assert.AreEqual(((Friend)actualPaths[1].Peek()).FriendMember.Name, "Gustavo");

            // First element from Stack on first list, its MemberId must be equal
            Assert.AreEqual(((Friend)expectedPaths[0].Pop()).MemberId, ((Friend)actualPaths[0].Pop()).MemberId);

            // Last element from Stack on first list, its FriendMember Website Topics must contain "Star Wars"
            Assert.AreEqual(((Friend)expectedPaths[0].Pop()).FriendMember.WebsiteTopics.Contains("Star Wars"),
                            ((Friend)actualPaths[0].Pop()).FriendMember.WebsiteTopics.Contains("Star Wars"));

            // First element from Stack on second list, its MemberId must be equal
            Assert.AreEqual(((Friend)expectedPaths[1].Pop()).MemberId, ((Friend)actualPaths[1].Pop()).MemberId);

            // Last element from Stack on second list, its FriendMember Website Topics must contain "Star Wars"
            Assert.AreEqual(((Friend)expectedPaths[1].Pop()).FriendMember.WebsiteTopics.Contains("Star Wars"),
                            ((Friend)actualPaths[1].Pop()).FriendMember.WebsiteTopics.Contains("Star Wars"));
        }
    }
}
