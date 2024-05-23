﻿using Microsoft.AspNetCore.Mvc;
using NNTracker.Data;
using NNTracker.Models;

using NNTracker.Views.Shared.SearchBar;

namespace NNTracker.Controllers
{
    public class CategoryController : Controller
    {
        private ApplicationDBcontext db;
        public CategoryController(ApplicationDBcontext db)
        {
            this.db = db;
        }

        public IActionResult Index(string searchText="")
        {
            IEnumerable<Category> categories;
            if (searchText != "" && searchText != null)
            {
                categories = db.Categories.Where(b => b.Category_Name.Contains(searchText)).ToList();
            }
            else
                categories = db.Categories.ToList();
                Spager searchPager = new Spager() { Action = "Index", Controller = "Category", searchText = searchText };
                ViewBag.SearchPager = searchPager;

            return View(categories);

           


        }
        [HttpGet]
        public ActionResult Create()
        {
            return View(new Category());
        }
        [HttpPost]
        public IActionResult Create(Category category)
        {

            if (!category.Category_Name.Any(char.IsLetter))
            {
                ModelState.AddModelError("Category_Name", "Name must be letters");

            }
            if (ModelState.IsValid)
            {

                // Add the new student entity to the context
                db.Categories.Add(category);

                // Save changes to the database
                db.SaveChanges();
                TempData["AlertMessage"] = "Category Created Successfully...!";
                return RedirectToAction("Index");

            }

            return View(category);
        }
        public IActionResult Details(int id)
        {

            var ctr = db.Categories.Where(x => x.Category_Id == id).FirstOrDefault();
            if (ctr == null)
            {
                return new NotFoundResult();
            }
            return View(ctr);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var ctr = db.Categories.Where(x => x.Category_Id == id).FirstOrDefault();
            if (ctr == null)
            {
                return new NotFoundResult();
            }
            return View(ctr);

        }
        [HttpPost]
        public IActionResult Delete(Category ctr)
        {
            //var ctr = db.Categories.Where(x => x.Category_Id == id).FirstOrDefault();
            db.Categories.Remove(ctr);
            db.SaveChanges();
            TempData["AlertMessage"] = "Category Deleted Successfully...!";
            return RedirectToAction("Index");

        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var ctr = db.Categories.Where(x => x.Category_Id == id).FirstOrDefault();
            if (ctr == null)
            {
                return new NotFoundResult();
            }
            if (!ctr.Category_Name.Any(char.IsLetter))
            {
                ModelState.AddModelError("Category_Name", "Name must be at least 3 letters ");

            }
            return View(ctr);
        }
        [HttpPost]
        public IActionResult Edit(Category ctr)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Update(ctr);
                db.SaveChanges();
                //return RedirectToAction("Index");
                TempData["AlertMessage"] = "Category Updated Successfully...!";
                return RedirectToAction("Index");
            }
            return View(ctr);

        }


    }
}
