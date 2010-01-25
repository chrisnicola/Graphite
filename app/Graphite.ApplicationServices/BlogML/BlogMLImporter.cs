using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using BlogML.Xml;

namespace Graphite.ApplicationServices.BlogML
{
	public class BlogMLImporter : IBlogImporter
	{
		private readonly IPostTasks _tasks;

		public BlogMLImporter(IPostTasks tasks) {
			_tasks = tasks;
		}

		public void Import(XmlReader xmlData) {	
			var blog = BlogMLSerializer.Deserialize(xmlData);
			
			
		}
	}

	public interface IBlogImporter {
		void Import(XmlReader xmlData);
	}
}
