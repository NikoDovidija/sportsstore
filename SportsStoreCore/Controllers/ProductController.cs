using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportsStoreCore.Models;
namespace SportsStoreCore.Controllers
{
    public class ProductController : Controller
    {

        IProductRepository repo;
        public ProductController(IProductRepository productRepository)
        {
            repo = productRepository;
        }
        public ViewResult List() => View(repo.Products);
    }
}