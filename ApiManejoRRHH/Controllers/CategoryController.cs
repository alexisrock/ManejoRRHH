using ApiManejoRRHH.Helpers;
using Core.Interfaces;
using Core.Repository;
using Domain.Common;
using Domain.Common.Enum;
using Domain.Dto;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ApiManejoRRHH.Controllers
{
    /// <summary>
    /// Controlador de Categoria 
    /// </summary>

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoryController : ControllerBase
    {

        private readonly ITipoTableService<CategoriaRequest> categoryService;

        /// <summary>
        /// Constructor
        /// </summary>

        public CategoryController(ITipoTableService<CategoriaRequest> categoryService)
        {
            this.categoryService = categoryService;
        }



        /// <summary>
        /// Metodo de creacion del cliente       
        /// </summary>
        ///<param name="categoriaRequest">
        ///  IdCategoria :   no es necesario enviarlo  <br/>
        ///  Description: descripcion de la categoria
        /// </param>
        ///
        /// <returns></returns>
        /// /// <remarks>
        /// Request de ejemplo:
        ///  
        ///     {
        ///          "Description": "Lenguajes de programacion"
        /// 
        ///     }
        ///
        /// </remarks>

        [HttpPost, Route("[action]")]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] CategoriaRequest categoriaRequest)
        {
            try
            {
                var client = await categoryService.Create(categoriaRequest);
                if (client.StatusCode == HttpStatusCode.OK)
                    return Ok(client);
                else
                    return Problem(client.Message, statusCode: (int)client.StatusCode);
            }
            catch (Exception)
            {
                return Problem();
            }
        }


        /// <summary>
        /// Metodo de actualizacion de la categoria     
        /// </summary>
        ///<param name="categoriaRequest">
        ///  <strong> IdCategoria : </strong>   Numero Id de la categoria <strong> * Obligatorio </strong> <br/>    
        ///  Description: descripcion de la categoria
        /// </param>
        ///
        /// <returns></returns>
        /// /// <remarks>
        /// Request de ejemplo:
        ///  
        ///     {
        ///         "IdCategoria": 1,
        ///         "Description": "Lenguajes de programacion"
        /// 
        ///     }
        ///
        /// </remarks>

        [HttpPut, Route("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromBody] CategoriaRequest categoriaRequest)
        {
            try
            {
                var client = await categoryService.Update(categoriaRequest);
                if (client.StatusCode == HttpStatusCode.OK)
                    return Ok(client);
                else
                    return Problem(client.Message, statusCode: (int)client.StatusCode);
            }
            catch (Exception)
            {
                return Problem();
            }
        }




        /// <summary>
        /// Obtener categorias 
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
                var categoria = await categoryService.GetList(TipoTabla.Categoria);
                return Ok(categoria);
            }
            catch (Exception)
            {
                return Problem();
            }
        }





    }
}
