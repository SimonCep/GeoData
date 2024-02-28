using GeoDataInfrastructure.Models;
using GeoDataInfrastructure.Repositories;

namespace GeoDataBusiness.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public Task CreateAsync(User user)
    {
        return _userRepository.CreateAsync(user);
    }

    public Task<User?> GetAsync(string email)
    {
        return _userRepository.GetAsync(email);
    }
}