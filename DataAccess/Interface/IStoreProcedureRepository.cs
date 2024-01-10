using Domain.Entities.StoreProcedure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interface
{
    public interface IStoreProcedureRepository
    {

        Task<List<SPEmployeesByClient>>? GetByEmploiesId(int id);
        Task<List<SPProcessCandidateByClient>>? GetProcessCandidateByClient(int idClient, int idCandidato);
        Task<List<SPProcessByUser>>? GetProcessCandidateByUser(int idUser);
        Task<List<SPRejectedCandidatesByUser>>? GetRejectedCandidatesByUser(int idUser);
        Task<SPInfoEmployee?> GetSPInfoEmployeeById(long idEmpleado);
        Task<List<SPHistoricalNoverltyEmployee>>? GetHistoricalNoverltyByEmployee(long idEmpleado);

    }
}
