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
    public class WorkModalityController : ControllerBase
    {
        private readonly ITipoTableService<object> modalidadTrabajoService;

        /// <summary>
        /// Constructor
        /// </summary>
        public WorkModalityController(ITipoTableService<object> modalidadTrabajoService)
        {
            this.modalidadTrabajoService = modalidadTrabajoService;
        }




        /// <summary>
        /// Obtener modalidad de trabajo 
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
                var modalidadTrabajo = await modalidadTrabajoService.GetList(TipoTabla.ModalidadTrabajo);
                return Ok(modalidadTrabajo);
            }
            catch (Exception)
            {
                return Problem();
            }
        }
    }
}
