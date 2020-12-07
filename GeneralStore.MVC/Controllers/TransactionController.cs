using GeneralStore.MVC.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
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
        // GET: Transaction/{id}
        [Route("/{id}")]
        public ActionResult Details(int id)
        {
            Transaction transaction = _db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }

            return View(transaction);
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

        // GET: Transaction/Delete/:id
        [Route("delete/{id}")]
        public ActionResult Delete([Required] int id)
        {
            Transaction transaction = _db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]        
        public ActionResult Delete(Transaction model)
        {
            //var product = _db.Products.Find(transaction.ProductId);
            //product.InventoryCount += 1;
            var transaction = _db.Transactions.Find(model.TransactionId);
            _db.Transactions.Remove(transaction);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        [Route("edit/{id}")]
        public ActionResult Edit(int id)
        {
            Transaction transaction = _db.Transactions.Find(id);
            if (transaction == null)
            {
                return View("NotFound");
            }
            ViewData["Customers"] = _db.Customers.Select(c => new SelectListItem
            {
                Text = c.FirstName + " " + c.LastName,
                Value = c.CustomerId.ToString()
            });
            ViewData["Products"] = _db.Products.Select(p => new SelectListItem
            {
                Text = p.Name,
                Value = p.ProductId.ToString()
            });

            return View(transaction);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Transaction model)
        {
            ViewData["Customers"] = _db.Customers.Select(c => new SelectListItem
            {
                Text = c.FirstName + " " + c.LastName,
                Value = c.CustomerId.ToString()
            });
            ViewData["Products"] = _db.Products.Select(p => new SelectListItem
            {
                Text = p.Name,
                Value = p.ProductId.ToString()
            });

            Transaction transaction = _db.Transactions.Find(model.TransactionId);
            transaction.CustomerId = model.CustomerId;
            transaction.ProductId = model.ProductId;
            _db.SaveChanges();

            // whenever an error happens, you can do something like this
            if (model.CustomerId != 1)
            {
                ViewData["ErrorMessage"] = "You didn't pick me!";
                return View(model);
            }

            return View(model);
        }        
    }
}