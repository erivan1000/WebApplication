using WebApplication1.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;

namespace WebApplication1.Controllers
{
    public class ProdutoController : Controller
    {

        // GET: Produto
        db_produtoStoreEntities1 dbObj = new db_produtoStoreEntities1();

        public ActionResult Produto(tblProduto obj)
        {
                return View(obj);
        }
        [HttpPost]
        public ActionResult AddProduto(tblProduto model)
        {
            tblProduto obj = new tblProduto();
            if (ModelState.IsValid)
            {
                obj.Id = model.Id;
                obj.Nome = model.Nome;
                obj.Descricao = model.Descricao;
                obj.Ativo = model.Ativo;
                obj.Perecivel = model.Perecivel;
                obj.Categoria = model.Categoria;

                if (model.Id == 0)
                {
                    dbObj.tblProduto.Add(obj);
                    dbObj.SaveChanges();
                }
                else
                {
                    dbObj.Entry(obj).State = EntityState.Modified;
                    dbObj.SaveChanges();
                }
                
            }
            ModelState.Clear();

                return View("Produto");
        }
        


        public ActionResult ProdutoList()
        {
            var res = dbObj.tblProduto.ToList();
            return View(res);
        }

        public ActionResult Deletar(int id)
        {
            var res = dbObj.tblProduto.Where(x => x.ID == id).First();
            dbObj.tblProduto.Remove(res);
            dbObj.SaveChanges();

            var list = dbObj.tblProduto.ToList();

            return View("ProdutoList", list);
        }
    }
}