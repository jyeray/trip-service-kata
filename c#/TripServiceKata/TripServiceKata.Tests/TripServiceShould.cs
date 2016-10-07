using System;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using TripServiceKata.Exception;
using TripServiceKata.Trip;
using TripServiceKata.User;

namespace TripServiceKata.Tests
{
    [TestFixture]
    public class TripServiceShould
    {
        [Test]
        public void throw_NotLoggedInUserException_when_user_is_not_logged()
        {
            var tripService = new TesteableTripService();

            Action getTripsByUserAction = () => tripService.GetTripsByUser(null);

            getTripsByUserAction.ShouldThrow<UserNotLoggedInException>();
        }
    }

    public class TesteableTripService : TripService
    {
        protected override User.User GetLoggedUser()
        {
            return null;
        }
    }
}
