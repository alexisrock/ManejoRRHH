using Domain.Entities;
using Domain.Entities.StoreProcedure;
using Domain.Entities.View;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ManejoRHContext : DbContext
    {


        public ManejoRHContext(DbContextOptions<ManejoRHContext> options): base(options){}

        public virtual DbSet<Configuracion> Configuraciones { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual DbSet<Rol> Roles { get; set; }
        public virtual DbSet<Cliente> Clientes { get; set; }
        public virtual DbSet<Categoria> Categorias { get; set; }
        public virtual DbSet<TipoContrato> TipoContratos { get; set; }
        public virtual DbSet<TipoSalario> TipoSalarios { get; set; }
        public virtual DbSet<EstadoVacante> EstadoVacantes { get; set; }
        public virtual DbSet<Vacante> Vacantes { get; set; }
        public virtual DbSet<SkillVacante> SkillVacantes { get; set; }
        public virtual DbSet<ModalidadTrabajo> ModalidadTrabajos { get; set; }
        public virtual DbSet<TipoDocumento> TipoDocumentos { get; set; }
        public virtual DbSet<Candidato> Candidatos { get; set; }
        public virtual DbSet<TipoEstudio> TipoEstudios { get; set; }
        public virtual DbSet<EstudioCandidato> EstudioCandidatos { get; set; }
        public virtual DbSet<ReferenciasLaboralesCandidato> ReferenciasLaboralesCandidatos { get; set; }
        public virtual DbSet<ReferenciasPersonalesCandidato> ReferenciasPersonalesCandidato { get; set; }
        public virtual DbSet<EstadoCandidato> EstadoCandidatos { get; set; }
        public virtual DbSet<EstadoProceso> EstadoProcesos { get; set; }
        public virtual DbSet<Proceso> Procesos { get; set; }
        public virtual DbSet<Contrato> Contratos { get; set; }
        public virtual DbSet<Empleado> Empleados { get; set; }
        public virtual DbSet<TipoCertificado> TipoCertificados { get; set; }
        public virtual DbSet<CertificadosEmpleado> CertificadosEmpleados { get; set; }
        public virtual DbSet<CertificadoEstudiantilEmpleado> CertificadoEstudiantilEmpleados { get; set; }
        public virtual DbSet<SPEmployeesByClient> SPEmployeesByClients { get; set; }
        public virtual DbSet<SPProcessCandidateByClient> SPProcessCandidateByClients { get; set; }
        public virtual DbSet<SPProcessByUser> SPProcessByUsers { get; set; }
        public virtual DbSet<SPRejectedCandidatesByUser> SPRejectedCandidatesByUsers { get; set; }
        public virtual DbSet<TipoNovedad> TipoNovedades { get; set; }
        public virtual DbSet<Novedad> Novedades { get; set; }
        public virtual DbSet<Comision> Comisiones { get; set; }
        public virtual DbSet<VWEmployeesByComision> VWEmployeesByComisiones { get; set; }
        public virtual DbSet<SPInfoEmployee> SPInfoEmployees { get; set; }
        public virtual DbSet<SPHistoricalNoverltyEmployee> SPHistoricalNoverltyEmployees { get; set; }







        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Method intentionally left empty.
            modelBuilder.Entity<VWEmployeesByComision>().HasNoKey().ToView("VWEmployeesByComision");
            modelBuilder.Entity<SPEmployeesByClient>().HasNoKey().ToView("SPEmployeesByClient");
            modelBuilder.Entity<SPProcessCandidateByClient>().HasNoKey().ToView("SPProcessCandidateByClient");
            modelBuilder.Entity<SPProcessByUser>().HasNoKey().ToView("SPProcessByUser");
            modelBuilder.Entity<SPRejectedCandidatesByUser>().HasNoKey().ToView("SPRejectedCandidatesByUser");
            modelBuilder.Entity<SPInfoEmployee>().HasNoKey().ToView("SPInfoEmployee");
            modelBuilder.Entity<SPHistoricalNoverltyEmployee>().HasNoKey().ToView("SPHistoricalNoverltyEmployee");
        }
    }
}
