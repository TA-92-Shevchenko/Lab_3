using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace _3
{
    public partial class PostOfficeContext : DbContext
    {
        public PostOfficeContext()
        {
        }

        public PostOfficeContext(DbContextOptions<PostOfficeContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Parcel> Parcels { get; set; }
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<PostOffice> PostOffices { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-C18IDHU;Database= Post Office;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<Client>(entity =>
            {
                entity.ToTable("Client");

                entity.Property(e => e.Id)
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("email")
                    .IsFixedLength(true);

                entity.Property(e => e.PhoneNumber).HasColumnName("Phone_number");
            });

            modelBuilder.Entity<Parcel>(entity =>
            {
                entity.ToTable("Parcel");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.RecipientAddress)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("Recipient_Address")
                    .IsFixedLength(true);

                entity.Property(e => e.RecipientId)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("Recipient_Id")
                    .IsFixedLength(true);

                entity.Property(e => e.SenderAddress)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("Sender_Address")
                    .IsFixedLength(true);

                entity.Property(e => e.SenderDate)
                    .HasColumnType("date")
                    .HasColumnName("Sender_Date");

                entity.Property(e => e.SenderId)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("Sender_Id")
                    .IsFixedLength(true);

                entity.Property(e => e.Size)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("size")
                    .IsFixedLength(true);

                entity.Property(e => e.Weight).HasColumnName("weight");
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.ToTable("Person");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Birthday).HasColumnType("date");

                entity.Property(e => e.ClientId)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("Client_Id")
                    .IsFixedLength(true);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("First_name")
                    .IsFixedLength(true);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("Last_name")
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<PostOffice>(entity =>
            {
                entity.HasKey(e => e.AddressId);

                entity.ToTable("Post_Office");

                entity.Property(e => e.AddressId)
                    .HasMaxLength(10)
                    .HasColumnName("Address_Id")
                    .IsFixedLength(true);

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsFixedLength(true);

                entity.Property(e => e.Street)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsFixedLength(true);

                entity.Property(e => e.StreetNumber)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("street_number")
                    .IsFixedLength(true);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
