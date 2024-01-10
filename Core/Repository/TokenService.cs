using Core.Interfaces;
using DataAccess.Interface;
using Domain.Common.Enum;
using Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repository
{
    public class TokenService : ITokenService
    {
        private readonly IRepository<Configuracion> configuiuracionRepository;
        private readonly IRepository<Usuario> usuarioRepository;

        public TokenService(IRepository<Configuracion> configuiuracionRepository, IRepository<Usuario> usuarioRepository) {         
            this.configuiuracionRepository = configuiuracionRepository;
            this.usuarioRepository = usuarioRepository;
        }   
        public async Task<bool> ValidateToken(string token)
        {        
           
            try
            {
                var tokenHeader = new JwtSecurityTokenHandler();
                var secreKey = (await configuiuracionRepository.GetByParam(x => x.Id.Equals(ParamConfig.JwtSecretKey.ToString())))?.Value ?? string.Empty ;
                var jwtIssuerToken = (await configuiuracionRepository.GetByParam(x => x.Id.Equals(ParamConfig.JwtIssuerToken.ToString())))?.Value;
                var jwtAudienceToken = (await configuiuracionRepository.GetByParam(x => x.Id.Equals(ParamConfig.JwtIssuerToken.ToString())))?.Value;
                var key = Encoding.ASCII.GetBytes(secreKey);
                var tokenParameter = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer= true,
                    ValidIssuer = jwtIssuerToken, 
                    ValidateAudience = true,
                    ValidAudience = jwtAudienceToken,  
                    ClockSkew = TimeSpan.Zero
                };           

                tokenHeader.ValidateToken(token, tokenParameter,out SecurityToken securutyToken);
                var jwtToken = (JwtSecurityToken)securutyToken;
                var isOk = await SearchUser(jwtToken.Claims.First(t => t.Type == "unique_name").Value);
                return isOk;    
            }
            catch
            {
                return false;
            }          
        }



        private async Task<bool> SearchUser(string username)
        {
            var user = await usuarioRepository.GetByParam(x => x.UserName.Equals(username));
            return user != null;
        }
    }
}
