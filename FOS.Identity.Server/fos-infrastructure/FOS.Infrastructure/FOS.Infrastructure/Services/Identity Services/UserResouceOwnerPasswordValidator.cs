using FOS.Repository.Interfaces;
using IdentityModel;
using IdentityServer4.Validation;

namespace AMS.API.Authenticator.IdentityServices
{
    public class UserResouceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly IUserRepository _userRepository;
        public UserResouceOwnerPasswordValidator(IUserRepository userRepository)
        {
            _userRepository=userRepository;
        }
        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            if(await _userRepository.ValidateCredentials(context.UserName, context.Password))
            {
                var user = await _userRepository.FindByUsername(context.UserName);
                context.Result = new GrantValidationResult(user.UserId.ToString(), OidcConstants.AuthenticationMethods.Password);
            }
        }
    }
}
