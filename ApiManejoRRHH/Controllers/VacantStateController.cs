using ApiManejoRRHH.Helpers;
using Core.Interfaces;
using Domain.Common.Enum;
using Domain.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiManejoRRHH.Controllers
{
    /// <summary>
    /// Controlador de Categoria 
    /// </summary>

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class VacantStateController : ControllerBase
    {
        private readonly ITipoTableService<object> estadoVacanteService;

        /// <summary>
        /// Constructor
        /// </summary>
        public VacantStateController(ITipoTableService<object> estadoVacanteService)
        {
            this.estadoVacanteService= estadoVacanteService;
        }




        /// <summary>
        /// Obtener Estados de la vacante 
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
                var estadoVacante = await estadoVacanteService.GetList(TipoTabla.EstadoVacante);
                return Ok(estadoVacante);
            }
            catch (Exception)
            {
                return Problem();
            }
        }

    }
}
