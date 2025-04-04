using Project_IV.Dtos;
using Project_IV.Entities;

namespace Project_IV.Mappers
{
    public static class UserMapper
    {
        public static UserDto ToDto(this User user)
        {
            return new UserDto
            {
                UserId = user.Id,
                Username = user.UserName,
                Bio = user.Bio,
                GenderId = user.GenderId,
                Email = user.Email,
                StateId = user.StateId,
                Age = user.Age,
                Images = user.Images.Select(i => i.ToDto()).ToList()
            };
        }

        public static User ToEntity(this UserDto userDto)
        {
            return new User
            {
                UserName = userDto.Username,
                Bio = userDto.Bio,
                GenderId = userDto.GenderId,
                StateId = userDto.StateId,
                Email= userDto.Email,
                Age = userDto.Age,
                Images = userDto.Images.Select(i => i.ToEntity()).ToList()
            };
        }
    }
}
