using AutoMapper;
using HotelManagement.DTOs.Service;
using HotelManagement.Models;
using HotelManagement.serviceInterfaces;
using HotelManagement.UnitOfWork;

namespace HotelManagement.Services
{
    public class Service_ser : IService
    {
        private readonly IMapper _mapper;

        private readonly IUnitOfWork _unitOfWork;

        public Service_ser(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<GetService>> GetService()
        {
            var service = await _unitOfWork.GenericRepository<Service>()
                .GetAllAsunc();

            if (!service.Any())
                throw new ArgumentNullException("Service is empty..!");

            return _mapper.Map<IEnumerable<GetService>>(service);
        }


        public async Task<GetService> GetService(int id)
        {
            var service = await _unitOfWork.GenericRepository<Service>()
                .GetByIdAsync(p => p.Id == id) ??
                throw new ArgumentNullException("Service is empty..!");

            return _mapper.Map<GetService>(service);
        }

        public async Task PutService(PutService service)
        {
            if (service == null) throw new ArgumentNullException();

            var isExist = _unitOfWork.GenericRepository<Service>()
              .GetByIdAsync(p => p.Id == service.Id)
              ?? throw new ArgumentNullException(nameof(service));

            _unitOfWork.GenericRepository<Service>()
                .Put(_mapper.Map<Service>(service));
            await _unitOfWork.SaveChangesAsync();
        }


        public async Task PostService(PostService service)
        {
            if (service == null) throw new ArgumentException();

            await _unitOfWork
                .GenericRepository<Service>()
                .PostAsync(_mapper.Map<Service>(service));

            await _unitOfWork.SaveChangesAsync();

        }


        public async Task DeleteService(int id)
        {
            _unitOfWork.GenericRepository<Service>()
                .Delete(id);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
