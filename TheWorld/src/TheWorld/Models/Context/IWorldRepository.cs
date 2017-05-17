using System.Collections.Generic;
using TheWorld.Models.Types;

namespace TheWorld.Models.Context
{
    public interface IWorldRepository
    {
        IEnumerable<Trip> GetAllTrips();
    }
}