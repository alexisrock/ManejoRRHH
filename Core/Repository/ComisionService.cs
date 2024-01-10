using Core.Interfaces;
using DataAccess.Interface;
using Domain.Common;
using Domain.Dto;
using Domain.Entities;
using System.Net;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities.View;
using System.Collections.Generic;

namespace Core.Repository
{
    public class ComisionService : IComisionService
    {

        private readonly IRepository<Comision> comisionRepository;
        private readonly IRepository<VWEmployeesByComision> vWEmployeesByComisionRepository;
        private readonly IMapper mapper;
        public ComisionService(IRepository<Comision> comisionRepository, IMapper mapper, IRepository<VWEmployeesByComision> vWEmployeesByComisionRepository)
        {
            this.comisionRepository = comisionRepository;
            this.mapper = mapper;
            this.vWEmployeesByComisionRepository = vWEmployeesByComisionRepository;
        }

        public async Task<BaseResponse> Create(ComisionRequest comisionRequest)
        {
            var baseResponse = new BaseResponse();
            try
            {

                await InsertComision(comisionRequest);
                baseResponse = MapperBaseResponse("Comision creada con exito", HttpStatusCode.OK);

            }
            catch (Exception ex)
            {

                baseResponse.Message = ex.Message;
                baseResponse.StatusCode = HttpStatusCode.NotFound;
            }
            return baseResponse;
        }
        private async Task InsertComision(ComisionRequest comisionRequest)
        {
            var comision = mapper.Map<Comision>(comisionRequest);
            comision.FechaCreacion = DateTime.Now;
            await comisionRepository.Insert(comision);
        }

        public async Task<BaseResponse> Update(ComisionRequest comisionRequest)
        {
            var baseResponse = new BaseResponse();
            try
            {
                var comision = await ValidaIdComision(comisionRequest.IdComision);
                if (comision is not null)
                {
                    await UpdateComision(comision, comisionRequest);
                    baseResponse = MapperBaseResponse("Comision actualizada con exito", HttpStatusCode.OK);
                }
                else
                {
                    baseResponse = MapperBaseResponse("El id de la comision no se encuentra", HttpStatusCode.NotFound);
                }
            }
            catch (Exception ex)
            {

                baseResponse.Message = ex.Message;
                baseResponse.StatusCode = HttpStatusCode.NotFound;
            }
            return baseResponse;
        }       
        private async Task UpdateComision(Comision comision, ComisionRequest comisionRequest)
        {
            comision.IdEmpleado = comisionRequest.IdEmpleado;
            comision.FechaIngreso = comisionRequest.FechaIngreso;
            comision.Activo = comisionRequest.Activo;
            comision.IdUsuarioComision = comisionRequest.IdUsuarioComision;
            await comisionRepository.Update(comision);
        }

        public async Task<BaseResponse> UpdateState(ComisionStatusRequest comisionStatusRequest)
        {
            var baseResponse = new BaseResponse();
            try
            {
                var comision = await ValidaIdComision(comisionStatusRequest.IdComision);
                if (comision is not null)
                {
                    UpdateStateComision(comision, comisionStatusRequest);
                    baseResponse = MapperBaseResponse("Comision actualizada con exito", HttpStatusCode.OK);
                }
                else
                {
                    baseResponse = MapperBaseResponse("El id de la comision no se encuentra", HttpStatusCode.NotFound);
                }
            }
            catch (Exception ex)
            {

                baseResponse.Message = ex.Message;
                baseResponse.StatusCode = HttpStatusCode.NotFound;
            }
            return baseResponse;
        }
        private async Task UpdateStateComision(Comision comision, ComisionStatusRequest comisionStatusReques)
        {
            comision.Activo = comisionStatusReques.Activo;
            await comisionRepository.Update(comision);  
        }
        private async Task<Comision> ValidaIdComision(long idComision)
        {
            return await comisionRepository.GetById(idComision);
        }

        private BaseResponse MapperBaseResponse(string mensaje, HttpStatusCode httpStatusCode)
        {
            var baseResponse = new BaseResponse();
            baseResponse.Message = mensaje;
            baseResponse.StatusCode = httpStatusCode;
            return baseResponse;
        }


        public async Task<ComisionResponse> GetComisionById(long idComision)
        {
            var comisionResponse = new ComisionResponse();
            try
            {
                var comision = await ValidaIdComision(idComision);
                if(comision is not null)
                {
                    comisionResponse = mapper.Map<ComisionResponse>(comision);
                    comisionResponse.StatusCode = HttpStatusCode.OK;
                }
                else
                {
                    comisionResponse.StatusCode = HttpStatusCode.NotFound;
                    comisionResponse.Message = "El id de la comision no se encuentra";
                }
            }
            catch (Exception ex)
            {
                comisionResponse.StatusCode = HttpStatusCode.InternalServerError;
                comisionResponse.Message = ex.Message;
            }
            return comisionResponse;
        }

        public async Task<List<VWEmployeesByComisionResponse>> GetComisiones()
        {
            List<VWEmployeesByComisionResponse> vWEmployeesByComisionResponse;
            try
            {
                var vWEmployeesByComision = await vWEmployeesByComisionRepository.GetAll();
                vWEmployeesByComisionResponse = MapperVWEmployeesByComisionRespons(vWEmployeesByComision);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return vWEmployeesByComisionResponse;
        }
        private List<VWEmployeesByComisionResponse> MapperVWEmployeesByComisionRespons(List<VWEmployeesByComision> vWEmployeesByComision)
        {

            List<VWEmployeesByComisionResponse> vWEmployeesByComisionResponses = new();
            if (vWEmployeesByComision is not null && vWEmployeesByComision.Count > 0 )
            {
                vWEmployeesByComision.ForEach(x =>
                {
                    var VWEmployeesByComisionResponse = new VWEmployeesByComisionResponse();
                    VWEmployeesByComisionResponse.Activo = x.Activo;
                    VWEmployeesByComisionResponse.UserName = x.UserName;
                    VWEmployeesByComisionResponse.NombreEmpleado = x.NombreEmpleado;
                    VWEmployeesByComisionResponse.FechaIngreso = x.FechaIngreso.ToString("yyyy-MM-dd");
                    VWEmployeesByComisionResponse.IdComision = x.IdComision;
                    vWEmployeesByComisionResponses.Add(VWEmployeesByComisionResponse);

                });
            }
            return vWEmployeesByComisionResponses;
        }

    }
}
