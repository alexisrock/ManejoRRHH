using Core.Interfaces;
using DataAccess.Interface;
using Domain.Common;
using Domain.Common.Enum;
using Domain.Dto;
using Domain.Entities;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Net;
using System.Security.Cryptography;
using AutoMapper;
using Domain.Entities.StoreProcedure;
using Core.Common;

namespace Core.Repository
{
    public class UserService : IUserService
    {
        
        private readonly IRepository<Configuracion> configuiuracionRepository;
        private readonly IRepository<Usuario> usuarioRepository;
        private readonly IStoreProcedureRepository storeProcedureRepository;
        private readonly IMapper mapper;


        public UserService( IRepository<Configuracion> configuiuracionRepository, IRepository<Usuario> usuarioRepository, IMapper mapper, IStoreProcedureRepository storeProcedureRepository)
        {
            this.configuiuracionRepository = configuiuracionRepository;
            this.usuarioRepository = usuarioRepository;
            this.mapper = mapper;
            this.storeProcedureRepository = storeProcedureRepository;
        }

        public async Task<UserTokenResponse>  GetAuthentication(UserTokenRequest userTokenRequest)
        {
            var UserTokenResponse = new UserTokenResponse();
            try
            {

                var user = await ValidateUserName(userTokenRequest.UserName);
                if (user  is null)
                {
                    UserTokenResponse.StatusCode = HttpStatusCode.Unauthorized;
                    UserTokenResponse.Message = "Usuario no encontrado";
                    return UserTokenResponse;
                }

                string pass = DecodeBase64Password(userTokenRequest.Password);
                if (!await ValidatePassword(pass, user.Password))
                {
                    UserTokenResponse.StatusCode = HttpStatusCode.Unauthorized;
                    UserTokenResponse.Message = "Password no valido";
                    return UserTokenResponse;
                }
                UserTokenResponse = await MapperUserTokenResponse(user);               
            }
            catch (Exception ex)
            {
                UserTokenResponse.StatusCode = HttpStatusCode.InternalServerError;
                UserTokenResponse.Message =  ex.Message;
            }
            return UserTokenResponse;
        }       
        private async Task<Usuario?> ValidateUserName(string? userName)
        {
            var user = await usuarioRepository.GetByParam(x => x.UserName.Equals(userName));
            return user;       
        }
        private static string DecodeBase64Password(string password) 
        {
            var base64EncodedBytes = System.Convert.FromBase64String(password);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);             
        }
        public async Task<bool> ValidatePassword(string? password, string encryptedPassword)
        {

            var keyEncrypted = (await configuiuracionRepository.GetByParam(x => x.Id.Equals(ParamConfig.KeyEncrypted.ToString())))?.Value ?? string.Empty;
            var iVEncrypted = (await configuiuracionRepository.GetByParam(x => x.Id.Equals(ParamConfig.IVEncrypted.ToString())))?.Value ?? string.Empty;
            byte[] key = Encoding.UTF8.GetBytes(keyEncrypted);
            byte[] iv = Encoding.UTF8.GetBytes(iVEncrypted);
            using (TripleDES aes = TripleDES.Create())
            {            
               
                ICryptoTransform decryptor = aes.CreateDecryptor(key, iv);
                byte[] encryptedPasswordBytes = Convert.FromBase64String(encryptedPassword);
                byte[] decryptedPasswordBytes = decryptor.TransformFinalBlock(encryptedPasswordBytes, 0, encryptedPasswordBytes.Length);
                string decryptedPassword = Encoding.UTF8.GetString(decryptedPasswordBytes);
                return decryptedPassword == password;               
            }              
        }
        private async Task<UserTokenResponse> MapperUserTokenResponse(Usuario user)
        {
            UserTokenResponse UserTokenResponse ;
            UserTokenResponse = mapper.Map<UserTokenResponse>(user);
            UserTokenResponse.IdSesion = 1;
            UserTokenResponse.StatusCode = HttpStatusCode.OK;
            UserTokenResponse.Token = await GenerateToken(user.UserName);
            return UserTokenResponse;   
        }
        private async Task<string> GenerateToken(string? userName = "")
        {
            var secretKey = (await configuiuracionRepository.GetByParam(x => x.Id.Equals(ParamConfig.JwtSecretKey.ToString())))?.Value ?? string.Empty;
            var jwtIssuerToken = (await configuiuracionRepository.GetByParam(x => x.Id.Equals(ParamConfig.JwtIssuerToken.ToString())))?.Value ?? string.Empty;
            var jwtAudienceToken = (await configuiuracionRepository.GetByParam(x => x.Id.Equals(ParamConfig.JwtIssuerToken.ToString())))?.Value ?? string.Empty;
            var jwtExpireTime = (await configuiuracionRepository.GetByParam(x => x.Id.Equals(ParamConfig.JwtExpireTime.ToString())))?.Value ?? string.Empty;

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            ClaimsIdentity claimsIdentity = new(new[] { new Claim(ClaimTypes.Name, userName) });
            var currentDate = DateTime.Now;
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = tokenHandler.CreateJwtSecurityToken(
                audience: jwtAudienceToken,
                issuer: jwtIssuerToken,
                subject: claimsIdentity,
                notBefore: currentDate,
                expires: currentDate.AddMinutes(Convert.ToInt32(jwtExpireTime)),
                signingCredentials: signingCredentials);
            var jwtTokenString = tokenHandler.WriteToken(jwtSecurityToken);
            return jwtTokenString;
        }      

        public async Task<UserResponse> CreateUser(UserRequest userRequest)
        {
            var userResponse = new UserResponse();

            try
            {
                var validateUser = await ValidateUserName(userRequest.UserName);
                if (validateUser is null)
                {
                    var usuario = mapper.Map<Usuario>(userRequest);
                    string pass = DecodeBase64Password(userRequest.Password);
                    usuario.Password = await EncryptedPassword(pass);
                    await InsertUser(usuario);
                    userResponse.UserName = usuario.UserName;
                    userResponse.IdRol = usuario.IdRol;
                    userResponse.StatusCode = HttpStatusCode.OK;
                    userResponse.Message = $"El usuario {usuario.UserName} a sido creado con exito";
                }
                else
                {
                    userResponse.StatusCode = HttpStatusCode.Conflict;
                    userResponse.Message = $"Ya existe un usuario con el nombre {userRequest.UserName}";
                }              

            }
            catch (Exception ex)
            {
                userResponse.StatusCode = HttpStatusCode.InternalServerError;
                userResponse.Message = ex.Message;
            }
            return userResponse;
        }      
        private async Task<string> EncryptedPassword(string password)
        {
            var keyEncrypted = (await configuiuracionRepository.GetByParam(x => x.Id.Equals(ParamConfig.KeyEncrypted.ToString())))?.Value ?? string.Empty;
            var iVEncrypted = (await configuiuracionRepository.GetByParam(x => x.Id.Equals(ParamConfig.IVEncrypted.ToString())))?.Value ?? string.Empty;


            byte[] key = Encoding.UTF8.GetBytes(keyEncrypted);
            byte[] iv = Encoding.UTF8.GetBytes(iVEncrypted);

            using (TripleDES aes = TripleDES.Create())
            {
                             

                ICryptoTransform encryptor = aes.CreateEncryptor(key, iv);

                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] encryptedPasswordBytes = encryptor.TransformFinalBlock(passwordBytes, 0, passwordBytes.Length);

                string encryptedPassword = Convert.ToBase64String(encryptedPasswordBytes);
                return encryptedPassword;
            }            
        }
        private async Task InsertUser(Usuario usuario)
        {
            await usuarioRepository.Insert(usuario);
        }
         
        public async Task<BaseResponse> UpdateUser(UserCreateRequest userRequest)
        {
            var  baseResponse = new BaseResponse();
            try
            {
                var user = await GetUserById(userRequest.Id);
                if (user is not null)
                {
                    await UpdateUser(user, userRequest);
                    baseResponse = GetResponse("Usuario actualizado con exito", HttpStatusCode.OK);
                }
                else
                {
                    baseResponse = GetResponse("Id Usuario no encontrado", HttpStatusCode.NotFound);
                }

            }
            catch (Exception ex)
            {
                baseResponse.StatusCode = HttpStatusCode.InternalServerError; 
                baseResponse.Message = ex.Message;
            }
            return baseResponse;
        }
        private async Task<Usuario> GetUserById(int idUser)
        {
            return await usuarioRepository.GetById(idUser);
        }
        private async Task UpdateUser(Usuario usuario, UserCreateRequest userRequest)
        {

            usuario.UserName = userRequest.UserName;
            if (!string.IsNullOrEmpty(userRequest.Password))
            {
                string pass = DecodeBase64Password(userRequest.Password);
                usuario.Password = await EncryptedPassword(pass);
            }
            usuario.IdRol = userRequest.IdRol;
            await usuarioRepository.Update(usuario);
        }
        private BaseResponse GetResponse(string mensaje, HttpStatusCode httpStatusCode)
        {
            return new BaseResponse()
            {
                StatusCode = httpStatusCode,
                Message = mensaje,
            };
        }

        public async Task<List<UsersResponse>> GetUser() 
        {
            List<UsersResponse> list;
            try
            {
                var users = await GetUsuarios();
                list = MapperUserResponse(users);
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private async Task<List<Usuario>> GetUsuarios()
        {
            return await usuarioRepository.GetAllByParamIncluding(null, (x => x.Rol));
        }
        private List<UsersResponse> MapperUserResponse(List<Usuario> usuarios)
        {
            var list = new List<UsersResponse>();    
            if (usuarios is not null &&  usuarios.Count > 0)
            {
                foreach (var item in usuarios)
                { 
                    var userResponse = new UsersResponse();
                    userResponse.UserName = item.UserName;
                    userResponse.DescripcionRol = item.Rol.Description;
                    userResponse.IdRol = item.IdRol;
                    list.Add(userResponse);
                }
            }
            return list;
        }

        public async Task<UserResponse> GetUserId(int idUser)
        {
            var userResponse = new UserResponse();
            try
            {
                var user = await GetUserById(idUser);
                if (user is not null)
                {
                    userResponse  = mapper.Map<UserResponse>(user);
                    userResponse.StatusCode = HttpStatusCode.OK;
                }
                else
                {
                    userResponse.StatusCode = HttpStatusCode.NotFound;
                    userResponse.Message = "El id no se encuentra";
                }
            }
            catch (Exception ex)
            {
                userResponse.StatusCode=HttpStatusCode.InternalServerError;
                userResponse.Message = ex.Message;
            }
            return userResponse;
        }

        public async Task<List<SPProcessByUserResponse>> GetProccesByUser(int idUser)
        {

            List<SPProcessByUserResponse> list;
            try
            {
                var users = await GetSPProcessByUser(idUser);
                list = MapperSPProcessByUserResponse(users);
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private async Task<List<SPProcessByUser>> GetSPProcessByUser(int idUser)
        {
            return await storeProcedureRepository.GetProcessCandidateByUser(idUser);
        }
        private List<SPProcessByUserResponse> MapperSPProcessByUserResponse(List<SPProcessByUser> sPProcessByUsers)
        {
            var list = new List<SPProcessByUserResponse>();
            if (sPProcessByUsers is not null && sPProcessByUsers.Count >0)
            {
                foreach (var item in sPProcessByUsers)
                {

                    var sPProcessByUserResponse = mapper.Map<SPProcessByUserResponse>(item);
                    list.Add(sPProcessByUserResponse);
                }
            }
            return list;
        }

        public async Task<ReportingRejectedCandidatesResponse> GetRejectedCandidatesByUser(int idUser)
        {
            var reportingRejectedCandidatesResponse = new ReportingRejectedCandidatesResponse();
            try
            {
                var proceesRejected = await GetSPRejectedCandidatesByUser(idUser);
                reportingRejectedCandidatesResponse.UrlExcel = await SaveExcel(proceesRejected, idUser);
                reportingRejectedCandidatesResponse.StatusCode = HttpStatusCode.OK;
                reportingRejectedCandidatesResponse.Message = "Descarga Exitosa";
            }
            catch (Exception ex)
            {
                reportingRejectedCandidatesResponse.Message = ex.Message;
                reportingRejectedCandidatesResponse.StatusCode = HttpStatusCode.InternalServerError;                
            }
            return reportingRejectedCandidatesResponse; 
        }
        private async Task<List<SPRejectedCandidatesByUser>> GetSPRejectedCandidatesByUser(int idUser)
        {
            return await storeProcedureRepository.GetRejectedCandidatesByUser(idUser);
        }
        private async Task<string> SaveExcel(List<SPRejectedCandidatesByUser> sPRejectedCandidatesByUsers, int idUser) 
        {
            var pathExcel = (await configuiuracionRepository.GetByParam(x => x.Id.Equals(ParamConfig.PathExcelRejected.ToString())))?.Value ?? string.Empty;

            var Savefile = new SaveFiles();
            if (sPRejectedCandidatesByUsers is not null && sPRejectedCandidatesByUsers.Count > 0 )
            {
                var objectFileSaveExcel = new ObjectFileSaveExcel();
                objectFileSaveExcel.Lista = sPRejectedCandidatesByUsers;
                objectFileSaveExcel.IdUser = idUser;
                objectFileSaveExcel.Path = pathExcel;
                return Savefile.SaveExcel(objectFileSaveExcel);
            }
            return string.Empty;         
        }
    }
}
