using System.Collections.Generic;
using TripServiceKata.Exception;
using TripServiceKata.User;

namespace TripServiceKata.Trip
{
    public class TripService
    {
        public List<Trip> GetTripsByUser(User.User user)
        {
            if (GetLoggedUser() != null)
            {
                foreach(User.User friend in user.GetFriends())
                {
                    if (friend.Equals(GetLoggedUser()))
                    {
                        return FindTripsByUser(user);
                    }
                }
                return new List<Trip>();
            }
            throw new UserNotLoggedInException();
        }

        protected virtual List<Trip> FindTripsByUser(User.User user)
        {
            return TripDAO.FindTripsByUser(user);
        }

        protected virtual User.User GetLoggedUser()
        {
            return UserSession.GetInstance().GetLoggedUser();
        }
    }
}
