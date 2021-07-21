using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.EntityFrameworkCore;

namespace PublicFoundation
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public class FenContext : DbContext
        {
            public DbSet<Account> accounts { get; set; }
            public DbSet<Category> categories { get; set; }
            public DbSet<Member> members { get; set; }
            public DbSet<Service> services { get; set; }
            public DbSet<ServiceLine> servicelines { get; set; }
            public DbSet<Link> links { get; set; }
            protected override void OnConfiguring(DbContextOptionsBuilder optionsbuilder)
            {
                optionsbuilder.UseSqlite("Data Source=fen.sqlite");
            }
        }
        public class Account
        {
            public int id { get; set; }
            public string name_account { get; set; }
        }
        public class Category
        {
            public int id { get; set; }
            public string title { get; set; }
            public string abbreviation { get; set; }
        }
        public class Member
        {
            public int id { get; set; }
            public string registration_number { get; set; }
            public string first_name { get; set; }
            public string middle_name { get; set; }
            public string last_name { get; set; }
            public int age { get; set; }
            public string gender { get; set; }
            public string bday { get; set; }
            public string phone_number { get; set; }
            public int disability_group { get; set; }
            public int category_id { get; set; }
            public string status { get; set; }
            public string entry_date { get; set; }
            public string residence_street { get; set; }
            public string residence_apartment_number { get; set; }
            public string residence_house_number { get; set; }
            public string residence_locality { get; set; }
            public string residence_district { get; set; }
            public string residence_region { get; set; }
            public string residence_postal_code { get; set; }
            public string actual_district { get; set; }
            public string passport_series { get; set; }
            public string passport_number { get; set; }
            public string passport_issued_by { get; set; }
            public string passport_issued_date { get; set; }
            public string passport_division_code { get; set; }
            public bool gosuslugi_registration_availability { get; set; }
            public string inn { get; set; }
            public string snils { get; set; }
            public string pension_certificate_number { get; set; }
            public string vtek_number { get; set; }
        }
        public class Service
        {
            public int id { get; set; }
            public string data { get; set; }
            public int invalid_id { get; set; }
        }
        public class ServiceLine
        {
            public int id { get; set; }
            public int service_id { get; set; }
            public string title { get; set; }
            public string description { get; set; }
            public string estimation { get; set; }
            public string issued_by { get; set; }
        }
        public class Link
        {
            public int id { get; set; }
            public int invalid_id { get; set; }
            public string web_place { get; set; }
            public string link { get; set; }
        }
    }
}