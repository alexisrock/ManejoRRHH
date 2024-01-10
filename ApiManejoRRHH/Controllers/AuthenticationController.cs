using Domain.Dto;
using Core.Interfaces;
using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace ApiManejoRRHH.Controllers
{
    /// <summary>
    /// Controlador de Autenticacion
    /// </summary>

    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {

       
        private readonly IUserService userService;

        /// <summary>
        /// Constructor
        /// </summary>
        public AuthenticationController(  IUserService userService)
        {             
            this.userService = userService;
        }



        /// <summary>
        /// Metodo de autenticacion       
        /// </summary>
        ///<param name="userTokenRequest">
        /// <strong> UserName : </strong> nombre de usuario que registro <strong>* Obligatorio</strong> <br/>
        /// <strong> Password : </strong> contraseña registrada del usuario codificada en base 64 <strong>* Obligatorio</strong>
        /// </param>
        ///
        /// <returns></returns>
        /// /// <remarks>
        /// Request de ejemplo:
        ///  
        ///     {
        ///        "UserName": "prueba",
        ///        "Password": "cHJ1RUJB"
        /// 
        ///     }
        ///
        /// </remarks>

        [HttpPost, Route("[action]")]
        [ProducesResponseType(typeof(UserTokenResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Authentication([FromBody] UserTokenRequest userTokenRequest)
        {
            try
            {
                var user = await userService.GetAuthentication(userTokenRequest);
                if (user.StatusCode == HttpStatusCode.OK)
                    return Ok(user);
                else
                    return Problem(user.Message, statusCode:(int)user.StatusCode);
            }
            catch (Exception)
            {
                return Problem();
            }

        }




    }
}
