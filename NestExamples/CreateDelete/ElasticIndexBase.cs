using Elasticsearch.Net;
using Nest;
using NestExamples.Entities;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NestExamples.CreateDelete
{
	public class ElasticIndexBase : IElasticIndex
	{
		protected string _elasticServer;
		protected string _indexName;
		private static readonly Logger Log = LogManager.GetCurrentClassLogger();

		public ElasticIndexBase() { }

		public ElasticIndexBase(string indexName)
		{
			_indexName = indexName;
		}

		public virtual void CreateIndex(ElasticClient client)
		{
			var createDescriptor = GetCreateIndexDescriptor();
			var response = client.CreateIndex(_indexName, createDescriptor);
			if (response != null)
			{
				Log.Debug(response.DebugInformation);
			}
		}

		protected virtual Func<CreateIndexDescriptor, CreateIndexDescriptor> GetCreateIndexDescriptor()
		{
			throw new NotImplementedException();
		}

		public virtual void DeleteIndexIfExists(ElasticClient client)
		{
			// TODO: Check whether the index exists, and delete only if it exists.
			//_client.IndexExists()
			try {
				DeleteIndex(client);
			} catch (Exception ex) {
				Log.Info(ex.Message);
			}
		}

		public virtual void DeleteIndex(ElasticClient client)
		{
			_elasticServer = client.ConnectionSettings.ConnectionPool.Nodes.ToArray()[0].Uri.ToString();
			string url = _elasticServer + _indexName;
			Log.Info("Deleting Index: " + _indexName);
			Log.Info("URL: " + url);

			WebRequest req = WebRequest.Create(url);
			req.Method = "DELETE";
			req.ContentType = "application/json";

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

		//public virtual void PopulateData(ElasticClient client)
		//{
		//	var descriptor = new BulkDescriptor();
		//	//foreach (var entity in users)
		//	//{
		//	//	var user = entity;
		//	//	descriptor.Index<T>(op => op
		//	//		.Index(this._indexName)
		//	//		.Version(DateTime.Now.Ticks)
		//	//		.VersionType(VersionType.External)
		//	//		.Document(user)
		//	//		);
		//	//}

		//	// Execute the bulk indexing operation
		//	var response = client.Bulk(descriptor);

		//	// Log the request/response
		//	if (response != null)
		//	{
		//		// TODO: Log the response.
		//		Log.Debug("Indexed the users");
		//	}
		//	else
		//	{
		//		Log.Error("[ElasticSearch] Unknown error - received NULL response from bulk operation.");
		//	}
		//	Thread.Sleep(5000);
		//}

		public virtual void PopulateData(ElasticClient client)
		{
			var descriptor = new BulkDescriptor();
			AddElementsToIndex(descriptor);

			// Execute the bulk indexing operation
			var response = client.Bulk(descriptor);

			// Log the request/response
			if (response != null)
			{
				// TODO: Log the response.
				Log.Debug("Indexed the users");
				Log.Debug(response.DebugInformation);
			}
			else
			{
				Log.Error("[ElasticSearch] Unknown error - received NULL response from bulk operation.");
			}
			Thread.Sleep(5000);
		}

		protected virtual void AddElementsToIndex(BulkDescriptor desc)
		{
		}

		protected virtual void AddElementsToIndex<T>(BulkDescriptor descriptor, List<T> elements) where T : class
		{
			foreach(T element in elements)
			{
				descriptor.Index<T>(op => op.Index(_indexName)
											.Version(DateTime.Now.Ticks)
											.VersionType(VersionType.External)
											.Document(element));
			}
		}

		public T[] Reverse<T>(T[] array)
		{
			var result = new T[array.Length];
			int j = 0;
			for (int i = array.Length; i >= 0; i--)
			{
				result[j] = array[i];
				j++;
			}
			return result;
		}

		public virtual Type GetObjectType()
		{
			throw new NotImplementedException();
		}

		public virtual void ExecuteQueries(ElasticClient client)
		{
			throw new NotImplementedException();
		}
	}
}
