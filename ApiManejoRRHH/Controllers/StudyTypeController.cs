using ApiManejoRRHH.Helpers;
using Core.Interfaces;
using Domain.Common.Enum;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiManejoRRHH.Controllers
{
    /// <summary>
    /// Controlador de tipo estudio 
    /// </summary>

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StudyTypeController : ControllerBase
    {
        private readonly ITipoTableService<object> tipoEstudioService;

        /// <summary>
        /// Constructor
        /// </summary>
        public StudyTypeController(ITipoTableService<object> tipoEstudioService)
        {
            this.tipoEstudioService = tipoEstudioService;
        }


        /// <summary>
        /// Obtener tipo estudio 
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
                var modalidadTrabajo = await tipoEstudioService.GetList(TipoTabla.TipoEstudio);
                return Ok(modalidadTrabajo);
            }
            catch (Exception)
            {
                return Problem();
            }
        }




    }
}
