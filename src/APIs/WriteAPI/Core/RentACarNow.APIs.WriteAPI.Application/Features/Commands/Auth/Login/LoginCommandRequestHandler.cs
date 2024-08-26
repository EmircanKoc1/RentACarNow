using MediatR;
using Microsoft.IdentityModel.Tokens;
using RentACarNow.Common.Constants.JWT;
using RentACarNow.Common.Infrastructure.Extensions;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Read.Mongo;
using System.IdentityModel.Tokens.Jwt;
using mongoEntites = RentACarNow.Common.MongoEntities;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Auth.Login
{
    public class LoginCommandRequestHandler : IRequestHandler<LoginCommandRequest, LoginCommandResponse>
    {
    //    private readonly IMongoAdminReadRepository _adminReadRepository;
    //    private readonly IMongoCustomerReadRepository _customerReadRepository;
    //    private readonly IMongoEmployeeReadRepository _employeeReadRepository;

    //    public LoginCommandRequestHandler(IMongoAdminReadRepository adminReadRepository, IMongoCustomerReadRepository customerReadRepository, IMongoEmployeeReadRepository employeeReadRepository)
    //    {
    //        _adminReadRepository = adminReadRepository;
    //        _customerReadRepository = customerReadRepository;
    //        _employeeReadRepository = employeeReadRepository;
    //    }

        public async Task<LoginCommandResponse> Handle(LoginCommandRequest request, CancellationToken cancellationToken)
        {
            IEnumerable<mongoEntites.Claim> claims = null;

            var tokenHandler = new JwtSecurityTokenHandler();

            var signingCred = new SigningCredentials(new SymmetricSecurityKey(JWTConstants.SECURITY_KEY.ConvertToByteArray()), SecurityAlgorithms.HmacSha256);


            var token = new JwtSecurityToken(
                claims: claims.Select(c => new System.Security.Claims.Claim(c.Key, c.Value)),
                issuer: JWTConstants.ISSUER,
                audience: JWTConstants.AUDIENCE,
                expires: DateTime.Now.AddDays(JWTConstants.EXPIRE_MIN),
                signingCredentials: signingCred);


            var stringToken = tokenHandler.WriteToken(token);


            return new LoginCommandResponse() { JwtToken = stringToken };

        }
    }


}
