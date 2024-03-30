using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NarcisKH.Class;
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
            List<CityProvince> cityProvinces = new List<CityProvince>
            {
            new CityProvince { Id = 1, Name_kh = "កោះកុង", Name_en = "Koh Kong", DeliveryFee = 1.5f },
            new CityProvince { Id = 2, Name_kh = "កណ្ដាល", Name_en = "Kandal", DeliveryFee = 1.5f },
            new CityProvince { Id = 3, Name_kh = "ភ្នំពេញ", Name_en = "Phnom Penh", DeliveryFee = 0 },
            new CityProvince { Id = 4, Name_kh = "ព្រៃវែង", Name_en = "Prey Veng", DeliveryFee = 1.5f },
            new CityProvince { Id = 5, Name_kh = "ស្ទឹងត្រែង", Name_en = "Svay Rieng", DeliveryFee = 1 },
            new CityProvince { Id = 6, Name_kh = "ព្រះសីហនុ", Name_en = "Sihanoukville", DeliveryFee = 2 },
            new CityProvince { Id = 7, Name_kh = "ស្វាយរៀង", Name_en = "Kampong Speu", DeliveryFee = 3 },
            new CityProvince { Id = 8, Name_kh = "កែប", Name_en = "Kep", DeliveryFee = 0 },
            new CityProvince { Id = 9, Name_kh = "ឧត្តរមានជ័យ", Name_en = "Oddar Meanchey", DeliveryFee = 0 },
            new CityProvince { Id = 10, Name_kh = "មណ្ឌលគិរី", Name_en = "Ratanakiri", DeliveryFee = 0 },
            new CityProvince { Id = 11, Name_kh = "បន្ទាយមានជ័យ", Name_en = "Banteay Meanchey", DeliveryFee = 0 },
            new CityProvince { Id = 12, Name_kh = "កំពត", Name_en = "Kampong Thom", DeliveryFee = 0 },
            new CityProvince { Id = 13, Name_kh = "សៀមរាប", Name_en = "Siem Reap", DeliveryFee = 4 },
            new CityProvince { Id = 14, Name_kh = "ពោធិ៍សាត់", Name_en = "Pursat", DeliveryFee = 0 },
            new CityProvince { Id = 15, Name_kh = "កំពង់ចាម", Name_en = "Kampong Cham", DeliveryFee = 0 },
            new CityProvince { Id = 16, Name_kh = "ត្បូងឃ្មុំ", Name_en = "Tbong Khmum", DeliveryFee = 0 },
            new CityProvince { Id = 17, Name_kh = "តាកែវ", Name_en = "Takeo", DeliveryFee = 0 },
            new CityProvince { Id = 18, Name_kh = "រតនគិរី", Name_en = "Kratie", DeliveryFee = 0 },
            new CityProvince { Id = 19, Name_kh = "ក្រចេះ", Name_en = "Phnom Penh", DeliveryFee = 5 },
            new CityProvince { Id = 20, Name_kh = "ព្រះវិហារ", Name_en = "Preah Vihear", DeliveryFee = 0 },
            new CityProvince { Id = 21, Name_kh = "បាត់ដំបង", Name_en = "Battambang", DeliveryFee = 0 },
            new CityProvince { Id = 22, Name_kh = "កំពង់ឆ្នាំង", Name_en = "Kampong Chhnang", DeliveryFee = 0 },
            new CityProvince { Id = 23, Name_kh = "កំពង់ធំ", Name_en = "Kampong Thom", DeliveryFee = 0 },
            new CityProvince { Id = 24, Name_kh = "ប៉ៃលិន", Name_en = "Pailin", DeliveryFee = 0 },
            new CityProvince { Id = 25, Name_kh = "កំពង់ស្ពឺ", Name_en = "Kampong Speu", DeliveryFee = 0 },
            };
            modelBuilder.Entity<CityProvince>().HasData(cityProvinces);
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
                    Name = "Users"
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
                    Name = "XS"
                },
                new Size
                {
                    Id = 2,
                    Name = "S"
                },
                new Size
                {
                    Id = 3,
                    Name = "M"
                },
                new Size
                {
                    Id = 4,
                    Name = "L"
                },
                new Size
                {
                    Id = 5,
                    Name = "XL"
                },
                new Size
                {
                    Id = 6,
                    Name = "XXL"
                }
            };
            modelBuilder.Entity<Size>().HasData(sizes);
            List<Model> Models = new List<Model>
            {
                new Model
                {
                    Id = 1,
                    Name = "Kira",
                    Age = null,
                    Height = 171,
                    Weight = 55,
                    Top = "S",
                    Bottom = "M",
                    ProfilePicture = "https://narciskh-model-images.s3.ap-southeast-1.amazonaws.com/kira.png",
                },
                new Model
                {
                    Id = 2,
                    Name = "Ari",
                    Age = null,
                    Height = 163,
                    Weight = 46,
                    Top = "S",
                    Bottom = "S",
                    ProfilePicture = "https://narciskh-model-images.s3.ap-southeast-1.amazonaws.com/ari.png",
                },
                new Model
                {
                    Id = 3,
                    Name = "Ho Jeoung",
                    Age = null,
                    Height = 165,
                    Weight = 49,
                    Top = "S",
                    Bottom = "S",
                    ProfilePicture = "https://narciskh-model-images.s3.ap-southeast-1.amazonaws.com/Ho+Jeoung.png",
                },
                new Model
                {
                    Id = 4,
                    Name = "Seo Young",
                    Age = null,
                    Height = 164,
                    Weight = 45,
                    Top = "S",
                    Bottom = "S",
                    ProfilePicture = "https://narciskh-model-images.s3.ap-southeast-1.amazonaws.com/Seo+Young.png",
                },
                new Model
                {
                    Id = 5,
                    Name = "Ga Eul",
                    Age = null,
                    Height = 164,
                    Weight = 45,
                    Top = "S",
                    Bottom = "S",
                    ProfilePicture = "https://narciskh-model-images.s3.ap-southeast-1.amazonaws.com/Ga+Eul.png",
                },

            };
            modelBuilder.Entity<Model>().HasData(Models);
            List<Cloth> clothes = new List<Cloth>
            {
                new Cloth {
                    Id = 1,
                    Name= "Wrap style pleated skirt dress",
                    Price = 10,
                    Discount = 0,
                    ImagePaths = new List<string>
                    {
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/C1/C1-030/C1-030+(1_2).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/C1/C1-030/C1-030+(1).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/C1/C1-030/C1-030+(2).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/C1/C1-030/C1-030+(3).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/C1/C1-030/C1-030+(4).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/C1/C1-030/C1-030+(5).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/C1/C1-030/C1-030+(6).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/C1/C1-030/C1-030+(7).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/C1/C1-030/C1-030+(8).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/C1/C1-030/C1-030+(9).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/C1/C1-030/C1-030+(10).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/C1/C1-030/C1-030+(11).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/C1/C1-030/C1-030+(12).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/C1/C1-030/C1-030+(13).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/C1/C1-030/C1-030+(14).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/C1/C1-030/C1-030+(15).jpg",
                    },
                    CategoryId = 3,
                    Description= "\tFormal fabric with a smooth surface, wrap-style dress. The waist size can be adjusted with the strap inserted into the waist of the right layer to create a dress fit, skirt pleated detail from the left body waist cut, long sleeves with a slim silhouette, and a pleated line at the back. Emphasis on lines with darts.",
                    SizeIds = new List<int> { 1, 2, 3, 4, 5, 6 },
                    ModelId = 1,
                    Code = "C1-030"
                },
                new Cloth
                {
                    Id = 2,
                    Name = "V-neck raglan short sleeve top",
                    Price = 10,
                    Discount = 0,
                    ImagePaths = new List<string>
                    {
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-005/A3-005-1.jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-005/A3-005-2.jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-005/A3-005-3.jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-005/A3-005-4.jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-005/A3-005-5.jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-005/A3-005-6.jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-005/A3-005-7.jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-005/A3-005_B3-014+(1)_c.jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-005/A3-005_B3-014+(1).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-005/A3-005_B3-014+(2)_c.jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-005/A3-005_B3-014+(2).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-005/A3-005_B3-014+(3).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-005/A3-005_B3-014+(4).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-005/A3-005_B3-014+(5).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-005/A3-005_B3-014+(5)_c.jpg",
                    },
                    CategoryId = 1,
                    Description = "Soft and drapery white fabric, V-neck neatly finished with inner hem, raglan sleeves, a silhouette with side seams gathered toward the center of the front, double layer point at the front cut, and short length at the center of the front.",
                    SizeIds = new List<int> { 1, 2, 3, 4, 5, 6 },
                    ModelId = 2,
                    Code = "A3-005"
                },
                new Cloth
                {
                    Id = 3,
                    Name = "Linen short sleeve top",
                    Price = 10,
                    Discount = 0,
                    ImagePaths = new List<string>
                    {
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-001/A3-001+(1).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-001/A3-001+(2).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-001/A3-001+(3).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-001/A3-001+(4).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-001/A3-001+(5).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-001/A3-001+(6).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-001/A3-001+(7).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-001/A3-001+(8).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-001/A3-001+(9).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-001/A3-001+(10).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-001/A3-001+(11).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-001/A3-001+(12).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-001/A3-001+(13).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-001/A3-001+(14).jpg",

                    },
                    CategoryId = 1,
                    Description = "\tLinen fabric with a natural mood, basic round neck shape and 1/4\" spaced inbinding for a neat finish, back neck keyhole opening, and a straight silhouette for plenty of room.",
                    SizeIds = new List<int> { 1, 2, 3, 4, 5, 6 },
                    ModelId = 4,
                    Code = "A3-001"
                },
                new Cloth
                {
                    Id = 4,
                    Name = "Cropped Asymmetric Frilled Top",
                    Price = 10,
                    Discount = 0,
                    ImagePaths = new List<string>
                    {
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-002/A3-002_crop.jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-002/A3-002_rsz+(1).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-002/A3-002_rsz+(2).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-002/A3-002_rsz+(3).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-002/A3-002_rsz+(4).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-002/A3-002-P+(1).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-002/A3-002-P+(2).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-002/A3-002-P+(3).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-002/A3-002-P+(4).jpg",
                    },
                    CategoryId = 1,
                    Description = "Fitted long-sleeve top with a cropped asymmetric cut, frilled edges and a ribbed finish.",
                    SizeIds = new List<int> { 1, 2, 3, 4, 5, 6 },
                    ModelId = 5,
                    Code = "A3-002"

                },
                new Cloth
                {
                    Id = 5,
                    Name = "Round crop top",
                    Price = 10,
                    Discount = 0,
                    ImagePaths = new List<string>
                    {
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-006/A3-006+(1).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-006/A3-006+(2).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-006/A3-006+(3).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-006/A3-006+(4).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-006/A3-006+(5).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-006/A3-006+(6).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-006/A3-006+(7).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-006/A3-006+(8).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-006/A3-006+(9).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-006/A3-006+(10).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-006/A3-006+(11).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-006/A3-006+(12).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-006/A3-006+(13).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-006/A3-006_crop.jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-006/A3-006-1.jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-006/A3-006-2.jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-006/A3-006-3.jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-006/A3-006-rsz+(1).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-006/A3-006-rsz+(2).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-006/A3-006-rsz+(3).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-006/A3-006-rsz+(4).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-006/A3-006-rsz+(5).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-006/A3-006-rsz+(6).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-006/A3-006-rsz+(7).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-006/A3-006-rsz+(8).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-006/A3-006-rsz+(9).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-006/A3-006-rsz+(10).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-006/A3-006-rsz+(11).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-006/A3-006-rsz+(12).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-006/A3-006-rsz+(13).jpg",


                    },
                    CategoryId = 1,
                    Description = "\tSoft cotton fabric, cropped length, basic round neck shape, half-length short sleeves, and finished with a fabric neckband.",
                    SizeIds = new List<int> { 1, 2, 3, 4, 5, 6 },
                    ModelId = 3,
                    Code = "A3-006"

                },
                new Cloth
                {
                    Id = 6,
                    Name = "V-neck shoulder detail top",
                    Price = 10,
                    Discount = 0,
                    ImagePaths = new List<string>
                    {
                       "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-011/A3-011-1.jpg",
                       "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-011/A3-011-2.jpg",
                       "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-011/A3-011-3.jpg",
                       "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-011/A3-011-4.jpg",
                       "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-011/A3-011-5.jpg",
                       "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-011/A3-011-6.jpg",
                       "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-011/A3-011-7.jpg",
                       "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-011/A3-011-8.jpg",
                       "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-011/A3-011-9.jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-011/A3-011-10.jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-011/A3-011+(1).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-011/A3-011+(2).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-011/A3-011+(3).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-011/A3-011+(4).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-011/A3-011+(5).jpg",
                    },
                    CategoryId = 1,
                    Description = "Light fabric with a twill texture, two shoulder slits, chin details, V-neck shaped inner hem, volume created by pleats in the center of the sleeves, a clean scooty finish that holds the pleats at the end of the sleeves, and a sense of leisure with the pleat details in the center of the back.",
                    ModelId = 2,
                    SizeIds = new List<int> { 1, 2, 3, 4, 5, 6 },
                    Code = "A3-011"

                },
                new Cloth
                {
                    Id = 7,
                    Name = "Cowl neck side shirring top",
                    Price = 10,
                    Discount = 0,
                    ImagePaths = new List<string>
                    {
                       "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-012/A3-012+(1).jpg",
                       "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-012/A3-012+(2).jpg",
                       "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-012/A3-012+(3).jpg",
                       "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-012/A3-012+(4).jpg",
                       "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-012/A3-012+(5).jpg",
                       "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-012/A3-012+(6).jpg",
                       "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-012/A3-012+(7).jpg",
                       "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-012/A3-012+(8).jpg",
                       "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-012/A3-012+(9).jpg",
                            "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-012/A3-012+(10).jpg",
                            "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-012/A3-012+(11).jpg",
                            "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-012/A3-012+(12).jpg",
                            "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-012/A3-012+(13).jpg",
                            "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-012/A3-012+(14).jpg",
                            "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-012/A3-012+(15).jpg",
                            "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-012/A3-012+(16).jpg",
                            "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-012/A3-012+(17).jpg",
                            "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-012/A3-012+(18).jpg",
                    },
                    CategoryId = 1,
                    Description = "\tStretchy and comfortable fabric, drapery cowl neck, shirring details on both lower side seams, relatively short length and fitted silhouette.",
                    ModelId = 5,
                    SizeIds = new List<int> { 1, 2, 3, 4, 5, 6 },
                    Code = "A3-012"
                },
                new Cloth
                {
                    Id = 8,
                    Name = "Linen V-neck basic top",
                    Price = 14,
                    Discount = 10,
                    ImagePaths = new List<string>
                    {
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-013/A3-013-1.jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-013/A3-013-2.jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-013/A3-013-3.jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-013/A3-013-4.jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-013/A3-013-5.jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-013/A3-013-6.jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-013/A3-013-7.jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-013/A3-013-8.jpg",

                    },
                    CategoryId = 1,
                    Description = "\tCourtyard linen fabric, V-neck shape with neat 1/4\" gap inbinding finish, features a front center incision, and a straight box silhouette.",
                    ModelId = 4,
                    SizeIds = new List<int> { 1, 2, 3, 4, 5, 6 },
                    Code = "A3-013"
                },
                new Cloth
                {
                    Id = 9,
                    Name = "Boxy-Fit Curved-Hem T-Shirt",
                    Description = "A boxy-fit T-shirt with a curved hem, short sleeves, and a round neckline.",
                    Code = "A3-015",
                    Price = 13,
                    Discount = 0,
                    ImagePaths = new List<string>
                    {
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-015/A3-015+(1).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-015/A3-015+(2).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-015/A3-015+(3).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-015/A3-015+(4).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-015/A3-015+(5).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-015/A3-015+(6).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-015/A3-015+(7).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-015/A3-015+(8).jpg",
                    },
                    CategoryId = 1,
                    ModelId = 3,
                    SizeIds = new List<int> { 1, 2, 3, 4, 5, 6 },
                },
                new Cloth
                {
                    Id = 10,
                    Name = "Camisole tank top",
                    Description = "\tThick cotton fabric, camisole-style tank top, length adjustable with shoulder straps, features a princess line and chest slit, and a fitted line that highlights the silhouette.",
                    Code = "A3-016",
                    Price = 10,
                    Discount = 0,
                    ImagePaths = new List<string>
                    {
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-016BK/A3-016-1.jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-016BK/A3-016-2.jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-016BK/A3-016-3.jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-016BK/A3-016-4.jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-016BK/A3-016-5.jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-016BK/A3-016-6.jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-016BK/A3-016-7.jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-016BK/A3-016-8.jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-016BK/A3-016-7_2.jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-016BK/A3-016-8_2.jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-016BK/A3-016-9.jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-016BK/A3-016+(1).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-016BK/A3-016+(2).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-016BK/A3-016+(3).jpg",

                    },
                    CategoryId = 1,
                    ModelId = 1,
                    SizeIds = new List<int> { 1, 2, 3, 4, 5, 6 },

                },
                new Cloth
                {
                    Id = 11,
                    Name = "V-neck lip trim detail setup",
                    Description = "Combination of soft cotton fabric and ribbed fabric, neckband finished with ribs, ribbed trim that highlights the shoulder and arm lines, high ribbed cuffs, cropped length with differences in front and back length, waist elastic band pants, front Side seam crossed over, pocket located on the side seam line.",
                    Code = "A3-018",
                    Price = 10,
                    Discount = 0,
                    ImagePaths = new List<string>
                    {
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-018/A3-018+(2).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-018/A3-018+(3).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-018/A3-018+(4).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-018/A3-018+(5).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-018/A3-018+(6).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-018/A3-018+(7).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-018/A3-018+(8).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-018/A3-018+(9).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-018/A3-018p+(1).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-018/A3-018p+(2).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-018/A3-018p+(3).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-018/A3-018p+(4).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-018/A3-018p+(5).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-018/A3-018p+(6).jpg",
                    },
                    CategoryId = 1,
                    ModelId = 5,
                    SizeIds = new List<int> { 1, 2, 3, 4, 5, 6 },
                },
                new Cloth
                {
                    Id = 12,
                    Name = "Sleeveless crop top",
                    Description = "\tLightweight fabric with crinkled texture, wide shoulder width with shoulder pad inserts, back keyhole button opening, 1/4\" binding at center back, 1/4\" binding strap ribbon tie at hem.",
                    Code = "A3-019",
                    Price = 10,
                    Discount = 0,
                    ImagePaths = new List<string>
                    {
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-019/A3-019+(1).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-019/A3-019+(2).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-019/A3-019+(3).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-019/A3-019+(3)_2.jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-019/A3-019+(4).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-019/A3-019+(5).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-019/A3-019+(6).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-019/A3-019+(7).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-019/A3-019+(8).jpg",
                    },
                    CategoryId = 1,
                    ModelId = 2,
                    SizeIds = new List<int> { 1, 2, 3, 4, 5, 6 },
                    },
                new Cloth
                {
                    Id = 13,
                    Name = "U-Neck Satin Sleeveless Top",
                    Description = "\tSatin fabric with a soft touch and luster, neat inner hem finish with U-neck shape, and shirttail finish at the bottom.",
                    Code = "A3-020",
                    Price = 10,
                    Discount = 0,
                    ImagePaths = new List<string>
                    {
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-020/A3-020_ari-1_crop.jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-020/A3-020_ari-2_crop.jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-020/A3-020_ari-4_crop2.jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-020/A3-020_ari-4_crop.jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-020/A3-020_ari-5_crop.jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-020/A3-020_ari-1_rsz.jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-020/A3-020_ari-5_rsz.jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-020/A3-020_ari-7_rsz.jpg"
                    },
                    CategoryId = 1,
                    ModelId = 2,
                    SizeIds = new List<int> { 1, 2, 3, 4, 5, 6 },


                },
                new Cloth
                {
                    Id = 14,
                    Name = "Asymmetric V-neck blouse",
                    Description = "\tFabric with a sense of surface, V-neck shape and drapery asymmetrical layer at the center front slit, left side seam slit, 3/4 sleeves, pleated details on both sides based on the cuff opening.",
                    Code = "A3-023",
                    Price = 10,
                    Discount = 0,
                    ImagePaths = new List<string>
                    {
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-023/A3-023+(0).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-023/A3-023+(1).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-023/A3-023+(2).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-023/A3-023+(3).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-023/A3-023+(4).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-023/A3-023+(5).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-023/A3-023+(6).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-023/A3-023+(7).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-023/A3-023+(8).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-023/A3-023+(9).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-023/A3-023+(10).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-023/A3-023+(11).jpg",
                    },
                    CategoryId = 1,
                    ModelId = 1,
                    SizeIds = new List<int> { 1, 2, 3, 4, 5, 6 },
                },
                new Cloth
                {
                    Id=15,
                    Name = "High-neck halter sleeveless top",
                    Description = "Dry textured cotton fabric, high neck halter style, shirring detail on the neck cut line, back neck keyhole and pearl button opening, armhole edge 1/4\" inbinding finish, cropped length.",
                    Code = "A3-024",
                    Price = 10,
                    Discount = 0,
                    ImagePaths = new List<string>
                    {
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-024/A3-024+(1).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-024/A3-024+(2).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-024/A3-024+(3).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-024/A3-024+(4).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-024/A3-024+(5).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-024/A3-024+(6).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-024/A3-024+(7).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-024/A3-024+(8).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-024/A3-024+(5-2).jpg",
                    },
                    CategoryId = 1,
                    ModelId = 1,
                    SizeIds = new List<int> { 1, 2, 3, 4, 5, 6 },
                },
                new Cloth
                {
                    Id = 16,
                    Name = "Basic t-shirt",
                    Description = "Soft cotton fabric, basic round neck shape, short sleeves, and a clean finish.",
                    Code = "A3-028",
                    Price = 8,
                    Discount = 0,
                    ImagePaths = new List<string>
                    {
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-028/A3-028+(2).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-028/A3-028+(3).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-028/A3-028+(4).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-028/A3-028+(5).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-028/A3-028+(6).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-028/A3-028+(7).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-028/A3-028+(8).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-028/A3-028+(9).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-028/A3-028+(10).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-028/A3-028+(11).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-028/A3-028+(12).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-028/A3-028+(13).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-028/A3-028+(14).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-028/A3-028+(15).jpg",
                        "https://cloth-images.s3.ap-southeast-1.amazonaws.com/A3/A3-028/A3-028+(16).jpg",

                    },

                    CategoryId = 1,
                    ModelId = 4,
                    SizeIds = new List<int> { 1, 2, 3, 4, 5, 6 },
                
                }
            };
            modelBuilder.Entity<Cloth>().HasData(clothes);
            base.OnModelCreating(modelBuilder);

        }

        public DbSet<NarcisKH.Models.User> Users { get; set; } = default!;
        public DbSet<NarcisKH.Models.Order> Orders { get; set; } = default!;
        public DbSet<NarcisKH.Models.CityProvince> CityProvinces { get; set; } = default!;
        public DbSet<NarcisKH.Models.OrderStatus> OrderStatuses { get; set; } = default!;
        public DbSet<NarcisKH.Models.PaymentStatus> PaymentStatuses { get; set; } = default!;
        public DbSet<NarcisKH.Models.PaymentMethod> PaymentMethods { get; set; } = default!;
        public DbSet<NarcisKH.Models.DeliveryStatus> DeliveryStatuses { get; set; } = default!;
        public DbSet<NarcisKH.Models.Role> Roles { get; set; } = default!;
        public DbSet<NarcisKH.Models.Category> Categories { get; set; } = default!;
        public DbSet<NarcisKH.Models.Cloth> Clothes { get; set; } = default!;
        public DbSet<NarcisKH.Models.Size> Sizes { get; set; } = default!;
        public DbSet<NarcisKH.Models.SizeAndClothQuantity> SizeAndClothQuantities { get; set; } = default!;
        public DbSet<NarcisKH.Models.Status> Statuses { get; set; } = default!;
        public DbSet<NarcisKH.Models.Model> Models { get; set; } = default!;
    }
}
