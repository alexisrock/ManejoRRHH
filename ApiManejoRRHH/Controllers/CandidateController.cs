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
    public class CandidateController : ControllerBase
    {

        private readonly ICandidatoService candidatoService;

        /// <summary>
        /// Constructor
        /// </summary>
        public CandidateController(ICandidatoService candidatoService)
        {
            this.candidatoService = candidatoService;
        }


        /// <summary>
        /// Metodo de creacion del candidato       
        /// </summary>
        /// <param name="candidatoRequest">
        /// IdCandidato :  No es necesario enviarlo <br/>
        /// <strong> Documento : </strong> Numero de identificacion del candidato <strong> * Obligatorio </strong> <br/>
        /// <strong> IdTipoDocumento : </strong> Tipo de identificacion del candidato (Ver metodo   <strong>* Obligatorio </strong> <br/>
        /// <strong> PrimerNombre : </strong> Primer nomnbre del candidato  <strong>* Obligatorio </strong> <br/>
        /// SegundoNombre :   Segundo nombre del candidato   <br/>
        /// <strong> PrimerApellido : </strong> Primer apèllido del candidato   <strong>* Obligatorio </strong> <br/>
        /// SegundoApellido :   Segundo apèllido del candidato   <br/>
        /// NumeroTelefonico :   numero celular o telefonico del candidato   <br/>
        /// Correo :   correo electronico del candidato   <br/>
        /// Base64CV :   base 64 de la hoja de vida del candidato  <br/>
        /// IdUser:   Id del usuario que se logueo en el sistema <br/>
        /// <br/>
        /// ListEstudioCandidatoRequest : lista con los datos donde estudio el candidato <br/>
        ///  IdTipoEstudio:Id del tipo de estudio indica si es primaria bachiller pregrado etc <br/>
        ///  Institucion: nombre de la institucion  educativa <br/>
        ///  YearFinally: Año de finalizacion del estudio <br/>
        ///  TituloObtenido: nombre del titulo obtenido <br/>
        ///  TituloObtenido: nombre del titulo obtenido <br/>
        ///  <br/>
        ///  ListReferenciasLaboralesCandidatoRequest: lista con los datos de las referencais laborales del candidato <br/>
        ///  <strong> Empresa : </strong> : nombre de la empresa donde trabajo el candidato <br/>
        ///  <strong> Telefono : </strong>: telefono de la empresa donde trabajo el candidato <br/>
        ///  NombreContacto: nonbre del jefe directo  de la empresa donde trabajo el candidato <br/>
        ///  CargoContacto: cargo del jefe directo  de la empresa donde trabajo el candidato <br/>
        ///  MotivoRetiro: mnotivo del retiro  <br/>
        ///  CargoDesempenado: cargo que desempeño el candidato  <br/>
        ///  Desempeno: indica como fue el desempeño del candidato en el puesto de trabajo  <br/>
        ///  Verificado: indica si la referencia fue validada  <br/>
        ///  <br/>
        ///  ListReferenciasPersonalesCandidatoRequest: lista con los datos de las referencais personales del candidato <br/>
        ///  <strong> NombreContacto : </strong>:  nombre de la persona que referencia el candidato  <br/>
        ///  <strong> Telefono : </strong>:  telefono de la persona que referencia el candidato  <br/>
        ///  Parentesco:parentesco con el candidato <br/>
        ///  TiempoConocido: tiempo de conocer al candidato<br/>
        ///  Verificado: indica si la referencia fue validada  <br/>
        ///  
        ///  
        /// </param>
        /// <returns></returns>
        /// /// <remarks>
        /// Request de ejemplo:
        /// 
        ///      {
        ///             "Documento": "1010180198",
        ///             "IdTipoDocumento": 1,
        ///             "PrimerNombre": "carlos",
        ///             "SegundoNombre": "manuel",
        ///             "PrimerApellido": "ruiz",
        ///             "SegundoApellido": "giraldo",
        ///             "NumeroTelefonico": "3275675432",
        ///             "Correo": "cgiraldo0@gmail.com",
        ///             "Base64CV": "",
        ///             "IdUser": "",     
        ///             "ListEstudioCandidatoRequest": [
        ///               {
        ///                 "IdTipoEstudio": 1,
        ///             "Institucion": "C.E.D. Chuniza",
        ///             "YearFinally": 2001,
        ///             "TituloObtenido": "Basica primaria"
        ///               },
        ///               {
        ///                 "IdTipoEstudio": 2,
        ///                 "Institucion": "C.E.D.Bosco IV",
        ///                 "YearFinally": 2005,
        ///                 "TituloObtenido": "Bachillerato Academico"
        ///               },
        ///               {
        ///              "IdTipoEstudio": 4,
        ///                "Institucion": "SENA",
        ///                "YearFinally": 2017,
        ///                "TituloObtenido": "ADSI"
        ///               }
        ///             ],
        ///             "ListReferenciasLaboralesCandidatoRequest": [
        ///              {
        ///                  "Empresa": "Pagos inteligentes",
        ///                  "Telefono": "7659870",
        ///                  "NombreContacto": "leonardo beltran",
        ///                  "CargoContacto": "Cordinador TI",
        ///                  "MotivoRetiro": "retiro voluntario",
        ///                  "CargoDesempenado": "desarrollador de software senior",
        ///                  "Desempeno": "bien",
        ///                  "Verificado": false
        ///               },
        ///               {
        ///                  "Empresa": "OL Software",
        ///                  "Telefono": "2006785",
        ///                  "NombreContacto": "Leandro casallas",
        ///                  "CargoContacto": "Arquitencto de software",
        ///                  "MotivoRetiro": "retiro voluntario",
        ///                  "CargoDesempenado": "desarrollador de software senior",
        ///                  "Desempeno": "bien",
        ///                  "Verificado": false
        ///                 }  
        ///           ],
        ///             "ListReferenciasPersonalesCandidatoRequest": [
        ///                 {
        ///                  "NombreContacto": "martha lucia paez",
        ///                  "Telefono": "3115670932",
        ///                  "Parentesco": "esposa",
        ///                  "TiempoConocido": 7,
        ///                   "Verificado": false
        ///                 }
        ///             ]
        ///  }
        ///  
        /// </remarks>

        [HttpPost, Route("[action]")]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] CandidatoRequest candidatoRequest)
        {
            try
            {
                var candidat = await candidatoService.Create(candidatoRequest);
                if (candidat.StatusCode == HttpStatusCode.OK)
                    return Ok(candidat);
                else
                    return Problem(detail: candidat.Message, statusCode: (int)candidat.StatusCode);
            }
            catch (Exception)
            {
                return Problem();
            }
        }



        /// <summary>
        /// Metodo de actualizacion del candidato     
        /// </summary>
        /// <param name="candidatoRequest">
        /// <strong> IdCandidato : </strong> :  Numero Id de la tabla del candidato  <strong> * Obligatorio </strong> <br/>
        /// <strong> Documento : </strong> Numero de identificacion del candidato <strong> * Obligatorio </strong> <br/>
        /// <strong> IdTipoDocumento : </strong> Tipo de identificacion del candidato (Ver metodo   <strong>* Obligatorio </strong> <br/>
        /// <strong> PrimerNombre : </strong> Primer nomnbre del candidato  <strong>* Obligatorio </strong> <br/>
        /// SegundoNombre :   Segundo nombre del candidato   <br/>
        /// <strong> PrimerApellido : </strong> Primer apèllido del candidato   <strong>* Obligatorio </strong> <br/>
        /// SegundoApellido :   Segundo apèllido del candidato   <br/>
        /// NumeroTelefonico :   numero celular o telefonico del candidato   <br/>
        /// Correo :   correo electronico del candidato   <br/>
        /// Base64CV :   base 64 de la hoja de vida del candidato  <br/>
        /// <strong> IdUser : </strong>    Id del usuario que se logueo en el sistema <br/>
        /// <br/>
        /// ListEstudioCandidatoRequest : lista con los datos donde estudio el candidato <br/>
        ///  IdTipoEstudio:Id del tipo de estudio indica si es primaria bachiller pregrado etc <br/>
        ///  Institucion: nombre de la institucion  educativa <br/>
        ///  YearFinally: Año de finalizacion del estudio <br/>
        ///  TituloObtenido: nombre del titulo obtenido <br/>
        ///  TituloObtenido: nombre del titulo obtenido <br/>
        ///  <br/>
        ///  ListReferenciasLaboralesCandidatoRequest: lista con los datos de las referencais laborales del candidato <br/>
        ///  <strong> Empresa : </strong> : nombre de la empresa donde trabajo el candidato  <strong> * Obligatorio </strong>  <br/>
        ///  <strong> Telefono : </strong>: telefono de la empresa donde trabajo el candidato  <strong> * Obligatorio </strong>  <br/>
        ///  NombreContacto: nonbre del jefe directo  de la empresa donde trabajo el candidato <br/>
        ///  CargoContacto: cargo del jefe directo  de la empresa donde trabajo el candidato <br/>
        ///  MotivoRetiro: mnotivo del retiro  <br/>
        ///  CargoDesempenado: cargo que desempeño el candidato  <br/>
        ///  Desempeno: indica como fue el desempeño del candidato en el puesto de trabajo  <br/>
        ///  Verificado: indica si la referencia fue validada  <br/>
        ///  <br/>
        ///  ListReferenciasPersonalesCandidatoRequest: lista con los datos de las referencais personales del candidato <br/>
        ///  <strong> NombreContacto : </strong>:  nombre de la persona que referencia el candidato  <strong> * Obligatorio </strong>  <br/>
        ///  <strong> Telefono : </strong>:  telefono de la persona que referencia el candidato   <strong> * Obligatorio </strong> <br/>
        ///  Parentesco:parentesco con el candidato <br/>
        ///  TiempoConocido: tiempo de conocer al candidato<br/>
        ///  Verificado: indica si la referencia fue validada  <br/>
        ///  
        ///  
        /// </param>
        /// <returns></returns>
        /// /// <remarks>
        /// Request de ejemplo:
        /// 
        ///      {
        ///             "IdCandidato": 1,
        ///             "Documento": "1010180198",
        ///             "IdTipoDocumento": 1,
        ///             "PrimerNombre": "carlos",
        ///             "SegundoNombre": "manuel",
        ///             "PrimerApellido": "ruiz",
        ///             "SegundoApellido": "giraldo",
        ///             "NumeroTelefonico": "3275675432",
        ///             "Correo": "cgiraldo0@gmail.com",
        ///             "Base64CV": "",
        ///             "IdUser": "",                  
        ///             "ListEstudioCandidatoRequest": [
        ///               {
        ///                 "IdTipoEstudio": 1,
        ///             "Institucion": "C.E.D. Chuniza",
        ///             "YearFinally": 2001,
        ///             "TituloObtenido": "Basica primaria"
        ///               },
        ///               {
        ///                 "IdTipoEstudio": 2,
        ///                 "Institucion": "C.E.D.Bosco IV",
        ///                 "YearFinally": 2005,
        ///                 "TituloObtenido": "Bachillerato Academico"
        ///               },
        ///               {
        ///              "IdTipoEstudio": 4,
        ///                "Institucion": "SENA",
        ///                "YearFinally": 2017,
        ///                "TituloObtenido": "ADSI"
        ///               }
        ///             ],
        ///             "ListReferenciasLaboralesCandidatoRequest": [
        ///              {
        ///                  "Empresa": "Pagos inteligentes",
        ///                  "Telefono": "7659870",
        ///                  "NombreContacto": "leonardo beltran",
        ///                  "CargoContacto": "Cordinador TI",
        ///                  "MotivoRetiro": "retiro voluntario",
        ///                  "CargoDesempenado": "desarrollador de software senior",
        ///                  "Desempeno": "bien",
        ///                  "Verificado": false
        ///               },
        ///               {
        ///                  "Empresa": "OL Software",
        ///                  "Telefono": "2006785",
        ///                  "NombreContacto": "Leandro casallas",
        ///                  "CargoContacto": "Arquitencto de software",
        ///                  "MotivoRetiro": "retiro voluntario",
        ///                  "CargoDesempenado": "desarrollador de software senior",
        ///                  "Desempeno": "bien",
        ///                  "Verificado": false
        ///                 }  
        ///           ],
        ///             "ListReferenciasPersonalesCandidatoRequest": [
        ///                 {
        ///                  "NombreContacto": "martha lucia paez",
        ///                  "Telefono": "3115670932",
        ///                  "Parentesco": "esposa",
        ///                  "TiempoConocido": 7,
        ///                   "Verificado": false
        ///                 }
        ///             ]
        ///  }
        ///  
        /// </remarks>


        [HttpPut, Route("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromBody] CandidatoRequest candidatoRequest)
        {
            try
            {
                var candidat = await candidatoService.Update(candidatoRequest);
                if (candidat.StatusCode == HttpStatusCode.OK)
                    return Ok(candidat);
                else
                    return Problem(detail: candidat.Message, statusCode: (int)candidat.StatusCode);
            }
            catch (Exception)
            {
                return Problem();
            }
        }



        /// <summary>
        /// Metodo para actualizar si el candidato esta activo
        /// </summary>
        ///<param name="candidatoStateRequest">
        /// <strong> IdCandidato : </strong>   Numero Id de la tabla del candidato  <strong> * Obligatorio </strong> <br/>
        /// <strong> IdUser : </strong> :   Id del usuario que se logueo en el sistema <br/>
        /// <strong> Activo : </strong>    valor del candidato debe ser true o false <strong> * Obligatorio </strong>  
        /// </param>
        ///
        /// <returns></returns>
        /// /// <remarks>
        /// Request de ejemplo:
        ///  
        ///     {
        ///        "IdCandidato": 1,
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
        public async Task<IActionResult> UpdateActiveCandidato([FromBody] CandidatoActiveRequest candidatoStateRequest)
        {
            try
            {
                var candidat = await candidatoService.UpdateActiveCandidato(candidatoStateRequest);
                if (candidat.StatusCode == HttpStatusCode.OK)
                    return Ok(candidat);
                else
                    return Problem(detail: candidat.Message, statusCode: (int)candidat.StatusCode);
            }
            catch (Exception)
            {
                return Problem();
            }
        }


        /// <summary>
        /// Metodo para actualizar  el estado de un candidato  
        /// </summary>
        ///<param name="candidatoStateRequest">
        /// <strong> IdCandidato : </strong>   Numero Id de la tabla del candidato  <strong> * Obligatorio </strong> <br/>
        /// <strong> IdUser : </strong> :   Id del usuario que se logueo en el sistema <br/>
        /// <strong> IdEstadoCandidato : </strong>   Id del estado del candidato <strong> * Obligatorio </strong> <br/>
        ///  Comentarios:  comentarios acerca del candidato  
        /// </param>    
        /// <returns></returns>
        /// /// <remarks>
        /// Request de ejemplo:
        ///  
        ///     {
        ///        "IdCandidato": 1,
        ///        "IdUser": 1,
        ///        "IdEstadoCandidato": 2,
        ///        "Comentarios": "string"
        /// 
        ///     }
        ///
        /// </remarks>

        [HttpPut, Route("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateStateCandidato([FromBody] CandidatoStateRequest candidatoStateRequest)
        {
            try
            {
                var candidat = await candidatoService.UpdateStateCandidato(candidatoStateRequest);
                if (candidat.StatusCode == HttpStatusCode.OK)
                    return Ok(candidat);
                else
                    return Problem(detail: candidat.Message, statusCode: (int)candidat.StatusCode);
            }
            catch (Exception)
            {
                return Problem();
            }
        }





        /// <summary>
        /// Metodo para verificar las referencias laborales del candidato    
        /// </summary>
        /// <param name="referenciasLaboralesVerifyRequests">
        /// <strong> IdReferenciasLaboralesCandidato : </strong>   Numero Id de referencia laborales <strong> * Obligatorio </strong> <br/>
        /// Verificado: indica si la referencia fue validada  <br/>
        /// </param>        
        /// <returns></returns>
        /// /// <remarks>
        /// Request de ejemplo:
        ///  
        ///     {
        ///         "IdReferenciasLaboralesCandidato": 0,
        ///         "Verificado": true
        /// 
        ///     }
        ///
        /// </remarks>


        [HttpPut, Route("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateVerifyEmploymentRef([FromBody] List<ReferenciasLaboralesVerifyRequest> referenciasLaboralesVerifyRequests)
        {
            try
            {
                var candidat = await candidatoService.UpdateVerifyRefLaborales(referenciasLaboralesVerifyRequests);
                if (candidat.StatusCode == HttpStatusCode.OK)
                    return Ok(candidat);
                else
                    return Problem(detail: candidat.Message, statusCode: (int)candidat.StatusCode);
            }
            catch (Exception)
            {
                return Problem();
            }
        }


        /// <summary>
        /// Metodo para verificar las referencias personales del candidato    
        /// </summary>
        ///<param name="referenciasPersonalesVerifyRequests">
        ///  <strong> IdReferenciasPersonalesCandidato : </strong>   Numero Id de referencia personales <strong> * Obligatorio </strong> <br/>
        ///  Verificado: indica si la referencia fue validada  
        /// </param>
        ///
        /// <returns></returns>
        /// /// <remarks>
        /// Request de ejemplo:
        ///  
        ///     {
        ///         "IdReferenciasPersonalesCandidato": 0,
        ///         "Verificado": true
        /// 
        ///     }
        ///
        /// </remarks>

        [HttpPut, Route("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateVerifyPersonalRef([FromBody] List<ReferenciasPersonalesVerifyRequest> referenciasPersonalesVerifyRequests)
        {
            try
            {
                var candidat = await candidatoService.UpdateVerifyRefPersonales(referenciasPersonalesVerifyRequests);
                if (candidat.StatusCode == HttpStatusCode.OK)
                    return Ok(candidat);
                else
                    return Problem(detail: candidat.Message, statusCode: (int)candidat.StatusCode);
            }
            catch (Exception)
            {
                return Problem();
            }
        }



        /// <summary>
        /// Obtener candidatos 
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
                var candidatos = await candidatoService.GetAllCandidatos();
                return Ok(candidatos);
            }
            catch (Exception)
            {
                return Problem();
            }
        }


        /// <summary>
        /// Obtener estudios del candidatos 
        /// </summary>    
        /// <param name="idCandidato">
        /// <strong> IdCandidato : </strong>   Numero Id de la tabla del candidato  <strong> * Obligatorio </strong> <br/>
        /// </param>
        /// <returns></returns> 

        [HttpGet, Route("[action]/{idCandidato}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetStudies(int idCandidato)
        {
            try
            {
                var estudiosCandidato = await candidatoService.GetAllEstudiosCandidato(idCandidato);
                return Ok(estudiosCandidato);
            }
            catch (Exception)
            {
                return Problem();
            }
        }


        /// <summary>
        /// Obtener referencias personales del candidatos 
        /// </summary>  
        /// <param name="idCandidato">
        /// <strong> IdCandidato : </strong>   Numero Id de la tabla del candidato  <strong> * Obligatorio </strong> <br/>
        /// </param>
        /// <returns></returns> 
        [HttpGet, Route("[action]/{idCandidato}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPersonalReferences(int idCandidato)
        {
            try
            {
                var refPersonalesCandidato = await candidatoService.GetAllRefPersonalesCandidato(idCandidato);
                return Ok(refPersonalesCandidato);
            }
            catch (Exception)
            {
                return Problem();
            }
        }


        /// <summary>
        /// Obtener referencias laborales  del candidatos 
        /// </summary>  
        /// <param name="idCandidato">
        /// <strong> IdCandidato : </strong>   Numero Id de la tabla del candidato  <strong> * Obligatorio </strong> <br/>
        /// </param>
        /// <returns></returns>     
        [HttpGet, Route("[action]/{idCandidato}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetEmploymentReferences(int idCandidato)
        {
            try
            {
                var refPersonalesCandidato = await candidatoService.GetAllRefPersonalesCandidato(idCandidato);
                return Ok(refPersonalesCandidato);
            }
            catch (Exception)
            {
                return Problem();
            }
        }

    }
}
