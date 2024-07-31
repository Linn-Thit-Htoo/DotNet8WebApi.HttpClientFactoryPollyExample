﻿using DotNet8WebApi.HttpClientFactoryPollyExample.Models;
using DotNet8WebApi.HttpClientFactoryPollyExample.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DotNet8WebApi.HttpClientFactoryPollyExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var lst = await _productService.GetProducts();
            return Ok(lst);
        }
    }
}