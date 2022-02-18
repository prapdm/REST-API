using Microsoft.EntityFrameworkCore;
using ShopApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShopApi
{
    public class DBSeeder
    {
        private readonly ShopDBContext _dbContext;

        public DBSeeder(ShopDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Seed()
        {

            if (_dbContext.Database.CanConnect())
            {

             
                var pendingMigrations = _dbContext.Database.GetPendingMigrations();
                if (pendingMigrations != null && pendingMigrations.Any())
                {
                    
                    _dbContext.Database.Migrate();
                }


                if (!_dbContext.Users.Any())
                {
                    var users = GetUsers();
                    _dbContext.Users.AddRange(users);
                    _dbContext.SaveChanges();
                }

                if (!_dbContext.Shops.Any())
                {
                    var shops = GetShops();
                    _dbContext.Shops.AddRange(shops);
                    _dbContext.SaveChanges();
                }

            }
        }

        private IEnumerable<Shop> GetShops()
        {
            var shops = new List<Shop>()
            {
                new Shop()
                {
                    Name = "Furniture Shop",
                    Category = "Furniture",
                    Description = "The furniture shop is the best shop around the city. This is being run under the store owner and our aim is to provide quality product and hassle free customer service.",
                    ContactEmail = "contact@fshop.com",
                    Website = "www.fshop.com",
                    Phone = "456244767",
                    HasDelivery = true,
                    Product = new List<Product>()
                    {
                        new Product()
                        {
                            Name = "Ash Double Bed",
                            Description =  "A bed is a piece of furniture which is used as a place to sleep or relax.",
                            Price = 220M,
                            Category = "bed, master bed",
                        },
                        new Product()
                        {
                            Name = "Brown Hardwood Double Bed",
                            Description =  "A bed is a piece of furniture which is used as a place to sleep or relax.",
                            Price = 130M,
                            Category = "bed, master bed",
                        },
                        new Product()
                        {
                            Name = "Deluxe Mahagony Double Bed",
                            Description =  "A bed is a piece of furniture which is used as a place to sleep or relax.",
                            Price = 330M,
                            Category = "bed, master bed",
                        },
                        new Product()
                        {
                            Name = "Vince Ottoman Double Bed",
                            Description =  "A bed is a piece of furniture which is used as a place to sleep or relax.",
                            Price = 280M,
                            Category = "bed, master bed",
                        },
                        new Product()
                        {
                            Name = "Yuxin Sofa Set",
                            Description =  "a piece of furniture, also called a couch",
                            Price = 1280M,
                            Category = "sofa, sofa set",
                        },
                        new Product()
                        {
                            Name = "Castlery Double Sofa",
                            Description =  "a piece of furniture, also called a couch",
                            Price = 680M,
                            Category = "sofa, sofa set",
                        },
                        new Product()
                        {
                            Name = "Ava Bedside Table",
                            Description =  "A table is an item of furniture with a flat top and one or more legs, used as a surface for working at, eating from or on which to place things.",
                            Price = 75M,
                            Category = "table, bedside table",
                        },
                        new Product()
                        {
                            Name = "Hana Bedside Table",
                            Description =  "A table is an item of furniture with a flat top and one or more legs, used as a surface for working at, eating from or on which to place things.",
                            Price = 125M,
                            Category = "table, bedside table",
                        },
                        new Product()
                        {
                            Name = "White Cozy Chair",
                            Description =  "One of the basic pieces of furniture, a chair is a type of seat.",
                            Price = 165M,
                            Category = "chair, cozy chair",
                        },

                    },
                    Address = new Address()
                    {
                        City = "Krakow",
                        Street = "Długa 5",
                        PostalCode = "30-001",
                    },
                },
                new Shop()
                {
                    Name = "Bags Shop",
                    Category = "bags",
                    Description = "The Bag shop is the best shop around the city. This is being run under the store owner and our aim is to provide quality product and hassle free customer service.",
                    ContactEmail = "contact@bags.com",
                    Website = "www.bags.com",
                    Phone = "435653455",
                    HasDelivery = true,
                    Product = new List<Product>()
                    {
                        new Product()
                        {
                            Name = "Gucci Handbag",
                            Description =  "Luxury Italian fashion house Gucci is renowned for its instantly recognisable bags and accessories, infusing its unique sense of quality and exquisite design into each piece. This pink logo print leather backpack from Gucci features top handles, a drawstring fastening, a pebbled leather texture, a removable zipped pouch and a vintage Gucci logo.",
                            Price = 450M,
                            Category = "hand bags",
                        },
                        new Product()
                        {
                            Name = "Armani Handbag",
                            Description =  "Black logo embossed messenger bag from Giorgio Armani featuring an adjustable shoulder strap, a top zip fastening and a front zip pocket.",
                            Price = 300M,
                            Category = "hand bags",
                        },
                        new Product()
                        {
                            Name = "The Marc Jacobs",
                            Description =  "Adjustable shoulder straps, 12\" drop Dimensions: 22.9\"W x 6.1\"D x 13\"H Magnetic closure Interior zip compartment Leather hang tag with log and lock Signature gold-tone hardware Unlined Leather with polyurethane coating",
                            Price = 120M,
                            Category = "shoulder bags",
                        },
                        new Product()
                        {
                            Name = "Gucci Purse",
                            Description =  "Luxury Italian fashion house Gucci is renowned for its instantly recognisable bags and accessories, infusing its unique sense of quality and exquisite design into each piece. This pink logo print leather backpack from Gucci features top handles, a drawstring fastening, a pebbled leather texture, a removable zipped pouch and a vintage Gucci logo.",
                            Price = 90M,
                            Category = "purse",
                        },
                        new Product()
                        {
                            Name = "Givenchy Purse",
                            Description =  "Established in 1952, Givenchy's stance on contemporary elegance is perfectly captured through the brand’s premium accessory collections. Crafted from calf leather, this grey GV3 croc-effect shoulder bag from Givenchy features a chain top handle with logo charm, a detachable shoulder strap, a front flap closure, a metal logo plaque to the front, gold-tone hardware and suede panels.",
                            Price = 60M,
                            Category = "purse",
                        },
                        new Product()
                        {
                            Name = "Armani Silver Purse",
                            Description =  "The name Giorgio Armani has become synonymous with clean lines and Italian style. One of the most recognisable fashion houses in the world, the label has dressed some of the world’s most beautiful women.",
                            Price = 120M,
                            Category = "purse",
                        },
                        new Product()
                        {
                            Name = "KAAI Pyramid Bag",
                            Description =  "Established in 1952, Givenchy's stance on contemporary elegance is perfectly captured through the brand’s premium accessory collections. Crafted from calf leather, this grey GV3 croc-effect shoulder bag from Givenchy features a chain top handle with logo charm, a detachable shoulder strap, a front flap closure, a metal logo plaque to the front, gold-tone hardware and suede panels.",
                            Price = 75M,
                            Category = "purse",
                        },

                    },
                    Address = new Address()
                    {
                        City = "Wrocław",
                        Street = "Jagodowa 56",
                        PostalCode = "54-001",
                    },
                },

            };
            return shops;
        }
    

        private static IEnumerable<User> GetUsers()
        {
            var users = new List<User>()
            {
                new User()
                {
                    Role = new Role()
                    {
                        Name = "Administrator",
                        CreatedAt = DateTime.Now
                    },
                    Name = "Jan",
                    Surname = "Admiński",
                    Email = "admin@manager.pl",
                    Phone = "654125456",
                    IsActive = true,
                    PasswordHash = "AQAAAAEAACcQAAAAEApugCGEqb6o7UxWAZkMa5nzUgVEJZCVheEEyofMZ7/AVucqyq1KjjqQ1/CReYjtcg==",
                    CreatedAt = DateTime.Now
                },
                new User()
                {
                    Role = new Role()
                    {
                        Name = "Manager",
                        CreatedAt = DateTime.Now
                    },
                    Name = "Stefan",
                    Surname = "Fajkowski",
                    Email = "manager@manager.pl",
                    Phone = "734242553",
                    IsActive = true,
                    PasswordHash = "AQAAAAEAACcQAAAAEApugCGEqb6o7UxWAZkMa5nzUgVEJZCVheEEyofMZ7/AVucqyq1KjjqQ1/CReYjtcg==",
                    CreatedAt = DateTime.Now

                },
                new User()
                {
                    Role = new Role()
                    {
                        Name = "User",
                        CreatedAt = DateTime.Now
                    },
                    Name = "August",
                    Surname = "Wienkowski",
                    Email = "user@manager.pl",
                    Phone = "553334234",
                    IsActive = true,
                    PasswordHash = "AQAAAAEAACcQAAAAEApugCGEqb6o7UxWAZkMa5nzUgVEJZCVheEEyofMZ7/AVucqyq1KjjqQ1/CReYjtcg==",
                    CreatedAt = DateTime.Now

                }
            };

            return users;

        }





    }
}
