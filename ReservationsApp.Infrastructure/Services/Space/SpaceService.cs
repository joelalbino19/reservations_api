using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mapster;
using Reservation.Application.Interfaces.Space;
using Reservation.Application.Models.Space;
using Reservation.Domain.Interfaces;

namespace Reservation.Application.Services.Space
{
    public class SpaceService : ISpaceService
    {
        private readonly ISpaceRepository _spaceRepository;

        public SpaceService(ISpaceRepository spaceRepository)
        {
            _spaceRepository = spaceRepository;
        }

        public async Task AddAsync(CreateSpaceDto space)
        {
            var newSpace = space.Adapt<Domain.Entities.Space>();

            await _spaceRepository.AddAsync(newSpace);
        }

        public async Task DeleteAsync(int id)
        {
            var space = await _spaceRepository.GetById(id) ?? throw new KeyNotFoundException("Space not fount");

            await _spaceRepository.DeleteAsync(space);
        }

        public async Task<List<SpaceDto>> GetAll()
        {
            var spaces =  await _spaceRepository.GetAll();

            return spaces.Adapt<List<SpaceDto>>();  
        }
    }
}
