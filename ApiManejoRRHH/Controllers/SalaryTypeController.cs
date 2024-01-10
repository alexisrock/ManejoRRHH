using Core.Interfaces;
using Domain.Common.Enum;
using Domain.Common;
using Domain.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using ApiManejoRRHH.Helpers;

namespace ApiManejoRRHH.Controllers
{
    /// <summary>
    /// Controlador de Tipo de salarios 
    /// </summary>

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SalaryTypeController : ControllerBase
    {

        private readonly ITipoTableService<TipoSalarioRequest> tipoTableService;

        /// <summary>
        /// Constructor
        /// </summary>

        public SalaryTypeController(ITipoTableService<TipoSalarioRequest> tipoTableService)
        {
            this.tipoTableService = tipoTableService;
        }



        /// <summary>
        /// Metodo de creacion del Tipo de salario   
        /// </summary>         
        ///<param name="objectRequest">
        /// <strong> Description : </strong> : Descripcion del tipo de salario <strong> * Obligatorio </strong>   
        /// </param>    
        /// <returns></returns>
        /// /// <remarks>
        /// Request de ejemplo:
        ///  
        ///     {
        ///       
        ///        "Description": "Salario 64 / 40"         
        /// 
        ///     }
        ///
        /// </remarks>
        [HttpPost, Route("[action]")]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] TipoSalarioRequest objectRequest)
        {
            try
            {
                var salary = await tipoTableService.Create(objectRequest);
                if (salary.StatusCode == HttpStatusCode.OK)
                    return Ok(salary);
                else
                    return Problem(salary.Message, statusCode: (int)salary.StatusCode);
            }
            catch (Exception)
            {
                return Problem();
            }
        }

        /// <summary>
        /// Metodo de actualizacion del Tipo de salario   
        /// </summary>
        ///<param name="objectRequest">
        ///  <strong> IdSalario : </strong> Numero Id del tipo de salario <strong> * Obligatorio </strong> <br/>
        /// <strong> Description : </strong> : Descripcion del tipo de salario <strong> * Obligatorio </strong>   
        /// </param>    
        /// <returns></returns>
        /// /// <remarks>
        /// Request de ejemplo:
        ///  
        ///     {
        ///        "IdSalario": 1,
        ///        "Description": "Salario 64 / 40"         
        /// 
        ///     }
        ///
        /// </remarks>
        [HttpPut, Route("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromBody] TipoSalarioRequest objectRequest)
        {
            try
            {
                var salary = await tipoTableService.Update(objectRequest);
                if (salary.StatusCode == HttpStatusCode.OK)
                    return Ok(salary);
                else
                    return Problem(salary.Message, statusCode: (int)salary.StatusCode);
            }
            catch (Exception)
            {
                return Problem();
            }
        }


        /// <summary>
        /// Obtener tipos de salario 
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
                var salary = await tipoTableService.GetList(TipoTabla.Salario);
                return Ok(salary);
            }
            catch (Exception)
            {
                return Problem();
            }
        }


     
    }
}
