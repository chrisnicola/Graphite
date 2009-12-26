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
		private User _user;
		private IUserRepository _userRepository;
		private NHMembershipProvider _membershipProvider;

		[SetUp]
		public void SetUp() {
			var salt = NHMembershipProvider.GenerateSalt(16);
			_user = new User()
			        {Username = "Username", Salt = salt, Password = NHMembershipProvider.CreatePasswordHash(salt, "Password")};
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
			Assert.That(((NHMembershipUserWrapper) _membershipProvider.GetUser(_user.Username, false)).User == _user);
		}

		[Test]
		public void CanGetUsernameByEmailAddress() {
			Assert.That(_membershipProvider.GetUserNameByEmail(_user.Email) == _user.Username);
		}

		[Test]
		public void CanChangePasswordForUser() {
			Assert.That(_membershipProvider.ChangePassword(_user.Username, "PasswordWrong", "NewPassword"), Is.False);
			Assert.That(_membershipProvider.ChangePassword(_user.Username, "Password", "NewPassword"));
			Assert.That(_membershipProvider.ValidateUser(_user.Username, "NewPassword"));
		}
		[Test]
		public void CanValidateUserWithPassword() {
			Assert.That(_membershipProvider.ValidateUser(_user.Username, "Password"), Is.True);
			Assert.That(_membershipProvider.ValidateUser(_user.Username, "WrongPassword"), Is.False);
		}
	}
}
