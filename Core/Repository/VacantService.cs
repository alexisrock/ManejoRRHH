using AutoMapper;
using Core.Interfaces;
using DataAccess;
using DataAccess.Interface;
using Domain.Common;
using Domain.Common.Enum;
using Domain.Dto;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Net;


namespace Core.Repository
{
    public class VacantService : IVacantService
    {


        private readonly IRepository<Vacante> vacanteRepository;
        private readonly IRepository<SkillVacante> skillVacanteRepository;      
        private readonly IMapper mapper;
        private readonly ManejoRHContext manejoRHContext;

        public VacantService(IRepository<Vacante> vacanteRepository, IRepository<SkillVacante> skillVacanteRepository, IMapper mapper, ManejoRHContext manejoRHContext )
        {
            this.vacanteRepository = vacanteRepository;
            this.skillVacanteRepository = skillVacanteRepository;
            this.mapper = mapper;
            this.manejoRHContext = manejoRHContext;            
        }

        public async Task<BaseResponse> Create(VacanteRequest vacanteRequest)
        {
            var outPut = new BaseResponse();
            var strategy = manejoRHContext.Database.CreateExecutionStrategy();

            await strategy.ExecuteAsync(async () =>
            {
                using (var transaction = manejoRHContext.Database.BeginTransaction())
                {
                    try
                    {
                        var idVacante = await InsertVacante(vacanteRequest);
                        await InsertSkillVacantes(vacanteRequest, idVacante);
                        await transaction.CommitAsync();
                        outPut = MapperResponse();

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
        private async Task<int> InsertVacante(VacanteRequest vacanteRequest)
        {
            var Vacante = mapper.Map<Vacante>(vacanteRequest);
            Vacante.IdEstadoVacante = TipoEstadoVacante.Activo.GetIdTipoEstado();
            Vacante.IdUserCreated = vacanteRequest.IdUser;
            Vacante.DateCreated = DateTime.Now;
            await vacanteRepository.Insert(Vacante);
            return Vacante.IdVacante;
        }
        private async Task InsertSkillVacantes(VacanteRequest vacanteRequest, int idVacante)
        {
            foreach (var vacante in vacanteRequest.ListSkillsVacante)
            {
                var skillVacante = mapper.Map<SkillVacante>(vacante);
                skillVacante.IdVacante = idVacante;
                await skillVacanteRepository.Insert(skillVacante);
            }
        }
        private static BaseResponse MapperResponse()
        {
            return new BaseResponse()
            {
                StatusCode = HttpStatusCode.OK,
                Message = "Vacante creada con exito"
            };
        }

        public async Task<BaseResponse> Update(VacanteRequest vacanteRequest)
        {
            var outPut = new BaseResponse();
            var strategy = manejoRHContext.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using (var transaction = manejoRHContext.Database.BeginTransaction())
                {
                    try
                    {
                        await UpdateVacante(vacanteRequest);
                        await DeleteSkillVacante(vacanteRequest);
                        await InsertSkillVacantes(vacanteRequest, vacanteRequest.IdVacante);
                        await transaction.CommitAsync();
                        outPut = MapperUpdateResponse();

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
        private async Task UpdateVacante(VacanteRequest vacanteRequest)
        {
            var vacante = await vacanteRepository.GetById(vacanteRequest.IdVacante);
            if (vacante is not null)
            {
                vacante.IdCliente = vacanteRequest.IdCliente;
                vacante.DescripcionCargo = vacanteRequest.DescripcionCargo;
                vacante.Profesion = vacanteRequest.Profesion;
                vacante.TiempoExperiencia = vacanteRequest.TiempoExperiencia;
                vacante.IdContrato = vacanteRequest.IdContrato;
                vacante.IdSalario = vacanteRequest.IdSalario;
                vacante.Horario = vacanteRequest.Horario;
                vacante.IdModalidadTrabajo = vacanteRequest.IdModalidadTrabajo;
                vacante.Idioma = vacanteRequest.Idioma;
                vacante.PorcentajeIdioma = vacanteRequest.PorcentajeIdioma;
                vacante.PruebaTecnica = vacanteRequest.PruebaTecnica;
                vacante.DescripcionFunciones = vacanteRequest.DescripcionFunciones;
                vacante.IdEstadoVacante = TipoEstadoVacante.Activo.GetIdTipoEstado(); 
                vacante.Comentarios = vacanteRequest.Comentarios;
                vacante.Comentarios = vacanteRequest.Comentarios;
                vacante.UserIdModified = vacanteRequest.IdUser;
                vacante.DateModified = DateTime.Now;
                await vacanteRepository.Update(vacante);
            }
        }
        private async Task DeleteSkillVacante(VacanteRequest vacanteRequest)
        {

            var listSkillVacantes = await skillVacanteRepository.GetListByParam(x => x.IdVacante == vacanteRequest.IdVacante && x.Activo);
            if (listSkillVacantes is not null || listSkillVacantes?.Count > 0)
            {
                foreach (var item in listSkillVacantes)
                {
                    item.Activo = false;
                    await skillVacanteRepository.Update(item);
                }
            }
        }
        private static BaseResponse MapperUpdateResponse()
        {
            return new BaseResponse()
            {
                StatusCode = HttpStatusCode.OK,
                Message = "Vacante actualizada con exito"
            };
        }


        public async Task<BaseResponse> UpdateState(VacanteStateRequest vacanteStateRequest) 
        {
            var outPut = new BaseResponse();
            try
            {
                var vacante = await vacanteRepository.GetById(vacanteStateRequest.IdVacante);
                if (vacante is not null) 
                {
                    vacante.IdEstadoVacante = vacanteStateRequest.IdEstadoVacante;
                    vacante.UserIdModified = vacanteStateRequest.IdUser;
                    vacante.DateModified = DateTime.Now;
                    await vacanteRepository.Update(vacante);
                }

            }
            catch (Exception ex)
            {
                outPut.StatusCode = HttpStatusCode.InternalServerError;
                outPut.Message = ex.Message;
            }
        
            return outPut;
        }

        public async Task<List<VacanteDetailResponse>> GetAllVacantes()
        {
            List<VacanteDetailResponse> listVacantes;
            try
            {
                var list = await vacanteRepository.GetAllByParamIncluding(null, (x => x.ModalidadTrabajo),(x => x.EstadoVacante), (x => x.Salario), (x => x.Contrato));
                listVacantes = MapperListVacanteResponse(list);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listVacantes;
        }
        private List<VacanteDetailResponse> MapperListVacanteResponse(List<Vacante> vacantes)
        {
            var listVacantes = new List<VacanteDetailResponse>();
            foreach (var item in vacantes)
            {
                var vacante = mapper.Map<VacanteDetailResponse>(item);
                vacante.DescripcionContrato = item.Contrato?.Description;
                vacante.DescripcionSalario = item.Salario?.Description;
                vacante.DescripcionModalidadTrabajo = item.ModalidadTrabajo?.Description;
                vacante.DescripcionEstadoVacante = item.EstadoVacante?.Description;
                listVacantes.Add(vacante);
            }
            return listVacantes;    
        }

        public async Task<VacanteResponse> GetById(int idVacante) 
        {
            VacanteResponse vacanteResponse;
            try
            {
                var vacante = await vacanteRepository.GetById(idVacante);
                vacanteResponse = mapper.Map<VacanteResponse>(vacante);
                vacanteResponse.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception)
            {

                throw;
            }        
            return vacanteResponse;
        }

        public async Task<List<SkillVacanteResponse>> GetSkillsByIdVacante(int idVacante)
        {
            List<SkillVacanteResponse> listSkillVacante;

            try
            {
                var skills = await skillVacanteRepository.GetListByParam(x => x.IdVacante== idVacante && x.Activo);
                listSkillVacante = MapperSkillVacanteResponse(skills);

            }
            catch (Exception)
            {
                throw;
            }

            return listSkillVacante;
        }
        private List<SkillVacanteResponse> MapperSkillVacanteResponse(List<SkillVacante> skillVacantes)
        {

           var listSkillVacante = new List<SkillVacanteResponse>();
            foreach (var item in skillVacantes)
            {
                var skillVacante = new SkillVacanteResponse();

                skillVacante.IdCategoria = item.IdCategoria;
                skillVacante.Descripcion = item.DescripcionSkill;
                listSkillVacante.Add(skillVacante);
            }
            return listSkillVacante;
        }




    }
}
