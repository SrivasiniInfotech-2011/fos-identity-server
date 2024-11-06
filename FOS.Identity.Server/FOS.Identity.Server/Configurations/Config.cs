using FOS.Models.Constants;
using IdentityServer4;
using IdentityServer4.Models;
namespace FOS.Identity.Server.Configurations
{
    public class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email()
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource(
                                Constants.ApiResource.UserApi,
                                Constants.ApiResource.UserApiResouceDisplayName,
                                new List<string> {
                                                    Constants.UserClaim.Role,
                                                    Constants.UserClaim.Admin,
                                                    Constants.UserClaim.User,
                                                    Constants.UserClaim.DataEventRecords,
                                                    Constants.UserClaim.AdminDataEventRecords,
                                                    Constants.UserClaim.UserAdminEventRecords
                                                 })
                {
                    ApiSecrets =
                    {
                        new Secret(Constants.ApiResource.ApiResourceSecret.Sha256())
                    },
                    Scopes =
                    {
                        //"dataEventRecordsScope",
                        Constants.ApiResource.UserApiScope,
                    }
                }
            };
        }

        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>
            {
                new ApiScope
                {
                    //Name = Constants.APP_SCOPE,
                    //DisplayName = Constants.UserClaim.ScopeDisplayName,
                    //UserClaims = { Constants.UserClaim.Role, Constants.UserClaim.Admin, Constants.UserClaim.User, Constants.UserClaim.DataEventRecords, Constants.UserClaim.AdminDataEventRecords, Constants.UserClaim.UserAdminEventRecords }
                    Name =Constants.ApiResource.UserApiScope,
                    DisplayName = Constants.UserClaim.ScopeDisplayName,
                    UserClaims = { Constants.UserClaim.Role, Constants.UserClaim.Admin, Constants.UserClaim.User, Constants.UserClaim.DataEventRecords, Constants.UserClaim.AdminDataEventRecords, Constants.UserClaim.UserAdminEventRecords }
                }
            };
        }

        // clients want to access resources (aka scopes)
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId =Constants.APP_CLIENT_ID,

                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    AccessTokenType = AccessTokenType.Jwt,
                    AccessTokenLifetime = 3600,
                    IdentityTokenLifetime = 3600,
                    UpdateAccessTokenClaimsOnRefresh = true,
                    SlidingRefreshTokenLifetime = 30,
                    AllowOfflineAccess = true,
                    RefreshTokenExpiration = TokenExpiration.Absolute,
                    RefreshTokenUsage = TokenUsage.OneTimeOnly,
                    AlwaysSendClientClaims = true,
                    Enabled = true,
                    ClientSecrets=  new List<Secret> { new Secret(Constants.APP_SECRET.Sha256()) },
                    AllowedScopes = {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                        Constants.ApiResource.UserApiScope
                    }
                }
            };
        }
    }
}
