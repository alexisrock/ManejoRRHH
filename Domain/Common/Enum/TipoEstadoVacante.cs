using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common.Enum
{
    public enum TipoEstadoVacante
    {
        Activo,
        Inactivo,
        Suspendido,
        Cerrado,

    }



    public static class TipoEstadoVacanteExtend
    {


        public static int GetIdTipoEstado(this TipoEstadoVacante tipoEstadoVacante )
        {

            return tipoEstadoVacante switch
            {
                TipoEstadoVacante.Activo => 1,
                TipoEstadoVacante.Inactivo => 2,                
                TipoEstadoVacante.Suspendido => 3,
                TipoEstadoVacante.Cerrado => 4,               
                _ => 0
            };

        }
    }
}
