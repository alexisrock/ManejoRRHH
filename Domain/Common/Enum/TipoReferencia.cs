using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common.Enum
{
    public enum TipoReferencia
    {

        laborales,
        personales,
    }

    public static class TipoReferenciaExtend
    {


        public static int GetIdTipoReferencia(this TipoReferencia tipoReferencia)
        {

            return tipoReferencia switch
            {
                TipoReferencia.personales => 1,
                TipoReferencia.laborales => 2,                
                _ => 0
            };

        }
    }
}
