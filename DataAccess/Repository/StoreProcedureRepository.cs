using Azure;
using DataAccess.Interface;
using Domain.Entities;
using Domain.Entities.StoreProcedure;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class StoreProcedureRepository : IStoreProcedureRepository
    {

        private readonly ManejoRHContext _manejoRHContext;

        public StoreProcedureRepository(ManejoRHContext manejoRHContext)
        {
            _manejoRHContext = manejoRHContext;
        }

        public async Task<List<SPEmployeesByClient>>? GetByEmploiesId(int id)
        {
            var  employeesByClient = await _manejoRHContext.SPEmployeesByClients.FromSqlRaw("EXEC SPEmployeesByClient @IdClient", new SqlParameter("@IdClient", id)).ToListAsync();
            return employeesByClient;
        }           

        public async Task<List<SPProcessCandidateByClient>>? GetProcessCandidateByClient(int idClient, int idCandidato)
        {
            var candidateProcess = await _manejoRHContext.SPProcessCandidateByClients.FromSqlRaw("EXEC SPProcessCandidateByClient @IdClient, @IdCandidato  ", new SqlParameter("@IdClient", idClient), new SqlParameter("@IdCandidato", idCandidato)).ToListAsync();
            return candidateProcess;
        }

        public async Task<List<SPProcessByUser>>? GetProcessCandidateByUser(int idUser)
        {
            var processByUser = await _manejoRHContext.SPProcessByUsers.FromSqlRaw("EXEC SPProcessByUser @IdUser", new SqlParameter("@IdUser", idUser)).ToListAsync();
            return processByUser;
        }

        public async Task<List<SPRejectedCandidatesByUser>>? GetRejectedCandidatesByUser(int idUser)
        {
            var processByUser = await _manejoRHContext.SPRejectedCandidatesByUsers.FromSqlRaw("EXEC SPRejectedCandidatesByUser @IdUser", new SqlParameter("@IdUser", idUser)).ToListAsync();
            return processByUser;
        }

        public async Task<SPInfoEmployee?> GetSPInfoEmployeeById(long idEmpleado)
        {
            var empleyee = await _manejoRHContext.SPInfoEmployees.FromSqlRaw("EXEC SPInfoEmployee @IdEmpleado", new SqlParameter("@IdEmpleado", idEmpleado)).ToListAsync();
            return empleyee.FirstOrDefault();
        }

        public async Task<List<SPHistoricalNoverltyEmployee>>? GetHistoricalNoverltyByEmployee(long idEmpleado)
        {
            var empleyee = await _manejoRHContext.SPHistoricalNoverltyEmployees.FromSqlRaw("EXEC SPHistoricalNoverltyEmployee @IdEmpleado", new SqlParameter("@IdEmpleado", idEmpleado)).ToListAsync();
            return empleyee;
        }

    }
}
