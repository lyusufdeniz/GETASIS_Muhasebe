using GETASIS_Muhasebe.Entities;
using Microsoft.EntityFrameworkCore;

namespace GETASIS_Muhasebe.DAL
{
    public class Context : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=lenovo-yusuf\\sqlexpress;database=getasis_muhasebe;integrated security=true");


        }
        public DbSet<Cari> Cari { get; set; }
        public DbSet<Doviz> Doviz { get; set; }
        public DbSet<Hesap> Hesap { get; set; }
        public DbSet<Odeme> Odeme { get; set; }
        public DbSet<OdemeTip> OdemeTip { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OdemeTip>()
              .HasData(
               new OdemeTip { Id = 1, Ad = "Personel" },
               new OdemeTip { Id = 2, Ad = "Cari" },
               new OdemeTip { Id = 3, Ad = "Vergi" },
               new OdemeTip { Id = 4, Ad = "Şahsi" },
               new OdemeTip { Id = 5, Ad = "Diğer" });

            modelBuilder.Entity<Doviz>()
           .HasData(
            new Doviz { Id = 1, Ad = "Türk Lirası", Kisa = "₺" },
            new Doviz { Id = 2, Ad = "Dolar", Kisa = "$" },
            new Doviz { Id = 3, Ad = "Euro", Kisa = "€" },
            new Doviz { Id = 4, Ad = "Altın", Kisa = "Au" });

            modelBuilder.Entity<Odeme>().Property(p => p.VadeTarihi).HasDefaultValue(null);
            modelBuilder.Entity<Odeme>().Property(p => p.Silindi).HasDefaultValue(0);


            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
       .SelectMany(t => t.GetForeignKeys())
       .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
                fk.DeleteBehavior = DeleteBehavior.Restrict;
        }

    }
}
