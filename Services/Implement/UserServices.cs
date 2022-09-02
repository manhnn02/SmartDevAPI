using System;
using AutoMapper;
using DAL;
using DAL.Implement;
using DAL.Models;
using Services.Helpers;
using Services.Models;

namespace Services.Implement
{
    public class UserServices:IUserServices
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper mapper;

        public UserServices(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            this.mapper = mapper;
        }
        public UserVM AddUser(UserVM user)
        {
            try
            {
                var checkValid = ValidateInputs(user);
                if (!checkValid)
                    return new UserVM() { ResponseCode = ResponseCode.BadRequest };

                //check existed
                var existedUser = UserExists(user.UserId, user.UserEmail);
                if (existedUser)
                    return new UserVM() { ResponseCode = ResponseCode.BadRequest };

                user.UserPass = Services.Helpers.CommonFunctions.MD5Hash(user.UserPass);

                var added = _userRepository.AddUser(user);
                if (added == null)
                    return new UserVM() { ResponseCode = ResponseCode.Error };

                UserVM response = mapper.Map<UserVM>(added);
                response.ResponseCode = ResponseCode.Success;
                return response;
            }
            catch (Exception ex)
            {
                return new UserVM() { ResponseCode = ResponseCode.Error };
            }
        }

        public bool DeleteUser(long uID)
        {
            try
            {
                if (uID <= 0)
                    return false;

                return _userRepository.DeleteUser(uID);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<UserVM> GetAll()
        {
            try
            {
                var lstUsers = _userRepository.GetAll();
                List<UserVM> response = new List<UserVM>();
                if (lstUsers.Count > 0)
                {
                    foreach (var item in lstUsers)
                    {
                        UserVM obj = mapper.Map<UserVM>(item);
                        obj.ResponseCode = ResponseCode.Success;

                        response.Add(obj);
                    }
                }

                return response;
            }
            catch (Exception ex)
            {
                return new List<UserVM>() { new UserVM() { ResponseCode = ResponseCode.Error } };
            }
        }

        public UserVM GetUser(long uID)
        {
            try
            {
                if (uID <= 0)
                    return new UserVM() { ResponseCode = ResponseCode.BadRequest };

                var user = _userRepository.GetUser(uID);

                if (user != null)
                {
                    var response = mapper.Map<UserVM>(user);
                    response.ResponseCode = ResponseCode.Success;
                    return response;
                }
                else
                    return new UserVM() { ResponseCode = ResponseCode.BadRequest };
            }
            catch (Exception ex)
            {
                return new UserVM() { ResponseCode = ResponseCode.Error };
            }
        }

        public UserVM UpdateUser(UserVM user)
        {
            try
            {
                var checkValid = ValidateInputs(user);
                if (!checkValid)
                    return new UserVM() { ResponseCode = ResponseCode.BadRequest };

                //check existed
                var existedUser = UserExists(user.UserId, user.UserEmail);
                if (!existedUser)
                    return new UserVM() { ResponseCode = ResponseCode.BadRequest };

                user.UserPass = Services.Helpers.CommonFunctions.MD5Hash(user.UserPass);
                var updateRes = _userRepository.UpdateUser(user);
                if (updateRes == null)
                    return new UserVM() { ResponseCode = ResponseCode.Error };

                var response = mapper.Map<UserVM>(updateRes);
                response.ResponseCode = ResponseCode.Success;
                return response;
            }
            catch (Exception ex)
            {
                return new UserVM() { ResponseCode = ResponseCode.Error };
            }
        }

        private bool ValidateInputs(User user)
        {
            //validate input
            if (user == null || String.IsNullOrEmpty(user.UserName) || String.IsNullOrEmpty(user.UserEmail) || String.IsNullOrEmpty(user.UserPass))
                return false;

            return true;
        }

        public bool UserExists(long id, string email)
        {
            return _userRepository.UserExists(id, email);
        }
    }
}

