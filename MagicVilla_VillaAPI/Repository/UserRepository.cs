using MagicVilla.Data;
using MagicVilla.Models;
using MagicVilla.Models.Dto;
using MagicVilla.Repository.IRepository;

namespace MagicVilla.Repository;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _db;

    public UserRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public bool IsUniqueUser(string username)
    {
        throw new NotImplementedException();
    }

    public Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDto)
    {
        throw new NotImplementedException();
    }

    public Task<LocalUser> Register(RegisterationRequestDTO registerationRequestDto)
    {
        throw new NotImplementedException();
    }
}