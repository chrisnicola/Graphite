namespace Graphite.Web.Controllers.ViewModels {
	public class NewUserViewModel : UserViewModel {
		public string Password { get; set; }
		public string VerifyPassword { get; set; }
	}
}