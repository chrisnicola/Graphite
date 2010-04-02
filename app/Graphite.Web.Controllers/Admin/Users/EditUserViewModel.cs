namespace Graphite.Web.Controllers.Admin.Users{
	public class EditUserViewModel : UserViewModel{
		public string NewPassword { get; set; }
		public string OldPassword { get; set; }
		public string VerifyPassword { get; set; }
	}
}