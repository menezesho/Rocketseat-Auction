using Auction.API.Contracts;
using Auction.API.Filters;
using Auction.API.Models;
using Auction.API.Repositories;
using Auction.API.Repositories.DataAccess;
using Auction.API.Services;
using Auction.API.UseCases.Auctions.GetCurrent;
using Auction.API.UseCases.Offers.CreateOffer;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<AuthenticationUserAttribute>();
builder.Services.AddScoped<LoggedUser>();
builder.Services.AddScoped<CreateOfferUseCase>();
builder.Services.AddScoped<GetCurrentAuctionUseCase>();
builder.Services.AddScoped<IAuctionRepository, AuctionRepository>();
builder.Services.AddScoped<IOfferRepository, OfferRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddDbContext<AuctionDbContext>(options =>
{
    options.UseMySQL($"server={Env.ip};port={Env.port};User Id={Env.user};database={Env.database};password={Env.password};");
});

builder.Services.AddHttpContextAccessor();

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
