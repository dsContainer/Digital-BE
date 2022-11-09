using Digital.Data.Entities;
using Digital.Infrastructure.Model.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digital.Infrastructure.Service.UserService
{
    public interface IUserService
    {
        List<User> GetUsers();
        User GetUser(Guid id);
        User CreateUser(UserRequest userRequest);
        User UpdateUser(Guid id, UserRequest userRequest);
        User DeletedUser(Guid id, bool isDeleted);
    }
}
