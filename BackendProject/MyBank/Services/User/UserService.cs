using AutoMapper;
using DAL;
using Models.Domain.Entites;
using Models.DTO.User;
using Services.Helpers;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Services.User
{
    public interface IUserService
    {
        Task<bool> SignupAsync(SignupRequestDTO request);
        Task<AuthenticatedUserDTO> AuthenticateAsync(AuthenticateRequestDTO request);
        Task<decimal> GetUserBalanceAsync(AuthenticatedUserDTO request);
    }

    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJWTManager _jWTManager;

        public UserService(IMapper mapper, IUnitOfWork unitOfWork, IJWTManager jWTManager)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _jWTManager = jWTManager;
        }

        public async Task<bool> SignupAsync(SignupRequestDTO request)
        {
            var existingUser = _unitOfWork.RegisterUserRepository
                .GetSingleBy(a => a.Username.ToLower() == request.Username.ToLower());

            if (existingUser != null) return false;

            var regUser = _mapper.Map<RegisterUser>(request);

            regUser.LastLogin = null;
            regUser.PasswordSalt = PasswordHelper.PasswordSalt(request.Username);
            regUser.PasswordHash = PasswordHelper.PasswordHash(request.Password, regUser.PasswordSalt);
            
            var user = _mapper.Map<Models.Domain.Entites.User>(request);
            
            _unitOfWork.UserRepository.Add(user);
            _unitOfWork.Save();

            regUser.UserId = user.UserId;
            _unitOfWork.RegisterUserRepository.Add(regUser);
            _unitOfWork.Save();

            return true;
        }
        public async Task<AuthenticatedUserDTO> AuthenticateAsync(AuthenticateRequestDTO request)
        {
            // Validate the username and password against the stored user data
            var regUser = _unitOfWork.RegisterUserRepository
                .GetSingleBy(a => a.Username.ToLower() == request.Username.ToLower());

            if (regUser == null || 
                !PasswordHelper.VerifyPasswordHash(request.Password, regUser.PasswordHash, regUser.PasswordSalt))
                return null; // Authentication failed
            
            var user = _unitOfWork.UserRepository.GetSingleBy(a => a.UserId == regUser.UserId);

            // Save the login time, IP address, and other data in the database
            if (regUser.LastLogin == null) // First time login
            {
                user.Balance = user?.Balance == 0 ? 5.00m : (decimal)user?.Balance; // Add 5 GBP balance as a gift
                _unitOfWork.UserRepository.Edit(user);
            }
            regUser.LastLogin = DateTime.UtcNow;
            regUser.Ipaddress = request.Ipaddress;
            regUser.Device = request.Device;
            regUser.Browser = request.Browser;

            _unitOfWork.RegisterUserRepository.Edit(regUser);
            _unitOfWork.Save();

            var rsp = _mapper.Map<AuthenticatedUserDTO>(user);

            // Generate a JWT token
            rsp.Token = _jWTManager.GenerateJwtToken(rsp);

            rsp.UserId = null;
            rsp.Username = null;
            
            return rsp;
        }

        public async Task<decimal> GetUserBalanceAsync(AuthenticatedUserDTO request)
        {
            var user = _unitOfWork.UserRepository
                .GetSingleBy(a => a.UserId == request.UserId);

            return user?.Balance ?? 0; 
        }



    }
}
