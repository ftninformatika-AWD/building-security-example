using BuildingExample.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BuildingExample.Repositories
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Building> Buildings { get; set; }
        public DbSet<Apartment> Apartments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed Roles
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole { Name = "Administrator", NormalizedName = "ADMINISTRATOR" },
                new IdentityRole { Name = "Seller", NormalizedName = "SELLER" }
            );

            // Seed Buildings
            modelBuilder.Entity<Building>().HasData(
                new Building
                {
                    Id = 1,
                    Address = "123 Main St",
                    Floors = 5,
                    HasElevator = true,
                    YearOfConstruction = 2000
                },
                new Building
                {
                    Id = 2,
                    Address = "456 Oak Ave",
                    Floors = 6,
                    HasElevator = false,
                    YearOfConstruction = 1995
                },
                new Building
                {
                    Id = 3,
                    Address = "789 Pine Blvd",
                    Floors = 4,
                    HasElevator = true,
                    YearOfConstruction = 2010
                },
                new Building
                {
                    Id = 4,
                    Address = "101 Maple Dr",
                    Floors = 8,
                    HasElevator = true,
                    YearOfConstruction = 2005
                },
                new Building
                {
                    Id = 5,
                    Address = "202 Birch Rd",
                    Floors = 3,
                    HasElevator = false,
                    YearOfConstruction = 1990
                }
            );

            modelBuilder.Entity<Apartment>().HasData(
                new Apartment
                {
                    Id = 1,
                    ApartmentNumber = "101",
                    Floor = 1,
                    Area = 50,
                    NumberOfRooms = 2,
                    BuildingId = 1
                },
                new Apartment
                {
                    Id = 2,
                    ApartmentNumber = "102",
                    Floor = 1,
                    Area = 55,
                    NumberOfRooms = 2,
                    BuildingId = 1
                },
                new Apartment
                {
                    Id = 3,
                    ApartmentNumber = "201",
                    Floor = 2,
                    Area = 60,
                    NumberOfRooms = 3,
                    BuildingId = 1
                },
                new Apartment
                {
                    Id = 4,
                    ApartmentNumber = "202",
                    Floor = 2,
                    Area = 65,
                    NumberOfRooms = 3,
                    BuildingId = 1
                },
                new Apartment
                {
                    Id = 5,
                    ApartmentNumber = "301",
                    Floor = 3,
                    Area = 70,
                    NumberOfRooms = 3,
                    BuildingId = 2
                },
                new Apartment
                {
                    Id = 6,
                    ApartmentNumber = "302",
                    Floor = 3,
                    Area = 75,
                    NumberOfRooms = 3,
                    BuildingId = 2
                },
                new Apartment
                {
                    Id = 7,
                    ApartmentNumber = "401",
                    Floor = 4,
                    Area = 80,
                    NumberOfRooms = 4,
                    BuildingId = 3
                },
                new Apartment
                {
                    Id = 8,
                    ApartmentNumber = "402",
                    Floor = 4,
                    Area = 85,
                    NumberOfRooms = 4,
                    BuildingId = 3
                },
                new Apartment
                {
                    Id = 9,
                    ApartmentNumber = "501",
                    Floor = 5,
                    Area = 90,
                    NumberOfRooms = 5,
                    BuildingId = 4
                },
                new Apartment
                {
                    Id = 10,
                    ApartmentNumber = "502",
                    Floor = 5,
                    Area = 95,
                    NumberOfRooms = 5,
                    BuildingId = 4
                },
                new Apartment
                {
                    Id = 11,
                    ApartmentNumber = "601",
                    Floor = 6,
                    Area = 100,
                    NumberOfRooms = 6,
                    BuildingId = 5
                },
                new Apartment
                {
                    Id = 12,
                    ApartmentNumber = "602",
                    Floor = 6,
                    Area = 105,
                    NumberOfRooms = 6,
                    BuildingId = 5
                }
            );

        }
    }
}
