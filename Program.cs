using LinkVault.Data;
using LinkVault.Middleware;
using LinkVault.Models;
using LinkVault.Repositories.User;
using LinkVault.Services.Bookmark;
using LinkVault.Services.BookmarkNote;
using LinkVault.Services.Category;
using LinkVault.Services.Note;
using LinkVault.Services.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using System.Reflection.Metadata;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

builder.Services.AddScoped<ICategoryService,CategoryService>();
builder.Services.AddScoped<INoteService, NoteService>();
builder.Services.AddScoped<IBookNoteService, BookNoteService>();
builder.Services.AddScoped<IBookmarkService, BookmarkService>();
builder.Services.AddScoped<IAuthService, AuthService>();
//builder.Services.AddScoped<IUserRepo, UserRepo>();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));



builder.Services.AddIdentity<User, IdentityRole>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddControllers();
builder.Services.AddAuthorization();


builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Description = "JWT Authorization header using the Bearer scheme."
    });

    options.AddSecurityRequirement(document=> new OpenApiSecurityRequirement
    {
       [new OpenApiSecuritySchemeReference("bearer",document)]=[]
    });
});


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    if (!await roleManager.RoleExistsAsync("User"))
        await roleManager.CreateAsync(new IdentityRole("User"));

    if (!await roleManager.RoleExistsAsync("Admin"))
        await roleManager.CreateAsync(new IdentityRole("Admin"));
}



app.UseMiddleware<GlobalExceptionMiddleware>();
app.UseMiddleware<RequestLoggingMiddelware>();
// Configure the HTTP request pipeline.

//if (app.Environment.IsDevelopment())
//{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "LinkVault v1");
    });
//}
//if (app.Environment.IsDevelopment())
//{

app.UseCors("AllowAll");
app.MapOpenApi();
app.UseSwagger();
app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
