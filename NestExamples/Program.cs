using NestExamples.CreateDelete;
using NestExamples.Entities;
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

				IElasticIndex ip = new IPLocationIndex("iplocationindex");
				search.CreateIndex(ip);
				search.PopulateData(ip);
				search.DeleteIndex(ip);
			}
			catch(Exception ex)
			{
				Log.Error(ex.Message);
				Log.Error(ex.StackTrace);
			}
		}
	}
}
