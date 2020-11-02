using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API;
using API.Interfaces;
using API.DTOs;
using AutoMapper;

namespace Controllers
{
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly IUserRepository _userRespository;
        private readonly IMapper _mapper;
        public UsersController(IUserRepository userRespository, IMapper mapper)
        {
            _mapper = mapper;
            _userRespository = userRespository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
        {
            var users = await _userRespository.GetMembersAsync();

            return Ok(users);
        }

        [HttpGet("{username}")]
        public async Task<ActionResult<MemberDto>> GetUser(string username)
        {
            return await _userRespository.GetMemberAsync(username);
            
        }
    }
}