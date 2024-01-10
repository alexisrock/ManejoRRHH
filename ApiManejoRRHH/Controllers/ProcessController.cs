using ApiManejoRRHH.Helpers;
using Core.Interfaces;
using Core.Repository;
using Domain.Common;
using Domain.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ApiManejoRRHH.Controllers
{
    /// <summary>
    /// Controlador de vacante 
    /// </summary>

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProcessController : ControllerBase
    {

        private readonly IProcesoService procesoService;


        /// <summary>
        /// Constructor
        /// </summary>
        public ProcessController(IProcesoService procesoService)
        {
            this.procesoService = procesoService;
        }




        /// <summary>
        /// Metodo de creacion del proceso        
        /// </summary>
        ///<param name="procesoRequest">
        /// <strong> Idvacante : </strong>    Id de la vacante <strong> * Obligatorio  </strong>  <br/>
        /// <strong> IdCandidato : </strong> :  Numero Id de la tabla del candidato  <strong> * Obligatorio  </strong>     
        /// </param>    
        /// <returns></returns>
        /// /// <remarks>
        /// Request de ejemplo:
        ///  
        ///     {
        ///        "Idvacante": 1,
        ///        "IdCandidato": 1 
        ///     }
        ///
        /// </remarks>


        [HttpPost, Route("[action]")]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] ProcesoRequest procesoRequest)
        {
            try
            {
                var process = await procesoService.Create(procesoRequest);
                if (process.StatusCode == HttpStatusCode.OK)
                    return Ok(process);
                else
                    return Problem(detail: process.Message, statusCode: (int)process.StatusCode);
            }
            catch (Exception)
            {
                return Problem();
            }
        }


        /// <summary>
        /// Metodo para actualizar  el estado del candidato dentro del proceso
        /// </summary>
        ///<param name="procesoEstadoRequest">
        /// <strong> IdProceso : </strong>   Numero Id del proceso <strong> * Obligatorio  </strong>  <br/>
        /// <strong> IdEstado : </strong> :  Numero Id del estado <strong> * Obligatorio  </strong>     
        /// </param>    
        /// <returns></returns>
        /// /// <remarks>
        /// Request de ejemplo:
        ///  
        ///     {
        ///        "IdProceso": 1,
        ///        "IdEstado": 2 
        ///     }
        ///
        /// </remarks>


        [HttpPut, Route("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateStateCandidato([FromBody] ProcesoEstadoRequest procesoEstadoRequest)
        {
            try
            {
                var process = await procesoService.UpdateDisponibilidaCandidato(procesoEstadoRequest);
                if (process.StatusCode == HttpStatusCode.OK)
                    return Ok(process);
                else
                    return Problem(detail: process.Message, statusCode: (int)process.StatusCode);
            }
            catch (Exception)
            {
                return Problem();
            }
        }



        /// <summary>
        /// Obtener candidatos por vacante 
        /// </summary>         
        ///<param name="idVacante">
        /// <strong> Idvacante : </strong>    Id de la vacante <strong> * Obligatorio  </strong>
        /// </param>
        /// <returns></returns>  
        [HttpGet, Route("[action]/{idVacante}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllByIdVacant(int idVacante)
        {
            try
            {
                var candidatos = await procesoService.GetCandidatosByVacante(idVacante);
                return Ok(candidatos);
            }
            catch (Exception)
            {
                return Problem();
            }
        }


    }
}
