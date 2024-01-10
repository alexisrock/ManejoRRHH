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
    /// Controlador de Tipo documento 
    /// </summary>

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeeController : ControllerBase
    {


        private readonly IEmpleadoService empleadoService;

        public EmployeeController(IEmpleadoService empleadoService)
        {
            this.empleadoService = empleadoService;
        }

        /// <summary>
        /// Metodo para crear los certificados estudiantiles, personales y laborales del empleado   
        /// </summary>         
        ///<param name="contratoRequest">   
        /// <strong> IdCandidato : </strong> :  Numero Id de la tabla del candidato  <strong> * Obligatorio </strong>   
        /// CertificadosLaborales:  lista de los certificados laborales en base 64  <br/>
        /// CertificadosPersonales:  lista de los certificados personales en base 64  <br/>
        ///  CertificadosEstudiantiles:  lista de los certificados estudiantiles
        ///  
        /// </param>    
        /// <returns></returns>
        /// /// <remarks>
        /// Request de ejemplo:
        ///  
        ///     {
        ///         
        ///        "IdCandidato": 1,
        ///      
        /// 
        ///     }
        ///
        /// </remarks>


        [HttpPost, Route("[action]")]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateCertificados([FromBody] ContratoCreateRequest contratoRequest)
        {
            try
            {
                var contract = await empleadoService.CreateCertificados(contratoRequest);
                if (contract.StatusCode == HttpStatusCode.OK)
                    return Ok(contract);
                else
                    return Problem(contract.Message, statusCode: (int)contract.StatusCode);
            }
            catch (Exception)
            {
                return Problem();
            }
        }


        /// <summary>
        /// Metodo para actualizar los certificados estudiantiles, personales y laborales del empleado   
        /// </summary>         
        ///<param name="contratoRequest">   
        /// <strong> IdCandidato : </strong> :  Numero Id de la tabla del candidato  <strong> * Obligatorio </strong>   
        /// CertificadosLaborales:  lista de los certificados laborales en base 64  <br/>
        /// CertificadosPersonales:  lista de los certificados personales en base 64  <br/>
        ///  CertificadosEstudiantiles:  lista de los certificados estudiantiles
        ///  
        /// </param>    
        /// <returns></returns>
        /// /// <remarks>
        /// Request de ejemplo:
        ///  
        ///     {
        ///         
        ///        "IdCandidato": 1,
        ///      
        /// 
        ///     }
        ///
        /// </remarks>


        [HttpPut, Route("[action]")]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateCertificados([FromBody] ContratoEditRequest contratoRequest)
        {
            try
            {
                var contract = await empleadoService.UpdateCertificados(contratoRequest);
                if (contract.StatusCode == HttpStatusCode.OK)
                    return Ok(contract);
                else
                    return Problem(contract.Message, statusCode: (int)contract.StatusCode);
            }
            catch (Exception)
            {
                return Problem();
            }
        }



        /// <summary>
        /// Obtener informacion del empleado por id    
        /// </summary>
        ///<param name="idEmpleado">
        /// <strong> idEmpleado : </strong> numero Id delempleado <strong>* Obligatorio</strong> 
        /// </param>
        /// <returns></returns>  

        [HttpGet, Route("[action]/{idEmpleado}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetrejectedProcessByUser(long idEmpleado)
        {
            try
            {
                var employee = await empleadoService.GetInfoEmployeeById(idEmpleado);
                if (employee.StatusCode == HttpStatusCode.OK)
                    return Ok(employee);
                else
                    return Problem(employee.Message, statusCode: (int)employee.StatusCode);
            }
            catch (Exception)
            {
                return Problem();
            }
        }

        /// <summary>
        /// Obtener las novedades del empleado 
        /// </summary>         
        /// <returns></returns> 

        [HttpGet, Route("[action]/{idEmpleado}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetHistoricalNoverlty(long idEmpleado)
        {
            try
            {
                var employee = await empleadoService.GetHistoricalNoverltyEmployees(idEmpleado);
                return Ok(employee);
            }
            catch (Exception)
            {
                return Problem();
            }
        }


        /// <summary>
        /// Obtener los certificados del empleado 
        /// </summary>         
        /// <returns></returns> 

        [HttpGet, Route("[action]/{idEmpleado}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCertificadosByEmployee(long idEmpleado)
        {
            try
            {
                var employee = await empleadoService.GetCertificadosByEmployee(idEmpleado);
                return Ok(employee);
            }
            catch (Exception)
            {
                return Problem();
            }
        }




    }
}
