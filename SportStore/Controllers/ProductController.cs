﻿using Microsoft.AspNetCore.Mvc;
using SportStore.Models;
using SportStore.Models.Repository;

namespace SportStore.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository _repository;
        public ProductController(IProductRepository repository)
        {
            _repository = repository;
        }

        public ViewResult List() => View(_repository.Products);
    }
}
