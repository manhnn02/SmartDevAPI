using System;
using AutoMapper;
using DAL;
using DAL.Implement;
using DAL.Models;
using Services.Helpers;
using Services.Models;

namespace Services.Implement
{
    public class UserServices : IUserServices
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper mapper;

        public UserServices(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            this.mapper = mapper;
        }
        public APIResponse AddUser(UserVM user)
        {
            try
            {
                var checkValid = ValidateInputs(user);
                if (!checkValid)
                    return new APIResponse() { Success = false, Message = "Invalid input" };

                //check existed
                var existedUser = UserExists(user.USER_ID, user.USER_EMAIL);
                if (existedUser)
                    return new APIResponse() { Success = false, Message = "Existed" };

                user.USER_PASS = Services.Helpers.CommonFunctions.MD5Hash(user.USER_PASS);

                var added = _userRepository.AddUser(mapper.Map<User>(user));
                if (added == null)
                    return new APIResponse() { Success = false, Message = "Internal error" };

                return new APIResponse() { Success = true, Message = "Successful", Data = mapper.Map<UserVM>(added) };
            }
            catch (Exception ex)
            {
                return new APIResponse() { Success = false, Message = "Internal error" };
            }
        }

        public APIResponse DeleteUser(long uID)
        {
            try
            {
                if (uID <= 0)
                    return new APIResponse() { Success = false, Message = "Invalid input" };
                return new APIResponse() { Success = true, Message = "Successful", Data = _userRepository.DeleteUser(uID) };
            }
            catch (Exception ex)
            {
                return new APIResponse() { Success = false, Message = "Internal error" };
            }
        }

        public APIResponse GetAll()
        {
            try
            {
                var lstUsers = _userRepository.GetAll();
                if (lstUsers.Count > 0)
                {
                    List<UserVM> response = new List<UserVM>();
                    foreach (var item in lstUsers)
                    {
                        UserVM obj = mapper.Map<UserVM>(item);
                        response.Add(obj);
                    }
                    return new APIResponse() { Success = true, Message = "Successful", Data = response };
                }
                else
                    return new APIResponse() { Success = false, Message = "No result" };
            }
            catch (Exception ex)
            {
                return new APIResponse() { Success = false, Message = "Internal error" };
            }
        }

        public APIResponse GetUser(long uID)
        {
            try
            {
                if (uID <= 0)
                    return new APIResponse() { Success = false, Message = "Invalid input" };

                var user = _userRepository.GetUser(uID);

                if (user != null)
                    return new APIResponse() { Success = true, Message = "Successful", Data = mapper.Map<UserVM>(user) };

                else
                    return new APIResponse() { Success = false, Message = "No result" };
            }
            catch (Exception ex)
            {
                return new APIResponse() { Success = false, Message = "Internal error" };
            }
        }

        public APIResponse UpdateUser(UserVM user)
        {
            try
            {
                var checkValid = ValidateInputs(user);
                if (!checkValid)
                    return new APIResponse() { Success = false, Message = "Invalid input" };

                //check existed
                var existedUser = UserExists(user.USER_ID, user.USER_EMAIL);
                if (!existedUser)
                    return new APIResponse() { Success = false, Message = "Not existed" };

                user.USER_PASS = Services.Helpers.CommonFunctions.MD5Hash(user.USER_PASS);
                var updateRes = _userRepository.UpdateUser(mapper.Map<User>(user));
                if (updateRes == null)
                    return new APIResponse() { Success = false, Message = "Internal error" };

                return new APIResponse() { Success = true, Message = "Successful", Data = mapper.Map<UserVM>(updateRes) };
            }
            catch (Exception ex)
            {
                return new APIResponse() { Success = false, Message = "Internal error" };
            }
        }

        private bool ValidateInputs(UserVM user)
        {
            //validate input
            if (user == null || String.IsNullOrEmpty(user.USER_NAME) || String.IsNullOrEmpty(user.USER_EMAIL) || String.IsNullOrEmpty(user.USER_PASS))
                return false;

            return true;
        }

        public bool UserExists(long id, string email)
        {
            return _userRepository.UserExists(id, email);
        }
    }
}

