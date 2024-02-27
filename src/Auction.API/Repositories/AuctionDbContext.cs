using Auction.API.Entities;
using Auction.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Auction.API.Repositories;

public class AuctionDbContext : DbContext
{
    public AuctionDbContext(DbContextOptions options) : base(options)
    {

    }

    public DbSet<AuctionEntity> Auctions { get; set; }
    public DbSet<ItemEntity> Items { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Offer> Offers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ItemEntity>()
            .HasOne<AuctionEntity>()
            .WithMany(auction => auction.Items)
            .HasForeignKey(item => item.AuctionId); // Aqui estamos especificando a chave estrangeira correta

        // Outras configurações do modelo, se houver
    }
}
