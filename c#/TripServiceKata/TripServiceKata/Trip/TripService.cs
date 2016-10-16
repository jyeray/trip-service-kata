using System.Collections.Generic;
using TripServiceKata.Exception;
using TripServiceKata.User;

namespace TripServiceKata.Trip
{
    public class TripService
    {
        public TripService(TripDAO tripDAO)
        {
            
        }

        public List<Trip> GetFriendTrips(User.User friend, User.User loggedInUser)
        {
            if (loggedInUser == null) throw new UserNotLoggedInException();
            return friend.HasFriend(loggedInUser) ? 
                FindTripsByUser(friend) : new List<Trip>();
        }

        protected virtual List<Trip> FindTripsByUser(User.User user)
        {
            return TripDAO.FindTripsByUser(user);
        }
    }
}
