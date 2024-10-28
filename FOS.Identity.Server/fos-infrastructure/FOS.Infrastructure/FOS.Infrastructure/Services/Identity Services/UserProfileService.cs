
using FOS.Models.Constants;
using FOS.Repository.Interfaces;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace AMS.API.Authenticator.IdentityServices
{
    public class UserProfileService : IProfileService
    {
        protected readonly ILogger Logger;


        protected readonly IUserRepository _userRepository;

        public UserProfileService(IUserRepository userRepository, ILogger<UserProfileService> logger)
        {
            _userRepository = userRepository;
            Logger = logger;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var sub = context.Subject.GetSubjectId();

            Logger.LogDebug("Get profile called for subject {subject} from client {client} with claim types {claimTypes} via {caller}",
                context.Subject.GetSubjectId(),
                context.Client.ClientName ?? context.Client.ClientId,
                context.RequestedClaimTypes,
                context.Caller);

            var user = await _userRepository.FindById(sub);

            var claims = new List<Claim>
            {
                new Claim("role",Constants.UserClaim.AdminDataEventRecords),
                new Claim("role", Constants.UserClaim.UserAdminEventRecords),
                new Claim("username", user.UserUserName!),
                new Claim("email", "seshadri.srivatsan@gmail.com")
            };

            context.IssuedClaims = claims;
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var user = await _userRepository.FindById(sub);
            context.IsActive = user != null;
        }
    }
}
