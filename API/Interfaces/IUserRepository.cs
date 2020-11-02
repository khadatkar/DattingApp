using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;

namespace API.Interfaces
{
    public interface IUserRepository
    {
       void Update(AppUser user);

        Task<bool> SaveAllAsync();

       Task<IEnumerable<AppUser>> GetUsersAsynch();

        Task<AppUser> GetUserByIdAsync(int id);

        Task<AppUser> GetUserByUsernameAsync(string Username);
        Task<IEnumerable<MemberDto>> GetMembersAsync();
        Task<MemberDto> GetMemberAsync(string username);
        

    }
}