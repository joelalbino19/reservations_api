using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reservation.Application.Models.Space;

namespace Reservation.Application.Interfaces.Space
{
    public interface ISpaceService
    {
        Task<List<SpaceDto>> GetAll();
        Task AddAsync(CreateSpaceDto space);
        Task DeleteAsync(int id);
    }
}
