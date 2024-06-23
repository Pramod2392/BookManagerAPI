using AutoMapper;
using BookManagerAPI.Repository.Interfaces;
using BookManagerAPI.Repository.Models;
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
        private readonly IMapper _mapper;

        public UserService(ISQLDBRepository sQLDBRepository, IMapper mapper)
        {
            this._sQLDBRepository = sQLDBRepository;
            this._mapper = mapper;
        }
        public async Task<ServiceResponse<UserResponseModel>> AddUserAsync(UserRequestModel userModel)
        {
            //_sQLDBRepository.AddNewUser(userModel);

            // User Id has data-type mis-match, please check

            var addUserModel = _mapper.Map<AddUserModel>(userModel);

            var response = await _sQLDBRepository.AddNewUser(addUserModel);
            
            if (response)
            {
                var returnModel = _mapper.Map<UserResponseModel>(userModel);
                return new ServiceResponse<UserResponseModel>(returnModel);
            }
            else
            {
                return new ServiceResponse<UserResponseModel>("Add user failed");
            }                        
        }
    }
}
