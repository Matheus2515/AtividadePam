using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebTccApi2.Models.Enuns;

namespace WebTccApi2.Models
{
    public class Corte
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public int Preco { get; set; }
        public string Estilo { get; set; }
        public string Sex { get; set; }

        public ClasseEnum Classe { get; set; }
    }
}
