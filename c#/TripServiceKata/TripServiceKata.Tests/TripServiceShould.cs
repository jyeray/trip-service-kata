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
        private const User.User UnusedUser = null;
        private static User.User loggedInUser;

        [Test]
        public void throw_NotLoggedInUserException_when_user_is_not_logged()
        {
            var tripService = new TesteableTripService();
            loggedInUser = UnusedUser;

            Action getTripsByUserAction = () => tripService.GetTripsByUser(AnotherUser);

            getTripsByUserAction.ShouldThrow<UserNotLoggedInException>();
        }

        [Test]
        public void returns_an_empty_list_if_users_are_not_friends()
        {   
            var tripService = new TesteableTripService();
            loggedInUser = AnUser;

            var anotherUserTrips = tripService.GetTripsByUser(AnotherUser);

            anotherUserTrips.ShouldBeEquivalentTo(new List<Trip.Trip>());
        }

        public class TesteableTripService : TripService
        {
            protected override User.User GetLoggedUser()
            {
                return loggedInUser;
            }
        }
    }

    
}
