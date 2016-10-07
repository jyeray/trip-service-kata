using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions;
using NUnit.Framework.Internal;
using NUnit.Framework;

namespace TripServiceKata.Tests
{
    [TestFixture]
    public class UserShould
    {
        private static readonly User.User AFriend = new User.User();
        private static readonly User.User NotAFriend = new User.User();
        private static readonly User.User OtherFriend = new User.User();
        private User.User user;

        [SetUp]
        public void SetUp()
        {
            user = new User.User();
            user.AddFriend(AFriend);
            user.AddFriend(OtherFriend);
        }

        [Test]
        public void returns_true_if_other_user_is_friend()
        {
            var isFriend = user.hasFriend(AFriend);

            isFriend.Should().BeTrue();
        }

        [Test]
        public void returns_false_if_other_user_is_not_a_friend()
        {
            var isFriend = user.hasFriend(NotAFriend);

            isFriend.Should().BeFalse();
        }
    }
}
