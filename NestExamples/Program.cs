using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NestExamples
{
	class Program
	{
		private static readonly Logger Log = LogManager.GetCurrentClassLogger();

		public static void Main(string[] args)
		{
			try
			{
				ElasticSearch search = new ElasticSearch();
				search.CreateIndex();
				search.PopulateUsers();
				search.Query();
				search.DeleteIndex();
				search.CreateAutoCompleteIndex("autocompleteindex");
				search.DeleteIndex("autocompleteindex");
			}
			catch(Exception ex)
			{
				Log.Error(ex.Message);
				Log.Error(ex.StackTrace);
			}
		}
	}
}
