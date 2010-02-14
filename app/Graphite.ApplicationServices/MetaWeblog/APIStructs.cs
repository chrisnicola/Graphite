using System;
using System.IO;
using System.Runtime.InteropServices;
using CookComputing.XmlRpc;
using System.Drawing;
using System.Drawing.Imaging;

namespace Graphite.ApplicationServices.MetaWeblog{
		[StructLayout(LayoutKind.Sequential), XmlRpcMissingMapping(MappingAction.Ignore)]
		public struct Author
		{
			public string user_id;
			public string user_login;
			public string display_name;
		}

		[StructLayout(LayoutKind.Sequential), XmlRpcMissingMapping(MappingAction.Ignore)]
		public struct CategoryInfo
		{
			public string categoryId;
			public string parentId;
			public string description;
			public string categoryName;
			public string title;
			public string htmlUrl;
			public string rssUrl;
		}

		[StructLayout(LayoutKind.Sequential), XmlRpcMissingMapping(MappingAction.Ignore)]
		public struct Comment
		{
			public DateTime date_created_gmt;
			public string user_id;
			public string comment_id;
			public string comment_parent;
			public string status;
			public string content;
			public string link;
			public string post_id;
			public string post_title;
			public string author;
			public string author_url;
			public string author_email;
			public string author_ip;
			public string type;
		}

		[StructLayout(LayoutKind.Sequential), XmlRpcMissingMapping(MappingAction.Ignore)]
		public struct CommentCount
		{
			public string approved;
			public int awaiting_moderation;
			public int spam;
			public int total_comments;
		}

		[StructLayout(LayoutKind.Sequential), XmlRpcMissingMapping(MappingAction.Ignore)]
		public struct CommentFilter
		{
			public string status;
			public string post_id;
			public int number;
			public int offset;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct CustomField
		{
			public string id;
			public string key;
			public string value;
		}

		[StructLayout(LayoutKind.Sequential), XmlRpcMissingMapping(MappingAction.Ignore)]
		public struct Data
		{
			public string name;
			public string type;
			public string base64;
			public bool overwrite;
		}

		[StructLayout(LayoutKind.Sequential), XmlRpcMissingMapping(MappingAction.Ignore)]
		public struct File
		{
			public string file;
			public string url;
			public string type;
		}

		[StructLayout(LayoutKind.Sequential), XmlRpcMissingMapping(MappingAction.Ignore)]
		public struct FileData
		{
			public string name;
			public string type;
			public byte[] bits;
		}

		[StructLayout(LayoutKind.Sequential), XmlRpcMissingMapping(MappingAction.Ignore)]
		public struct MediaObjectInfo
		{
			public string url;
		}

		[StructLayout(LayoutKind.Sequential), XmlRpcMissingMapping(MappingAction.Ignore)]
		public struct Option
		{
			public string option;
			public string value;
		}

		[StructLayout(LayoutKind.Sequential), XmlRpcMissingMapping(MappingAction.Ignore)]
		public struct Page
		{
			public DateTime dateCreated;
			public string userid;
			public string page_id;
			public string page_status;
			public string description;
			public string title;
			public string link;
			public string permaLink;
			public string[] categories;
			public string excerpt;
			public string text_more;
			public int mt_allow_comments;
			public int mt_allow_pings;
			public string wp_slug;
			public string wp_password;
			public string wp_author;
			public object wp_page_parent_id;
			public string wp_page_parent_title;
			public string wp_page_order;
			public string wp_author_id;
			public string wp_author_display_name;
			public DateTime date_created_gmt;
			public CustomField[] custom_fields;
			public string wp_page_template;
			public byte[] bits;
			public override string ToString()
			{
				return this.title;
			}
		}

		[StructLayout(LayoutKind.Sequential), XmlRpcMissingMapping(MappingAction.Ignore)]
		public struct PageMin
		{
			public DateTime dateCreated;
			public string page_id;
			public string page_title;
			public object page_parent_id;
		}

		[StructLayout(LayoutKind.Sequential), XmlRpcMissingMapping(MappingAction.Ignore)]
		public struct PageTemplate
		{
			public string name;
			public string description;
			public override string ToString()
			{
				return name;
			}
		}

		[StructLayout(LayoutKind.Sequential), XmlRpcMissingMapping(MappingAction.Ignore)]
		public struct PostInfo
		{
			public DateTime dateCreated;
			public string description;
			public string title;
			public string postid;
			public string[] categories;
			public string mt_text_more;
			public string text_more;
			public string permalink;
			public string userid;
			public string wp_slug;
			public string mt_keywords;
			public bool mt_allow_comments;
			public string mt_excerpt;

			public override string ToString()
			{
				return description;
			}
		}

		[StructLayout(LayoutKind.Sequential), XmlRpcMissingMapping(MappingAction.Ignore)]
		public struct PostStatusList
		{
			public string Status;
		}

		[StructLayout(LayoutKind.Sequential), XmlRpcMissingMapping(MappingAction.Ignore)]
		public struct TagInfo
		{
			public string id;
			public string name;
			public string count;
			public string slug;
			public string html_url;
			public string rss_url;
			public override string ToString()
			{
				return name;
			}
		}

		[StructLayout(LayoutKind.Sequential), XmlRpcMissingMapping(MappingAction.Ignore)]
		public struct UserBlog
		{
			public bool isAdmin;
			public string url;
			public string blogid;
			public string blogName;
			public string xmlrpc;
		}

		[StructLayout(LayoutKind.Sequential), XmlRpcMissingMapping(MappingAction.Ignore)]
		public struct UserInfo
		{
			public string url;
			public string blogid;
			public string blogName;
			public string firstname;
			public string lastname;
			public string email;
			public string nickname;
		}

		internal static class Utility
		{
			// Methods
			public static Image ConvertByteArrayToImage(byte[] byteArray)
			{
				if (byteArray != null)
				{
					MemoryStream stream = new MemoryStream(byteArray, 0, byteArray.Length);
					stream.Write(byteArray, 0, byteArray.Length);
					return Image.FromStream(stream, true);
				}
				return null;
			}


			public static byte[] ConvertImageToByteArray(Image imageToConvert, ImageFormat formatOfImage)
			{
				using (MemoryStream stream = new MemoryStream())
				{
					imageToConvert.Save(stream, formatOfImage);
					return stream.ToArray();
				}
			}


		}
}