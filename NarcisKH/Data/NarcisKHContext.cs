using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NarcisKH.Models;

namespace NarcisKH.Data
{
    public class NarcisKHContext : DbContext
    {
        public NarcisKHContext(DbContextOptions<NarcisKHContext> options)
        : base(options)
        {
           
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            List<CityProvinces> cityProvinces = new List<CityProvinces>
            {
            new CityProvinces { Id = 1, Name_kh = "កោះកុង", Name_en = "Koh Kong", DeliveryFee = 1.5f },
            new CityProvinces { Id = 2, Name_kh = "កណ្ដាល", Name_en = "Kandal", DeliveryFee = 1.5f },
            new CityProvinces { Id = 3, Name_kh = "ភ្នំពេញ", Name_en = "Phnom Penh", DeliveryFee = 0 },
            new CityProvinces { Id = 4, Name_kh = "ព្រៃវែង", Name_en = "Prey Veng", DeliveryFee = 1.5f },
            new CityProvinces { Id = 5, Name_kh = "ស្ទឹងត្រែង", Name_en = "Svay Rieng", DeliveryFee = 1 },
            new CityProvinces { Id = 6, Name_kh = "ព្រះសីហនុ", Name_en = "Sihanoukville", DeliveryFee = 2 },
            new CityProvinces { Id = 7, Name_kh = "ស្វាយរៀង", Name_en = "Kampong Speu", DeliveryFee = 3 },
            new CityProvinces { Id = 8, Name_kh = "កែប", Name_en = "Kep", DeliveryFee = 0 },
            new CityProvinces { Id = 9, Name_kh = "ឧត្តរមានជ័យ", Name_en = "Oddar Meanchey", DeliveryFee = 0 },
            new CityProvinces { Id = 10, Name_kh = "មណ្ឌលគិរី", Name_en = "Ratanakiri", DeliveryFee = 0 },
            new CityProvinces { Id = 11, Name_kh = "បន្ទាយមានជ័យ", Name_en = "Banteay Meanchey", DeliveryFee = 0 },
            new CityProvinces { Id = 12, Name_kh = "កំពត", Name_en = "Kampong Thom", DeliveryFee = 0 },
            new CityProvinces { Id = 13, Name_kh = "សៀមរាប", Name_en = "Siem Reap", DeliveryFee = 4 },
            new CityProvinces { Id = 14, Name_kh = "ពោធិ៍សាត់", Name_en = "Pursat", DeliveryFee = 0 },
            new CityProvinces { Id = 15, Name_kh = "កំពង់ចាម", Name_en = "Kampong Cham", DeliveryFee = 0 },
            new CityProvinces { Id = 16, Name_kh = "ត្បូងឃ្មុំ", Name_en = "Tbong Khmum", DeliveryFee = 0 },
            new CityProvinces { Id = 17, Name_kh = "តាកែវ", Name_en = "Takeo", DeliveryFee = 0 },
            new CityProvinces { Id = 18, Name_kh = "រតនគិរី", Name_en = "Kratie", DeliveryFee = 0 },
            new CityProvinces { Id = 19, Name_kh = "ក្រចេះ", Name_en = "Phnom Penh", DeliveryFee = 5 },
            new CityProvinces { Id = 20, Name_kh = "ព្រះវិហារ", Name_en = "Preah Vihear", DeliveryFee = 0 },
            new CityProvinces { Id = 21, Name_kh = "បាត់ដំបង", Name_en = "Battambang", DeliveryFee = 0 },
            new CityProvinces { Id = 22, Name_kh = "កំពង់ឆ្នាំង", Name_en = "Kampong Chhnang", DeliveryFee = 0 },
            new CityProvinces { Id = 23, Name_kh = "កំពង់ធំ", Name_en = "Kampong Thom", DeliveryFee = 0 },
            new CityProvinces { Id = 24, Name_kh = "ប៉ៃលិន", Name_en = "Pailin", DeliveryFee = 0 },
            new CityProvinces { Id = 25, Name_kh = "កំពង់ស្ពឺ", Name_en = "Kampong Speu", DeliveryFee = 0 },
            };
            modelBuilder.Entity<CityProvinces>().HasData(cityProvinces);
            List<OrderStatus> orderStatuses = new List<OrderStatus>
            {
                new OrderStatus
                {
                    Id = 1,
                    Name = "Pending",
                    Note = "The order is pending"
                },
                new OrderStatus
                {
                    Id = 2,
                    Name = "Processing",
                },
                new OrderStatus
                {
                    Id = 3,
                    Name = "Delivering",
                },
                new OrderStatus
                {
                    Id = 4,
                    Name = "Completed",
                },
                new OrderStatus
                {
                    Id = 5,
                    Name = "Cancelled",
                }

            };  

            modelBuilder.Entity<OrderStatus>().HasData(orderStatuses);
            List<PaymentStatus> paymentStatuses = new List<PaymentStatus>
            {
                new PaymentStatus
                {
                    Id = 1,
                    Name = "NotPaid",
                },
                new PaymentStatus
                {
                    Id = 2,
                    Name = "Paid",
                },
                new PaymentStatus
                {
                    Id = 3,
                    Name = "Refunded",
                }
            };
            modelBuilder.Entity<PaymentStatus>().HasData(paymentStatuses);
            List<PaymentMethod> paymentMethods = new List<PaymentMethod>
            {
                new PaymentMethod
                {
                    Id = 1,
                    Name = "CashOnDelivery",
                },
                new PaymentMethod
                {
                    Id = 2,
                    Name = "BankTransfer",
                },
                
            };
            modelBuilder.Entity<PaymentMethod>().HasData(paymentMethods);
            List<DeliveryStatus> deliveryStatuses = new List<DeliveryStatus>
            {
                new DeliveryStatus
                {
                    Id = 1,
                    Name = "Pending",
                },
                new DeliveryStatus
                {
                    Id = 2,
                    Name = "Processing",
                },
                new DeliveryStatus
                {
                    Id = 3,
                    Name = "Delivered",
                },
            };
            modelBuilder.Entity<DeliveryStatus>().HasData(deliveryStatuses);

            List<Role> roles = new List<Role>
            {
                new Role
                {
                    Id = 1,
                    Name = "Admin"
                },
                new Role
                {
                    Id = 2,
                    Name = "User"
                },
                new Role
                {
                    Id = 3,
                    Name = "Developer"
                }
            };
            modelBuilder.Entity<Role>().HasData(roles);
            List<User> users = new List<User>
            {
                new User
                {
                    Id = 1,
                    Username = "admin",
                    Password = "admin",
                    PhoneNumber = "123456789"
                },
                new User
                {
                    Id = 2,
                    Username = "user",
                    Password = "user",
                    PhoneNumber = "987654321"
                }
            };
            modelBuilder.Entity<User>().HasData(users);
            List<Category> categories = new List<Category>
            {
                new Category
                {
                    Id = 1,
                    Name = "Top & T-shirt",
                    ParentId = null
                },
                new Category
                {
                    Id = 2,
                    Name = "Shirt & Blouse",
                    ParentId = null
                },
                new Category
                {
                    Id = 3,
                    Name = "One-Piece & Dress",
                    ParentId = null
                },
                new Category
                {
                    Id = 4,
                    Name = "Pants & Shorts",
                    ParentId = null
                },
                new Category
                {
                    Id = 5,
                    Name = "Skirt",
                    ParentId = null
                },
                new Category
                {
                    Id = 6,
                    Name = "Mini-Midi",
                    ParentId = 3
                },
                new Category
                {
                    Id = 7,
                    Name = "Long-Maxi",
                    ParentId = 3
                },
                new Category
                {
                    Id = 8,
                    Name = "Jumpsuit",
                    ParentId = 3
                },
                new Category
                {
                    Id = 9,
                    Name = "Trousers",
                    ParentId = 4
                },
                new Category
                {
                    Id = 10,
                    Name = "Shorts",
                    ParentId = 4
                },

            };
            modelBuilder.Entity<Category>().HasData(categories);
            List<Size> sizes = new List<Size>
            {
                new Size
                {
                    Id = 1,
                    Name = "S"
                },
                new Size
                {
                    Id = 2,
                    Name = "M"
                },
                new Size
                {
                    Id = 3,
                    Name = "L"
                },
                new Size
                {
                    Id = 4,
                    Name = "XL"
                },
                new Size
                {
                    Id = 5,
                    Name = "XXL"
                }
            };
            modelBuilder.Entity<Size>().HasData(sizes);
            //List<Status> statuses = new List<Status>
            //{
            //    new Status
            //    {
            //        Id = 1,
            //        OrderStatus = orderStatuses[0],
            //        PaymentStatus = paymentStatuses[0],
            //        PaymentMethod = paymentMethods[0],
            //        DeliveryStatus = deliveryStatuses[0],
            //    }
            //};
            //modelBuilder.Entity<Status>().HasData(statuses);
            //List<Order> orders = new List<Order>
            //{
            //    new Order
            //    {
            //        Id = 1,
            //        FullName = "Teng Sambo",
            //        Address = "Phnom Penh, Dangkor, Dangkor",
            //        Phone = "087827181",
            //        Note = "Please call me before deliver",
            //        CityProvince = cityProvinces[3],
            //        Employee = users[0],
            //        Status = statuses[0],
            //        CreatedDate = DateTime.Now,
            //        UpdatedDate = DateTime.Now

            //    }
            //};
            //modelBuilder.Entity<Order>().HasData(orders);
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<NarcisKH.Models.User> User { get; set; } = default!;
        public DbSet<NarcisKH.Models.Order> Order { get; set; } = default!;
        public DbSet<NarcisKH.Models.CityProvinces> CityProvinces { get; set; } = default!;
        public DbSet<NarcisKH.Models.OrderStatus> OrderStatus { get; set; } = default!;
        public DbSet<NarcisKH.Models.PaymentStatus> PaymentStatus { get; set; } = default!;
        public DbSet<NarcisKH.Models.PaymentMethod> PaymentMethod { get; set; } = default!;
        public DbSet<NarcisKH.Models.DeliveryStatus> DeliveryStatus { get; set; } = default!;
        public DbSet<NarcisKH.Models.Role> Role { get; set; } = default!;
        public DbSet<NarcisKH.Models.Category> Category { get; set; } = default!;
        public DbSet<NarcisKH.Models.Cloth> Cloth { get; set; } = default!;
        public DbSet<NarcisKH.Models.Size> Size { get; set; } = default!;
        public DbSet<NarcisKH.Models.SizeAndClothQuantity> SizeAndClothQuantity { get; set; } = default!;


    }
}
