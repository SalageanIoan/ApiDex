using ApiDex.Infrastructure.Data;
using ApiDex.Server.DependencyInjection.Extensions;
using ApiDex.Server.Endpoints.Assistant;
using ApiDex.Server.Endpoints.Documentation;
using ApiDex.Server.Endpoints.Rag;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddServer();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApiDexDbContext>();
    dbContext.Database.Migrate();

    var ragSettings = scope.ServiceProvider.GetRequiredService<ApiDex.Domain.Rag.IRagSettingsManager>();
    ragSettings.EnsureInitializedAsync().GetAwaiter().GetResult();

    var vectorStore = scope.ServiceProvider.GetRequiredService<ApiDex.Domain.Rag.IVectorStoreRepository>();
    vectorStore.EnsureCollectionAsync().GetAwaiter().GetResult();

    var graphStore = scope.ServiceProvider.GetRequiredService<ApiDex.Domain.Rag.IGraphStoreRepository>();
    graphStore.EnsureSchemaAsync().GetAwaiter().GetResult();
}

app.UseExceptionHandler();

app.UseCors(policy => policy
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseHttpsRedirection();

app.MapGet("/", () => Results.Redirect("/swagger")).ExcludeFromDescription();
app.MapDocumentationEndpoints();
app.MapRagEndpoints();
app.MapAssistantEndpoints();

app.Run();