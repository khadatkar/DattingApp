using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API;
using API.DTOs;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public UserRepository(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;

        }

        public async Task<MemberDto> GetMemberAsync(string username)
        {
            return await _context.Users
            .Where(x => x.UserName == username)
            .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
            .Select(user => new MemberDto { }).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<MemberDto>> GetMembersAsync()
        {
            return await _context.Users
                .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        async Task<AppUser> IUserRepository.GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        async Task<AppUser> IUserRepository.GetUserByUsernameAsync(string Username)
        {
            return await _context.Users
            .Include(p => p.Photos)
            .SingleOrDefaultAsync(x => x.UserName == Username);
        }

        async Task<IEnumerable<AppUser>> IUserRepository.GetUsersAsynch()
        {
            return await _context.Users
            .Include(p => p.Photos)
            .ToListAsync();
        }

        async Task<bool> IUserRepository.SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        void IUserRepository.Update(AppUser user)
        {
            _context.Entry(user).State = EntityState.Modified;
        }
    }
}