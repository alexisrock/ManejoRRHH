using AutoMapper;
using Core.Interfaces;
using DataAccess.Interface;
using Domain.Common;
using Domain.Common.Enum;
using Domain.Dto;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repository
{
    public class ProcesoService : IProcesoService
    {
        private readonly IRepository<Proceso> procesoRepository;

        private readonly IMapper mapper;

        public ProcesoService(IMapper mapper, IRepository<Proceso> procesoRepository)
        {
            this.mapper = mapper;
            this.procesoRepository = procesoRepository;
        }

        public async Task<BaseResponse> Create(ProcesoRequest procesoRequest)
        {
            var output = new BaseResponse();

            try
            {
                await Insert(procesoRequest);
                output = MapperBaseResponse("Candidato agregado con exito");

            }
            catch (Exception ex)
            {
                output.StatusCode = HttpStatusCode.InternalServerError;
                output.Message = ex.Message;

            }
            return output;

        }
        private async Task Insert(ProcesoRequest procesoRequest)
        {
            var proceso = mapper.Map<Proceso>(procesoRequest);
            proceso.IdEstadoProceso = TipoEstadoProceso.EnProceso.GetIdEstadoProceso();
            await procesoRepository.Insert(proceso);
        }
        private static BaseResponse MapperBaseResponse(string cadena)
        {

            return new BaseResponse()
            {
                StatusCode = HttpStatusCode.OK,
                Message = cadena
            };

        }


        public async Task<BaseResponse> UpdateDisponibilidaCandidato(ProcesoEstadoRequest procesoEstadoRequest)
        {
            var output = new BaseResponse();

            try
            {

                await UpdateStateCandidate(procesoEstadoRequest);
                output = MapperBaseResponse("Estado del candidato cambiado");

            }
            catch (Exception ex)
            {
                output.StatusCode = HttpStatusCode.InternalServerError;
                output.Message = ex.Message;

            }
            return output;

        }


        private async Task UpdateStateCandidate(ProcesoEstadoRequest procesoEstadoRequest)
        {
            var process = await procesoRepository.GetByParam(x => x.IdProceso == procesoEstadoRequest.IdProceso);
            if (process is not null)
            {
                process.IdEstadoProceso = procesoEstadoRequest.IdEstado;
                await procesoRepository.Update(process);
            }
        }


        public async Task<List<ProcesoResponse>> GetCandidatosByVacante(int idVacante)
        {

            List<ProcesoResponse> listCantidatos;
            try
            {
                var list = await procesoRepository.GetAllByParamIncluding((x => x.Idvacante == idVacante), (e => e.EstadoProceso), (e => e.Candidato), (e => e.Vacante));
                listCantidatos = MappeListCandidatosInclude(list);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listCantidatos;
        }
        private List<ProcesoResponse> MappeListCandidatosInclude(List<Proceso> procesos)
        {
            var listCantidatos = new List<ProcesoResponse>();
            foreach (var item in procesos)
            {
                var procesoResponse = new ProcesoResponse();
                procesoResponse.IdProceso = item.IdProceso;
                procesoResponse.Idvacante = item.Idvacante;
                procesoResponse.DescriptionVacante = item.Vacante?.DescripcionCargo;
                procesoResponse.IdCandidato = item.IdCandidato;
                procesoResponse.DescriptionNombreCandidato = string.Concat(item.Candidato?.PrimerNombre, " ", item.Candidato?.SegundoNombre, " ", item.Candidato?.PrimerApellido, "  ", item.Candidato?.SegundoApellido);
                procesoResponse.IdEstadoProceso = item.IdEstadoProceso;
                procesoResponse.DescriptionEstadoProceso = item.EstadoProceso?.Description;
                listCantidatos.Add(procesoResponse);
            }

            return listCantidatos;
        }


    }
}
