using System.Collections.Generic;

namespace PracticaMad.Model.ValoracionServiceNS
{
    public class ValoracionBlock
    {
        public List<ValoracionDTO.ValoracionDTO> Valoraciones { get; private set; }
        public bool ExistMoreValoraciones { get; private set; }

        public ValoracionBlock(List<ValoracionDTO.ValoracionDTO> valoraciones, bool existMoreValoraciones)
        {
            this.Valoraciones = valoraciones;
            this.ExistMoreValoraciones = existMoreValoraciones;
        }
    }
}
