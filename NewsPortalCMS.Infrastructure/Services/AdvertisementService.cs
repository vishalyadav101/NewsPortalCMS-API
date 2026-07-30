using AutoMapper;
using NewsPortalCMS.Application.DTOs.Advertisement;
using NewsPortalCMS.Application.Interfaces.Repositories;
using NewsPortalCMS.Application.Interfaces.Services;
using NewsPortalCMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NewsPortalCMS.Application.Services
{
    public class AdvertisementService : IAdvertisementService
    {
        private readonly IAdvertisementRepository _repository;
        private readonly IMapper _mapper;

        public AdvertisementService(
            IAdvertisementRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        public async Task<AdvertisementResponseDto> CreateAsync(
            CreateAdvertisementDto dto)
        {
            var advertisement = _mapper.Map<Advertisement>(dto);

            advertisement.Id = Guid.NewGuid();
            advertisement.CreatedDate = DateTime.UtcNow;

            await _repository.AddAsync(advertisement);

            return _mapper.Map<AdvertisementResponseDto>(advertisement);
        }


        public async Task<IEnumerable<AdvertisementResponseDto>> GetAllAsync()
        {
            var advertisements = await _repository.GetAllAsync();

            return _mapper.Map<IEnumerable<AdvertisementResponseDto>>(advertisements);
        }


        public async Task<AdvertisementResponseDto?> GetByIdAsync(Guid id)
        {
            var advertisement = await _repository.GetByIdAsync(id);

            if (advertisement == null)
                return null;

            return _mapper.Map<AdvertisementResponseDto>(advertisement);
        }


        public async Task<bool> UpdateAsync(
            Guid id,
            UpdateAdvertisementDto dto)
        {
            var advertisement = await _repository.GetByIdAsync(id);

            if (advertisement == null)
                return false;

            _mapper.Map(dto, advertisement);

            advertisement.UpdatedDate = DateTime.UtcNow;

            await _repository.UpdateAsync(advertisement);

            return true;
        }


        public async Task<bool> DeleteAsync(Guid id)
        {
            var advertisement = await _repository.GetByIdAsync(id);

            if (advertisement == null)
                return false;

            await _repository.DeleteAsync(advertisement);

            return true;
        }
    }
}