using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using RentACarNow.Common.Constants.JWT;
using RentACarNow.Common.Infrastructure.Extensions;

namespace RentACarNow.APIs.WriteAPI.WebAPI.Extensions
{
    public static class AuthorizeExtensions
    {

        public static IServiceCollection SetAuthorize(this IServiceCollection services, bool active = true)
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


            services.AddAuthorization(
                config =>
                {
                    config.AddPolicy("WriteAPI.Brand", policy =>
                    {
                        if (active)
                            policy.RequireClaim("Permission", "WriteAPI.Brand");
                    });

                    config.AddPolicy("WriteAPI.Admin", policy =>
                    {
                        if (active)
                            policy.RequireClaim("Permission", "WriteAPI.Admin");
                    });

                    config.AddPolicy("WriteAPI.Customer", policy =>
                    {
                        if (active)
                            policy.RequireClaim("Permission", "WriteAPI.Customer");
                    });

                    config.AddPolicy("WriteAPI.Car", policy =>
                    {
                        if (active)
                            policy.RequireClaim("Permission", "WriteAPI.Car");
                    });

                    config.AddPolicy("WriteAPI.Claim", policy =>
                    {
                        if (active)
                            policy.RequireClaim("Permission", "WriteAPI.Claim");
                    });

                    config.AddPolicy("WriteAPI.Employee", policy =>
                    {
                        if (active)
                            policy.RequireClaim("Permission", "WriteAPI.Employee");
                    });

                    config.AddPolicy("WriteAPI.Feature", policy =>
                    {
                        if (active)
                            policy.RequireClaim("Permission", "WriteAPI.Feature");
                    });

                    config.AddPolicy("WriteAPI.Rental", policy =>
                    {
                        if (active)
                            policy.RequireClaim("Permission", "WriteAPI.Rental");
                    });

                });

            return services;
        }
    }
}
