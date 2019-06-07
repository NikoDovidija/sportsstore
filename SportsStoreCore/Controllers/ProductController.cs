using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportsStoreCore.Models;
using SportsStoreCore.Models.ViewModels;

namespace SportsStoreCore.Controllers
{
    public class ProductController : Controller
    {


        IProductRepository repo;
        public int PageSize = 4;

        public ProductController(IProductRepository productRepository)
        {
            repo = productRepository;
        }
        public ViewResult List(string category,int productPage = 1)
            //=> View(repo.Products
            //.OrderBy(p => p.ProductID)
            //.Skip((productPage - 1) * PageSize)
            //.Take(PageSize));
            => View(new ProductsListViewModel{
                Products = 
                repo.Products.OrderBy(p => p.ProductID)
                .Where(p=> category == null || p.Category == category)
                .Skip((productPage - 1) * PageSize)
                .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    TotalItems = category == null ? repo.Products.Count() : repo.Products.Where(e=>e.Category ==category).Count() ,
                    CurrentPage = productPage,
                    ItemsPerPage = PageSize
                },
                CurrentCategory = category
                
            });
    }
}