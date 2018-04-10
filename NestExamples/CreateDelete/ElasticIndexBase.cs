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
	public class ElasticIndexBase<T> : IElasticIndex where T : class
	{
		protected string _elasticServer;
		protected string _indexName;
		protected ElasticClient _client;
		protected static readonly Logger Log = LogManager.GetCurrentClassLogger();

		public ElasticIndexBase() { }

		public ElasticIndexBase(ElasticClient client, string indexName)
		{
			_client = client;
			_indexName = indexName;
			_elasticServer = client.ConnectionSettings.ConnectionPool.Nodes.ToArray()[0].Uri.ToString();
		}

		public virtual void CreateIndex()
		{
			DeleteIndexIfExists();
			var createDescriptor = GetCreateIndexDescriptor();
			var response = _client.CreateIndex(_indexName, createDescriptor);
			if (response != null)
			{
				Log.Debug(response.DebugInformation);
			}
		}

		public void CreateIndexFromFile(string fileName)
		{
			string url = _elasticServer + _indexName;
			Log.Info("URL: " + url);

			string schema = File.ReadAllText(fileName);
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

		protected virtual Func<CreateIndexDescriptor, CreateIndexDescriptor> GetCreateIndexDescriptor()
		{
			throw new NotImplementedException();
		}

		public virtual void DeleteIndexIfExists()
		{
			// TODO: Check whether the index exists, and delete only if it exists.
			//_client.IndexExists()
			try {
				DeleteIndex();
			} catch (Exception ex) {
				Log.Info(ex.Message);
			}
		}

		public virtual void DeleteIndex()
		{
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

		private void LogResponseAndSleep(IBulkResponse response)
		{
			if (response != null)
			{
				Log.Debug(response.DebugInformation);
			}
			else
			{
				Log.Error("[ElasticSearch] Unknown error - received NULL response from bulk operation.");
			}
			Thread.Sleep(5000);
		}

		public virtual void PopulateData()
		{
			var descriptor = new BulkDescriptor();
			AddElementsToIndex(descriptor);
			var response = _client.Bulk(descriptor);
			LogResponseAndSleep(response);
		}

		public virtual void PopulateData(List<T> elements)
		{
			var descriptor = new BulkDescriptor();
			AddElementsToIndex(descriptor, elements);
			var response = _client.Bulk(descriptor);
			LogResponseAndSleep(response);
		}

		protected virtual void AddElementsToIndex(BulkDescriptor desc)
		{
			throw new NotImplementedException();
		}

		protected virtual void AddElementsToIndex(BulkDescriptor descriptor, List<T> elements)
		{
			foreach(T element in elements)
			{
				descriptor.Index<T>(op => op.Index(_indexName)
											.Version(DateTime.Now.Ticks)
											.VersionType(VersionType.External)
											.Document(element));
			}
		}

		public ISearchResponse<T> ExecuteQuery(SearchDescriptor<T> searchDescriptor)
		{
			var response = _client.Search<T>(searchDescriptor);
			if (response != null)
			{
				Log.Debug(response.DebugInformation);
				Log.Debug("Result Count: " + response.Total);
				foreach (var user in response.Hits)
				{
					Log.Debug(user.Source.ToString());
				}
			}
			return response;
		}

		public IMultiSearchResponse ExecuteQueries(params SearchDescriptor<T>[] searchDescriptor)
		{
			var multiDescriptor = new MultiSearchDescriptor();
			foreach(var desc in searchDescriptor)
			{
				multiDescriptor.Search<T>(q => desc);
			}
			var allResponses = _client.MultiSearch(multiDescriptor);
			if (allResponses != null)
			{
				Log.Debug(allResponses.DebugInformation);
				foreach (var response in allResponses.GetResponses<T>())
				{
					Log.Debug("Result Count: " + response.Total);
					foreach (var hit in response.Hits)
					{
						Log.Debug(hit.Source.ToString());
					}
				}
			}
			return allResponses;
		}

		public virtual void ExecuteQueries()
		{
			throw new NotImplementedException();
		}
	}
}
