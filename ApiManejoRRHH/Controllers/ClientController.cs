using ApiManejoRRHH.Helpers;
using Core.Interfaces;
using Core.Repository;
using Domain.Common;
using Domain.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Reflection.Metadata;

namespace ApiManejoRRHH.Controllers
{
    /// <summary>
    /// Controlador de Cliente
    /// </summary>

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ClientController : ControllerBase
    {


        private readonly IClientService clientService;

        /// <summary>
        /// Constructor
        /// </summary>
        public ClientController(IClientService clientService)
        {
            this.clientService = clientService;
        }



        /// <summary>
        /// Metodo de creacion del cliente       
        /// </summary>
        ///<param name="clientRequest">
        /// <strong> IdUser : </strong>    Id del usuario que se logueo en el sistema <br/>
        ///  IdCliente :  no enviar   <br/>
        /// <strong> Name : </strong> :   nombre del cliente o empresa <br/>
        /// <strong> Nit : </strong>   nit del cliente o de la empresa  <strong> * Obligatorio </strong> <br/>
        ///  Base64File:  base 64 del logo del cliente o de la empresa
        ///  
        /// </param>    
        /// <returns></returns>
        /// /// <remarks>
        /// Request de ejemplo:
        ///  
        ///     {
        ///        "IdUser": 1,
        ///        "IdCliente": 1,
        ///        "Name": "`mpresa prueba S.A",
        ///        "Nit": "9067668848-2",
        ///        "Base64File": "",
        /// 
        ///     }
        ///
        /// </remarks>


        [HttpPost, Route("[action]")]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] ClientRequest clientRequest)
        {
            try
            {
                var client = await clientService.CreateClient(clientRequest);
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
        /// Metodo de actualizacion de la informnacion cliente       
        /// </summary>
        ///<param name="clientRequest">  
        /// <strong> IdUser : </strong>    Id del usuario que se logueo en el sistema <br/>
        /// <strong> IdCliente : </strong> Numero Id del cliente <strong> * Obligatorio </strong> <br/>
        /// <strong> Name : </strong> :   nombre del cliente o empresa <br/>
        /// <strong> Nit : </strong>   nit del cliente o de la empresa  <strong> * Obligatorio </strong> <br/>
        ///  Base64File:  base 64 del logo del cliente o de la empresa
        ///  
        /// </param>    
        /// <returns></returns>
        /// /// <remarks>
        /// Request de ejemplo:
        ///  
        ///     {
        ///        "IdUser": 1,
        ///        "IdCliente": 1,
        ///        "Name": "`mpresa prueba S.A",
        ///        "Nit": "9067668848-2",
        ///        "Base64File": "",
        /// 
        ///     }
        ///
        /// </remarks>



        [HttpPut, Route("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromBody] ClientRequest clientRequest)
        {
            try
            {
                var client = await clientService.UpdateCliet(clientRequest);
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
        /// Metodo de cancelar procesos de un candidato por cliente  cliente       
        /// </summary>
        ///<param name="cancelProcessClientRequest">
        /// <strong> IdCliente : </strong> Numero Id del cliente <strong> * Obligatorio </strong> <br/>
        /// <strong> IdCandidato : </strong> :  Numero Id de la tabla del candidato  <strong> * Obligatorio </strong>   
        /// </param>    
        /// <returns></returns>
        /// /// <remarks>
        /// Request de ejemplo:
        ///  
        ///     {
        ///        "IdCandidato": 1,
        ///        "IdCliente": 1         
        /// 
        ///     }
        ///
        /// </remarks>

        [HttpPut, Route("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CancelProcessCandidateByClient([FromBody] CancelProcessClientRequest cancelProcessClientRequest)
        {
            try
            {
                var client = await clientService.CancelProccessCandidateByClient(cancelProcessClientRequest);
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
        /// Obtener cliente por documento    
        /// </summary>
        ///<param name="nit">
        /// <strong> Nit : </strong>   nit del cliente o de la empresa  <strong> * Obligatorio </strong> 
        ///</param>
        /// <returns></returns>      

        [HttpGet, Route("[action]/{nit}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByDocument(string nit)
        {
            try
            {
                var client = await clientService.GetClientByDocument(nit);
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
        /// Obtener clientes 
        /// </summary>         
        /// <returns></returns>
        [HttpGet, Route("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetClients()
        {
            try
            {
                var client = await clientService.GetListClients();
                return Ok(client);                
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }




        /// <summary>
        /// Obtener empleados  por cliente    
        /// </summary>
        ///<param name="idClient">
        /// <strong> IdCliente : </strong> Numero Id del cliente <strong> * Obligatorio </strong>  
        /// </param>
        /// <returns></returns>     
        [HttpGet, Route("[action]/{idClient}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetEmployeesByClient(int idClient)
        {
            try
            {
                var client = await clientService.GetEmployeesByClient(idClient);               
                return Ok(client);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }


        /// <summary>
        /// Obtener vacantes por cliente    
        /// </summary>       
        ///<param name="idClient">
        /// <strong> IdCliente : </strong> Numero Id del cliente <strong> * Obligatorio </strong>  
        /// </param>
        /// <returns></returns>      
        [HttpGet, Route("[action]/{idClient}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetVacantsByClient(int idClient)
        {
            try
            {
                var vacantClient = await clientService.GetVacantsByClient(idClient);
                return Ok(vacantClient);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

    }
}
