using AutoMapper;
using HotelManagement.DTOs.Invoice;
using HotelManagement.DTOs.Payment;
using HotelManagement.DTOs.Reservation;
using HotelManagement.DTOs.ReservationRooms;
using HotelManagement.DTOs.ReservationServices;
using HotelManagement.DTOs.Review;
using HotelManagement.DTOs.Role;
using HotelManagement.DTOs.Room;
using HotelManagement.DTOs.RoomType;
using HotelManagement.DTOs.Service;
using HotelManagement.DTOs.User;
using HotelManagement.Models;
using Microsoft.AspNetCore.Identity;

namespace HotelManagement.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {

            CreateMap<Invoice, GetInvoice>();
            CreateMap<PostInvoice, Invoice>()
                .AfterMap((src, dest) => dest.Isdeleted = false);
            CreateMap<PutInvoice, Invoice>()
                .AfterMap((src, dest) => dest.Isdeleted = false);
          


            CreateMap<Payment, GetPayment>();
            CreateMap<PostPayment, Payment>()
                .AfterMap((src, dest) => dest.Isdeleted = false);
            CreateMap<PutPayment, Payment>()
                .AfterMap((src, dest) => dest.Isdeleted = false);
                

            CreateMap<Reservation, GetReservation>();
            CreateMap<PostReservation, Reservation>();
            CreateMap<PutReservation, Reservation>();

            CreateMap<Review, GetReview>();
            CreateMap<PostReview, Review>();
            CreateMap<PutReview, Review>();

            CreateMap<Room, GetRoom>();
            CreateMap<PostRoom, Room>();
            CreateMap<PutRoom, Room>();

            CreateMap<RoomType, GetRoomType>();
            CreateMap<PostRoomType, RoomType>();
            CreateMap<PutRoomType, RoomType>();

            CreateMap<Service, GetService>();
            CreateMap<PostService, Service>();
            CreateMap<PutService, Service>();


            CreateMap<PostResRom, ReservationRoom>();
            CreateMap<PutResRom, ReservationRoom>();
            CreateMap<DeleteResRom, ReservationRoom>();
            CreateMap<ReservationRoom, GetResRom>();


            CreateMap<PostResSer, ReservationService>();
            CreateMap<PutResSer, ReservationService>();
            CreateMap<DeleteResSer, ReservationService>();
            CreateMap<ReservationService, GetResSer>();

            CreateMap<IdentityRole, GetRole>();
            CreateMap<PostRole, IdentityRole>();

            CreateMap<AppUser, GetUser>();
        }
    }
}
