using BookManagerAPI.Service.Models;
using BookManagerAPI.Service.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagerAPI.Service.Interfaces
{
    public interface IUserService
    {
        public Task<bool> AddUserAsync(UserRequestModel userModel); 
    }
}
