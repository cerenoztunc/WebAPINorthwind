using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPINorthwind_1.DesignPatterns;
using WebAPINorthwind_1.DTOClasses;
using WebAPINorthwind_1.Models;

namespace WebAPINorthwind_1.Controllers
{
    public class CategoryController : ApiController
    {
        NorthwindEntities _db;

        public CategoryController()
        {
            _db = DBTool.DBInstance;
        }

        [HttpGet]
        public List<CategoryDTO> ListCategories()
        {
            return _db.Categories.Select(x => new CategoryDTO
            {
                Name = x.CategoryName,
                ID = x.CategoryID,
                Description = x.Description
            }).ToList();

        }

        [HttpGet]
        public CategoryDTO GetCategory(int id)
        {
            return _db.Categories.Where(x => x.CategoryID == id).Select(x => new CategoryDTO
            {
                Name = x.CategoryName,
                ID = x.CategoryID,
                Description = x.Description
            }).FirstOrDefault();
        }


        [HttpPost]
        public List<CategoryDTO> AddCategory(Category item)
        {
            _db.Categories.Add(item);
            _db.SaveChanges();
            return ListCategories();
        }

        [HttpDelete]
        public List<CategoryDTO> DeleteCategory(int id)
        {
            _db.Categories.Remove(_db.Categories.Find(id));
            _db.SaveChanges();
            return ListCategories();
        }

        [HttpPut]
        public List<CategoryDTO> UpdateCategory(Category item)
        {
            Category guncellenecek = _db.Categories.Find(item.CategoryID);
            guncellenecek.CategoryName = item.CategoryName;
            guncellenecek.Description = item.Description;
            _db.SaveChanges();
            return ListCategories();

            /*
             CategoryDTO

            CategoryDTO guncellenen = new CategoryDTO();
            guncellenen.Name = guncellenecek.CategoryName;

            return guncellenen;
             
             
             
             */
        }



        [HttpGet]
        public List<CategoryDTO> SearchCategory(string item)
        {
            return _db.Categories.Where(x => x.CategoryName.Contains(item)).Select(x => new CategoryDTO
            {
                ID = x.CategoryID,
                Name = x.CategoryName,
                Description = x.Description
            }).ToList();
        }
    }
}
