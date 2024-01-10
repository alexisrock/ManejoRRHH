using AutoMapper;
using Core.Common;
using Core.Interfaces;
using DataAccess;
using DataAccess.Interface;
using Domain.Common;
using Domain.Common.Enum;
using Domain.Dto;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Net;


namespace Core.Repository
{
    public class ContratoService : IContratoService
    {
        private readonly IRepository<Contrato> contratoRepository;
        private readonly IRepository<Empleado> empleadoRepository;
        private readonly IRepository<Candidato> candidatoRepository;
        private readonly IRepository<Vacante> vacanteRepository;
        private readonly IRepository<Proceso> procesoRepository;
        private readonly IRepository<CertificadosEmpleado> certificadosEmpleadoRepository;
       
        private readonly IRepository<Configuracion> configuiuracionRepository;

        private SaveFiles saveFile;
        
        private readonly IMapper mapper;
        private readonly ManejoRHContext manejoRHContext;


        public ContratoService(IMapper mapper, ManejoRHContext manejoRHContext, IRepository<Contrato> contratoRepository, IRepository<Empleado> empleadoRepository
            , IRepository<Candidato> candidatoRepository, IRepository<Vacante> vacanteRepository, IRepository<Proceso> procesoRepository, IRepository<CertificadosEmpleado> certificadosEmpleadoRepository
            , IRepository<Configuracion> configuiuracionRepository)
        {
            this.mapper = mapper;
            this.manejoRHContext = manejoRHContext;
            this.contratoRepository = contratoRepository;
            this.empleadoRepository = empleadoRepository;
            this.candidatoRepository = candidatoRepository;
            this.vacanteRepository = vacanteRepository;
            this.procesoRepository = procesoRepository;
          
           
            this.configuiuracionRepository = configuiuracionRepository;
            saveFile = new SaveFiles();
        }

        public async Task<BaseResponse> Create(ContratoRequest contratoRequest)  
        {

            var outPut = new BaseResponse();
            var strategy = manejoRHContext.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using (var transaction = manejoRHContext.Database.BeginTransaction())
                {
                    try
                    {
                        var resulContrato = await ValidateContratoProceso(contratoRequest);
                        var resultValidationCandidato = await ValidateCandidato(contratoRequest.IdCandidato);
                        if (resulContrato && resultValidationCandidato)
                        {
                            await InsertContrato(contratoRequest);
                            await InsertEmpleado(contratoRequest);                            
                            await UpdateStateToContractCandidate(contratoRequest);
                            int idVacante = await GetVacantByProceso(contratoRequest.IdProceso);
                            await UpdateStateVacant(idVacante);
                            await UpdaateStateCandidateByProcess(idVacante, contratoRequest.IdCandidato);
                            await UpdateStateProcessContract(idVacante, contratoRequest.IdCandidato);
                            await transaction.CommitAsync();
                            outPut = MapperResponse("Contrato creado con exito");
                        }
                        else
                        {
                            string mensaje = resultValidationCandidato != true ? "El candidato tiene un contrato activo" : "Ya existe un proceso activo asociado a un contrato";
                            outPut = MapperResponseFail(mensaje);
                        }
                    }
                    catch (Exception ex)       
                    {
                        await transaction.RollbackAsync();
                        outPut.StatusCode = HttpStatusCode.InternalServerError;
                        outPut.Message = ex.Message;

                    }
                }
            });
            return outPut;
        }
        private async Task<bool> ValidateContratoProceso(ContratoRequest contratoRequest)
        {
            var procesoComtrato = await contratoRepository.GetByParam(x => x.IdProceso == contratoRequest.IdProceso && x.Activo);
            return procesoComtrato == null;
        }
        private async Task<bool> ValidateCandidato(int idCandidato)
        {
            var candidato = await candidatoRepository.GetById(idCandidato);           
            return candidato is not null && candidato?.IdEstadoCandidato != TipoEstadoCandidato.Contratado.GetIdEstadoCandidato();
        }   
        private async Task InsertContrato(ContratoRequest contratoRequest)
        {
            var contrato = mapper.Map<Contrato>(contratoRequest);
            contrato.Activo = true;
            contrato.IdUserCreated = contratoRequest.IdUser;
            contrato.DateCreated = DateTime.Now;
            await contratoRepository.Insert(contrato);
        }
        private async Task InsertEmpleado(ContratoRequest contratoRequest)
        {
            var empleado = new Empleado();
            empleado.IdCliente = contratoRequest.IdCliente;
            empleado.IdCandidato = contratoRequest.IdCandidato;
            empleado.Activo = true;
            await empleadoRepository.Insert(empleado);
        }       
        private async Task<int> GetVacantByProceso(long idProceso)
        {
            var proceso = await procesoRepository.GetById(idProceso);
            return proceso?.Idvacante ?? 0;
        }                      
        private async Task UpdateStateVacant(int idVacante)
        {
            var vacante = await vacanteRepository.GetById(idVacante);
            if (vacante is not null)
            {
                vacante.IdEstadoVacante = TipoEstadoVacante.Cerrado.GetIdTipoEstado();
                await vacanteRepository.Update(vacante);
            }            
        }
     

        public async Task<BaseResponse> Update(ContratoRequest contratoRequest)
        {

            var outPut = new BaseResponse();
            var strategy = manejoRHContext.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
            using (var transaction = manejoRHContext.Database.BeginTransaction())
            {
                try
                {
                    var resulContrato = await ValidateIdContratoByProceso(contratoRequest);
                    if (resulContrato)
                    {

                            await UpdateStateCandidateBefore(contratoRequest);
                            await UpdateEmpleadoContrato(contratoRequest);
                            await UpdateStateToContractCandidate(contratoRequest);
                            int idVacante = await GetVacantByProceso(contratoRequest.IdProceso);
                            await UpdaateStateCandidateByProcess(idVacante, contratoRequest.IdCandidato);
                            await UpdateStateProcessContract(idVacante, contratoRequest.IdCandidato);                      
                            await transaction.CommitAsync();
                            outPut = MapperResponse("Empleado actualizado con exito");

                        }
                        else
                        {
                            outPut = MapperResponseFail("Ya existe un proceso asociado a ese contrato o el contrato no existe");
                        }
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();
                        outPut.StatusCode = HttpStatusCode.InternalServerError;
                        outPut.Message = ex.Message;
                    }
                }
            });
            return outPut;
        }
        private async Task<bool> ValidateIdContratoByProceso(ContratoRequest contratoRequest)
        {
            var procesoComtrato = await contratoRepository.GetByParam(x => x.IdProceso == contratoRequest.IdProceso && x.IdContrato == contratoRequest.IdContrato && x.Activo);
            return procesoComtrato != null;
        }
        private async Task UpdateEmpleadoContrato(ContratoRequest contratoRequest)
        {

            var contrato = await contratoRepository.GetById(contratoRequest.IdContrato);
            if (contrato is not null)
            {
                contrato.IdCandidato = contratoRequest.IdCandidato;
                contrato.UserIdModified = contratoRequest.IdUser;
                contrato.DateModified = DateTime.Now;
                await contratoRepository.Update(contrato);
            }
        }
        private async Task UpdateStateCandidateBefore(ContratoRequest contratoRequest)
        {
            var idCandidatoAnterior = await contratoRepository.GetById(contratoRequest.IdContrato);            
            if (idCandidatoAnterior is not null)
            {
                await UpdateStateOutOfWorkToCandidate(idCandidatoAnterior.IdCandidato, contratoRequest.IdUser);
            }
        }
        private async Task UpdateStateOutOfWorkToCandidate(int idCandidato, int idUser)
        {
            var candidato = await candidatoRepository.GetById(idCandidato);
            if (candidato is not null)
            {
                candidato.IdEstadoCandidato = TipoEstadoCandidato.Disponible.GetIdEstadoCandidato();
                candidato.UserIdModified = idUser;
                candidato.DateModified = DateTime.Now;
                await candidatoRepository.Update(candidato);
            }
        } 

        public async Task<BaseResponse> UpdateEstadoActivoContrato(ContratoEstadoRequest contratoEstadoRequest) 
        {
            var outPut = new BaseResponse();
            try
            {
                var contrato = await contratoRepository.GetById(contratoEstadoRequest.IdContrato);
                if (contrato is not null)
                {
                    contrato.Activo = contratoEstadoRequest.Activo;
                    contrato.UserIdModified = contratoEstadoRequest.IdUser;
                    contrato.DateModified = DateTime.Now;
                    await contratoRepository.Update(contrato);
                    outPut = MapperResponse("Estado del contrato actualizado con exito");
                }
                else
                {
                    outPut = MapperResponseFail("La numero de contrato no fue encontrado");
                }

            }
            catch (Exception ex)
            {

                outPut.StatusCode = HttpStatusCode.InternalServerError;
                outPut.Message = ex.Message;
            }
            return outPut;
        }

        private async Task UpdateStateToContractCandidate(ContratoRequest contratoRequest)
        {
            var candidato = await candidatoRepository.GetById(contratoRequest.IdCandidato);
            if (candidato is not null)
            {
                candidato.IdEstadoCandidato = TipoEstadoCandidato.Contratado.GetIdEstadoCandidato();
                candidato.UserIdModified = contratoRequest.IdUser;
                candidato.DateModified = DateTime.Now;
                await candidatoRepository.Update(candidato);
            }
        }
        private async Task UpdaateStateCandidateByProcess(int idVacante , int idCantidato)
        {
            var procesos = await procesoRepository.GetListByParam(x => x.Idvacante == idVacante && x.IdCandidato != idCantidato);
            foreach (var item in procesos)
            {
                item.IdEstadoProceso = TipoEstadoProceso.Rechazado.GetIdEstadoProceso();
                await procesoRepository.Update(item);
            }
        }
        private static BaseResponse MapperResponse(string mensaje)
        {
            return new BaseResponse()
            {
                StatusCode = HttpStatusCode.OK,
                Message = mensaje
            };
        }
        private static BaseResponse MapperResponseFail(string mensaje)
        {
            return new BaseResponse()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = mensaje
            };
        }
        private async Task UpdateStateProcessContract(int idVacante, int idCandidato)
        {
            var proceso = await procesoRepository.GetByParam(x => x.Idvacante == idVacante &&  x.IdCandidato == idCandidato);
            if (proceso is not null)
            {

                var procesoUpdate = await procesoRepository.GetById(proceso.IdProceso);
                procesoUpdate.IdEstadoProceso = TipoEstadoProceso.Contratado.GetIdEstadoProceso();
                await procesoRepository.Update(procesoUpdate);
            } 
        }


        public async Task<ContratoSingleResponse> GetContratoById(long idContrato)
        {
            var contrato = new  ContratoSingleResponse();
            try
            {
                var contract = await contratoRepository.GetById(idContrato);
                if (contract is not null)
                {
                    contrato = mapper.Map<ContratoSingleResponse>(contract);
                    contrato.StatusCode = HttpStatusCode.OK;
                }
                else
                {
                    contrato.Message = "Numero de contrato no encontrado";
                    contrato.StatusCode = HttpStatusCode.BadRequest;
                }
            }
            catch (Exception ex)
            {
                contrato.Message = ex.Message;
                contrato.StatusCode = HttpStatusCode.InternalServerError;


            }
            return contrato;
        }

        public async Task<List<ContratoResponse>> GetListContratos()
        {
            var listRefPersonalesResponse = new List<ContratoResponse>();
            try
            {
                var listContrato = await contratoRepository.GetAllByParamIncluding(null, (x => x.Candidato));
                listRefPersonalesResponse = MapperListContratoResponse(listContrato);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listRefPersonalesResponse;
        }
        private List<ContratoResponse> MapperListContratoResponse(List<Contrato> listContrato)
        {
            var listRefPersonalesResponse = new List<ContratoResponse>();

            listContrato.ForEach(item =>
            {
                var contratoResponse = mapper.Map<ContratoResponse>(item);
                contratoResponse.NombreCandidato = string.Concat(item.Candidato?.PrimerNombre, " ", item.Candidato?.SegundoNombre, " ", item.Candidato?.PrimerApellido, "  ", item.Candidato?.SegundoApellido);
                listRefPersonalesResponse.Add(contratoResponse);
            });
            return listRefPersonalesResponse;
        }

    }
}
