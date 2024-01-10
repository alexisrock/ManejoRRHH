using Domain.Entities.StoreProcedure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common
{
    public class ObjectFileSave
    {
        public string Base64String { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;
    }

    public class ObjectFileSaveExcel
    {
        public string Path { get; set; } = string.Empty;
        public int IdUser { get; set; }
        public List<SPRejectedCandidatesByUser>? Lista { get; set; }
    }
}
