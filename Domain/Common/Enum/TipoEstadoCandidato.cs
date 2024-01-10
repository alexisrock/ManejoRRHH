using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common.Enum
{
    public enum TipoEstadoCandidato
    {

        EnviadoComercial,
        DevolucionComercial,
        Disponible,
        Contratado,
    }

    public static class EstadoCandidatoExtend
    {

        public static int GetIdEstadoCandidato(this TipoEstadoCandidato estadoCandidato)
        { 
        
            return estadoCandidato switch
            {
                TipoEstadoCandidato.EnviadoComercial => 1,
                TipoEstadoCandidato.DevolucionComercial => 2,                
                TipoEstadoCandidato.Disponible => 3,
                TipoEstadoCandidato.Contratado => 4,               
                _ => 0
            };

        }

    }
}
