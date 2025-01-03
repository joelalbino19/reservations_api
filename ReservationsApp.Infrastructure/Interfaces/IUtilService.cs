using Reservation.Application.Models.Util;

namespace Reservation.Application.Interfaces
{
    public interface IUtilService
    {
        Task<List<UserSelectDto>> GetUsers();
        Task<List<SpaceSelectDto>> GetSpaces();
    }
}
