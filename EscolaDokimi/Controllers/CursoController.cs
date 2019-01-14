using EscolaDokimi.Models;
using EscolaDokimi.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using PagedList;

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
            return RedirectToAction("Consultar");
        }

        public async Task<ActionResult> Consultar(int numeroPagina = 1, int qtdePorPagina = 5)
        {
            CursoConsultaViewModel cursoConsulta = new CursoConsultaViewModel();
            cursoConsulta.NumeroPagina = numeroPagina;
            cursoConsulta.QtdePorPagina = qtdePorPagina;
            
            if(TempData["ListaConsultaCursos"] == null)
            {
                //Faz uso do System.Data.Entity para utilizar o ToListAsync
                IEnumerable<Curso> listaCursos = await dbContext.Cursos.ToListAsync();
                cursoConsulta.Cursos = listaCursos.Select(a => (CursoViewModel)a).ToList();
                TempData["ListaConsultaCursos"] = cursoConsulta.Cursos;
            }
            else
            {
                cursoConsulta.Cursos = (IEnumerable<CursoViewModel>)TempData["ListaConsultaCursos"];
                TempData.Keep("ListaConsultaCursos");
            }
            return View(cursoConsulta);
        }

        [HttpPost]
        public async Task<ActionResult> Consultar(CursoConsultaViewModel cursoConsultaVw, int? numeroPagina = null, int? qtdePorPagina = null)
        {
            if(cursoConsultaVw.CodigoCurso != null)
            {
                IEnumerable<Curso> listaCursos = await dbContext.Cursos.Where(c => c.CodigoCurso == cursoConsultaVw.CodigoCurso).ToListAsync();
                if (listaCursos != null)
                {
                    cursoConsultaVw.Cursos = listaCursos.Select(a => (CursoViewModel)a).ToList();
                }
            }
            else
            {
                IEnumerable<Curso> listaCursos = await dbContext.Cursos.Where(c => c.Nome.Contains(cursoConsultaVw.Nome)).ToListAsync();
                if (listaCursos != null)
                {
                    cursoConsultaVw.Cursos = listaCursos.Select(a => (CursoViewModel)a).ToList();
                }
            }
            TempData["ListaConsultaCursos"] = cursoConsultaVw.Cursos;
            TempData.Keep("ListaConsultaCursos");
            return PartialView("_ListaCursos", cursoConsultaVw.Cursos.ToPagedList(cursoConsultaVw.NumeroPagina, cursoConsultaVw.QtdePorPagina));
        }

        [HttpGet]
        public async Task<ActionResult> Detalhes(int id)
        {
            CursoDetalhesViewModel detalhesCursoVw = new CursoDetalhesViewModel();
            Curso curso = await dbContext.Cursos.Where(c => c.CodigoCurso == id).SingleOrDefaultAsync();
            //Conversão explicita da entidade para Vw
            detalhesCursoVw = (CursoDetalhesViewModel)curso;
            return View(detalhesCursoVw);
        }
    }
}