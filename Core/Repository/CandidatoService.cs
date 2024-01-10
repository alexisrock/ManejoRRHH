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
 
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repository
{
    public class CandidatoService : ICandidatoService
    {
        private readonly IRepository<Candidato> candidatoRepository;
        private readonly IRepository<EstudioCandidato> estudioCandidatoRepository;
        private readonly IRepository<ReferenciasLaboralesCandidato> referenciasLaboralesCandidatoRepository;
        private readonly IRepository<ReferenciasPersonalesCandidato> referenciasPersonalesCandidatoRepository;
        private readonly IRepository<Configuracion> configuiuracionRepository;

        private readonly IMapper mapper;
        private readonly ManejoRHContext manejoRHContext;

        public CandidatoService(IRepository<Candidato> candidatoRepository, IRepository<EstudioCandidato> estudioCandidatoRepository, IRepository<ReferenciasLaboralesCandidato> referenciasLaboralesCandidatoRepository
            , IRepository<ReferenciasPersonalesCandidato> referenciasPersonalesCandidatoRepository, IMapper mapper, ManejoRHContext manejoRHContext, IRepository<Configuracion> configuiuracionRepository)
        {
            this.candidatoRepository = candidatoRepository;
            this.estudioCandidatoRepository = estudioCandidatoRepository;
            this.referenciasLaboralesCandidatoRepository = referenciasLaboralesCandidatoRepository;
            this.referenciasPersonalesCandidatoRepository = referenciasPersonalesCandidatoRepository;
            this.mapper = mapper;
            this.manejoRHContext = manejoRHContext;
            this.configuiuracionRepository = configuiuracionRepository;
        }

        public async Task<BaseResponse> Create(CandidatoRequest candidatoRequest)
        {
            var outPut = new BaseResponse();
            var strategy = manejoRHContext.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using (var transaction = manejoRHContext.Database.BeginTransaction())
                {
                    try
                    {
                        var validationResult = await ValidateCreationCandidato(candidatoRequest.Documento);
                        if (validationResult)
                        {
                            var idCandidato = await InsertCandidato(candidatoRequest);
                            await InsertEstudios(candidatoRequest.ListEstudioCandidatoRequest, idCandidato);
                            await InserReferenciasLaborales(candidatoRequest.ListReferenciasLaboralesCandidatoRequest, idCandidato);
                            await InsertReferenciasPersonales(candidatoRequest.ListReferenciasPersonalesCandidatoRequest, idCandidato);
                            await transaction.CommitAsync();
                            outPut = MapperResponse();
                        }
                        else
                        {
                            outPut = MapperResponseFail();
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
        private async Task<bool> ValidateCreationCandidato(string documento)
        {
            var candidato = await candidatoRepository.GetByParam(x => x.Documento.Trim() == documento.Trim());
            return candidato == null;
        }
        private async Task<int> InsertCandidato(CandidatoRequest candidatoRequest)
        {
            var candidato = mapper.Map<Candidato>(candidatoRequest);
            candidato.IdEstadoCandidato =  TipoEstadoCandidato.EnviadoComercial.GetIdEstadoCandidato();
            candidato.IdUserCreated = candidatoRequest.IdUser;
            candidato.DateCreated = DateTime.Now;
            string nameFile = string.Concat("CV", candidatoRequest.Documento , candidatoRequest.PrimerApellido);
            candidato.UrlCV = string.IsNullOrEmpty(candidatoRequest.Base64CV) ? null : await GetPathDocsPdf(candidatoRequest.Base64CV, nameFile);
            candidato.Activo = true;
            await candidatoRepository.Insert(candidato);
            return candidato.IdCandidato;
        }
        private async Task<string> GetPathDocsPdf(string base64File, string clientName)
        {
            var saveFile = new SaveFiles();
            var pathLogos = (await configuiuracionRepository.GetByParam(x => x.Id.Equals(ParamConfig.PathDocsCandidatos.ToString())))?.Value ?? string.Empty;

            var objectFileSave = new ObjectFileSave();
            objectFileSave.FilePath = pathLogos;
            objectFileSave.Base64String = base64File;
            objectFileSave.FileName = $"{clientName}.pdf";
            var pathFile = saveFile.SaveFileBase64(objectFileSave);
            return pathFile;
        }
        private async Task InsertEstudios(List<EstudioCandidatoRequest>? ListEstudioCandidatoRequest, int idCandidato)
        {
            if (ListEstudioCandidatoRequest is not null)
            {
                foreach (var item in ListEstudioCandidatoRequest)
                {
                    var estudios = mapper.Map<EstudioCandidato>(item);
                    estudios.IdCandidato = idCandidato;
                    estudios.Activo = true;
                    await estudioCandidatoRepository.Insert(estudios);
                }
            }
        }
        private async Task InserReferenciasLaborales(List<ReferenciasLaboralesCandidatoRequest>? ListReferenciasLaboralesCandidatoRequest, int idCandidato)
        {
            if (ListReferenciasLaboralesCandidatoRequest is not null)
            {
                foreach (var item in ListReferenciasLaboralesCandidatoRequest)
                {
                    var referenciaLaboral = mapper.Map<ReferenciasLaboralesCandidato>(item);
                    referenciaLaboral.IdCandidato = idCandidato;
                    referenciaLaboral.Activo = true;
                    await referenciasLaboralesCandidatoRepository.Insert(referenciaLaboral);
                }
            }
        }
        private async Task InsertReferenciasPersonales(List<ReferenciasPersonalesCandidatoRequest>? ListReferenciasLaboralesCandidatoRequest, int idCandidato)
        {
            if (ListReferenciasLaboralesCandidatoRequest is not null)
            {
                foreach (var item in ListReferenciasLaboralesCandidatoRequest)
                {
                    var referenciaPersonal = mapper.Map<ReferenciasPersonalesCandidato>(item);
                    referenciaPersonal.IdCandidato = idCandidato;
                    referenciaPersonal.Activo = true;
                    await referenciasPersonalesCandidatoRepository.Insert(referenciaPersonal);
                }
            }
        }
        private static BaseResponse MapperResponse()
        {
            return new BaseResponse()
            {
                StatusCode = HttpStatusCode.OK,
                Message = "Candidato creado con exito"
            };
        }
        private static BaseResponse MapperResponseFail()
        {
            return new BaseResponse()
            {
                StatusCode = HttpStatusCode.Conflict,
                Message = "El Candidato ya fue creado con el documento digitado"
            };
        }


        public async Task<BaseResponse> Update(CandidatoRequest candidatoRequest)
        {
            var outPut = new BaseResponse();
            var strategy = manejoRHContext.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using (var transaction = manejoRHContext.Database.BeginTransaction())
                {
                    try
                    {
                        var updateResult = await UpdateCandidato(candidatoRequest);
                        if (updateResult)
                        {
                            await DeleteEstudios(candidatoRequest.IdCandidato);
                            await InsertEstudios(candidatoRequest.ListEstudioCandidatoRequest, candidatoRequest.IdCandidato);
                            await DeleteReferenciaLaborales(candidatoRequest.IdCandidato);
                            await InserReferenciasLaborales(candidatoRequest.ListReferenciasLaboralesCandidatoRequest, candidatoRequest.IdCandidato);
                            await DeleteReferenciasPersonales(candidatoRequest.IdCandidato);
                            await InsertReferenciasPersonales(candidatoRequest.ListReferenciasPersonalesCandidatoRequest, candidatoRequest.IdCandidato);
                            await transaction.CommitAsync();
                            outPut = MapperResponseUpdate();
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
        private async Task<bool> UpdateCandidato(CandidatoRequest candidatoRequest)
        {
            var candidato = await candidatoRepository.GetById(candidatoRequest.IdCandidato);
            if (candidato is not null)
            {
                candidato.IdTipoDocumento = candidatoRequest.IdTipoDocumento;
                candidato.Documento = candidatoRequest.Documento;
                candidato.PrimerNombre = candidatoRequest.PrimerNombre;
                candidato.SegundoNombre = candidatoRequest.SegundoNombre;
                candidato.PrimerApellido = candidatoRequest.PrimerApellido;
                candidato.SegundoApellido = candidatoRequest.SegundoApellido;
                candidato.SegundoApellido = candidatoRequest.SegundoApellido;
                candidato.NumeroTelefonico = candidatoRequest.NumeroTelefonico;
                candidato.Correo = candidatoRequest.Correo;
                string nameFile = candidatoRequest.Documento + candidatoRequest.PrimerApellido;
                candidato.UrlCV = string.IsNullOrEmpty(candidatoRequest.Base64CV) ? candidato.UrlCV : await GetPathDocsPdf(candidatoRequest.Base64CV, nameFile);
                candidato.UserIdModified = candidatoRequest.IdUser;
                candidato.DateModified = DateTime.Now;
                await candidatoRepository.Update(candidato);
                return true;
            }
            return false;
        }
        private async Task DeleteEstudios(int idVacante)
        {
            var listEstudiosCandidato = await estudioCandidatoRepository.GetListByParam(x => x.IdCandidato == idVacante);
            if (listEstudiosCandidato is not null || listEstudiosCandidato?.Count > 0)
            {
                foreach (var item in listEstudiosCandidato)
                {
                    item.Activo = false;
                    await estudioCandidatoRepository.Update(item);
                }
            }
        }
        private async Task DeleteReferenciaLaborales(int idVacante)
        {
            var listReferencais = await referenciasLaboralesCandidatoRepository.GetListByParam(x => x.IdCandidato == idVacante);
            if (listReferencais is not null || listReferencais?.Count > 0)
            {
                foreach (var item in listReferencais)
                {
                    item.Activo = false;
                    await referenciasLaboralesCandidatoRepository.Update(item);
                }
            }
        }
        private async Task DeleteReferenciasPersonales(int idVacante)
        {
            var listReferencais = await referenciasPersonalesCandidatoRepository.GetListByParam(x => x.IdCandidato == idVacante);
            if (listReferencais is not null || listReferencais?.Count > 0)
            {
                foreach (var item in listReferencais)
                {
                    item.Activo = false;
                    await referenciasPersonalesCandidatoRepository.Update(item);
                }
            }
        }
        private static BaseResponse MapperResponseUpdate()
        {
            return new BaseResponse()
            {
                StatusCode = HttpStatusCode.OK,
                Message = "Datos actualizados con exito"
            };
        }


        public async Task<BaseResponse> UpdateActiveCandidato(CandidatoActiveRequest candidatoActiveRequest) 
        {
            var outPut = new BaseResponse();
            try
            {
                var candidato = await candidatoRepository.GetById(candidatoActiveRequest.IdCandidato);
                if (candidato is not null)
                {
                    candidato.Activo = candidatoActiveRequest.Activo;
                    candidato.UserIdModified = candidatoActiveRequest.IdUser;
                    candidato.DateModified = DateTime.Now;
                    await candidatoRepository.Update(candidato);
                    outPut = MapperResponseUpdate();
                }
                else
                {
                    outPut = MapperResponseUpdateFailed();
                }

            }
            catch (Exception ex)
            {                
                outPut.StatusCode = HttpStatusCode.InternalServerError;
                outPut.Message = ex.Message;

            }
            return outPut;
        }
        private static BaseResponse MapperResponseUpdateFailed()
        {
            return new BaseResponse()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = "El id del candidato no existe"
            };
        }


        public async Task<BaseResponse> UpdateStateCandidato(CandidatoStateRequest candidatoStateRequest)
        {
            var outPut = new BaseResponse();
            try
            {
                var candidato = await candidatoRepository.GetById(candidatoStateRequest.IdCandidato);
                if (candidato is not null)
                {
                    candidato.IdEstadoCandidato = candidatoStateRequest.IdEstadoCandidato;
                    candidato.Comentarios = candidatoStateRequest.Comentarios;
                    candidato.UserIdModified = candidatoStateRequest.IdUser;
                    candidato.DateModified = DateTime.Now;
                    await candidatoRepository.Update(candidato);
                    outPut = MapperResponseUpdate();
                }
                else
                {
                    outPut = MapperResponseUpdateFailed();
                }

            }
            catch (Exception ex)
            {
                outPut.StatusCode = HttpStatusCode.InternalServerError;
                outPut.Message = ex.Message;

            }
            return outPut;


        }


        public async Task<BaseResponse> UpdateVerifyRefLaborales(List<ReferenciasLaboralesVerifyRequest> referenciasLaboralesVerifyRequests)
        {
            var outPut = new BaseResponse();
            try
            {
                if (referenciasLaboralesVerifyRequests is not null || referenciasLaboralesVerifyRequests?.Count > 0)
                {
                    await UpdateVerifyRefLaboral(referenciasLaboralesVerifyRequests);
                    outPut = MapperResponseUpdateRef(TipoReferencia.laborales.ToString());
                }
            }
            catch (Exception ex)
            {
                outPut.StatusCode = HttpStatusCode.InternalServerError;
                outPut.Message = ex.Message;

            }
            return outPut;
        }
        private async Task UpdateVerifyRefLaboral(List<ReferenciasLaboralesVerifyRequest> referenciasLaboralesVerifyRequests)
        {

            foreach (var item in referenciasLaboralesVerifyRequests)
            {
                var refLaboral = await referenciasLaboralesCandidatoRepository.GetById(item.IdReferenciasLaboralesCandidato);
                if (refLaboral is not null)
                {
                    refLaboral.Verificado = item.Verificado;
                    await referenciasLaboralesCandidatoRepository.Update(refLaboral);
                }
            }
        }
        private static BaseResponse MapperResponseUpdateRef(string tipoRef)
        {
            return new BaseResponse()
            {
                StatusCode = HttpStatusCode.OK,
                Message = $"Referencias {tipoRef} actualizadas con exito"
            };
        }


        public async Task<BaseResponse> UpdateVerifyRefPersonales(List<ReferenciasPersonalesVerifyRequest> referenciasPersonalesVerifyRequests)
        {

            var outPut = new BaseResponse();
            try
            {
                if (referenciasPersonalesVerifyRequests is not null || referenciasPersonalesVerifyRequests?.Count > 0)
                {
                    await UpdateVerifyRefPersonal(referenciasPersonalesVerifyRequests);
                    outPut = MapperResponseUpdateRef(TipoReferencia.personales.ToString());
                }
            }
            catch (Exception ex)
            {
                outPut.StatusCode = HttpStatusCode.InternalServerError;
                outPut.Message = ex.Message;

            }
            return outPut;
        }
        private async Task UpdateVerifyRefPersonal(List<ReferenciasPersonalesVerifyRequest> referenciasPersonalesVerifyRequests)
        {
            foreach (var item in referenciasPersonalesVerifyRequests)
            {
                var refLaboral = await referenciasPersonalesCandidatoRepository.GetById(item.IdReferenciasPersonalesCandidato);
                if (refLaboral is not null)
                {
                    refLaboral.Verificado = item.Verificado;
                    await referenciasPersonalesCandidatoRepository.Update(refLaboral);
                }
            }
        }


        public async Task<List<CandidatoResponse>> GetAllCandidatos()
        {
            List<CandidatoResponse> listCantidatos;
            try
            {
                var list = await candidatoRepository.GetAll();
                listCantidatos = MappeListCandidatos(list);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listCantidatos;  
        }
        private List<CandidatoResponse> MappeListCandidatos(List<Candidato> candidatos)
        {
            var listCantidatosResponse = new List<CandidatoResponse>();
            foreach (var item in candidatos)
            {
                var cantidato = mapper.Map<CandidatoResponse>(item);
                listCantidatosResponse.Add(cantidato);
            }
            return listCantidatosResponse;
        }


        public async Task<List<EstudioCandidatoResponse>> GetAllEstudiosCandidato(int idCandidato)
        {
            var listEstudiosResponse = new List<EstudioCandidatoResponse>();
            try
            {
                var listEstudios = await estudioCandidatoRepository.GetListByParam(x => x.IdCandidato == idCandidato && x.Activo);
                listEstudiosResponse = MappeListEstudiosCandidatos(listEstudios);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listEstudiosResponse;
        }
        private List<EstudioCandidatoResponse> MappeListEstudiosCandidatos(List<EstudioCandidato> estudioCandidatos)
        {
            var listEstudiosResponse = new List<EstudioCandidatoResponse>();
            foreach (var item in estudioCandidatos)
            {
                var cantidato = mapper.Map<EstudioCandidatoResponse>(item);
                listEstudiosResponse.Add(cantidato);
            }
            return listEstudiosResponse;
        }


        public async Task<List<ReferenciasPersonalesResponse>> GetAllRefPersonalesCandidato(int idCandidato)
        {
            var listRefPersonalesResponse = new List<ReferenciasPersonalesResponse>();
            try
            {
                var listRefPersonales= await referenciasPersonalesCandidatoRepository.GetListByParam(x => x.IdCandidato == idCandidato && x.Activo);
                listRefPersonalesResponse = MappeListRefPersonalesCandidatos(listRefPersonales);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listRefPersonalesResponse;
        }
        private List<ReferenciasPersonalesResponse> MappeListRefPersonalesCandidatos(List<ReferenciasPersonalesCandidato> listRefPersonales)
        {
            var listRefPersonalesResponse = new List<ReferenciasPersonalesResponse>();
            foreach (var item in listRefPersonales)
            {
                var cantidato = mapper.Map<ReferenciasPersonalesResponse>(item);
                listRefPersonalesResponse.Add(cantidato);
            }
            return listRefPersonalesResponse;
        }


        public async Task<List<ReferenciasLaboralesResponse>> GetAllRefLaboralesCandidato(int idCandidato)
        {
            var listRefLaboralesResponse = new List<ReferenciasLaboralesResponse>();
            try
            {
                var listRefLaborales = await referenciasLaboralesCandidatoRepository.GetListByParam(x => x.IdCandidato == idCandidato && x.Activo);
                listRefLaboralesResponse = MappeListRefLaboralesCandidatos(listRefLaborales);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listRefLaboralesResponse;
        }
        private List<ReferenciasLaboralesResponse> MappeListRefLaboralesCandidatos(List<ReferenciasLaboralesCandidato> listRefLaborales)
        {
            var listRefLaboralesResponse = new List<ReferenciasLaboralesResponse>();
            foreach (var item in listRefLaborales)
            {
                var cantidato = mapper.Map<ReferenciasLaboralesResponse>(item);
                listRefLaboralesResponse.Add(cantidato);
            }
            return listRefLaboralesResponse;
        }



    }
}