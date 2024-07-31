using DotNet8WebApi.HttpClientFactoryPollyExample.Models;

namespace DotNet8WebApi.HttpClientFactoryPollyExample.Services;

public class ProductService : IProductService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public ProductService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<List<ProductModel>> GetProducts()
    {
        try
        {
            var client = _httpClientFactory.CreateClient("ExampleClient");
            HttpResponseMessage response = await client.GetAsync("/products");
            response.EnsureSuccessStatusCode();
            string jsonStr = await response.Content.ReadAsStringAsync();

            return jsonStr.DeserializeObject<List<ProductModel>>()!;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
