﻿using Microsoft.EntityFrameworkCore;
namespace parcial.Models
{
    public class DBcontexto : DbContext
    {
        public DBcontexto(DbContextOptions<DBcontexto> options):base(options) { 
        }
        public DbSet<usuario> usuario { get; set; }
        public DbSet<oferta> oferta { get; set; }
        public DbSet<busquedas> busquedas { get; set; }



    }
}
