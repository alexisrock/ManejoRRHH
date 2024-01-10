using ApiManejoRRHH.Helpers;
using Core.Interfaces; 
using Domain.Common; 
using Domain.Dto; 
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
    public class VacantController : ControllerBase
    {


        private readonly IVacantService vacantService;


        /// <summary>
        /// Constructor
        /// </summary>
        public VacantController(IVacantService vacantService)
        {
            this.vacantService = vacantService;
        }



        /// <summary>
        /// Metodo de creacion del Vacante       
        /// </summary>
        ///<param name="vacanteRequest">
        /// <strong> IdUser : </strong>    Id del usuario que se logueo en el sistema <br/>
        /// <strong> IdCliente : </strong> Numero Id del cliente <strong> * Obligatorio </strong> <br/>
        /// <strong> DescripcionCargo : </strong> :  nombre de la vacante o del cargo vacante <strong> * Obligatorio </strong> <br/>
        /// <strong> Profesion : </strong> :  profesion que requiere la vacante <strong> * Obligatorio </strong> <br/>
        /// <strong> TiempoExperiencia : </strong> :  Tiempo de experiencia requerido <strong> * Obligatorio </strong> <br/>
        /// <strong> IdContrato : </strong> :  numero id del tipo de contrato de la vacante <strong> * Obligatorio </strong> <br/>
        /// <strong> IdSalario : </strong> :  numero id del tipo de salario de la vacante <strong> * Obligatorio </strong> <br/>
        ///  Horario: horario de la jornada laboral<br/>
        /// <strong> IdModalidadTrabajo : </strong> :  numero id del tipo de salario de la vacante <strong> * Obligatorio </strong> <br/>
        ///  Idioma: idioma requerido de la vacante<br/>
        ///  PorcentajeIdioma: Porcentaje del idioma requerido de la vacante<br/>
        ///  PruebaTecnica: campo que indica si la vacante requiere prueba tecnica<br/>
        ///  DescripcionFunciones: funciones que tiene la vacante<br/>
        ///  Comentarios: comentarios adicionales de la vacante<br/>
        ///  <strong> IdUser : </strong>    Id del usuario que se logueo en el sistema <br/>   
        ///  ListSkillsVacante: lista con las skill que requiere la vacante <br/>
        ///  <strong> IdCategoria : </strong>    Id de la categoria <br/>   
        ///  DescripcionSkill: descripcion de la categoria o de la habilidad <br/>
        ///  Activo: Valor true o falso para activar la habilidad de la vacante
        /// </param>    
        /// <returns></returns>
        /// /// <remarks>
        /// Request de ejemplo:
        ///  
        ///     {
        ///        "IdUser": 1,
        ///        "IdCliente": 1,
        ///        "DescripcionCargo": "Jaba backend senior",
        ///        "Profesion": "ing de sistemas, ing de software etc",
        ///        "TiempoExperiencia": 3,
        ///        "IdContrato": 1,
        ///        "IdSalario": 1,
        ///        "Horario": "lunes a viernes",
        ///        "IdModalidadTrabajo": 1,
        ///        "Horario": "lunes a viernes",        
        ///        "Idioma": "Ingles",
        ///        "PorcentajeIdioma": "80%",
        ///        "PruebaTecnica": true,
        ///        "DescripcionFunciones": "realizar los requerimientos tecnicos, realizar test unitarios",
        ///        "Comentarios": "",
        ///        "IdUser": 1,
        ///         "ListSkillsVacante": [
        ///             {
        ///               "IdCategoria": 0,
        ///               "DescripcionSkill": "string",
        ///               "Activo": true
        ///             }
        ///          ]
        ///     }
        ///
        /// </remarks>
        [HttpPost, Route("[action]")]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] VacanteRequest vacanteRequest)
        {
            try
            {
                var vacante = await vacantService.Create(vacanteRequest);
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
        /// Metodo de actualizacion de la Vacante     
        /// </summary>
        ///<param name="vacanteRequest">
        /// <strong> IdVacante : </strong>  Numero  Id de la vacante <br/>
        /// <strong> IdUser : </strong>    Id del usuario que se logueo en el sistema <br/>
        /// <strong> IdCliente : </strong> Numero Id del cliente <strong> * Obligatorio </strong> <br/>
        /// <strong> DescripcionCargo : </strong> :  nombre de la vacante o del cargo vacante <strong> * Obligatorio </strong> <br/>
        /// <strong> Profesion : </strong> :  profesion que requiere la vacante <strong> * Obligatorio </strong> <br/>
        /// <strong> TiempoExperiencia : </strong> :  Tiempo de experiencia requerido <strong> * Obligatorio </strong> <br/>
        /// <strong> IdContrato : </strong> :  numero id del tipo de contrato de la vacante <strong> * Obligatorio </strong> <br/>
        /// <strong> IdSalario : </strong> :  numero id del tipo de salario de la vacante <strong> * Obligatorio </strong> <br/>
        ///  Horario: horario de la jornada laboral<br/>
        /// <strong> IdModalidadTrabajo : </strong> :  numero id del tipo de salario de la vacante <strong> * Obligatorio </strong> <br/>
        ///  Idioma: idioma requerido de la vacante<br/>
        ///  PorcentajeIdioma: Porcentaje del idioma requerido de la vacante<br/>
        ///  PruebaTecnica: campo que indica si la vacante requiere prueba tecnica<br/>
        ///  DescripcionFunciones: funciones que tiene la vacante<br/>
        ///  Comentarios: comentarios adicionales de la vacante<br/>
        ///  <strong> IdUser : </strong>    Id del usuario que se logueo en el sistema <br/>   
        ///  ListSkillsVacante: lista con las skill que requiere la vacante <br/>
        ///  <strong> IdCategoria : </strong>    Id de la categoria <br/>   
        ///  DescripcionSkill: descripcion de la categoria o de la habilidad <br/>
        ///  Activo: Valor true o falso para activar la habilidad de la vacante
        /// </param>    
        /// <returns></returns>
        /// /// <remarks>
        /// Request de ejemplo:
        ///  
        ///     {
        ///        "IdVacante": 1,
        ///        "IdUser": 1,
        ///        "IdCliente": 1,
        ///        "DescripcionCargo": "Jaba backend senior",
        ///        "Profesion": "ing de sistemas, ing de software etc",
        ///        "TiempoExperiencia": 3,
        ///        "IdContrato": 1,
        ///        "IdSalario": 1,
        ///        "Horario": "lunes a viernes",
        ///        "IdModalidadTrabajo": 1,
        ///        "Horario": "lunes a viernes",        
        ///        "Idioma": "Ingles",
        ///        "PorcentajeIdioma": "80%",
        ///        "PruebaTecnica": true,
        ///        "DescripcionFunciones": "realizar los requerimientos tecnicos, realizar test unitarios",
        ///        "Comentarios": "",
        ///        "IdUser": 1,
        ///         "ListSkillsVacante": [
        ///             {
        ///               "IdCategoria": 0,
        ///               "DescripcionSkill": "string",
        ///               "Activo": true
        ///             }
        ///          ]
        ///     }
        ///
        /// </remarks>

        [HttpPut, Route("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromBody] VacanteRequest vacanteRequest)
        {
            try
            {
                var vacante = await vacantService.Update(vacanteRequest);
                if (vacante.StatusCode == HttpStatusCode.OK)
                    return Ok(vacante);
                else
                    return Problem(detail: vacante.Message, statusCode: (int)vacante.StatusCode);
            }
            catch (Exception)
            {
                return Problem();
            }
        }

        /// <summary>
        /// Metodo para cambiar el estado de la vacante  
        /// </summary>
        ///<param name="vacanteStateRequest">
        /// <strong> IdVacante : </strong>  Numero  Id de la vacante <strong> * Obligatorio </strong> <br/>
        /// <strong> IdEstadoVacante : </strong> :  Numero Id del estado de la vacante <strong> * Obligatorio </strong> <br/>  
        /// <strong> IdUser : </strong>    Id del usuario que se logueo en el sistema <strong> * Obligatorio </strong> 
        /// </param>    
        /// <returns></returns>
        /// /// <remarks>
        /// Request de ejemplo:
        ///  
        ///     {
        ///        "IdVacante": 1,
        ///        "IdEstadoVacante": 1,
        ///        "IdUser": 1
        /// 
        ///     }
        ///
        /// </remarks>

        [HttpPut, Route("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateState([FromBody] VacanteStateRequest vacanteStateRequest)
        {
            try
            {
                var vacante = await vacantService.UpdateState(vacanteStateRequest);
                if (vacante.StatusCode == HttpStatusCode.OK)
                    return Ok(vacante);
                else
                    return Problem(detail: vacante.Message, statusCode: (int)vacante.StatusCode);
            }
            catch (Exception)
            {
                return Problem();
            }
        }


        /// <summary>
        /// Obtener Vacantes 
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
                var vacantes = await vacantService.GetAllVacantes();
                return Ok(vacantes);
            }
            catch (Exception)
            {
                return Problem();
            }
        }


        /// <summary>
        /// Obtener datos por Id de la vacante  
        /// </summary>
        ///<param name="idVacante">
        /// <strong> IdCliente : </strong> Numero Id de la vacante <strong> * Obligatorio </strong>  
        /// </param>
        /// <returns></returns> 

        [HttpGet, Route("[action]/{idVacante}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(int idVacante)
        {
            try
            {
                var vacante = await vacantService.GetById(idVacante);
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
        /// Obtener skill por Id de la vacante  
        /// </summary>
        ///<param name="idVacante">
        /// <strong> IdCliente : </strong> Numero Id de la vacante <strong> * Obligatorio </strong>  
        /// </param>
        /// <returns></returns> 
        [HttpGet, Route("[action]/{idVacante}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SkillVacanteById(int idVacante)
        {
            try
            {
                var vacantes = await vacantService.GetSkillsByIdVacante(idVacante);
                return Ok(vacantes);
            }
            catch (Exception)
            {
                return Problem();
            }
        }



    }
}
