using Dish_List_INT20H.Controllers;
using Dish_List_INT20H.Database;
using Dish_List_INT20H.Models;
using Dish_List_INT20H.Models.Payloads;
using Dish_List_INT20H.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("CORSPolicy",
        builder =>
        {
            builder
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowAnyOrigin();
        });
});

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(SwaggerGenOptions =>
{
    SwaggerGenOptions.SwaggerDoc("v1", new OpenApiInfo { Title = "API Gateway", Version = "v1" });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(swaggerUIOptions =>
{
    swaggerUIOptions.DocumentTitle = "Dish List INT20H";
    swaggerUIOptions.SwaggerEndpoint("/swagger/v1/swagger.json", "Dish List INT20H");
    swaggerUIOptions.RoutePrefix = string.Empty;
});
app.UseHttpsRedirection();

app.UseCors("CORSPolicy");


#region "Products Endpoints"

app.MapGet("/products", async () => await ProductController.GetProductsAsync()).WithTags("Products Endpoints").WithDisplayName("Get All Products");

app.MapGet("/product", async (Guid id, string? filter) => await ProductController.GetProductByIdAsync(id)).WithTags("Products Endpoints");

#endregion

#region Users Enpoints
app.MapGet("/users", async () => await UserController.GetUsersAsync()).WithTags("Users Endpoints");

app.MapGet("/getUsersProduct", async (Guid userId) =>
{
    return new GetItemsList<ProductQuantity>(await UserController.GetUsersProduct(userId));
}).WithTags("Users Endpoints");
app.MapGet("/getGrouperUsersProduct", async (Guid userId) =>
{
    return await UserController.GetGroupedUsersProduct(userId);
}).WithTags("Users Endpoints");

app.MapPost("/addUserProduct", async(AddUserProductPayload payload) =>
{
    return new GetItemsList<ProductQuantity>(await UserController.AddUserProduct(payload.UserId, payload.ProductId, payload.Quantity));
}).WithTags("Users Endpoints");

app.MapPut("/changeUserProductQuantity", async (AddUserProductPayload payload) =>
{
    return new GetItemsList<ProductQuantity>(await UserController.ChangeUserProductQuantity(payload.UserId, payload.ProductId, payload.Quantity));
}).WithTags("Users Endpoints");

app.MapDelete("/deleteUserProduct", async ([FromBody] DeleteUserProductPayload payload) =>
{
    return new GetItemsList<ProductQuantity>(await UserController.DeleteUserProduct(payload.UserId, payload.ProductId));
}).WithTags("Users Endpoints");
#endregion

#region "Dishes Endpoint"

app.MapGet("/dishes", 
    async (string? filterNarionality, int? filterComplaxity, int? filterMinTime, int? filterMaxTime, Guid? userId) =>
           await DishController.GetDishesAsync(filterNarionality, filterComplaxity, filterMinTime, filterMaxTime, userId)).WithTags("Dishes Endpoints");

app.MapGet("/dish", async (Guid id) => await DishController.GetDishByIdAsync(id)).WithTags("Dishes Endpoints");

#endregion

app.MapGet("/getImage", async (string path) => ImageController.GetImage(path)).WithTags("Image Endpoints");

#region TECH

app.MapGet("/generateInstance", async () =>
{
    ProductController.GenerateInstatceInDB();
    DishController.GenerateInstatceDishesInDB();
    UserController.GenerateUserInstanse();
}).WithTags("Tech Endpoints");

app.MapPost("/createProduct", async (Product productToCreate) =>
{
    bool createSuccessful = await ProductController.CreateProductAsync(productToCreate);
    return createSuccessful ? Results.Ok("Create successful.") : Results.BadRequest();

}).WithTags("Tech Endpoints");


#endregion

app.Run();