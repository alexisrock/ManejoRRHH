using ApiManejoRRHH.Helpers;
using Core.Interfaces;
using Domain.Common.Enum;
using Domain.Dto;
using Microsoft.AspNetCore.Mvc;

namespace ApiManejoRRHH.Controllers
{
    /// <summary>
    /// Controlador de Tipo documento 
    /// </summary>

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DocumentTypeController 
        : ControllerBase
    {

        private readonly ITipoTableService<object> tipoTableService;

        /// <summary>
        /// Constructor
        /// </summary>
        public DocumentTypeController(ITipoTableService<object> tipoTableService)
        {
            this.tipoTableService = tipoTableService;
        }


        /// <summary>
        /// Obtener Tipo de documentos 
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
                var tipòDocumento = await tipoTableService.GetList(TipoTabla.TipoDocumento);
                return Ok(tipòDocumento);
            }
            catch (Exception)
            {
                return Problem();
            }
        }

    }
}
