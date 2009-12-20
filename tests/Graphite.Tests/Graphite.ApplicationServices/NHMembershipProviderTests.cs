using System.Linq.Expressions;
using System.Web.Security;
using Graphite.ApplicationServices;
using Graphite.Core;
using Graphite.Data.Repositories;
using NUnit.Framework;
using Rhino.Mocks;

namespace Tests.Graphite.ApplicationServices
{
	[TestFixture]
	public class NHMembershipProviderTests
	{
		private readonly User _user = new User { Username = "username" };
		private IUserRepository _userRepository;
		private NHMembershipProvider _membershipProvider;

		[SetUp]
		public void SetUp() {
			_userRepository = MockRepository.GenerateStub<IUserRepository>();
			_userRepository.Stub(m => m.GetUser(_user.Username)).Return(_user);
			_membershipProvider = ((NHMembershipProvider) Membership.Provider);
			_membershipProvider.SetUserRepositoryForTesting(_userRepository);
		}

		[Test]
		public void CanCreateNewUserAndSaveToDatabase() {
			MembershipCreateStatus status;
			var user = _membershipProvider.CreateUser("Username", "Password", "email@email.com", "Something", "Answer", true, null,
				out status);
			_userRepository.AssertWasCalled(m => m.Save(Arg<User>.Matches(
				u => u.Username.Equals("Username") && 
					u.Email.Equals("email@email.com") && 
					u.PasswordQuestion.Equals("Something") && 
					u.PasswordAnswer.Equals("Answer") &&
					u.IsApproved
				)));
		}

		[Test]
		public void CanDeleteUser() {			
			_membershipProvider.DeleteUser(_user.Username, true);
			_userRepository.AssertWasCalled(m => m.Delete(Arg<User>.Matches(u => u.Username.Equals("Username"))));
		}

		[Test]
		public void CanGetUser() {
			Assert.That((_membershipProvider.GetUser(_user.Username, false) as NHMembershipUserWrapper).User == _user);
		}

		[Test]
		public void CanGetUserByEmailAddress() {
			Assert.That(_membershipProvider.GetUserNameByEmail(_user.Email) == _user.Username);
		}
	}
}
