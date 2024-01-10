using ApiManejoRRHH.Helpers;
using Core.Interfaces;
using Domain.Common.Enum;
using Domain.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiManejoRRHH.Controllers
{
    /// <summary>
    /// Controlador de tipo de novedad 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NoveltyTypeController : ControllerBase
    {

        private readonly ITipoTableService<object> tipoTableService;

        /// <summary>
        /// Constructor
        /// </summary>
        public NoveltyTypeController(ITipoTableService<object> tipoTableService)
        {
            this.tipoTableService = tipoTableService;
        }




        /// <summary>
        /// Obtener tipos de novedad 
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
                var contract = await tipoTableService.GetList(TipoTabla.TipoNovedad);
                return Ok(contract);
            }
            catch (Exception)
            {
                return Problem();
            }
        }
    }
}
