using System.Threading.Tasks;
using ElevenNote.Data.Entities;
using ElevenNote.Models.User;

namespace ElevenNote.Services.User
{
    public class IUserService
    {
        Task<bool> RegisterUserAsync(UserRegister model)
        {
            var entity = new UserEntity
            {
                Email = model.Email,
                Username = model.Username,
                Password = model.Password,
                DateCreated = DateTime.Now
            };
        }
    }
}