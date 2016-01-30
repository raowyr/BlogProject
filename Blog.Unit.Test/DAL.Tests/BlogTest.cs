﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain.Model.Entities;
using Data.Access.Layer;
using Data.Access.Layer.Base;
using Data.Access.Layer.Repositories;

namespace Blog.Unit.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestInitialize]
        public void Start()
        {
            SessionManager.Instance.Configure();
            SessionManager.Instance.BuildSchema();
        }

        [TestCleanup]
        public void Stop()
        {
            SessionManager.Instance.Close();
        }

        [TestMethod]
        public void TestMethod1()
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                BlogRepository br = new BlogRepository(uow.Current);
                Domain.Model.Entities.Blog b = new Domain.Model.Entities.Blog();
                b.Name = ".Net Help";
                b.Description = "Un blog dedicato allo sviluppo in ambiente .NET. Tanti articoli, tips and trick.";

                Author a = new Author();
                a.Name = "Pasquale";
                a.Surname = "Garzillo";
                a.BirthDate = Convert.ToDateTime("27/12/1987");
                a.Email = "@";

                Tag t = new Tag();
                t.Name = "c#";

                Category c = new Category();
                c.Name = "Generica";
                c.Description = "Categoria generica";

                Page p = new Page();
                p.Title = "Prima pagina del blog";
                p.Description = "Descrizione della prima pagina";
                p.Date = DateTime.Today;
                p.BodyText = "testo testo teso";
                p.Author = a;
                p.AddTag(t);
                p.Category = c;

                b.AddPage(p);
                br.Save(b);

                uow.Commit();

                Domain.Model.Entities.Blog bb = br.Load(b.Id);
            }
        }
    }
}
