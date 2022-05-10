using AutoMapper;
using EverlyExperts.Contracts;
using EverlyExperts.Data.Dtos;
using EverlyExperts.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EverlyExperts.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class FriendController : ControllerBase
    {
        private IRepositoryWrapper repository;
        private IMapper mapper;

        public FriendController(IRepositoryWrapper repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetFriendsByMemberId(int id)
        {
            try
            {
                var friends = await repository.Friend.GetAllFriendsByMemberIdAsync(id);

                return Ok(friends);
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("GetByTopic/{memberId}/{topic}")]
        public async Task<IActionResult> GetExpertsByMemberId(int memberId, string topic)
        {
            try
            {
                var friends = await repository.Friend.GetAllFriendsByMemberIdAsync(memberId);

                FriendHelper.InitHelper(repository);
                List<Stack<Friend>> expertsPaths = await FriendHelper.GetPathsToFriendsByTopic(friends, topic, false);

                List<List<Friend>> experts = new List<List<Friend>>();
                if (expertsPaths.Count > 0)
                {
                    List<Friend> expert = new List<Friend>();
                    foreach (Stack<Friend> expertPath in expertsPaths)
                    {
                        expert.Add(expertPath.Pop());
                    }

                    experts.Add(expert);
                }

                return Ok(experts);
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateFriend([FromBody] Friend friend)
        {
            try
            {
                if (friend == null)
                {
                    return BadRequest("Friend object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                // Generates additional friendship
                var addFriend = new Friend();
                addFriend.MemberId = friend.FriendId;
                addFriend.FriendId = friend.MemberId;

                // Adds both friends (A->B, B->A)
                repository.Friend.CreateFriend(friend);
                repository.Friend.CreateFriend(addFriend);

                await repository.SaveAsync();

                return Ok();
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
