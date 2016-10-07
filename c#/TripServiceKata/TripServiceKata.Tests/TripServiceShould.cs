using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using TripServiceKata.Exception;
using TripServiceKata.Trip;

namespace TripServiceKata.Tests
{
    [TestFixture]
    public class TripServiceShould
    {
        private static readonly User.User AnUser = new User.User();
        private static readonly User.User AnotherUser = new User.User();
        private static readonly User.User OtherUser = new User.User();
        private const User.User UnusedUser = null;
        private static User.User loggedInUser;
        private static readonly Trip.Trip ToGranCanaria = new Trip.Trip();
        private static readonly Trip.Trip ToMadrid = new Trip.Trip();
        private static readonly TesteableTripService TripService = new TesteableTripService();

        [SetUp]
        public void SetUp()
        {
            
        }

        [Test]
        public void throw_NotLoggedInUserException_when_user_is_not_logged()
        {
            loggedInUser = UnusedUser;

            Action getTripsByUserAction = () => TripService.GetTripsByUser(AnotherUser);

            getTripsByUserAction.ShouldThrow<UserNotLoggedInException>();
        }

        [Test]
        public void returns_an_empty_list_if_users_are_not_friends()
        {   
            loggedInUser = AnUser;
            OtherUser.AddFriend(AnotherUser);
            OtherUser.AddTrip(ToGranCanaria);
            OtherUser.AddTrip(ToMadrid);

            var anotherUserTrips = TripService.GetTripsByUser(OtherUser);

            anotherUserTrips.ShouldBeEquivalentTo(new List<Trip.Trip>());
        }

        [Test]
        public void returns_list_of_trips_if_users_are_friends()
        {
            loggedInUser = AnUser;
            AnotherUser.AddFriend(AnUser);
            AnotherUser.AddFriend(OtherUser);
            AnotherUser.AddTrip(ToGranCanaria);
            AnotherUser.AddTrip(ToMadrid);

            var anotherUserTrips = TripService.GetTripsByUser(AnotherUser);

            anotherUserTrips.ShouldBeEquivalentTo(new List<Trip.Trip>
            {
                ToGranCanaria,
                ToMadrid
            });
        }

        public class TesteableTripService : TripService
        {
            protected override User.User GetLoggedUser()
            {
                return loggedInUser;
            }

            protected override List<Trip.Trip> FindTripsByUser(User.User user)
            {
                return user.Trips();
            }
        }
    }

    
}
