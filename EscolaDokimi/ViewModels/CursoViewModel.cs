using EscolaDokimi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EscolaDokimi.ViewModels
{
    public class CursoViewModel
    {
        public int CodigoCurso { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }

        public static explicit operator CursoViewModel(Curso obj)
        {
            return new CursoViewModel
            {
                CodigoCurso = obj.CodigoCurso,
                Descricao = obj.Descricao,
                Nome = obj.Nome,
                Valor = obj.Valor
            };
        }
    }
}