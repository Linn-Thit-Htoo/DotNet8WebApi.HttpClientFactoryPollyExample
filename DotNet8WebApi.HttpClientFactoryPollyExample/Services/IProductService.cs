﻿namespace DotNet8WebApi.HttpClientFactoryPollyExample.Services;

public interface IProductService
{
    Task<List<ProductModel>> GetProducts();
}
