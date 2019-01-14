using EscolaDokimi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using PagedList.Mvc;

namespace EscolaDokimi.ViewModels
{
    public class CursoConsultaViewModel
    {
        public int? CodigoCurso { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public int NumeroPagina { get; set; }
        public int QtdePorPagina { get; set; }
        public bool trocandoPagina { get; set; }

        public IEnumerable<CursoViewModel> Cursos { get; set; }
    }
}