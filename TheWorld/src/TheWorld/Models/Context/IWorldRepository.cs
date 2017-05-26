using System.Collections.Generic;
using System.Threading.Tasks;
using TheWorld.Models.Types;

namespace TheWorld.Models.Context
{
    public interface IWorldRepository
    {
        IEnumerable<Trip> GetAllTrips();

        void AddTrip(Trip trip);
        Trip GetTripByName(string tripName);
        void AddStop(string tripName, Stop newStop);

        Task<bool> SaveChangesAsync();
        
    }
}