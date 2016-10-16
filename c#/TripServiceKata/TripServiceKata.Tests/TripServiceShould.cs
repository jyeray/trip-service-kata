using System;
using System.Collections.Generic;
using FluentAssertions;
using NSubstitute;
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
        private static readonly Trip.Trip ToGranCanaria = new Trip.Trip();
        private static readonly Trip.Trip ToMadrid = new Trip.Trip();
        private TripService tripService;

        [SetUp]
        public void SetUp()
        {
            tripService = new TripService(new TripDAO());
        }

        [Test]
        public void throw_NotLoggedInUserException_when_user_is_not_logged()
        {
            Action getTripsByUserAction = () => tripService.GetFriendTrips(AnotherUser, UnusedUser);

            getTripsByUserAction.ShouldThrow<UserNotLoggedInException>();
        }

        [Test]
        public void returns_an_empty_list_if_users_are_not_friends()
        {
            OtherUser.AddFriend(AnotherUser);
            OtherUser.AddTrip(ToGranCanaria);
            OtherUser.AddTrip(ToMadrid);

            var anotherUserTrips = tripService.GetFriendTrips(OtherUser, AnUser);

            anotherUserTrips.ShouldBeEquivalentTo(new List<Trip.Trip>());
        }

        [Test]
        public void returns_list_of_trips_if_users_are_friends()
        {
            var tripDAOStub = Substitute.For<TripDAO>();
            tripDAOStub.GetUserTrips(AnotherUser).Returns(AnotherUser.Trips());
            tripService = new TripService(tripDAOStub);
            AnotherUser.AddFriend(AnUser);
            AnotherUser.AddFriend(OtherUser);
            AnotherUser.AddTrip(ToGranCanaria);
            AnotherUser.AddTrip(ToMadrid);

            var anotherUserTrips = tripService.GetFriendTrips(AnotherUser, AnUser);

            anotherUserTrips.ShouldBeEquivalentTo(new List<Trip.Trip>
            {
                ToGranCanaria,
                ToMadrid
            });
        }
    }
}
