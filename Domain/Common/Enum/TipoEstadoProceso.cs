using Domain.Entities;

namespace Domain.Common.Enum
{
    public enum TipoEstadoProceso
    {


        EnProceso,
        Rechazado,
        Contratado,
        RevisionCliente,
    }



    public static class TipoEstadoProcesoExtend
    {

        public static int GetIdEstadoProceso(this TipoEstadoProceso tipoEstadoProceso) 
        {
            return tipoEstadoProceso switch
            {
                TipoEstadoProceso.EnProceso => 1,
                TipoEstadoProceso.Rechazado => 2,
                TipoEstadoProceso.Contratado => 3,  
                TipoEstadoProceso.RevisionCliente => 4,
                _ => 0
            };

        }


    }
}
