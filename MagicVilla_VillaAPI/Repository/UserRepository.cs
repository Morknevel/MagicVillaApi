using AutoMapper;
using MagicVilla.Data;
using MagicVilla.Models;
using MagicVilla.Models.Dto;
using MagicVilla.Repository.IRepository;

namespace MagicVilla.Repository;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _db;
    private readonly IMapper _mapper;

    public UserRepository(ApplicationDbContext db,IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public bool IsUniqueUser(string username)
    {
        var user = _db.LocalUsers.FirstOrDefault(x => x.UserName == username);
        if (user == null)
        {
            return true;
        }

        return false;
    }

    public async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDto)
    {
        throw new NotImplementedException();
    }

    public async Task<LocalUser> Register(RegisterationRequestDTO registerationRequestDto)
    {
        LocalUser user = _mapper.Map<LocalUser>(registerationRequestDto);
        _db.LocalUsers.Add(user);
        _db.SaveChanges();
        user.Password = "";
        return user;
    }
}