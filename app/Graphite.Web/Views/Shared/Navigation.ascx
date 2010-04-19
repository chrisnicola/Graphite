<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="Graphite.Web.Controllers.Home" %>
<%@ Import Namespace="Graphite.Web.Controllers.Login" %>
<%@ Import Namespace="Graphite.Web.Controllers.Syndication" %>
<%@ Import Namespace="Graphite.Web.Controllers.Posts" %>
<p>
  <%= Html.Image("~/Content/Images/graphitelogo.jpg")%>
</p>
<h2>Naviation Menu</h2>
<ul>
  <li><%= Html.ActionLink<HomeController>(c => c.Index(), "Home")%></li>
  <li><%= Html.ActionLink<LoginController>(c => c.Show(), "Login")%></li>
  <li><%= Html.ActionLink<PostController>(c => c.Index(), "Posts")%></li>
  <li><%= Html.ActionLink<FeedController>(c => c.Rss(), "RSS Feed")%></li>
  <li><%= Html.ActionLink<FeedController>(c => c.Atom(), "Atom Feed")%></li>
</ul>