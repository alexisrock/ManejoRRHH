using AutoMapper;
using Core.Common;
using Core.Interfaces;
using DataAccess.Interface;
using DataAccess.Repository;
using Domain.Common;
using Domain.Common.Enum;
using Domain.Dto;
using Domain.Entities;
using Domain.Entities.StoreProcedure;
using System.Collections.Generic;
using System.Net;

namespace Core.Repository
{
    public class EmpleadoService : IEmpleadoService
    {

        private readonly IRepository<CertificadoEstudiantilEmpleado> certificadoEstudiantilEmpleadoRepository;
        private readonly IRepository<CertificadosEmpleado> certificadosEmpleadoRepository;
        private readonly IRepository<Candidato> candidatoRepository;
        private readonly IRepository<Empleado> empleadoRepository;

        private readonly IRepository<Configuracion> configuiuracionRepository;
        private readonly IStoreProcedureRepository storeProcedureRepository;
        private readonly IMapper mapper;

        public EmpleadoService(IStoreProcedureRepository storeProcedureRepository, IMapper mapper, IRepository<CertificadoEstudiantilEmpleado> certificadoEstudiantilEmpleadoRepository
            , IRepository<Configuracion> configuiuracionRepository, IRepository<Candidato> candidatoRepository, IRepository<CertificadosEmpleado> certificadosEmpleadoRepository
            , IRepository<Empleado> empleadoRepository)
        {
            this.storeProcedureRepository = storeProcedureRepository;
            this.certificadoEstudiantilEmpleadoRepository = certificadoEstudiantilEmpleadoRepository;
            this.empleadoRepository = empleadoRepository;
            this.configuiuracionRepository = configuiuracionRepository;
            this.candidatoRepository = candidatoRepository;
            this.certificadosEmpleadoRepository = certificadosEmpleadoRepository;
            this.mapper = mapper;
        }


        public async Task<BaseResponse> CreateCertificados(ContratoCreateRequest contratoRequest)
        {
            var outPut = new BaseResponse();

            try
            {

                var nameFile = "Estudios" + await Name(contratoRequest.IdCandidato);
                await InsertCertificadosEstudiantiles(contratoRequest, nameFile);
                var nameFilePersonales = "CertPersonales" + await Name(contratoRequest.IdCandidato);
                await InsertCertificadosPersonales(contratoRequest, nameFilePersonales);
                var nameFileLaborales = "CertLaborales" + await Name(contratoRequest.IdCandidato);
                await InsertCertificadosLaborales(contratoRequest, nameFileLaborales);
                outPut = MapperResponse("Empleado actualizado con exito", HttpStatusCode.OK);
            }
            catch (Exception ex)
            {

                outPut = MapperResponse(ex.Message, HttpStatusCode.InternalServerError);
            }
            return outPut;
        }
        private async Task InsertCertificadosEstudiantiles(ContratoCreateRequest contratoRequest, string nameFile)
        {

            if (contratoRequest.CertificadosEstudiantiles is not null && contratoRequest.CertificadosEstudiantiles.Count > 0)
            {
                foreach (var item in contratoRequest.CertificadosEstudiantiles)
                {
                    var certificacionEstudiantil = new CertificadoEstudiantilEmpleado();
                    certificacionEstudiantil.IdCandidato = contratoRequest.IdCandidato;
                    certificacionEstudiantil.Activo = true;
                    var num = new Random();
                    nameFile = string.Concat(nameFile, num.Next().ToString());
                    certificacionEstudiantil.UrlCertificado = string.IsNullOrEmpty(item.Base64CertificadoEstudiantil) ? null : await GetPathDocsPdf(item.Base64CertificadoEstudiantil, nameFile);
                    await certificadoEstudiantilEmpleadoRepository.Insert(certificacionEstudiantil);
                }
            }

        }
        private async Task InsertCertificadosPersonales(ContratoCreateRequest contratoRequest, string nameFile)
        {
            if (contratoRequest.CertificadosPersonales is not null && contratoRequest.CertificadosPersonales.Count > 0)
            {
                foreach (var item in contratoRequest.CertificadosPersonales)
                {
                    var certificacionPersonal = new CertificadosEmpleado();
                    certificacionPersonal.IdCandidato = contratoRequest.IdCandidato;
                    certificacionPersonal.Activo = true;
                    var num = new Random();
                    certificacionPersonal.IdTipoCertificado = TipoReferencia.personales.GetIdTipoReferencia();
                    nameFile = string.Concat(nameFile, num.Next().ToString());
                    certificacionPersonal.UrlCertificado = string.IsNullOrEmpty(item.Base64CertificadoPersonal) ? null : await GetPathDocsPdf(item.Base64CertificadoPersonal, nameFile);
                    await certificadosEmpleadoRepository.Insert(certificacionPersonal);
                }
            }
        }
        private async Task InsertCertificadosLaborales(ContratoCreateRequest contratoRequest, string nameFile)
        {
            if (contratoRequest.CertificadosLaborales is not null && contratoRequest.CertificadosLaborales.Count > 0)
            {
                foreach (var item in contratoRequest.CertificadosLaborales)
                {
                    var certificacionLaborales = new CertificadosEmpleado();
                    certificacionLaborales.IdCandidato = contratoRequest.IdCandidato;
                    certificacionLaborales.Activo = true;
                    var num = new Random();
                    certificacionLaborales.IdTipoCertificado = TipoReferencia.laborales.GetIdTipoReferencia();
                    nameFile = string.Concat(nameFile, num.Next().ToString());
                    certificacionLaborales.UrlCertificado = string.IsNullOrEmpty(item.Base64CertificadoLaboral) ? null : await GetPathDocsPdf(item.Base64CertificadoLaboral, nameFile);
                    await certificadosEmpleadoRepository.Insert(certificacionLaborales);
                }
            }
        }
        private static BaseResponse MapperResponse(string mensaje, HttpStatusCode httpStatusCode)
        {
            return new BaseResponse()
            {
                StatusCode = httpStatusCode,
                Message = mensaje
            };
        }


        public async Task<BaseResponse> UpdateCertificados(ContratoEditRequest contratoRequest)
        {
            var outPut = new BaseResponse();

            try
            {
                await UpdateListCerticacionEstudiantil(contratoRequest);
                await UpdateListCerticacionPersonal(contratoRequest);
                await UpdateListCerticacionLaboral(contratoRequest);
                outPut = MapperResponse("Empleado actualizado con exito", HttpStatusCode.OK);
            }
            catch (Exception ex)
            {

                outPut = MapperResponse(ex.Message, HttpStatusCode.InternalServerError);
            }
            return outPut;
        }
        private async Task UpdateListCerticacionEstudiantil(ContratoEditRequest contratoRequest)
        {
            var nameFile = "Estudios" + await Name(contratoRequest.IdCandidato);
            if (contratoRequest.CertificadosEstudiantiles is not null && contratoRequest.CertificadosEstudiantiles.Count > 0)
            {
                foreach (var item in contratoRequest.CertificadosEstudiantiles)
                {
                    if (!item.Activo && item.IdCertificado > 0)
                        await DeleteCerticacionEstudiantil(item.IdCertificado);

                    if (item.Activo && !string.IsNullOrEmpty(item.Base64CertificadoEstudiantil))
                        await InsertEditCerticacionEstudiantil(item, nameFile, contratoRequest.IdCandidato);
                }

            }
        }
        private async Task DeleteCerticacionEstudiantil(int IdCertificadoEstudiantil)
        {
            var certificadoEstudiantil = await certificadoEstudiantilEmpleadoRepository.GetById(IdCertificadoEstudiantil);
            if (certificadoEstudiantil is not null)
            {
                certificadoEstudiantil.Activo = false;
                await certificadoEstudiantilEmpleadoRepository.Update(certificadoEstudiantil);
            }
        }
        private async Task InsertEditCerticacionEstudiantil(CertificadosEstudiantilesEditRequest certificado, string nameFile, int idCandidato)
        {
            var certificacionPersonal = new CertificadosEmpleado();
            certificacionPersonal.IdCandidato = idCandidato;
            certificacionPersonal.Activo = true;
            var num = new Random();
            certificacionPersonal.IdTipoCertificado = TipoReferencia.personales.GetIdTipoReferencia();
            nameFile = string.Concat(nameFile, num.Next().ToString());
            certificacionPersonal.UrlCertificado = string.IsNullOrEmpty(certificado.Base64CertificadoEstudiantil) ? null : await GetPathDocsPdf(certificado.Base64CertificadoEstudiantil, nameFile);
            await certificadosEmpleadoRepository.Insert(certificacionPersonal);


        }
        private async Task UpdateListCerticacionPersonal(ContratoEditRequest contratoRequest)
        {
            var nameFilePersonales = "CertPersonales" + await Name(contratoRequest.IdCandidato);
            if (contratoRequest.CertificadosPersonales is not null && contratoRequest.CertificadosPersonales.Count > 0)
            {
                foreach (var item in contratoRequest.CertificadosPersonales)
                {
                    if (!item.Activo && item.IdCertificado > 0)
                        await DeleteCerticacion(item.IdCertificado);

                    if (item.Activo && !string.IsNullOrEmpty(item.Base64CertificadoPersonal))
                        await InsertEditCerticacion(item, nameFilePersonales, contratoRequest.IdCandidato);
                }

            }
        }
        private async Task UpdateListCerticacionLaboral(ContratoEditRequest contratoRequest)
        {
            var nameFilePersonales = "CertPersonales" + await Name(contratoRequest.IdCandidato);
            if (contratoRequest.CertificadosLaborales is not null && contratoRequest.CertificadosLaborales.Count > 0)
            {
                foreach (var item in contratoRequest.CertificadosLaborales)
                {
                    if (!item.Activo && item.IdCertificado > 0)
                        await DeleteCerticacion(item.IdCertificado);

                    if (item.Activo && !string.IsNullOrEmpty(item.Base64CertificadoLaboral))
                        await InsertEditCerticacion(item, nameFilePersonales, contratoRequest.IdCandidato);
                }

            }
        }
        private async Task DeleteCerticacion(int IdCertificadoEstudiantil)
        {
            var certificado = await certificadosEmpleadoRepository.GetById(IdCertificadoEstudiantil);
            if (certificado is not null)
            {
                certificado.Activo = false;
                await certificadosEmpleadoRepository.Update(certificado);
            }
        }
        private async Task InsertEditCerticacion(CertificadoPersonalEditRequest certificado, string nameFile, int idCandidato)
        {
            var certificacionPersonal = new CertificadosEmpleado();
            certificacionPersonal.IdCandidato = idCandidato;
            certificacionPersonal.Activo = true;
            var num = new Random();
            certificacionPersonal.IdTipoCertificado = TipoReferencia.personales.GetIdTipoReferencia();
            nameFile = string.Concat(nameFile, num.Next().ToString());
            certificacionPersonal.UrlCertificado = string.IsNullOrEmpty(certificado.Base64CertificadoPersonal) ? null : await GetPathDocsPdf(certificado.Base64CertificadoPersonal, nameFile);
            await certificadosEmpleadoRepository.Insert(certificacionPersonal);


        }
        private async Task InsertEditCerticacion(CertificadoLaboralEditRequest certificado, string nameFile, int idCandidato)
        {
            var certificacionPersonal = new CertificadosEmpleado();
            certificacionPersonal.IdCandidato = idCandidato;
            certificacionPersonal.Activo = true;
            var num = new Random();
            certificacionPersonal.IdTipoCertificado = TipoReferencia.laborales.GetIdTipoReferencia();
            nameFile = string.Concat(nameFile, num.Next().ToString());
            certificacionPersonal.UrlCertificado = string.IsNullOrEmpty(certificado.Base64CertificadoLaboral) ? null : await GetPathDocsPdf(certificado.Base64CertificadoLaboral, nameFile);
            await certificadosEmpleadoRepository.Insert(certificacionPersonal);
        }

        private async Task<string> Name(int idCandidato)
        {
            var candidato = await candidatoRepository.GetById(idCandidato);
            if (candidato is not null)
                return $"{candidato.Documento}";

            return string.Empty;
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

        public async Task<SPInfoEmployeeResponse> GetInfoEmployeeById(long idEmpleado)
        {
            var sPInfoEmployeeResponse = new SPInfoEmployeeResponse();
            try
            {

                var employee = await GetInfoEmployee(idEmpleado);
                if (employee is not null)
                {
                    sPInfoEmployeeResponse = mapper.Map<SPInfoEmployeeResponse>(employee);
                    sPInfoEmployeeResponse.StatusCode = HttpStatusCode.OK;

                }
                else
                {
                    sPInfoEmployeeResponse.StatusCode = HttpStatusCode.NotFound;
                    sPInfoEmployeeResponse.Message = "Id de empleado no se encuentra";
                }

            }
            catch (Exception ex)
            {
                sPInfoEmployeeResponse.Message = ex.Message;
                sPInfoEmployeeResponse.StatusCode = HttpStatusCode.InternalServerError;
            }
            return sPInfoEmployeeResponse;
        }
        private async Task<SPInfoEmployee> GetInfoEmployee(long idEmpleado)
        {
            return await storeProcedureRepository.GetSPInfoEmployeeById(idEmpleado);
        }

        public async Task<List<SPHistoricalNoverltyEmployeeResponse>> GetHistoricalNoverltyEmployees(long idEmpleado)
        {
            List<SPHistoricalNoverltyEmployeeResponse> list;
            try
            {
                var historicalNpvelty = await GetSPHistoricalNoverltyEmployee(idEmpleado);
                list = MapperSPHistoricalNoverltyEmployeeResponse(historicalNpvelty);
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private async Task<List<SPHistoricalNoverltyEmployee>> GetSPHistoricalNoverltyEmployee(long idEmpleado)
        {
            return await storeProcedureRepository.GetHistoricalNoverltyByEmployee(idEmpleado);
        }
        private List<SPHistoricalNoverltyEmployeeResponse> MapperSPHistoricalNoverltyEmployeeResponse(List<SPHistoricalNoverltyEmployee> sPHistoricalNoverltyEmployees)
        {
            var sPHistoricalNoverltyEmployeeResponses = new List<SPHistoricalNoverltyEmployeeResponse>();
            if (sPHistoricalNoverltyEmployees is not null && sPHistoricalNoverltyEmployees.Count > 0)
            {
                sPHistoricalNoverltyEmployees.ForEach(emp =>
                {
                    var SPHistoricalNoverltyEmployeeResponse = mapper.Map<SPHistoricalNoverltyEmployeeResponse>(emp);
                    sPHistoricalNoverltyEmployeeResponses.Add(SPHistoricalNoverltyEmployeeResponse);

                });
            }
            return sPHistoricalNoverltyEmployeeResponses;
        }

        public async Task<CertificadosResponse> GetCertificadosByEmployee(long idEmpleado)
        {
            var certificadosResponse = new CertificadosResponse();
            try
            {

                var empleado = await GetEmployeeById(idEmpleado);
                if (empleado is not null)
                {
                    var certEstudiantiles = await GetCertificadoEstudiantils(empleado.IdCandidato);
                    certificadosResponse.CertificadoEstudiantilResponses = MapperCertificadoEstudiantilResponses(certEstudiantiles);
                    var certPersonales = await GetCertificadoEmpleado(empleado.IdCandidato, TipoReferencia.personales.GetIdTipoReferencia());
                    certificadosResponse.CertificadoPersonalResponses = MapperCertificadoPersonalResponse(certPersonales);
                    var certLaborales = await GetCertificadoEmpleado(empleado.IdCandidato, TipoReferencia.personales.GetIdTipoReferencia());
                    certificadosResponse.CertificadoLaboralResponses = MapperCertificadoLaboralesResponse(certLaborales);
                }

            }
            catch (Exception)
            {
                throw;
            }
            return certificadosResponse;
        }
        private async Task<Empleado> GetEmployeeById(long idEmpleado)
        {
            return await empleadoRepository.GetById(idEmpleado);
        }
        private async Task<List<CertificadoEstudiantilEmpleado>> GetCertificadoEstudiantils(int idCandidato)
        {
            return await certificadoEstudiantilEmpleadoRepository.GetListByParam(x => x.IdCandidato == idCandidato && x.Activo);
        }
        private  List<CertificadoEstudiantilResponse> MapperCertificadoEstudiantilResponses(List<CertificadoEstudiantilEmpleado> certificadoEstudiantilEmpleados)
        {
            var listCertEstudiantiles = new List<CertificadoEstudiantilResponse>();


            if (certificadoEstudiantilEmpleados is not null && certificadoEstudiantilEmpleados.Count >0 )
            {
                certificadoEstudiantilEmpleados.ForEach(x =>
                {
                    var CertificadoEstudiantilResponse = mapper.Map<CertificadoEstudiantilResponse>(x);
                    listCertEstudiantiles.Add(CertificadoEstudiantilResponse);

                });
            }
             return listCertEstudiantiles;
        }
        private async Task<List<CertificadosEmpleado>> GetCertificadoEmpleado(int idCandidato, int idTipoCertificado)
        {
            return await certificadosEmpleadoRepository.GetListByParam(x => x.IdCandidato== idCandidato && x.IdTipoCertificado== idTipoCertificado && x.Activo);
        }
        private  List<CertificadoPersonalResponse> MapperCertificadoPersonalResponse(List<CertificadosEmpleado> certificadosEmpleados)
        {
            var listCertEmpleado = new List<CertificadoPersonalResponse>();
            if (certificadosEmpleados is not null && certificadosEmpleados.Count > 0)
            {
                certificadosEmpleados.ForEach(x =>
                {
                    var CertificadoResponse = mapper.Map<CertificadoPersonalResponse>(x);
                    listCertEmpleado.Add(CertificadoResponse);

                });

            }
            return listCertEmpleado;
        }
        private  List<CertificadoLaboralResponse> MapperCertificadoLaboralesResponse(List<CertificadosEmpleado> certificadosEmpleados)
        {
            var listCertEmpleado = new List<CertificadoLaboralResponse>();
            if (certificadosEmpleados is not null && certificadosEmpleados.Count > 0)
            {
                certificadosEmpleados.ForEach(x =>
                {
                    var CertificadoResponse = mapper.Map<CertificadoLaboralResponse>(x);
                    listCertEmpleado.Add(CertificadoResponse);

                });

            }
            return listCertEmpleado;
        }

    }
}
