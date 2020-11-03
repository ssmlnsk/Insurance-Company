using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Insurance_Company.Models;

namespace Insurance_Company.Data
{
    public partial class InsuranceCompanyContext : DbContext
    {
        public InsuranceCompanyContext()
        {
        }

        public InsuranceCompanyContext(DbContextOptions<InsuranceCompanyContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Dolzhnosti> Dolzhnosti { get; set; }
        public virtual DbSet<GruppyKlientov> GruppyKlientov { get; set; }
        public virtual DbSet<Klienty> Klienty { get; set; }
        public virtual DbSet<Polisy> Polisy { get; set; }
        public virtual DbSet<Riski> Riski { get; set; }
        public virtual DbSet<Sotrudniki> Sotrudniki { get; set; }
        public virtual DbSet<VidyPolisov> VidyPolisov { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                // optionsBuilder.UseSqlite("Data Source=D:\\Учёба\\2 курс\\ОПБД\\БД Страховая компания\\Insurance-Company.db");
                //optionsBuilder.UseSqlServer("Data Source=SSMLNSK;Initial Catalog=Insurance-Company;Integrated Security=True");
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\mssqllocaldb;Initial Catalog=Insurance-Company;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Dolzhnosti>(entity =>
            {
                entity.HasKey(e => e.KodDolzhnosti);

                entity.Property(e => e.KodDolzhnosti)
                    .HasColumnName("Kod_dolzhnosti")
                    .HasColumnType("INT")
                    .ValueGeneratedNever();

                entity.Property(e => e.NaimenovanieDolzhnosti)
                    .IsRequired()
                    .HasColumnName("Naimenovanie_dolzhnosti")
                    .HasColumnType("VARCHAR(50)");

                entity.Property(e => e.Obyazannosti)
                    .IsRequired()
                    .HasColumnType("VARCHAR(100)");

                entity.Property(e => e.Oklad).HasColumnType("FLOAT");

                entity.Property(e => e.Trebovaniya)
                    .IsRequired()
                    .HasColumnType("VARCHAR(100)");
            });

            modelBuilder.Entity<GruppyKlientov>(entity =>
            {
                entity.HasKey(e => e.KodGruppy);

                entity.ToTable("Gruppy_klientov");

                entity.Property(e => e.KodGruppy)
                    .HasColumnName("Kod_gruppy")
                    .HasColumnType("INT")
                    .ValueGeneratedNever();

                entity.Property(e => e.Naimenovanie)
                    .IsRequired()
                    .HasColumnType("VARCHAR(50)");

                entity.Property(e => e.Opisanie)
                    .IsRequired()
                    .HasColumnType("VARCHAR(100)");
            });

            modelBuilder.Entity<Klienty>(entity =>
            {
                entity.HasKey(e => e.KodKlienta);

                entity.Property(e => e.KodKlienta)
                    .HasColumnName("Kod_klienta")
                    .HasColumnType("INT")
                    .ValueGeneratedNever();

                entity.Property(e => e.Adres)
                    .IsRequired()
                    .HasColumnType("VARCHAR(100)");

                entity.Property(e => e.DataRozhdeniya)
                    .IsRequired()
                    .HasColumnName("Data_rozhdeniya")
                    .HasColumnType("DATETIME");

                entity.Property(e => e.Fio)
                    .IsRequired()
                    .HasColumnName("FIO")
                    .HasColumnType("VARCHAR(50)");

                entity.Property(e => e.KodGruppy)
                    .HasColumnName("Kod_gruppy")
                    .HasColumnType("INT");

                entity.Property(e => e.PasportnyeDannye)
                    .IsRequired()
                    .HasColumnName("Pasportnye_dannye")
                    .HasColumnType("VARCHAR(50)");

                entity.Property(e => e.Pol)
                    .IsRequired()
                    .HasColumnType("CHAR(1)");

                entity.Property(e => e.Telefon)
                    .IsRequired()
                    .HasColumnType("VARCHAR(11)");

                entity.HasOne(d => d.KodGruppyNavigation)
                    .WithMany(p => p.Klienty)
                    .HasForeignKey(d => d.KodGruppy)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Polisy>(entity =>
            {
                entity.HasKey(e => e.NomerPolisa);

                entity.Property(e => e.NomerPolisa)
                    .HasColumnName("Nomer_polisa")
                    .HasColumnType("INT")
                    .ValueGeneratedNever();

                entity.Property(e => e.DataNachala)
                    .IsRequired()
                    .HasColumnName("Data_nachala")
                    .HasColumnType("DATETIME");

                entity.Property(e => e.DataOkonchaniya)
                    .IsRequired()
                    .HasColumnName("Data_okonchaniya")
                    .HasColumnType("DATETIME");

                entity.Property(e => e.KodKlienta)
                    .HasColumnName("Kod_klienta")
                    .HasColumnType("INT");

                entity.Property(e => e.KodSotrudnika)
                    .HasColumnName("Kod_sotrudnika")
                    .HasColumnType("INT");

                entity.Property(e => e.KodVidaPolisa)
                    .HasColumnName("Kod_vida_polisa")
                    .HasColumnType("INT");

                entity.Property(e => e.OtmetkaOVyplate)
                    .IsRequired()
                    .HasColumnName("Otmetka_o_vyplate")
                    .HasColumnType("CHAR(1)");

                entity.Property(e => e.OtmetkaObOkonchanii)
                    .IsRequired()
                    .HasColumnName("Otmetka_ob_okonchanii")
                    .HasColumnType("CHAR(1)");

                entity.Property(e => e.Stoimost).HasColumnType("FLOAT");

                entity.Property(e => e.SummaVyplaty)
                    .HasColumnName("Summa_vyplaty")
                    .HasColumnType("FLOAT");

                entity.HasOne(d => d.KodKlientaNavigation)
                    .WithMany(p => p.Polisy)
                    .HasForeignKey(d => d.KodKlienta)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.KodSotrudnikaNavigation)
                    .WithMany(p => p.Polisy)
                    .HasForeignKey(d => d.KodSotrudnika)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.KodVidaPolisaNavigation)
                    .WithMany(p => p.Polisy)
                    .HasForeignKey(d => d.KodVidaPolisa)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Riski>(entity =>
            {
                entity.HasKey(e => e.KodRiska);

                entity.Property(e => e.KodRiska)
                    .HasColumnName("Kod_riska")
                    .HasColumnType("INT")
                    .ValueGeneratedNever();

                entity.Property(e => e.Naimenovanie)
                    .IsRequired()
                    .HasColumnType("VARCHAR(50)");

                entity.Property(e => e.Opisanie)
                    .IsRequired()
                    .HasColumnType("VARCHAR(100)");

                entity.Property(e => e.SrednyayaVeroyatnost)
                    .HasColumnName("Srednyaya_veroyatnost")
                    .HasColumnType("FLOAT");
            });

            modelBuilder.Entity<Sotrudniki>(entity =>
            {
                entity.HasKey(e => e.KodSotrudnika);

                entity.Property(e => e.KodSotrudnika)
                    .HasColumnName("Kod_sotrudnika")
                    .HasColumnType("INT")
                    .ValueGeneratedNever();

                entity.Property(e => e.Adres)
                    .IsRequired()
                    .HasColumnType("VARCHAR(100)");

                entity.Property(e => e.DataRozdeniya)
                    .IsRequired()
                    .HasColumnName("Data_rozdeniya")
                    .HasColumnType("DATETIME");

                entity.Property(e => e.Fio)
                    .IsRequired()
                    .HasColumnName("FIO")
                    .HasColumnType("VARCHAR(50)");

                entity.Property(e => e.KodDolzhnosti)
                    .HasColumnName("Kod_dolzhnosti")
                    .HasColumnType("INT");

                entity.Property(e => e.PasportnyeDannye)
                    .IsRequired()
                    .HasColumnName("Pasportnye_dannye")
                    .HasColumnType("VARCHAR(50)");

                entity.Property(e => e.Pol)
                    .IsRequired()
                    .HasColumnType("CHAR(1)");

                entity.Property(e => e.Telefon)
                    .IsRequired()
                    .HasColumnType("VARCHAR(11)");

                entity.HasOne(d => d.KodDolzhnostiNavigation)
                    .WithMany(p => p.Sotrudniki)
                    .HasForeignKey(d => d.KodDolzhnosti)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<VidyPolisov>(entity =>
            {
                entity.HasKey(e => e.KodVidaPolisa);

                entity.ToTable("Vidy_polisov");

                entity.Property(e => e.KodVidaPolisa)
                    .HasColumnName("Kod_vida_polisa")
                    .HasColumnType("INT")
                    .ValueGeneratedNever();

                entity.Property(e => e.KodRiska)
                    .HasColumnName("Kod_riska")
                    .HasColumnType("INT");

                entity.Property(e => e.Naimenovanie)
                    .IsRequired()
                    .HasColumnType("VARCHAR(50)");

                entity.Property(e => e.Opisanie)
                    .IsRequired()
                    .HasColumnType("VARCHAR(100)");

                entity.Property(e => e.Usloviya)
                    .IsRequired()
                    .HasColumnType("VARCHAR(100)");

                entity.HasOne(d => d.KodRiskaNavigation)
                    .WithMany(p => p.VidyPolisov)
                    .HasForeignKey(d => d.KodRiska)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
