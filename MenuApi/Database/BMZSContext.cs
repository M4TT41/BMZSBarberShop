using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration.Conventions;
using BMZSApi.Models;

namespace BMZSApi.Database
{
    public class BMZSContext : DbContext
    {
        public DbSet<Fodrasz> Fodraszok { get; set; }
        public DbSet<Fodraszat> Fodraszatok { get; set; }
        public DbSet<Foglalas> Foglalasok { get; set; }
        public DbSet<Kepek> Kepek { get; set; }
        public DbSet<Szolgaltatas> Szolgaltatasok { get; set; }
        public DbSet<Varos> Varosok { get; set; }
        public DbSet<Vasarlo> Vasarlok { get; set; }
        public DbSet<Naptar> Naptar { get; set; }
        public DbSet<Szavazott> Szavazott { get; set; }

        public DbSet<Beosztas> Beosztasok { get; set; }
        public BMZSContext() : base("name=BMZSContext")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Egyéb----------
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            //Egyéb----------
            //Szavazas--------
            modelBuilder.Entity<Szavazott>()
                .HasKey(x => new { x.KepekId, x.VasarloId });

            modelBuilder.Entity<Szavazott>()
                .HasRequired(x => x.Kepek)
                .WithMany(y => y.Szavazottak)
                .HasForeignKey(x => x.KepekId);

            modelBuilder.Entity<Szavazott>()
                .HasRequired(x => x.Vasarlo)
                .WithMany(y => y.Szavazottak)
                .HasForeignKey(x => x.VasarloId);
            //Szavazas----------
            //Foglalas----------
            /*modelBuilder.Entity<Foglalas>()
                .HasKey(x => new { x.SzolgaltatasId, x.VasarloId, x.NaptarId });*/

            modelBuilder.Entity<Foglalas>()
                .HasRequired(x => x.Szolgaltatas)
                .WithMany(y => y.Foglalasok)
                .HasForeignKey(x => x.SzolgaltatasId);

            modelBuilder.Entity<Foglalas>()
                .HasRequired(x => x.Vasarlo)
                .WithMany(y => y.Foglalasok)
                .HasForeignKey(x => x.VasarloId);

            modelBuilder.Entity<Foglalas>()
                .HasRequired(x => x.Naptar)
                .WithMany(y => y.Foglalasok)
                .HasForeignKey(x => x.NaptarId);
            //Foglalas----------
        }
    }
}