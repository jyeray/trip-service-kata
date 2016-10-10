using System.Collections.Generic;
using TripServiceKata.Exception;
using TripServiceKata.User;

namespace TripServiceKata.Trip
{
    public class TripService
    {
        public List<Trip> GetTripsByUser(User.User user)
        {
            CheckUserIsLoggedIn();
            return user.HasFriend(GetLoggedUser()) ? 
                FindTripsByUser(user) : new List<Trip>();
        }

        private void CheckUserIsLoggedIn()
        {
            if (GetLoggedUser() == null) throw new UserNotLoggedInException();
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
