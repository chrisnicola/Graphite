<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewPage" %>

<%= Html.BeginForm<BlogMLController>(x => x.Import(), FormMethod.Post, new {enctype= "multipart/form-data"})) %>
   BlogML File: <input type="file" name="blogml" id="blogml" /> <input type="submit" value="Import" />
<%= Html.EndForm() %>