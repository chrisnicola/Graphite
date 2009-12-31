using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using System.Web.Security;
using Graphite.Core;
using Graphite.Data.Repositories;

namespace Graphite.ApplicationServices
{
	public interface IUserTasks {
		IEnumerable<User> GetUsers();
		User AddUser(CreateUserDetails user);
		User UpdateUser(EditUserDetails user);
		User AuthenticateUser(string username, string password);
		User GetUser(Guid id);
		void RemoveUser(Guid id);
	}

	public class UserTasks : IUserTasks {
		private readonly IUserRepository _users;
		public UserTasks(IUserRepository users) { _users = users; }
		
		public IEnumerable<User> GetUsers() {
			return _users.FindAll();
		}

		public User AddUser(CreateUserDetails user) {
			var newuser = new User {
				Username = user.Username,
				CreationDate = DateTime.Now,
				Email = user.Email,
				RealName = user.RealName,
				Salt = GenerateSalt(16)
			};
			newuser.Password = CreatePasswordHash(newuser.Salt, user.Password);
			return _users.Save(newuser);
		}

		public User UpdateUser(EditUserDetails details) {
			var user = _users.Get(details.Id);
			user.Username = details.Username;
			user.Email = details.Email;
			user.RealName = details.RealName;
			if (!string.IsNullOrEmpty(details.NewPassword)) user.Password = CreatePasswordHash(user.Salt, details.NewPassword);
			return user;
		}

		public User AuthenticateUser(string username, string password) {
			try {
				var user = _users.GetUser(username);
				if (user == null) throw new AuthenticationException("No such username");
				if (ValidPasswordForUser(_users.GetUser(username), password)) {
					FormsAuthentication.SetAuthCookie(username, false);
					return user;
				} else throw new AuthenticationException("Unable to validate username or password");
			} catch (Exception ex) { throw new AuthenticationException("An unkown error occurred", ex); }	
		}

		public User GetUser(Guid id) {
			return _users.Get(id);
		}

		public void RemoveUser(Guid id) {
			_users.Delete(id);
		}

		public static string GenerateSalt(int size)
		{
			var rng = new RNGCryptoServiceProvider();
			var buff = new byte[size];
			rng.GetBytes(buff);
			return Convert.ToBase64String(buff);
		}

		public static string CreatePasswordHash(string salt, string password)
		{
			string saltAndPwd = String.Concat(password, salt);
			byte[] data = Encoding.UTF8.GetBytes(saltAndPwd);
			using (HashAlgorithm sha = new SHA256Managed())
			{
				sha.TransformFinalBlock(data, 0, data.Length);
				return Convert.ToBase64String(sha.Hash);
			}
		}

		private static bool ValidPasswordForUser(User user, string password) { return CreatePasswordHash(user.Salt, password).Equals(user.Password); }
	}
}
