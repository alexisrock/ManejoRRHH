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
    /// Controlador de tipo de contratos 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ContractTypeController : ControllerBase
    {
        private readonly ITipoTableService<TipoContratoRequest> tipoTableService;

        /// <summary>
        /// Constructor
        /// </summary>

        public ContractTypeController(ITipoTableService<TipoContratoRequest> tipoTableService)
        {
            this.tipoTableService = tipoTableService;
        }



        /// <summary>
        /// Metodo de creacion del Tipo de contrato   
        /// </summary>         
        ///<param name="objectRequest">  
        /// <strong> Description : </strong> : Descripcion del tipo de contrato <strong> * Obligatorio </strong>   
        /// </param>    
        /// <returns></returns>
        /// /// <remarks>
        /// Request de ejemplo:
        ///  
        ///     {
        ///      
        ///        "Description": "Termino indefinido"         
        /// 
        ///     }
        ///
        /// </remarks>

        [HttpPost, Route("[action]")]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] TipoContratoRequest objectRequest)
        {
            try
            {
                var contract = await tipoTableService.Create(objectRequest);
                if (contract.StatusCode == HttpStatusCode.OK)
                    return Ok(contract);
                else
                    return Problem(contract.Message, statusCode: (int)contract.StatusCode);
            }
            catch (Exception)
            {
                return Problem();
            }
        }


        /// <summary>
        /// Metodo de actualizacion del Tipo de contrato   
        /// </summary>
        ///<param name="objectRequest">
        /// <strong> IdContrato : </strong> Numero Id del tipo de contrato  <strong> * Obligatorio </strong> <br/>
        /// <strong> Description : </strong> : Descripcion del tipo de contrato <strong> * Obligatorio </strong>   
        /// </param>    
        /// <returns></returns>
        /// /// <remarks>
        /// Request de ejemplo:
        ///  
        ///     {
        ///        "IdContrato":  1,  
        ///        "Description": "Termino indefinido"         
        /// 
        ///     }
        ///
        /// </remarks>


        [HttpPut, Route("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromBody] TipoContratoRequest objectRequest)
        {
            try
            {
                var contract = await tipoTableService.Update(objectRequest);
                if (contract.StatusCode == HttpStatusCode.OK)
                    return Ok(contract);
                else
                    return Problem(contract.Message, statusCode: (int)contract.StatusCode);
            }
            catch (Exception)
            {
                return Problem();
            }
        }


        /// <summary>
        /// Obtener tipos de contratos 
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
                var contract = await tipoTableService.GetList(TipoTabla.Contrato);
                return Ok(contract);
            }
            catch (Exception)
            {
                return Problem();
            }
        }


     
    }
}
