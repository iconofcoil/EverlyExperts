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
    public class MemberController : ControllerBase
    {
        private IRepositoryWrapper repository;
        private IMapper mapper;

        public MemberController(IRepositoryWrapper repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var members = await repository.Member.GetAllMembersAsync();

                return Ok(members);
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateOwner([FromBody] MemberCreationDto member)
        {
            try
            {
                if (member == null)
                {
                    return BadRequest("Member object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                var memberEntity = mapper.Map<Member>(member);
                
                repository.Member.CreateMember(memberEntity);
                
                await repository.SaveAsync();
                
                var createdMember = mapper.Map<MemberDto>(memberEntity);
                
                return CreatedAtRoute("MemberById", new { id = createdMember.Id }, createdMember);
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}