var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var retryPolicy = HttpPolicyExtensions
    .HandleTransientHttpError()
    .WaitAndRetryAsync(
        retryCount: 5,
        sleepDurationProvider: attempt => TimeSpan.FromSeconds(3),
        onRetry: (outcome, timespan, retryAttempt, context) =>
        {
            string message =
                $"Retrying due to: {outcome.Exception?.Message ?? outcome.Result.ReasonPhrase}. Wait time: {timespan}. Attempt: {retryAttempt}.";
            message.Dump();
        }
    );

//builder.Services.AddHttpClient<IProductService, ProductService>(opt =>
//{
//    opt.BaseAddress = new Uri(builder.Configuration.GetSection("SampleUri").Value!);
//}).AddPolicyHandler(retryPolicy);

builder.Services.AddScoped<IProductService, ProductService>();

builder
    .Services.AddHttpClient(
        "ExampleClient",
        client =>
        {
            client.BaseAddress = new Uri(builder.Configuration.GetSection("SampleUri").Value!);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.Timeout = TimeSpan.FromSeconds(30);
        }
    )
    .AddPolicyHandler(retryPolicy);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
