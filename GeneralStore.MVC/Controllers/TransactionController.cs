using GeneralStore.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GeneralStore.MVC.Controllers
{
    public class TransactionController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();
        // GET: Transaction
        public ActionResult Index()
        {
            return View(_db.Transactions.ToList());
        }

        // ViewData/ViewBags --covering this later
        // GET: Transaction/Create
        public ActionResult Create()
        {
            var viewModel = new CreateTransactionVewModel();
            // code to fill drop down list
            viewModel.Products = _db.Products.Select(p => new SelectListItem
            {
                Text = p.Name,
                Value = p.ProductId.ToString()
            });

            viewModel.Customers = _db.Customers.Select(c => new SelectListItem
            {
                Text = c.FirstName + " " + c.LastName,
                Value = c.CustomerId.ToString()
            });

            return View(viewModel);
        }

        // POST: Transaction/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateTransactionVewModel viewModel)
        {
            _db.Transactions.Add(new Transaction
            {
                CustomerId = viewModel.CustomerId,
                ProductId = viewModel.ProductId
            });
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}