using Nest;
using NestExamples.CreateDelete;
using NLog;
using System;

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
				ElasticClient client = search.GetClient();

				search.CreateIndex();
				search.PopulateUsers();
				search.Query();
				search.DeleteIndex();

				IElasticIndex ip = new IPLocationIndex(client, "iplocationindex");
				ip.CreateIndex();
				ip.PopulateData();
				ip.ExecuteQueries();
				ip.DeleteIndex();
			}
			catch(Exception ex)
			{
				Log.Error(ex.Message);
				Log.Error(ex.StackTrace);
			}
		}
	}
}
