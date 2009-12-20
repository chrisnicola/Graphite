using System;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Text;
using System.Web.Security;
using Graphite.Core;
using Graphite.Data.Repositories;
using SharpArch.Web.NHibernate;

namespace Graphite.ApplicationServices {
	public class NHMembershipProvider : MembershipProvider {
		private IUserRepository _repository;
		public NHMembershipProvider() { _repository = new UserRepository(); }

		public override bool EnablePasswordRetrieval { get { throw new NotImplementedException(); } }
		public override bool EnablePasswordReset { get { throw new NotImplementedException(); } }
		public override bool RequiresQuestionAndAnswer { get { throw new NotImplementedException(); } }
		public override string ApplicationName { get { throw new NotImplementedException(); } set { throw new NotImplementedException(); } }
		public override int MaxInvalidPasswordAttempts { get { throw new NotImplementedException(); } }
		public override int PasswordAttemptWindow { get { throw new NotImplementedException(); } }
		public override bool RequiresUniqueEmail { get { throw new NotImplementedException(); } }
		public override MembershipPasswordFormat PasswordFormat { get { throw new NotImplementedException(); } }
		public override int MinRequiredPasswordLength { get { throw new NotImplementedException(); } }
		public override int MinRequiredNonAlphanumericCharacters { get { throw new NotImplementedException(); } }
		public override string PasswordStrengthRegularExpression { get { throw new NotImplementedException(); } }
		public void SetUserRepositoryForTesting(IUserRepository repository) { _repository = repository; }

		public override void Initialize(string name, NameValueCollection config) {
			if (String.IsNullOrEmpty(name)) name = "NHMembershipProvider";
			base.Initialize(name, null);
		}

		public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion,
			string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status) {
			var salt = GenerateSalt(32);
			var user = new User {Username = username, 
				Password = CreatePasswordHash(salt, password), 
				Salt = salt,
				Email = email,
				PasswordQuestion = passwordQuestion,
				PasswordAnswer = passwordAnswer,
				IsApproved = true
			};
			try {
				SaveUser(user);
			} catch (Exception ex) {
				throw new MembershipCreateUserException("Unable to save user", ex);
			}
			status = MembershipCreateStatus.Success;
			return new NHMembershipUserWrapper(user, Name);
		}

		private static string GenerateSalt(int size)
		{
			var rng = new RNGCryptoServiceProvider();
			var buff = new byte[size];
			rng.GetBytes(buff);
			return Convert.ToBase64String(buff);
		}

		private static string CreatePasswordHash(string salt, string password) {
			string saltAndPwd = String.Concat(password, salt);
			byte[] data = Encoding.UTF8.GetBytes(saltAndPwd);
			using (HashAlgorithm sha = new SHA256Managed())
			{
				sha.TransformFinalBlock(data, 0, data.Length);
				return Convert.ToBase64String(sha.Hash);
			}
		}

		[Transaction]
		private void SaveUser(User user) { _repository.Save(user); }

		public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion,
			string newPasswordAnswer) { throw new NotImplementedException(); }

		public override string GetPassword(string username, string answer) { throw new NotImplementedException(); }
		public override bool ChangePassword(string username, string oldPassword, string newPassword) { throw new NotImplementedException(); }
		public override string ResetPassword(string username, string answer) { throw new NotImplementedException(); }
		public override void UpdateUser(MembershipUser user) { throw new NotImplementedException(); }
		public override bool ValidateUser(string username, string password) { throw new NotImplementedException(); }
		public override bool UnlockUser(string userName) { throw new NotImplementedException(); }
		public override MembershipUser GetUser(object providerUserKey, bool userIsOnline) { throw new NotImplementedException(); }
		public override MembershipUser GetUser(string username, bool userIsOnline) {
			return new NHMembershipUserWrapper(_repository.GetUser(username), Name);
		}
		
		public override string GetUserNameByEmail(string email) {
			var user = _repository.GetUserByEmail(email);
			return user.Username;
		}

		public override bool DeleteUser(string username, bool deleteAllRelatedData) {
			try {
				return DeleteUserFromDatabase(username);
			} catch {
				return false;
			}
		}

		[Transaction]
		private bool DeleteUserFromDatabase(string username) {
			User user = _repository.GetUser(username);
			if (user == null) return false;
			_repository.Delete(user);
			return true;
		}

		public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords) { throw new NotImplementedException(); }
		public override int GetNumberOfUsersOnline() { throw new NotImplementedException(); }

		public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize,
			out int totalRecords) { throw new NotImplementedException(); }

		public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize,
			out int totalRecords) { throw new NotImplementedException(); }
	}

	public class NHMembershipUserWrapper : MembershipUser {
		private readonly User _user;
		public NHMembershipUserWrapper() { }

		public NHMembershipUserWrapper(User user, string providerName)
			: base(
				providerName, user.Username, user.Id, user.Email, user.PasswordQuestion, user.PasswordAnswer, user.IsApproved,
				user.IsLockedOut, user.CreationDate, user.LastLoginDate, user.LastActivityDate, user.LastPasswordChangedDate,
				user.LastLockout) { _user = user; }

		public override string UserName { get { return _user.Username; } }
		public override object ProviderUserKey { get { return _user.Id; } }
		public override string Email { get { return _user.Email; } set { _user.Email = value; } }
		public override string PasswordQuestion { get { return _user.PasswordQuestion; } }
		public override bool IsApproved { get { return _user.IsApproved; } set { _user.IsApproved = value; } }
		public override bool IsLockedOut { get { return _user.IsLockedOut; } }
		public override DateTime CreationDate { get { return _user.CreationDate; } }
		public override DateTime LastLoginDate { get { return _user.LastLoginDate; } set { _user.LastLoginDate = value; } }
		public User User { get { return _user; } }
	}
}