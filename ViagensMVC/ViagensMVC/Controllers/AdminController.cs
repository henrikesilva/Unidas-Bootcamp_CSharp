using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViagensMVC.Db;
using ViagensMVC.Models;

namespace ViagensMVC.Controllers
{
    public class AdminController : Controller
    {
        private const string ActionDestinoListagem = "DestinoListagem";

        private ViagensOnlineDb ObterDbContext()
        {
            return new ViagensOnlineDb();

        }

        [HttpGet]
        public ActionResult DestinoListagem()
        {
            List<Destino> lista = null;
            using (var db = ObterDbContext())
            {
                lista = db.Destinos.ToList();
            }

            return View(lista);
        }

        [HttpGet]
        public ActionResult DestinoNovo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DestinoNovo(Destino destino)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new ValidationException("Favor preencher todos os campos obrigatórios");
                }

                if (Request.Files.Count == 0 ||
                    Request.Files[0].ContentLength == 0)
                {
                    throw new ArgumentException("É Necessário enviar uma foto");
                }

                destino.Foto = GravarFoto(Request);

                using(var db = ObterDbContext())
                {
                    db.Destinos.Add(destino);
                    db.SaveChanges();
                    return RedirectToAction(ActionDestinoListagem);
                }
            }catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(destino);
            }
        }

        private string GravarFoto(HttpRequestBase Request)
        {
            string nome = Path.GetFileName(Request.Files[0].FileName);
            string pathVirtual = "~/Imagens";
            pathVirtual += "/" + nome;
            string pathFisico = Request.MapPath(pathVirtual);

            Request.Files[0].SaveAs(pathFisico);
            return nome;
        }
    }
}