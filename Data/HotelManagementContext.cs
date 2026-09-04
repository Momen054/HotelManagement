using HotelManagement.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace HotelManagement.Data
{
    public class HotelManagementContext : IdentityDbContext<AppUser>
    {
        public HotelManagementContext()
        {
        }
        public HotelManagementContext(DbContextOptions<HotelManagementContext> options)
        : base(options)
        {
        }

        public DbSet<AppUser> appUser { get; set; }

        public DbSet<Invoice> Invoices { get; set; }

        public  DbSet<Payment> Payments { get; set; }

        public  DbSet<Reservation> Reservations { get; set; }

        public DbSet <ReservationRoom> ReservationRooms { get; set; }

        public  DbSet<ReservationService> ReservationServices { get; set; }

        public  DbSet<Review> Reviews { get; set; }

        public  DbSet<Room> Rooms { get; set; }

        public  DbSet<RoomType> RoomTypes { get; set; }

        public  DbSet<Service> Services { get; set; }

        public DbSet<RefreshToken> refreshTokens { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Invoice>(entity =>
            {

                entity.HasOne(e=>e.Reservation).WithOne(e=>e.Invoices)
                .HasForeignKey<Invoice>(s=>s.ReservationId)
                .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Payment).WithOne(e => e.Invoice)
                .HasForeignKey<Payment>(e => e.InvoiceId)
                .OnDelete(DeleteBehavior.Cascade);

                entity.Property(p=>p.SubTotal).HasColumnType("decimal(10,2)");

            });

            modelBuilder.Entity<Payment>(entity =>
            {

                entity.Property(p=>p.Amount).HasColumnType("decimal(10,2)");

            });

            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.HasOne(p => p.AppUser).WithMany(p=>p.Reservations)
                .HasForeignKey(p=>p.AppUserId)
                .OnDelete(DeleteBehavior.Cascade);
                entity.Property(p=>p.CheckIn).HasColumnType("datetime");
                entity.Property(p => p.CheckOut).HasColumnType("datetime");

            });

            modelBuilder.Entity<ReservationRoom>(entity =>
            {
                entity.HasKey(k => new{ k.RoomId, k.ReservationId});
                
                entity.HasOne(e => e.Reservation).WithMany(e => e.ReservationRooms)
                .HasForeignKey(k => k.ReservationId).
                OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Room).WithMany(e => e.ReservationRooms)
                .HasForeignKey(k => k.RoomId)
                .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<ReservationService>(entity =>
            {
                entity.HasKey(k => new { k.ServiceId, k.ReservationId });

                entity.HasOne(e => e.Reservation).WithMany(e => e.ReservationServices)
                .HasForeignKey(k => k.ReservationId).
                OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Service).WithMany(e => e.ReservationServices)
                .HasForeignKey(k => k.ServiceId)
                .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Review>(entity =>
            {

                entity.HasOne(e => e.Reservation).WithOne(e => e.Review)
                .HasForeignKey<Review>(k => k.ReservationId)
                .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(e => e.AppUser).WithOne(e => e.Review)
                .HasForeignKey<Review>(k=>k.AppUserId)
                .OnDelete(DeleteBehavior.NoAction);

                entity.Property(p => p.Comment).HasMaxLength(200);

            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.HasOne(e => e.RoomType).WithMany(e => e.Rooms)
                .HasForeignKey(k => k.RoomTypeId)
                .OnDelete(DeleteBehavior.Cascade);

            });

            modelBuilder.Entity<RoomType>(entity =>
            {
                entity.Property(p => p.PricePerNight).HasColumnType("decimal(8,2)");
            });
        }
    }
}
