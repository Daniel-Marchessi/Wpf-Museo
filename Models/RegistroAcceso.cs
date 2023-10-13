using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Museo.Models
{

    public class RegistroAcceso
    {
        public int RegistroId { get; set; }
        public int UsuarioId { get; set; }
        public DateTime FechaHoraAcceso { get; set; }
        public DateTime? FechaHoraSalida { get; set; }
    }
}

