using ApiManejoRRHH.Helpers;
using Core.Interfaces;
using Domain.Common;
using Domain.Dto; 
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ApiManejoRRHH.Controllers
{
    /// <summary>
    /// Controlador de Novedades
    /// </summary>

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NoveltyController : ControllerBase
    {

        private readonly INovedadService novedadService;

        public NoveltyController(INovedadService novedadService)
        {
            this.novedadService = novedadService;
        }





        /// <summary>
        /// Metodo de creacion del novedades       
        /// </summary>
        ///<param name="novedadRequest">        
        /// <strong> IdCandidato : </strong> :  Numero Id de la tabla del candidato  <strong> * Obligatorio </strong>   <br/>
        /// <strong> IdTipoNovedad : </strong> Numero Id del tipo de novedad  <strong> * Obligatorio </strong> <br/>
        /// Observacion:   observacion de la novedad <br/>
        /// Activo: Valor true o falso para activar la novedad <br/>
        /// Anio: año <br/>
        /// Mes: Mes <br/>
        /// Dia: Dia <br/>
        /// DiasIncapacidad: Dias de incapacidad <br/>
        /// DiasVacaciones: Dias de vacaciones <br/>
        /// DiasNoRemunerados: Dias no remunerados <br/>
        /// <strong> IdUser : </strong>    Id del usuario que se logueo en el sistema     
        /// </param>    
        /// <returns></returns>
        /// /// <remarks>
        /// Request de ejemplo:
        ///  
        ///     {
        ///        "IdCandidato": 1,
        ///        "IdTipoNovedad": 3,         
        ///         "Observacion": "el candidato presento una incapacidad",
        ///         "Activo": true,
        ///         "Anio": 2023,
        ///         "Mes": 9,
        ///         "Dia": 14,
        ///         "DiasIncapacidad": 2,
        ///         "DiasVacaciones": 0,
        ///         "DiasNoRemunerados": 0,
        ///         "IdUser": 1
        ///        
        /// 
        ///     }
        ///
        /// </remarks>
        [HttpPost, Route("[action]")]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] NovedadRequest novedadRequest)
        {
            try
            {
                var vacante = await novedadService.Create(novedadRequest);
                if (vacante.StatusCode == HttpStatusCode.OK)
                    return Ok(vacante);
                else
                    return Problem(vacante.Message, statusCode: (int)vacante.StatusCode);
            }
            catch (Exception)
            {
                return Problem();
            }
        }


        /// <summary>
        /// Metodo de actualizar el estado de la novedad
        /// </summary>
        ///<param name="novedadRequest">
        /// <strong> IdCandidato : </strong> :  Numero Id de la tabla del candidato  <strong> * Obligatorio </strong>  <br/>
        /// <strong> IdNovedad : </strong> :  Numero Id de la tabla del candidato  <strong> * Obligatorio </strong> <br/>  
        /// <strong> IdTipoNovedad : </strong> Numero Id del tipo de novedad  <strong> * Obligatorio </strong> <br/>
        /// Observacion:   observacion de la novedad <br/>
        /// Activo: Valor true o falso para activar la novedad <br/>
        /// Anio: año <br/>
        /// Mes: Mes <br/>
        /// Dia: Dia <br/>
        /// DiasIncapacidad: Dias de incapacidad <br/>
        /// DiasVacaciones: Dias de vacaciones <br/>
        /// DiasNoRemunerados: Dias no remunerados <br/>
        /// <strong> IdUser : </strong>    Id del usuario que se logueo en el sistema     
        /// </param>    
        /// <returns></returns>
        /// /// <remarks>
        /// Request de ejemplo:
        ///  
        ///     {
        ///        "IdNovedad": 1,
        ///        "IdCandidato": 1,
        ///        "IdTipoNovedad": 3,         
        ///        "Observacion": "el candidato presento una incapacidad",
        ///        "Activo": true,
        ///        "Anio": 2023,
        ///        "Mes": 9,
        ///        "Dia": 14,
        ///        "DiasIncapacidad": 2,
        ///        "DiasVacaciones": 0,
        ///        "DiasNoRemunerados": 0,
        ///        "IdUser": 1
        ///        
        /// 
        ///     }
        ///
        /// </remarks>
        [HttpPut, Route("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromBody] NovedadRequest novedadRequest)
        {
            try
            {
                var novedad = await novedadService.Update(novedadRequest);
                if (novedad.StatusCode == HttpStatusCode.OK)
                    return Ok(novedad);
                else
                    return Problem(detail: novedad.Message, statusCode: (int)novedad.StatusCode);
            }
            catch (Exception)
            {
                return Problem();
            }
        }



        /// <summary>
        /// Metodo de actualizar el estado de la novedad
        /// </summary>
        ///<param name="novedadStateRequest">
        /// <strong> IdNovedad : </strong> :  Numero Id de la tabla del candidato  <strong> * Obligatorio </strong>  <br/>
        /// <strong> IdUser : </strong>    Id del usuario que se logueo en el sistema <br/>
        /// Activo: Valor true o falso para activar la novedad    
        /// </param>    
        /// <returns></returns>
        /// /// <remarks>
        /// Request de ejemplo:
        ///  
        ///     {
        ///        "IdNovedad": 1,
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
        public async Task<IActionResult> UpdateState([FromBody] NovedadStateRequest novedadStateRequest)
        {
            try
            {
                var novedad = await novedadService.UpdateStateNovelty(novedadStateRequest);
                if (novedad.StatusCode == HttpStatusCode.OK)
                    return Ok(novedad);
                else
                    return Problem(detail: novedad.Message, statusCode: (int)novedad.StatusCode);
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
                var novedad = await novedadService.GetAll();
                return Ok(novedad);
            }
            catch (Exception)
            {
                return Problem();
            }
        }



        /// <summary>
        /// Obtener novedades por candidato 
        /// </summary>
        ///<param name="idCandidato">
        /// <strong> idCandidato : </strong>   Numero Id de la tabla del candidato  <strong> * Obligatorio </strong> 
        /// </param>
        /// <returns></returns> 
        [HttpGet, Route("[action]/{idCandidato}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(int idCandidato)
        {
            try
            {
                var novedad = await novedadService.GetNoveltyByCandidate(idCandidato);
                return Ok(novedad);
            }
            catch (Exception)
            {
                return Problem();
            }
        }

    }
}
