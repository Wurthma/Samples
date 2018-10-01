using EscolaDokimi.Models;
using EscolaDokimi.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EscolaDokimi.Controllers
{
    public class CursoController : Controller
    {
        ApplicationContext dbContext = new ApplicationContext();
        // GET: Curso
        public ActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Cadastrar(CursoViewModel cursoVw)
        {
            dbContext.Cursos.Add((Curso)cursoVw);
            await dbContext.SaveChangesAsync();
            return View(cursoVw);
        }

        public async Task<ActionResult> Consultar(CursoConsultaViewModel cursoConsulta)
        {
            //Faz uso do System.Data.Entity para utilizar o ToListAsync
            IEnumerable<Curso> listaCursos = await dbContext.Cursos.ToListAsync();
            cursoConsulta.Cursos = listaCursos.Select(a => (CursoViewModel)a).ToList();
            return View(cursoConsulta);
        }
    }
}