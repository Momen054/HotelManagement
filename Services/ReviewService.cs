using AutoMapper;
using HotelManagement.DTOs.Review;
using HotelManagement.Models;
using HotelManagement.serviceInterfaces;
using HotelManagement.UnitOfWork;

namespace HotelManagement.Services
{
    public class ReviewService : IReview
    {
        private readonly IMapper _mapper;

        private readonly IUnitOfWork _unitOfWork;

        public ReviewService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<GetReview>> GetReview()
        {
            var review = await _unitOfWork.GenericRepository<Review>()
                .GetAllAsunc();

            if (!review.Any())
                throw new ArgumentNullException("Review is empty..!");

            return _mapper.Map<IEnumerable<GetReview>>(review);
        }


        public async Task<GetReview> GetReview(int id)
        {
            var review = await _unitOfWork.GenericRepository<Review>()
                .GetByIdAsync(p => p.Id == id) ??
                throw new ArgumentNullException("Review is empty..!");

            return _mapper.Map<GetReview>(review);
        }

        public async Task<IEnumerable<GetReview>> GetGuestReview(string id)
        {
            var review = await _unitOfWork.GenericRepository<Review>()
                .GetAllAsunc(p => p.AppUserId == id);

            if (!review.Any())
                throw new ArgumentNullException("Review is empty..!");

            return _mapper.Map<IEnumerable<GetReview>>(review);
        }

        public async Task PutReview(PutReview review)
        {
            if (review == null) throw new ArgumentNullException();

            var isExist = _unitOfWork.GenericRepository<Review>()
              .GetByIdAsync(p => p.Id == review.Id)
              ?? throw new ArgumentNullException(nameof(review));

            _unitOfWork.GenericRepository<Review>()
                .Put(_mapper.Map<Review>(review));
            await _unitOfWork.SaveChangesAsync();
        }


        public async Task PostReview(PostReview review)
        {
            if (review == null) throw new ArgumentException();

            await _unitOfWork
                .GenericRepository<Review>()
                .PostAsync(_mapper.Map<Review>(review));

            await _unitOfWork.SaveChangesAsync();

        }


        public async Task DeleteReview(int id)
        {
            _unitOfWork.GenericRepository<Review>()
                .Delete(id);
           
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteGuestReview(int id,string userId)
        {
            var guestReview = await _unitOfWork.GenericRepository<Review>()
                .GetByIdAsync(p => p.Id == id && p.AppUserId == userId);

            if (guestReview != null)
            {
                _unitOfWork.GenericRepository<Review>()
                .Delete(id);

                await _unitOfWork.SaveChangesAsync();
            }
        }
    }
}
