using Digital.Data.Data;
using Digital.Data.Entities;
using Digital.Infrastructure.Model.Requests;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digital.Infrastructure.Service.UserService
{
    public class UserService : IUserService
    {
        ApplicationDBContext _context;
        public UserService(ApplicationDBContext context)
        {
            _context= context;
        }

        public List<User> GetUsers()
        {
            var users = _context.Users.ToList();
            return users;
        }

        public User GetUser(Guid id)
        {
            var user = _context.Users.Find(id);
            return user;
        }

        public User CreateUser(UserRequest userRequest)
        {
            User user = new()
            {
                Id = Guid.NewGuid(),
                Email = userRequest.Email,
                Phone = userRequest.Phone,
                Username = userRequest.Username,
                FullName = userRequest.FullName,
                Password = userRequest.Password,
                RoleId = userRequest.RoleId,
                SigId = userRequest.SigId,
                DateCreated = DateTime.Now,
                DateUpdated = DateTime.Now,
                IsDeleted = userRequest.IsDeleted
            };

            _context.Users.Add(user);

            _context.SaveChanges();

            return user;
        }

        public User UpdateUser(Guid id, UserRequest userRequest)
        {
            var user = _context.Users.Find(id);

            if (user != null)
            {
                user.Email = userRequest.Email;
                user.Phone = userRequest.Phone;
                user.Username = userRequest.Username;
                user.FullName = userRequest.FullName;
                user.Password = userRequest.Password;
                user.RoleId = userRequest.RoleId;
                user.SigId = userRequest.SigId;
                user.DateUpdated = DateTime.Now;
                user.IsDeleted = userRequest.IsDeleted;

                _context.Users.Update(user);

                _context.SaveChanges();
            }

            return user;
        }

        public User DeletedUser(Guid id, bool isDeleted)
        {
            var user = _context.Users.Find(id);

            if (user != null)
            {
                user.DateUpdated = DateTime.Now;
                user.IsDeleted = isDeleted;

                _context.Users.Update(user);

                _context.SaveChanges();
            }

            return user;
        }
    }
}
