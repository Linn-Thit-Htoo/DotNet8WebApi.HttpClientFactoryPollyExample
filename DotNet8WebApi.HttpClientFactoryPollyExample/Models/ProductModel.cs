namespace DotNet8WebApi.HttpClientFactoryPollyExample.Models
{
    public class ProductModel
    {
        public int id { get; set; }
        public string title { get; set; }
        public float price { get; set; }
        public string description { get; set; }
        public string category { get; set; }
        public string image { get; set; }
        public RatingModel rating { get; set; }
    }
}