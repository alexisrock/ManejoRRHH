using AutoMapper;
using Core.Interfaces;
using DataAccess.Interface;
using Domain.Common;
using Domain.Common.Enum;
using Domain.Dto;
using Domain.Entities;
using System.Net;

namespace Core.Repository
{
    public class TipoTableService<T> : ITipoTableService<T> where T : class
    {

        private readonly IRepository<Categoria> categoriaRepository;
        private readonly IRepository<TipoContrato> tipoContratoRepository;
        private readonly IRepository<TipoSalario> tipoSalarioRepository;
        private readonly IRepository<EstadoVacante> estadoVacanteRepository;
        private readonly IRepository<ModalidadTrabajo> modalidadTrabajoRepository;
        private readonly IRepository<TipoDocumento> tipoDocumentoRepository;
        private readonly IRepository<TipoEstudio> tipoEstudioRepository;
        private readonly IRepository<TipoNovedad> tipoNovedadoRepository;
        private readonly IRepository<Domain.Entities.EstadoCandidato> estadoCandidatoRepository;
        private readonly IMapper mapper;


        public TipoTableService(IRepository<Categoria> categoriaRepository, IMapper mapper, IRepository<TipoContrato> tipoContratoRepository,
            IRepository<TipoSalario> tipoSalarioRepository, IRepository<EstadoVacante> estadoVacanteRepository, IRepository<ModalidadTrabajo> modalidadTrabajoRepository,
            IRepository<TipoDocumento> tipoDocumentoRepository, IRepository<TipoEstudio> tipoEstudioRepository, IRepository<Domain.Entities.EstadoCandidato> estadoCandidatoRepository,
            IRepository<TipoNovedad> tipoNovedadoRepository)
        {
            this.categoriaRepository = categoriaRepository;
            this.mapper = mapper;
            this.tipoContratoRepository = tipoContratoRepository;
            this.tipoSalarioRepository = tipoSalarioRepository;
            this.estadoVacanteRepository = estadoVacanteRepository;
            this.modalidadTrabajoRepository = modalidadTrabajoRepository;
            this.tipoDocumentoRepository = tipoDocumentoRepository;
            this.tipoEstudioRepository = tipoEstudioRepository;
            this.estadoCandidatoRepository = estadoCandidatoRepository;
            this.tipoNovedadoRepository = tipoNovedadoRepository;
        }

        public async Task<BaseResponse> Create(object objRequest)
        {
            var output = new BaseResponse();

            try
            {
                switch (objRequest)
                {
                    case CategoriaRequest:
                        await Insert((CategoriaRequest)objRequest);
                        output = MapperBaseResponse(TipoTabla.Categoria.ToString());
                        break;
                    case TipoContratoRequest:
                        await Insert((TipoContratoRequest)objRequest);
                        output = MapperBaseResponse(TipoTabla.Contrato.ToString());
                        break;
                    case TipoSalarioRequest:
                        await Insert((TipoSalarioRequest)objRequest);
                        output = MapperBaseResponse(TipoTabla.Salario.ToString());
                        break;
                }

            }
            catch (Exception ex)
            {
                output.StatusCode = HttpStatusCode.InternalServerError;
                output.Message = ex.Message;

            }
            return output;
        }
        private async Task Insert(CategoriaRequest categoriaRequest)
        {
            var category = mapper.Map<Categoria>(categoriaRequest);
            await categoriaRepository.Insert(category);
        }
        private async Task Insert(TipoContratoRequest tipoContratoRequest)
        {
            var contract = mapper.Map<TipoContrato>(tipoContratoRequest);
            await tipoContratoRepository.Insert(contract);
        }
        private async Task Insert(TipoSalarioRequest tipoSalarioRequest)
        {
            var salary = mapper.Map<TipoSalario>(tipoSalarioRequest);
            await tipoSalarioRepository.Insert(salary);
        }
        private BaseResponse MapperBaseResponse(string tipoTabla)
        {
            var baseResponse = new BaseResponse();
            baseResponse.StatusCode = HttpStatusCode.OK;
            baseResponse.Message = $"{tipoTabla} creada con exito";
            return baseResponse;
        }


        public async Task<List<TipoTableResponse>> GetList(TipoTabla tipoTabla)
        {
            var list = new List<TipoTableResponse>();
            try
            {
                switch (tipoTabla)
                {
                    case TipoTabla.Categoria:
                        var listCategorias = await categoriaRepository.GetAll();
                        list = MapperListesponse(listCategorias);
                        break;
                    case TipoTabla.Contrato:
                        var listContrato = await tipoContratoRepository.GetAll();
                        list = MapperListesponse(listContrato);
                        break;
                    case TipoTabla.Salario:
                        var listSalario= await tipoSalarioRepository.GetAll();
                        list = MapperListesponse(listSalario);
                        break;
                    case TipoTabla.EstadoVacante:
                        var listEstadoVacante = await estadoVacanteRepository.GetAll();
                        list = MapperListesponse(listEstadoVacante);
                        break;
                    case TipoTabla.ModalidadTrabajo:
                        var listModalidadTrabajo= await modalidadTrabajoRepository.GetAll();
                        list = MapperListesponse(listModalidadTrabajo);
                        break;
                    case TipoTabla.TipoDocumento:
                        var listTipoDocumento = await tipoDocumentoRepository.GetAll();
                        list = MapperListesponse(listTipoDocumento);
                        break;
                    case TipoTabla.TipoEstudio:
                        var listTipoEstudio = await tipoEstudioRepository.GetAll();
                        list = MapperListesponse(listTipoEstudio);
                        break;
                    case TipoTabla.EstadoCandidato:
                        var listEstadosCandidato = await estadoCandidatoRepository.GetAll();
                        list = MapperListesponse(listEstadosCandidato);
                        break;
                    case TipoTabla.TipoNovedad:
                        var listTipoNovedado = await tipoNovedadoRepository.GetAll();
                        list = MapperListesponse(listTipoNovedado);
                        break;

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }


            return list;
        }
        private List<TipoTableResponse> MapperListesponse(List<Categoria> listCaterogias)
        {
            List<TipoTableResponse> listResponse = new List<TipoTableResponse>();

            listCaterogias.ForEach(c =>
            {
                var categoria = new TipoTableResponse();
                categoria.Id = c.IdCategoria;
                categoria.Description = c.Description;
                listResponse.Add(categoria);
            });
            return listResponse;
        }
        private List<TipoTableResponse> MapperListesponse(List<TipoContrato> listContrato)
        {
            List<TipoTableResponse> listResponse = new List<TipoTableResponse>();

            listContrato.ForEach(c =>
            {
                var contrato = new TipoTableResponse();
                contrato.Id = c.IdContrato;
                contrato.Description = c.Description;
                listResponse.Add(contrato);
            });
            return listResponse;
        }
        private List<TipoTableResponse> MapperListesponse(List<TipoSalario> listSalario)
        {
            List<TipoTableResponse> listResponse = new List<TipoTableResponse>();

            listSalario.ForEach(c =>
            {
                var contrato = new TipoTableResponse();
                contrato.Id = c.IdSalario;
                contrato.Description = c.Description;
                listResponse.Add(contrato);
            });
            return listResponse;
        }
        private List<TipoTableResponse> MapperListesponse(List<EstadoVacante> listEstadoVacante)
        {
            List<TipoTableResponse> listResponse = new List<TipoTableResponse>();

            listEstadoVacante.ForEach(c =>
            {
                var contrato = new TipoTableResponse();
                contrato.Id = c.IdEstadoVacante;
                contrato.Description = c.Description;
                listResponse.Add(contrato);
            });
            return listResponse;
        }
        private List<TipoTableResponse> MapperListesponse(List<ModalidadTrabajo> listModalidadTrabajo)
        {
            List<TipoTableResponse> listResponse = new List<TipoTableResponse>();

            listModalidadTrabajo.ForEach(c =>
            {
                var contrato = new TipoTableResponse();
                contrato.Id = c.IdModalidadTrabajo;
                contrato.Description = c.Description;
                listResponse.Add(contrato);
            });
            return listResponse;
        }
        private List<TipoTableResponse> MapperListesponse(List<TipoDocumento> listTipoDocumento)
        {
            List<TipoTableResponse> listResponse = new List<TipoTableResponse>();

            listTipoDocumento.ForEach(c =>
            {
                var contrato = new TipoTableResponse();
                contrato.Id = c.IdTipoDocumento;
                contrato.Description = c.Description;
                listResponse.Add(contrato);
            });
            return listResponse;

        }
        private List<TipoTableResponse> MapperListesponse(List<TipoEstudio> listTipoEstudio)
        {
            List<TipoTableResponse> listResponse = new List<TipoTableResponse>();

            listTipoEstudio.ForEach(c =>
            {
                var contrato = new TipoTableResponse();
                contrato.Id = c.IdTipoEstudio;
                contrato.Description = c.Description;
                listResponse.Add(contrato);
            });
            return listResponse;

        }
        private List<TipoTableResponse> MapperListesponse(List<Domain.Entities.EstadoCandidato> listEstadosCandidato)
        {
            List<TipoTableResponse> listResponse = new List<TipoTableResponse>();

            listEstadosCandidato.ForEach(c =>
            {
                var contrato = new TipoTableResponse();
                contrato.Id = c.IdEstadoCandidato;
                contrato.Description = c.Description;
                listResponse.Add(contrato);
            });
            return listResponse;

        }
        private List<TipoTableResponse> MapperListesponse(List<TipoNovedad> listTipoNovedado)
        {
            List<TipoTableResponse> listResponse = new List<TipoTableResponse>();

            listTipoNovedado.ForEach(c =>
            {
                var contrato = new TipoTableResponse();
                contrato.Id = c.IdTipoNovedad;
                contrato.Description = c.Description;
                listResponse.Add(contrato);
            });
            return listResponse;

        }

        public async Task<BaseResponse> Update(object objRequest)
        {

            var baseResponse = new BaseResponse();
            try
            {
                switch (objRequest)
                {
                    case CategoriaRequest:
                        await UpdateEmtity((CategoriaRequest)objRequest);
                        baseResponse = MapperBaseResponseUpdate(TipoTabla.Categoria.ToString());
                        break;
                    case TipoContratoRequest:
                        await UpdateEmtity((TipoContratoRequest)objRequest);
                        baseResponse = MapperBaseResponseUpdate(TipoTabla.Contrato.ToString());
                        break;
                    case TipoSalarioRequest:
                        await UpdateEmtity((TipoSalarioRequest)objRequest);
                        baseResponse = MapperBaseResponseUpdate(TipoTabla.Salario.ToString());
                        break;
                }
            }
            catch (Exception ex)
            {
                baseResponse.StatusCode = HttpStatusCode.InternalServerError;
                baseResponse.Message = ex.Message;
            }
            return baseResponse;
        }
        private async Task UpdateEmtity(CategoriaRequest categoriaRequest)
        {

            var categoria = await categoriaRepository.GetById(categoriaRequest.IdCategoria);
            if (categoria is not null)
            {
                categoria.Description = categoriaRequest.Description;
                await categoriaRepository.Update(categoria);
            }
        }
        private async Task UpdateEmtity(TipoContratoRequest tipoContratoRequest)
        {
            var contrato = await tipoContratoRepository.GetById(tipoContratoRequest.IdContrato);
            if (contrato is not null)
            {
                contrato.Description = tipoContratoRequest.Description;
                await tipoContratoRepository.Update(contrato);
            }

        }
        private async Task UpdateEmtity(TipoSalarioRequest tipoSalarioRequest)
        {
            var salario = await tipoSalarioRepository.GetById(tipoSalarioRequest.IdSalario);
            if (salario is not null)
            {
                salario.Description = tipoSalarioRequest.Description;
                await tipoSalarioRepository.Update(salario);
            }

        }
        private BaseResponse MapperBaseResponseUpdate(string tipoTabla)
        {
            var baseResponse = new BaseResponse();
            baseResponse.StatusCode = HttpStatusCode.OK;
            baseResponse.Message = $"{tipoTabla}  actualizada con exito";
            return baseResponse;
        }

    }
}
