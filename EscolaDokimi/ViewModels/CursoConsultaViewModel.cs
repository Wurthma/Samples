using EscolaDokimi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EscolaDokimi.ViewModels
{
    public class CursoConsultaViewModel
    {
        public int CodigoCurso { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }

        public IEnumerable<CursoViewModel> Cursos { get; set; }
    }
}