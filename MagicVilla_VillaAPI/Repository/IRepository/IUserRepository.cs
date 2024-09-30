using MagicVilla.Models;
using MagicVilla.Models.Dto;

namespace MagicVilla.Repository.IRepository;

public interface IUserRepository
{
    bool IsUniqueUser(string username);
    Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDto);
    Task<LocalUser> Register(RegisterationRequestDTO registerationRequestDto);
}