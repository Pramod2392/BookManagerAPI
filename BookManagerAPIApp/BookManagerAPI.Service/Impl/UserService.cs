using BookManagerAPI.Repository.Interfaces;
using BookManagerAPI.Service.Interfaces;
using BookManagerAPI.Service.Models;
using BookManagerAPI.Service.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagerAPI.Service.Impl
{
    public class UserService : IUserService
    {
        private readonly ISQLDBRepository _sQLDBRepository;

        public UserService(ISQLDBRepository sQLDBRepository)
        {
            this._sQLDBRepository = sQLDBRepository;
        }
        public Task<bool> AddUserAsync(UserRequestModel userModel)
        {
            //_sQLDBRepository.AddNewUser(userModel);
            throw new NotImplementedException();
        }
    }
}
