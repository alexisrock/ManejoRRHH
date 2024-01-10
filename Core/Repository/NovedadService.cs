using AutoMapper;
using Core.Interfaces;
using DataAccess.Interface;
using Domain.Common;
using Domain.Dto;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repository
{
    public class NovedadService: INovedadService
    {

		private readonly IRepository<Novedad> novedadRepository;
        private readonly IMapper mapper;

        public NovedadService(IRepository<Novedad> novedadRepository, IMapper mapper)
        {
            this.novedadRepository = novedadRepository;
            this.mapper = mapper;
        }

        public  async Task<BaseResponse> Create(NovedadRequest novedadRequest)
        {
            var baseResponse = new BaseResponse();
            try
			{
                await InsertNovedad(novedadRequest);
                baseResponse = MapperBaseResponse("Novedad creada con exito", HttpStatusCode.OK);
            }
			catch (Exception ex)
			{
                baseResponse.StatusCode = HttpStatusCode.InternalServerError; 
                baseResponse.Message = ex.Message;                 
			}
            return baseResponse;
        }
		private async Task InsertNovedad(NovedadRequest novedadRequest)
		{
            var novedad = mapper.Map<Novedad>(novedadRequest);
            novedad.IdUserCreated = novedadRequest.IdUser;
            novedad.DateCreated = DateTime.Now;
            await novedadRepository.Insert(novedad);
        }
        private BaseResponse MapperBaseResponse(string mensaje, HttpStatusCode httpStatusCode)
        {
            var baseResponse = new BaseResponse();
            baseResponse.StatusCode = httpStatusCode;
            baseResponse.Message = mensaje;
            return baseResponse;
        }

        public async Task<BaseResponse> Update(NovedadRequest novedadRequest)
        {
            var baseResponse = new BaseResponse();

            try
            {
                var novedad = await GetIdNovedad(novedadRequest.IdNovedad);
                if (novedad is not null)
                {
                    await UpdateNovedad(novedad, novedadRequest);
                    baseResponse = MapperBaseResponse("Novedad actualizada con exito", HttpStatusCode.OK);
                }
                else
                {
                    baseResponse = MapperBaseResponse("El id de la novedad no existe", HttpStatusCode.NotFound);
                }
            }
            catch (Exception ex)
            {
                baseResponse.StatusCode = HttpStatusCode.InternalServerError;
                baseResponse.Message = ex.Message;
            }
            return baseResponse;
        }
        private async Task UpdateNovedad(Novedad novedad, NovedadRequest novedadStateRequest)
        {
            novedad.IdTipoNovedad = novedadStateRequest.IdTipoNovedad;
            novedad.Observacion = novedadStateRequest.Observacion;
            novedad.Activo = novedadStateRequest.Activo;
            novedad.Anio = novedadStateRequest.Anio;
            novedad.Mes = novedadStateRequest.Mes;
            novedad.Dia = novedadStateRequest.Dia;
            novedad.DiasIncapacidad = novedadStateRequest.DiasIncapacidad;
            novedad.DiasVacaciones = novedadStateRequest.DiasVacaciones;
            novedad.DiasNoRemunerados = novedadStateRequest.DiasNoRemunerados;
            novedad.UserIdModified = novedadStateRequest.IdUser;
            novedad.DateModified = DateTime.Now;
            await novedadRepository.Update(novedad);
        }

        public async Task<BaseResponse> UpdateStateNovelty(NovedadStateRequest novedadStateRequest)
        {
            var baseResponse = new BaseResponse();
            try
            {
                var novedad = await GetIdNovedad(novedadStateRequest.IdNovedad);
                if (novedad is not null)
                {
                    await UpdateState(novedad, novedadStateRequest);
                    baseResponse = MapperBaseResponse("Se actualizo el estado de la novedad", HttpStatusCode.OK);
                }
                else
                {
                    baseResponse = MapperBaseResponse("El id de la novedad no existe", HttpStatusCode.NotFound);
                }

            }
            catch (Exception ex)
            {
                baseResponse.StatusCode= HttpStatusCode.InternalServerError;    
                baseResponse.Message = ex.Message;
            }
            return baseResponse;
        }
        private async Task<Novedad> GetIdNovedad(int IdNovedad)
        {
            return await novedadRepository.GetById(IdNovedad);
        }
        private async Task UpdateState(Novedad novedad , NovedadStateRequest novedadStateRequest)
        {
            novedad.Activo = novedadStateRequest.Activo;
            novedad.UserIdModified = novedadStateRequest.IdUser;
            novedad.DateModified = DateTime.Now;
            await novedadRepository.Update(novedad);
        }


        public async Task<List<NovedadResponse>> GetAll()
        {
            var novedadResponses = new List<NovedadResponse>();

            try
            {
                var novedades = await GetNovedades();
                novedadResponses = MapperNovedadResponse(novedades);
                return novedadResponses;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private async Task<List<Novedad>> GetNovedades()
        {
            return await novedadRepository.GetAllByParamIncluding(x => x.Activo, (x => x.TipoNovedad));
        }
        private List<NovedadResponse> MapperNovedadResponse(List<Novedad> novedads)
        {
            var novedadResponses = new List<NovedadResponse>();
            novedads.ForEach( x =>
            {
                var novedadResponse = new NovedadResponse();
                novedadResponse.Observacion = x.Observacion;
                novedadResponse.IdNovedad = x.IdNovedad;
                novedadResponse.FechaCreacion = x.DateCreated?.ToString("yyyy-MM-dd") ?? string.Empty;
                novedadResponse.TipoNovedadDescripcion = x.TipoNovedad.Description;
                novedadResponse.DiasIncapacidad = x.DiasIncapacidad;
                novedadResponse.DiasVacaciones = x.DiasVacaciones;
                novedadResponse.DiasNoRemunerados = x.DiasNoRemunerados;
                novedadResponses.Add(novedadResponse);

            } );

            return novedadResponses;
        }



        public async Task<List<NovedadResponse>> GetNoveltyByCandidate(int idCandidato)
        {
            var novedadResponses = new List<NovedadResponse>();

            try
            {
                var novedades = await GetNovedadByIdCandidate(idCandidato);
                novedadResponses = MapperNovedadResponse(novedades);
                return novedadResponses;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private async Task<List<Novedad>> GetNovedadByIdCandidate(int IdCandadate)
        {
            return await novedadRepository.GetAllByParamIncluding(x => x.Activo && x.IdCandidato== IdCandadate, (x => x.TipoNovedad));
        }
    }
}
