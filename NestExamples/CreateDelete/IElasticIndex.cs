using Nest;
using NestExamples.Entities;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NestExamples.CreateDelete
{
	public interface IElasticIndex
	{
		void CreateIndex(ElasticClient client);
		void DeleteIndex(ElasticClient client);
		void DeleteIndexIfExists(ElasticClient client);
		void PopulateData(ElasticClient client);
		void ExecuteQueries(ElasticClient client);
	}
}
