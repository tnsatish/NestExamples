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
	public class IndexFromFile : ElasticIndexBase<User>
	{
		private string _fileName;
		private static readonly Logger Log = LogManager.GetCurrentClassLogger();

		public IndexFromFile(ElasticClient client, string indexName, string fileName) : base(client, indexName)
		{
			_fileName = fileName;
		}

		public override void DeleteIndexIfExists()
		{
			// TODO: Check whether the index exists, and delete only if it exists.
			//_client.IndexExists()
			try
			{
				DeleteIndex();
			}
			catch (Exception ex)
			{
				Log.Info(ex.Message);
			}
		}

		public override void CreateIndex()
		{
			string url = _elasticServer + _indexName;
			Log.Info("URL: " + url);

			string schema = File.ReadAllText(_fileName);
			Log.Debug(schema);

			byte[] bytes = Encoding.UTF8.GetBytes(schema);

			WebRequest req = WebRequest.Create(url);
			req.Method = "PUT";
			req.ContentType = "application/json";

			Stream dataStream = req.GetRequestStream();
			dataStream.Write(bytes, 0, bytes.Length);
			dataStream.Close();

			try
			{
				var response = (HttpWebResponse)req.GetResponse();
				Log.Info("Response Status: " + response.StatusCode + " - " + response.StatusDescription);
				Log.Debug(new StreamReader(response.GetResponseStream()).ReadToEnd());
			}
			catch (WebException ex)
			{
				Log.Error(new StreamReader(ex.Response.GetResponseStream()).ReadToEnd());
				throw ex;
			}
		}
	}
}
