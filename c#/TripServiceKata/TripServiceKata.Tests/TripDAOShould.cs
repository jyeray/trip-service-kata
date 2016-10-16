using System;
using FluentAssertions;
using NUnit.Framework;
using TripServiceKata.Exception;
using TripServiceKata.Trip;

namespace TripServiceKata.Tests
{
    [TestFixture]
    public class TripDAOShould
    {
        [Test]
        public void throw_DependendClassCallDuringUnitTestException_when_getting_users_trips()
        {
            var tripDao = new TripDAO();
            Action action = () => tripDao.GetUserTrips(new User.User());

            action.ShouldThrow<DependendClassCallDuringUnitTestException>();
        }
    }
}