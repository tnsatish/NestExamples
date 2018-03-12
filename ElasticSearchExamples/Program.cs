using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElasticSearchExamples
{
	class Program
	{
		private static readonly Logger Log = LogManager.GetCurrentClassLogger();

		public static void Main(string[] args)
		{
			try
			{
				ElasticSearch search = new ElasticSearch();
				try
				{
					search.DeleteIndex();
				}
				catch (Exception)
				{
				}

				search.CreateIndex();
				search.PopulateUsers();
				search.Query();
				search.DeleteIndex();
			}
			catch(Exception ex)
			{
				Log.Error(ex.Message);
				Log.Error(ex.StackTrace);
			}
		}
	}
}
