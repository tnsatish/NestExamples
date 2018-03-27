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
	public class IndexFromFile<T> : ElasticIndexBase<T> where T: class
	{
		private string _fileName;
		private static readonly Logger Log = LogManager.GetCurrentClassLogger();

		public IndexFromFile(ElasticClient client, string indexName, string fileName) : base(client, indexName)
		{
			_fileName = fileName;
		}

		public override void CreateIndex()
		{
			CreateIndexFromFile(_fileName);
		}
	}
}
