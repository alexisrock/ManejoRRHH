using ApiManejoRRHH.Helpers;
using Core.Interfaces;
using Core.Repository;
using Domain.Common;
using Domain.Dto;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ApiManejoRRHH.Controllers
{
    /// <summary>
    /// Controlador de Cliente
    /// </summary>

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ComisionController : ControllerBase
    {

        private readonly IComisionService comisionService;

        /// <summary>
        /// Constructor
        /// </summary>
        public ComisionController(IComisionService comisionService)
        {
            this.comisionService = comisionService;
        }

        /// <summary>
        /// Metodo de actualizaacion de  la comision      
        /// </summary>
        ///<param name="comisionRequest">
        /// <strong> IdComision : </strong>    No enviar <br/>
        /// <strong> IdUsuarioComision : </strong>    Id del usuario que realizo la contratacion  <br/>
        /// <strong> IdEmpleado : </strong>  Numero id del empleado  <br/>
        ///  FechaIngreso:  Fecha de ingreso del empleado <br/>
        ///  Activo:  estado de la solicitud
        ///  
        /// </param>    
        /// <returns></returns>
        /// /// <remarks>
        /// Request de ejemplo:
        ///  
        ///     {
        ///        "IdComision": 1,
        ///        "IdUsuarioComision": 1,
        ///        "IdEmpleado": 1,
        ///        "FechaIngreso": "2023-05-06",
        ///        "Activo": true
        /// 
        ///     }
        ///
        /// </remarks>



        [HttpPost, Route("[action]")]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] ComisionRequest comisionRequest)
        {
            try
            {
                var comision = await comisionService.Create(comisionRequest);
                if (comision.StatusCode == HttpStatusCode.OK)
                    return Ok(comision);
                else
                    return Problem(comision.Message, statusCode: (int)comision.StatusCode);
            }
            catch (Exception)
            {
                return Problem();
            }
        }



        /// <summary>
        /// Metodo de actualizaacion de  la comision      
        /// </summary>
        ///<param name="comisionRequest">
        /// <strong> IdComision : </strong>    Numero Id de la comision <br/>
        /// <strong> IdUsuarioComision : </strong>    Id del usuario que realizo la contratacion  <br/>
        /// <strong> IdEmpleado : </strong>  Numero id del empleado  <br/>
        ///  FechaIngreso:  Fecha de ingreso del empleado <br/>
        ///  Activo:  estado de la solicitud
        ///  
        /// </param>    
        /// <returns></returns>
        /// /// <remarks>
        /// Request de ejemplo:
        ///  
        ///     {
        ///        "IdComision": 1,
        ///        "IdUsuarioComision": 1,
        ///        "IdEmpleado": 1,
        ///        "FechaIngreso": "2023-05-06",
        ///        "Activo": true
        /// 
        ///     }
        ///
        /// </remarks>


        [HttpPut, Route("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromBody] ComisionRequest comisionRequest)
        {
            try
            {
                var comision = await comisionService.Update(comisionRequest);
                if (comision.StatusCode == HttpStatusCode.OK)
                    return Ok(comision);
                else
                    return Problem(comision.Message, statusCode: (int)comision.StatusCode);
            }
            catch (Exception)
            {
                return Problem();
            }
        }


        /// <summary>
        /// Metodo de actualizacion del estado de la comision      
        /// </summary>
        ///<param name="comisionStatusRequest">
        /// <strong> IdComision : </strong>    Numero Id de la comision <br/>  
        ///  Activo:  estado de la solicitud true o false
        ///  
        /// </param>    
        /// <returns></returns>
        /// /// <remarks>
        /// Request de ejemplo:
        ///  
        ///     {
        ///        "IdComision": 1,    
        ///        "Activo": false
        /// 
        ///     }
        ///
        /// </remarks>


        [HttpPut, Route("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateState([FromBody] ComisionStatusRequest comisionStatusRequest)
        {
            try
            {
                var comision = await comisionService.UpdateState(comisionStatusRequest);
                if (comision.StatusCode == HttpStatusCode.OK)
                    return Ok(comision);
                else
                    return Problem(comision.Message, statusCode: (int)comision.StatusCode);
            }
            catch (Exception)
            {
                return Problem();
            }
        }


        /// <summary>
        /// Obtener comision por id    
        /// </summary>
        ///<param name="idComision">
        /// <strong> idComision : </strong>      Numero Id de la comision  <strong> * Obligatorio </strong> 
        ///</param>
        /// <returns></returns>      

        [HttpGet, Route("[action]/{idComision}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByIdComision(long idComision)
        {
            try
            {
                var comision = await comisionService.GetComisionById(idComision);
                if (comision.StatusCode == HttpStatusCode.OK)
                    return Ok(comision);
                else
                    return Problem(comision.Message, statusCode: (int)comision.StatusCode);
            }
            catch (Exception)
            {
                return Problem();
            }
        }



        /// <summary>
        /// Obtener comoisiones 
        /// </summary>         
        /// <returns></returns>
        [HttpGet, Route("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetComisiones()
        {
            try
            {
                var comosiones = await comisionService.GetComisiones();
                return Ok(comosiones);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }


    }
}
