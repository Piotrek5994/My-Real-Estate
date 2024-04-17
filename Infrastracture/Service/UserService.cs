using AutoMapper;
using Core.Commend.Create;
using Core.Commend.Update;
using Core.CommendDto;
using Core.Filter;
using Core.IRepositories;
using Infrastracture.ModelDto;
using Infrastracture.Service.IService;

namespace Infrastracture.Service;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IAddressRepository _addressRepository;
    private readonly IPhotoRepository _photoRepository;
    private readonly IPropertyRepository _propertyRepository;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, IMapper mapper, IAddressRepository addressRepository, IPhotoRepository photoRepository, IPropertyRepository propertyRepository)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _addressRepository = addressRepository;
        _photoRepository = photoRepository;
        _propertyRepository = propertyRepository;
    }
    public async Task<List<UserDto>> GetUserDto(UserFilter filter)
    {
        var result = await _userRepository.GetUser(filter);
        var userDtos = _mapper.Map<List<UserDto>>(result);
        return userDtos;
    }
    public async Task<string> Register(CreateUserDto userDto, string role)
    {
        CreateUser user = _mapper.Map<CreateUser>(userDto);
        string result = role switch
        {
            string r when r.Contains("User") => await _userRepository.CreateUser(user),
            string r when r.Contains("Admin") => await _userRepository.CreateAdmin(user),
            _ => "Invalid role"
        };

        return result;
    }
    public async Task<bool> UserUpdate(UpdateUser user, string userId) => await _userRepository.UpdateUser(user, userId);

    public async Task<bool> UserDelete(string id)
    {
        await AutomateDelete(id);
        bool result = await _userRepository.DeleteUser(id);
        return result;
    }
    private async Task AutomateDelete(string id)
    {
        await AutomateDeleteUserAvatar(id);
        await AutomateDeleteUserAdress(id);
        await AutomateDeleteUserPropertys(id);
    }
    private async Task AutomateDeleteUserPropertys(string id)
    {
        var user = await _userRepository.GetUser(new UserFilter { Id = id });
        foreach (var userProperty in user)
        {
            foreach (var PropertyUserId in userProperty.Properties)
            {
                 await _propertyRepository.DeleteProperty(PropertyUserId);
            }
        }
    }
    private async Task AutomateDeleteUserAdress(string id)
    {
        var address = await _addressRepository.GetAddress(new AddressFilter { UserId = id });
        foreach (var userAdress in address)
        {
             await _addressRepository.DeleteAddress(userAdress.Id);
        }
    }
    private async Task AutomateDeleteUserAvatar(string id)
    {
        await _photoRepository.DeleteAvatarPhoto(id);
        await _photoRepository.DeleteAvatarPhotoHead(id);
    }
}
