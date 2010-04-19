<%@ Control Language="C#" Inherits="ModelViewUserControl<PostIndexViewModel>" %>
<%@ Import Namespace="Graphite.Web.Controllers.Posts" %>
<div>
  <table>
    <tr>
        <th></th>
        <th>
            Title
        </th>
                <th>
            Author
        </th>
        <th>
            Date Created
        </th>
        <th>
            Date Published
        </th>
        <th>
            Allow Comments
        </th>
        <th>
            Published
        </th>
    </tr>
    <%
      foreach (PostShowViewModel item in Model.Posts) {%>
    <tr>
        <td>
          <%=Html.ActionLink<PostController>(c => c.Show(item.Slug), "View")%>
          <%
        if (Model.IsAuthenticated) {%> 
            | <%=Html.ActionLink<PostController>(x => x.Edit(item.Id), "Edit")%> | <%=Html.ActionLink<PostController>(x => x.Delete(item.Id), "Delete")%>
          <%
        }%>
        </td>
        <td>
            <%=item.Title%>
        </td>
        <td>
            <%=item.AuthorRealName%>
        </td>
        <td>
            <%=string.Format("{0:ddMMyyyy}", item.DateCreated)%>
        </td>
        <td>
            <%=string.Format("{0:ddMMyyyy}>", item.DatePublished)%>
        </td>
        <td>
            <%=Html.Encode(item.AllowComments)%>
        </td>
        <td>
            <%=Html.Encode(item.Published)%>
        </td>
    </tr>
    <%
      }%>
</table>
<%
      if (Model.IsAuthenticated) {%>
  <p>
    <%=Html.ActionLink<PostController>(x => x.New(new PostNewModel()), "Create New Post")%>
  </p>
<%
      }%>
</div>
