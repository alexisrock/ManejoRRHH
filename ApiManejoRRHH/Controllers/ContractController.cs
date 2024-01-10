using ApiManejoRRHH.Helpers;
using Core.Interfaces;
using Core.Repository;
using Domain.Common;
using Domain.Common.Enum;
using Domain.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ApiManejoRRHH.Controllers
{
    /// <summary>
    /// Controlador de contratos 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ContractController : ControllerBase
    {
        private readonly IContratoService contratoService;

        /// <summary>
        /// Constructor
        /// </summary>
        public ContractController(IContratoService contratoService)
        {
            this.contratoService = contratoService;
        }


        /// <summary>
        /// Metodo de creacion del Contrato   
        /// </summary>         
        ///<param name="contratoRequest">
        /// <strong> IdUser : </strong>    Id del usuario que se logueo en el sistema <br/>
        /// <strong> IdCliente : </strong> Numero Id del cliente <strong> * Obligatorio </strong> <br/>
        /// <strong> IdProceso : </strong> Numero Id del  proceso <strong> * Obligatorio </strong> <br/>
        /// <strong> IdCandidato : </strong> :  Numero Id de la tabla del candidato  <strong> * Obligatorio </strong>   
        /// </param>    
        /// <returns></returns>
        /// /// <remarks>
        /// Request de ejemplo:
        ///  
        ///     {
        ///         
        ///        "IdProceso": 1,
        ///        "IdCandidato": 1,
        ///        "IdUser": 1,
        ///        "IdCliente": 1,
        /// 
        ///     }
        ///
        /// </remarks>


        [HttpPost, Route("[action]")]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] ContratoRequest contratoRequest)
        {
            try
            {
                var contract = await contratoService.Create(contratoRequest);
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
        /// Metodo de actualizacion del  contrato   
        /// </summary>
        ///<param name="contratoRequest">         
        /// <strong> IdUser : </strong>    Id del usuario que se logueo en el sistema <br/>
        /// <strong> IdContrato : </strong>    Id del contrato <br/>
        /// <strong> IdCliente : </strong> Numero Id del cliente <strong> * Obligatorio </strong> <br/>
        /// <strong> IdProceso : </strong> Numero Id del  proceso <strong> * Obligatorio </strong> <br/>
        /// <strong> IdCandidato : </strong> :  Numero Id de la tabla del candidato  <strong> * Obligatorio </strong>   
        /// </param>    
        /// <returns></returns>
        /// /// <remarks>
        /// Request de ejemplo:
        ///  
        ///     {
        ///         
        ///        "IdProceso": 1,
        ///        "IdCandidato": 1,
        ///        "IdUser": 1,
        ///        "IdCliente": 1,
        /// 
        ///     }
        ///
        /// </remarks>
        [HttpPut, Route("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromBody] ContratoRequest contratoRequest)
        {
            try
            {
                var contract = await contratoService.Update(contratoRequest);
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
        /// Metodo de que cambia el estado a activo/desactivo del contrato   
        /// </summary>
        ///<param name="contratoEstadoRequest">
        /// <strong> IdUser : </strong>    Id del usuario que se logueo en el sistema <br/>
        /// <strong> IdContrato : </strong>    Id del contrato <br/>      
        ///  Activo:  valor true o false para activar o desactivar el contrato
        ///  
        /// </param>    
        /// <returns></returns>
        /// /// <remarks>
        /// Request de ejemplo:
        ///  
        ///     {    
        ///        "IdCandidato": 1,
        ///        "IdUser": 1,
        ///        "Activo": true
        /// 
        ///     }
        ///
        /// </remarks>
        [HttpPut, Route("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateStateActive([FromBody] ContratoEstadoRequest contratoEstadoRequest)
        {
            try
            {
                var contract = await contratoService.UpdateEstadoActivoContrato(contratoEstadoRequest);
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
        /// Obtener  contratos 
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
                var contract = await contratoService.GetListContratos();
                return Ok(contract);
            }
            catch (Exception)
            {
                return Problem();
            }
        }

        /// <summary>
        /// Obtener  contratos 
        /// </summary>         
        ///<param name="idContrato">
        /// <strong> IdContrato : </strong>    Id del contrato  <strong> * Obligatorio </strong> 
        ///</param>
        /// <returns></returns>  
        [HttpGet, Route("[action]/{idContrato}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(long idContrato)
        {
            try
            {
                var contract = await contratoService.GetContratoById(idContrato);
                return Ok(contract);
            }
            catch (Exception)
            {
                return Problem();
            }
        }



    }
}
