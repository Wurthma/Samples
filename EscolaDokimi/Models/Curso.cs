using EscolaDokimi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EscolaDokimi.Models
{
    public class Curso
    {
        public int CodigoCurso { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }

        public static explicit operator Curso(CursoViewModel obj)
        {
            return new Curso
            {
                CodigoCurso = obj.CodigoCurso,
                Descricao = obj.Descricao,
                Nome = obj.Nome,
                Valor = obj.Valor
            };
        }
    }
}