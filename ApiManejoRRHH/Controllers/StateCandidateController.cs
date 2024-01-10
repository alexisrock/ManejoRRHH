using ApiManejoRRHH.Helpers;
using Core.Interfaces;
using Domain.Common.Enum;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiManejoRRHH.Controllers
{
    /// <summary>
    /// Controlador de vacante 
    /// </summary>

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StateCandidateController : ControllerBase
    {

        private readonly ITipoTableService<object> estadoCandidatoService;

        /// <summary>
        /// Constructor
        /// </summary>
        public StateCandidateController(ITipoTableService<object> estadoCandidatoService)
        {
            this.estadoCandidatoService = estadoCandidatoService;
        }




        /// <summary>
        /// Obtener estado candidato
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
                var modalidadTrabajo = await estadoCandidatoService.GetList(TipoTabla.EstadoCandidato);
                return Ok(modalidadTrabajo);
            }
            catch (Exception)
            {
                return Problem();
            }
        }




    }
}
