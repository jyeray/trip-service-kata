using System.Collections.Generic;
using TripServiceKata.Exception;

namespace TripServiceKata.Trip
{
    public class TripService
    {
        private readonly TripDAO tripDao;

        public TripService(TripDAO tripDAO)
        {
            tripDao = tripDAO;
        }

        public List<Trip> GetFriendTrips(User.User friend, User.User loggedInUser)
        {
            if (loggedInUser == null) throw new UserNotLoggedInException();
            return friend.HasFriend(loggedInUser) ? 
                tripDao.GetTripsOf(friend) : new List<Trip>();
        }
    }
}
