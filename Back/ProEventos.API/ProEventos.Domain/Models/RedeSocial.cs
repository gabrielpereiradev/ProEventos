using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProEventos.Domain
{
    public class RedeSocial
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string URL { get; set; }
        //tem que criar o int eventoid e o evento evento para fazer 1 para 1
        public int? EventoId { get; set; }
        public Evento Evento { get; set; }
        public int? PalestranteId{ get; set; }
        public Palestrante Palestrante { get; set; }
        //tem que criar o int PalestranteId e o Palestrante Palestrante para fazer 1 para 1
    }
}
