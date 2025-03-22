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
                UserId = user.UserId,
                Username = user.Username,
                Bio = user.Bio,
                GenderId = user.GenderId,
                StateId = user.StateId,
                Age = user.Age,
                Images = user.Images.Select(i => i.ToDto()).ToList()
            };
        }

        public static User ToEntity(this UserDto userDto)
        {
            return new User
            {
                UserId = userDto.UserId,
                Username = userDto.Username,
                Bio = userDto.Bio,
                GenderId = userDto.GenderId,
                StateId = userDto.StateId,
                Age = userDto.Age,
                Images = userDto.Images.Select(i => i.ToEntity()).ToList()
            };
        }
    }
}
