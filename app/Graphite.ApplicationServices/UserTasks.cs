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
		void AddUser(User user);
		void UpdateUser(User user);
		void AuthenticateUser(string username, string password);
		User GetUser(Guid id);
	}

	public class UserTasks : IUserTasks {
		private readonly IUserRepository _users;
		public UserTasks(IUserRepository users) { _users = users; }
		
		public IEnumerable<User> GetUsers() {
			return _users.FindAll();
		}

		public void AddUser(User user) {
			user.Salt = GenerateSalt(16);
			user.Password = CreatePasswordHash(user.Salt, user.Password);
			_users.Save(user);
		}

		public void UpdateUser(User user) {
			_users.SaveOrUpdate(user);
		}

		public void AuthenticateUser(string username, string password) {
			try {
				var user = _users.GetUser(username);
				if (user == null) throw new AuthenticationException("No such username");
				if (ValidPasswordForUser(_users.GetUser(username), password)) FormsAuthentication.SetAuthCookie(username, false);
				else throw new AuthenticationException("Unable to validate username or password");
			} catch (Exception ex) { throw new AuthenticationException("An unkown error occurred", ex); }	
		}

		public User GetUser(Guid id) {
			return _users.Get(id);
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
