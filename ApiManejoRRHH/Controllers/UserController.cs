using ApiManejoRRHH.Helpers;
using Core.Interfaces;
using Core.Repository;
using Domain.Dto;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ApiManejoRRHH.Controllers
{

    /// <summary>
    /// Controlador de Usuario
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        /// <summary>
        /// Constructor
        /// </summary>
        public UserController(IUserService userService)
        {
            this.userService = userService;
        }




        /// <summary>
        /// Metodo de creacion del usuario       
        /// </summary>
        ///<param name="userRequest">
        /// <strong> UserName : </strong> nombre de usuario que registro <strong>* Obligatorio</strong> <br/>
        /// <strong> Password : </strong> contraseña registrada del usuario codificada en base 64 <strong>* Obligatorio</strong> <br/>
        /// <strong> IdRol : </strong> Numero id del rol <strong>* Obligatorio</strong>
        /// </param>
        ///
        /// <returns></returns>
        /// /// <remarks>
        /// Request de ejemplo:
        ///  
        ///     {
        ///        "UserName": "prueba",
        ///        "Password": "cHJ1RUJB",
        ///        "IdRol": 1      
        ///     }
        ///
        /// </remarks>

        [HttpPost, Route("[action]")]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] UserRequest userRequest)
        {
            try
            {
                var user = await userService.CreateUser(userRequest);
                if (user.StatusCode == HttpStatusCode.OK)
                    return Ok(user);
                else
                    return Problem(user.Message, statusCode: (int)user.StatusCode);
            }
            catch (Exception)
            {
                return Problem();
            }

        }




        /// <summary>
        /// Metodo de actualizacion del usuario   
        /// </summary>
        ///<param name="userRequest">
        /// <strong> Id : </strong> numero Id del usuario que registro <strong>* Obligatorio</strong> <br/>
        /// <strong> UserName : </strong> nombre de usuario que registro <strong>* Obligatorio</strong> <br/>
        /// <strong> Password : </strong> contraseña registrada del usuario codificada en base 64 <strong>* Obligatorio</strong> <br/>
        /// <strong> IdRol : </strong> Numero id del rol <strong>* Obligatorio</strong>
        /// </param>
        ///
        /// <returns></returns>
        /// /// <remarks>
        /// Request de ejemplo:
        ///  
        ///     {
        ///        "Id": 1,  
        ///        "UserName": "prueba",
        ///        "Password": "cHJ1RUJB",
        ///        "IdRol": 1      
        ///     }
        ///
        /// </remarks>
        [HttpPut, Route("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromBody] UserCreateRequest userRequest)
        {
            try
            {
                var vacante = await userService.UpdateUser(userRequest);
                if (vacante.StatusCode == HttpStatusCode.OK)
                    return Ok(vacante);
                else
                    return Problem(detail: vacante.Message, statusCode: (int)vacante.StatusCode);
            }
            catch (Exception)
            {
                return Problem();
            }
        }




        /// <summary>
        /// Obtener Usuarios 
        /// </summary>         
        /// <returns></returns> 

        [HttpGet, Route("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var vacantes = await userService.GetUser();
                return Ok(vacantes);
            }
            catch (Exception)
            {
                return Problem();
            }
        }




        /// <summary>
        /// Obtener datos por Id del Usuario  
        /// </summary>
        ///<param name="idUser">
        /// <strong> idUser : </strong> numero Id del usuario que registro <strong>* Obligatorio</strong> 
        /// </param>
        /// <returns></returns>  

        [HttpGet, Route("[action]/{idUser}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(int idUser)
        {
            try
            {
                var user = await userService.GetUserId(idUser);
                if (user.StatusCode == HttpStatusCode.OK)
                    return Ok(user);
                else
                    return Problem(user.Message, statusCode: (int)user.StatusCode);
            }
            catch (Exception)
            {
                return Problem();
            }
        }



        /// <summary>
        /// Obtener procesos por Id del Usuario  
        /// </summary>
        ///<param name="idUser">
        /// <strong> idUser : </strong> numero Id del usuario que registro <strong>* Obligatorio</strong> 
        /// </param>
        /// <returns></returns>  

        [HttpGet, Route("[action]/{idUser}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetProccessByUser(int idUser)
        {
            try
            {
                var processByUser = await userService.GetProccesByUser(idUser);
                return Ok(processByUser);
            }
            catch (Exception)
            {
                return Problem();
            }
        }


        /// <summary>
        /// Obtener candidatos rechazados por Id del Usuario  
        /// </summary>
        ///<param name="idUser">
        /// <strong> idUser : </strong> numero Id del usuario que registro <strong>* Obligatorio</strong> 
        /// </param>
        /// <returns></returns>  

        [HttpGet, Route("[action]/{idUser}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetrejectedProcessByUser(int idUser)
        {
            try
            {
                var user = await userService.GetRejectedCandidatesByUser(idUser);
                if (user.StatusCode == HttpStatusCode.OK)
                    return Ok(user);
                else
                    return Problem(user.Message, statusCode: (int)user.StatusCode);
            }
            catch (Exception)
            {
                return Problem();
            }
        }


    }
}
