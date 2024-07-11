using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using RentACarNow.Common.Constants.JWT;
using RentACarNow.Common.Infrastructure.Extensions;

namespace RentACarNow.APIs.ReadAPI.WebAPI.Extensions
{
    public static class AuthorizeExtensions
    {

        public static IServiceCollection SetAuthorize(this IServiceCollection services, bool active)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                      .AddJwtBearer(options =>
                      {
                          options.TokenValidationParameters = new TokenValidationParameters
                          {
                              ValidateIssuer = true,
                              ValidateAudience = true,
                              ValidateLifetime = true,
                              ValidateIssuerSigningKey = true,

                              IssuerSigningKey = new SymmetricSecurityKey(JWTConstants.SECURITY_KEY.ConvertToByteArray()),
                              ValidAudience = JWTConstants.AUDIENCE,
                              ValidIssuer = JWTConstants.ISSUER,
                          };


                      });


            if (active)
            {
                services.AddAuthorization(
               config =>
               {
                   config.AddPolicy("ReadAPI.Brand", policy =>
                   {
                       policy.RequireClaim("Permission", "ReadAPI.Brand");
                   });

                   config.AddPolicy("ReadAPI.Admin", policy =>
                   {
                       policy.RequireClaim("Permission", "ReadAPI.Admin");
                   });

                   config.AddPolicy("ReadAPI.Customer", policy =>
                   {
                       policy.RequireClaim("Permission", "ReadAPI.Customer");
                   });

                   config.AddPolicy("ReadAPI.Car", policy =>
                   {
                       policy.RequireClaim("Permission", "ReadAPI.Car");
                   });

                   config.AddPolicy("ReadAPI.Claim", policy =>
                   {
                       policy.RequireClaim("Permission", "ReadAPI.Claim");
                   });

                   config.AddPolicy("ReadAPI.Employee", policy =>
                   {
                       policy.RequireClaim("Permission", "ReadAPI.Employee");
                   });

                   config.AddPolicy("ReadAPI.Feature", policy =>
                   {
                       policy.RequireClaim("Permission", "ReadAPI.Feature");
                   });

                   config.AddPolicy("ReadAPI.Rental", policy =>
                   {
                       policy.RequireClaim("Permission", "ReadAPI.Rental");
                   });

               });
            }

            return services;
        }

    }
}
