using Elasticsearch.Net;
using Nest;
using NestExamples.CreateDelete;
using NestExamples.Entities;
using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NestExamples
{
	public class ElasticSearch
	{
		private readonly ElasticClient _client;

		private static readonly Logger Log = LogManager.GetCurrentClassLogger();
		private string _elasticServer;
		private string _indexName;

		public ElasticSearch()
		{
			var nodes = ConfigurationManager.AppSettings["ElasticSearchNodes"];
			_indexName = ConfigurationManager.AppSettings["Index"];
			if (string.IsNullOrEmpty(_indexName))
			{
				_indexName = "users" + DateTime.Now.Ticks;
			}

			if (!string.IsNullOrWhiteSpace(nodes))
			{
				var connectionPool = new StaticConnectionPool(nodes.Split(',').Select(uri => new Uri(uri)));
				_elasticServer = nodes.Split(',')[0];
				var config = new ConnectionSettings(connectionPool);
				config.DefaultIndex(this._indexName);

				var strMaxRetries = ConfigurationManager.AppSettings["MaxRetries"];
				int maxRetries;
				if (int.TryParse(strMaxRetries, out maxRetries) && maxRetries > 0)
				{
					config.MaximumRetries(maxRetries);
				}
				else
				{
					config.MaximumRetries(2);
				}
				config.DisableDirectStreaming();
				_client = new ElasticClient(config);
			}
			else
			{
				throw new ArgumentException("Elastic Search Nodes Configuration not available");
			}
		}

		public ElasticClient GetClient()
		{
			return _client;
		}

		public void CreateIndex()
		{
			IndexFromFile idx = new IndexFromFile(_client, _indexName, "IndexSchema.json");
			idx.DeleteIndexIfExists();
			idx.CreateIndex();
		}

		//public void CreateIndex(IElasticIndex idx)
		//{
		//	idx.DeleteIndexIfExists();
		//	idx.CreateIndex();
		//}

		//public void DeleteIndex(IElasticIndex idx)
		//{
		//	idx.DeleteIndex();
		//}

		//public void PopulateData(IElasticIndex idx)
		//{
		//	idx.PopulateData();
		//}

		//public void ExecuteQueries(IElasticIndex idx)
		//{
		//	idx.ExecuteQueries();
		//}

		public void DeleteIndex()
		{
			DeleteIndex(_indexName);
		}

		public void DeleteIndex(string index)
		{
			var idx = new ElasticIndexBase<User>(_client, index);
			idx.DeleteIndex();
		}

		public void PopulateUsers()
		{
			List<User> users = GetUsers();
			var descriptor = new BulkDescriptor();
			foreach (var entity in users)
			{
				var user = entity;
				descriptor.Index<User>(op => op
					.Index(this._indexName)
					.Version(DateTime.Now.Ticks)
					.VersionType(VersionType.External)
					.Document(user)
					);
			}

			// Execute the bulk indexing operation
			var response = this._client.Bulk(descriptor);

			// Log the request/response
			if (response != null)
			{
				// TODO: Log the response.
				Log.Debug("Indexed the users");
			}
			else
			{
				Log.Error("[ElasticSearch] Unknown error - received NULL response from bulk operation.");
			}
			Thread.Sleep(5000);
		}

		public void ElasticQuery(SearchDescriptor<User> searchDescriptor)
		{
			var response = _client.Search<User>(searchDescriptor);
			if (response != null)
			{
				Log.Debug(response.DebugInformation);
				Log.Debug("Result Count: " + response.Total);
				foreach (var user in response.Hits)
				{
					Log.Debug(user.Source.ToString());
				}
			}
		}

		public SearchDescriptor<User> QueryUsersByState(string state)
		{
			var searchDescriptor = new SearchDescriptor<User>().Index(_indexName);
			searchDescriptor.Query(q => q.Term(r => r.State, state));
			return searchDescriptor;
		}

		public SearchDescriptor<User> QueryMoreUsersByState(string state)
		{
			var searchDescriptor = new SearchDescriptor<User>().Index(_indexName);
			searchDescriptor.Query(q => q.Term(r => r.State, state));
			searchDescriptor.From(5);
			searchDescriptor.Size(100);
			return searchDescriptor;
		}

		public SearchDescriptor<User> QuerySortByState()
		{
			var searchDescriptor = new SearchDescriptor<User>().Index(_indexName);
			searchDescriptor.Sort(q => q.Field("state", SortOrder.Descending));
			searchDescriptor.From(0);
			searchDescriptor.Size(100);
			return searchDescriptor;
		}

		public SearchDescriptor<User> QueryIdBetween(int start, int end)
		{
			var searchDescriptor = new SearchDescriptor<User>().Index(_indexName);
			searchDescriptor.Query(q => q.Range(r => r.Field("id").GreaterThanOrEquals(start)) && q.Range(r => r.Field("id").LessThanOrEquals(end)));
			searchDescriptor.Sort(q => q.Field("id", SortOrder.Ascending));
			return searchDescriptor;
		}

		public SearchDescriptor<User> Query5(int start, int end, string state)
		{
			var searchDescriptor = new SearchDescriptor<User>().Index(_indexName);
			searchDescriptor.Query(q => (q.Range(r => r.Field("id").GreaterThanOrEquals(start)) && q.Range(r => r.Field("id").LessThanOrEquals(end))) || q.Term(r => r.State, state));
			searchDescriptor.Sort(q => q.Field("id", SortOrder.Ascending));
			return searchDescriptor;
		}

		public SearchDescriptor<User> Query6(int start, int end, string state)
		{
			var searchDescriptor = new SearchDescriptor<User>().Index(_indexName);
			QueryContainer query1 = Query<User>.Range(r => r.Field("id").GreaterThanOrEquals(start));
			QueryContainer query2 = Query<User>.Range(r => r.Field("id").LessThanOrEquals(end));
			QueryContainer query3 = Query<User>.Term(r => r.State, state);
			searchDescriptor.Query(q => q.Bool(r => r.Must(query1, query2, query3)));
			searchDescriptor.Sort(q => q.Field("id", SortOrder.Ascending));
			return searchDescriptor;
		}

		public SearchDescriptor<User> Query7(int start, int end, string state)
		{
			var searchDescriptor = new SearchDescriptor<User>().Index(_indexName);
			QueryContainer query1 = Query<User>.Range(r => r.Field(s => s.Id).GreaterThanOrEquals(start));
			QueryContainer query2 = Query<User>.Range(r => r.Field(s => s.Id).LessThanOrEquals(end));
			QueryContainer query3 = Query<User>.Term(r => r.State, state);
			searchDescriptor.Query(q => q.Bool(r => r.Must(query1, query2, query3)));
			searchDescriptor.Sort(q => q.Field(s => s.Id, SortOrder.Ascending));
			return searchDescriptor;
		}

		public SearchDescriptor<User> Query8(int start, int end, string state)
		{
			var searchDescriptor = new SearchDescriptor<User>().Index(_indexName);
			QueryContainer query1 = Query<User>.Range(r => r.Field(s => s.Id).GreaterThanOrEquals(start));
			QueryContainer query2 = Query<User>.Range(r => r.Field(s => s.Id).LessThanOrEquals(end));
			QueryContainer query12 = Query<User>.Bool(r => r.Must(query1, query2));
			QueryContainer query3 = Query<User>.Term(r => r.State, state);
			searchDescriptor.Query(q => q.Bool(r => r.Should(query12, query3)));
			searchDescriptor.Sort(q => q.Field(s => s.Id, SortOrder.Ascending));
			return searchDescriptor;
		}

		public SearchDescriptor<User> Query9(int start, int end, string state)
		{
			var searchDescriptor = new SearchDescriptor<User>().Index(_indexName);
			QueryContainer query1 = Query<User>.Range(r => r.Field(s => s.Id).GreaterThanOrEquals(start).LessThanOrEquals(end));
			QueryContainer query2 = Query<User>.Term(r => r.State, state);
			searchDescriptor.Query(q => q.Bool(r => r.Should(query1, query2)));
			searchDescriptor.Sort(q => q.Field(s => s.Id, SortOrder.Ascending));
			return searchDescriptor;
		}

		public SearchDescriptor<User> Query10(int start, int end, params string[] states)
		{
			var searchDescriptor = new SearchDescriptor<User>().Index(_indexName);
			QueryContainer query1 = Query<User>.Range(r => r.Field(s => s.Id).GreaterThanOrEquals(start).LessThanOrEquals(end));
			QueryContainer query2 = Query<User>.Terms(r => r.Field(s => s.State).Terms(states));
			searchDescriptor.Query(q => q.Bool(r => r.Should(query1, query2)));
			searchDescriptor.Sort(q => q.Field(s => s.Id, SortOrder.Ascending));
			return searchDescriptor;
		}

		public SearchDescriptor<User> QueryName(string name)
		{
			var searchDescriptor = new SearchDescriptor<User>().Index(_indexName);
			QueryContainer query1 = Query<User>.Match(r => r.Field(f => f.Name).Query(name));
			searchDescriptor.Query(q => q.Bool(r => r.Must(query1)));
			searchDescriptor.Sort(q => q.Field(s => s.Id, SortOrder.Ascending));
			return searchDescriptor;
		}

		public SearchDescriptor<User> QueryAutoComplete(string name)
		{
			var searchDescriptor = new SearchDescriptor<User>().Index(_indexName);
			QueryContainer query1 = Query<User>.Match(r => r.Field(f => f.Name.Suffix("autocomplete")).Query(name));
			searchDescriptor.Query(q => q.Bool(r => r.Must(query1)));
			searchDescriptor.Sort(q => q.Field(s => s.Id, SortOrder.Ascending));
			return searchDescriptor;
		}

		public SearchDescriptor<User> Query12()
		{
			var searchDescriptor = new SearchDescriptor<User>().Index(_indexName);
			searchDescriptor.Sort(q => q.Field(s => s.State, SortOrder.Ascending).Field(s => s.Id, SortOrder.Ascending));
			return searchDescriptor;
		}

		public SearchDescriptor<User> Query13()
		{
			var searchDescriptor = new SearchDescriptor<User>().Index(_indexName);
			SortDescriptor<User> sort = new SortDescriptor<User>();
			sort.Field("state", SortOrder.Descending);
			sort.Field("id", SortOrder.Descending);
			searchDescriptor.Sort(q => sort);
			return searchDescriptor;
		}

		public SearchDescriptor<User> Query14()
		{
			var searchDescriptor = new SearchDescriptor<User>().Index(_indexName);
			SortDescriptor<User> sort = new SortDescriptor<User>();
			sort.Field(q => q.State, SortOrder.Descending);
			sort.Field(q => q.Id, SortOrder.Descending);
			searchDescriptor.Sort(q => sort);
			return searchDescriptor;
		}

		public SearchDescriptor<User> Query15()
		{
			var searchDescriptor = new SearchDescriptor<User>().Index(_indexName);
			SortDescriptor<User> sort = new SortDescriptor<User>();
			// Missing last is the default sort. So, no need to explicitly define this. 
			sort.Field(f => f.Field(q => q.State).Order(SortOrder.Descending).MissingLast());
			sort.Field(q => q.Id, SortOrder.Descending);
			searchDescriptor.Sort(q => sort);
			return searchDescriptor;
		}

		public SearchDescriptor<User> QueryNotState1(string state)
		{
			var searchDescriptor = new SearchDescriptor<User>().Index(_indexName);
			QueryContainer query1 = Query<User>.Match(r => r.Field(f => f.State).Query(state));
			searchDescriptor.Query(q => q.Bool(r => r.MustNot(query1)));
			searchDescriptor.Sort(q => q.Field(s => s.Id, SortOrder.Ascending));
			return searchDescriptor;
		}

		public SearchDescriptor<User> QueryNotState2(string state)
		{
			var searchDescriptor = new SearchDescriptor<User>().Index(_indexName);
			QueryContainer query1 = Query<User>.Match(r => r.Field(f => f.State).Query(state));
			searchDescriptor.Query(q => q.Bool(r => r.Must(!query1)));
			searchDescriptor.Sort(q => q.Field(s => s.Id, SortOrder.Ascending));
			return searchDescriptor;
		}

		public SearchDescriptor<User> QueryNotState3(string state)
		{
			var searchDescriptor = new SearchDescriptor<User>().Index(_indexName);
			QueryContainer query1 = Query<User>.Match(r => r.Field(f => f.State).Query(state));
			searchDescriptor.Query(q => !query1);
			searchDescriptor.Sort(q => q.Field(s => s.Id, SortOrder.Ascending));
			return searchDescriptor;
		}

		public SearchDescriptor<User> QueryByCreatedDate(DateTime date)
		{
			var searchDescriptor = new SearchDescriptor<User>().Index(_indexName);
			QueryContainer query1 = Query<User>.DateRange(r => r.Field(f => f.CreatedDate).GreaterThanOrEquals(date));
			searchDescriptor.Query(q => q.Bool(r => r.Must(query1)));
			searchDescriptor.Sort(q => q.Field(s => s.Id, SortOrder.Ascending));
			return searchDescriptor;
		}

		public SearchDescriptor<User> QueryByNestedField(string value)
		{
			var searchDescriptor = new SearchDescriptor<User>().Index(_indexName);
			//QueryContainer query1 = Query<User>.Nested(r => r.Path("customData").Query(q => q.Bool(bq => bq.Must(
			//								m => m.Match(m1 => m1.Field(l => l.CustomData.Suffix("name")).Query("Name1")) 
			//								&& m.Match(m2 => m2.Field(l => l.CustomData.Suffix("value")).Query(value))))));

			// The following is better than above.
			QueryContainer query1 = Query<User>.Nested(r => r.Path("customData").Query(q => q.Bool(bq => bq.Must(
								m => m.Match(m1 => m1.Field(l => l.CustomData.Suffix("name")).Query("Name1")),
								m => m.Match(m2 => m2.Field(l => l.CustomData.Suffix("value")).Query(value))))));

			searchDescriptor.Query(q => q.Bool(r => r.Must(query1)));
			searchDescriptor.Sort(q => q.Field(s => s.Id, SortOrder.Ascending));
			return searchDescriptor;
		}

		public void SearchDescriptorToJson()
		{
			var searchDescriptor = new SearchDescriptor<User>().Index(new string[] { _indexName, "otherindex2" });
			QueryContainer query1 = Query<User>.DateRange(r => r.Field(f => f.CreatedDate).GreaterThanOrEquals(new DateTime(2010, 1, 1)));
			searchDescriptor.Query(q => q.Bool(r => r.Must(query1)));
			searchDescriptor.Sort(q => q.Field(s => s.Id, SortOrder.Ascending));

			Log.Info(((IUrlParameter)((ISearchRequest)searchDescriptor).Index).GetString(_client.ConnectionSettings));
			Log.Info(((IUrlParameter)((ISearchRequest)searchDescriptor).Index).GetString(new ElasticClient().ConnectionSettings));

			var memoryStream = new System.IO.MemoryStream();
			new ElasticClient().RequestResponseSerializer.Serialize(searchDescriptor, memoryStream);
			var json = System.Text.Encoding.UTF8.GetString(memoryStream.ToArray());
			Log.Info(json);
		}

		public void Query()
		{
			ElasticQuery(QueryUsersByState("TN"));
			ElasticQuery(QueryUsersByState("KL"));
			ElasticQuery(QueryUsersByState("KA"));
			ElasticQuery(QueryUsersByState("TS"));
			ElasticQuery(QueryUsersByState("MP"));
			ElasticQuery(QueryUsersByState("DL"));
			ElasticQuery(QueryUsersByState("UP"));

			ElasticQuery(QueryUsersByState("AP"));
			ElasticQuery(QueryMoreUsersByState("AP"));

			ElasticQuery(QuerySortByState());

			ElasticQuery(QueryIdBetween(25, 35));
			ElasticQuery(Query5(25, 35, "TN"));
			ElasticQuery(Query6(25, 35, "TN"));
			ElasticQuery(Query7(25, 35, "TN"));
			ElasticQuery(Query8(25, 35, "TN"));
			ElasticQuery(Query9(25, 35, "TN"));
			ElasticQuery(Query10(25, 35, "TN", "AP"));
			ElasticQuery(QueryAutoComplete("NA"));
			ElasticQuery(QueryAutoComplete("na"));
			ElasticQuery(QueryName("nagasatish"));
			ElasticQuery(QueryName("nagasatish tiruveedula"));
			ElasticQuery(Query12());
			ElasticQuery(Query13());
			ElasticQuery(Query14());
			ElasticQuery(Query15());
			ElasticQuery(QueryNotState1("AP"));
			ElasticQuery(QueryNotState2("AP"));
			ElasticQuery(QueryNotState3("AP"));
			ElasticQuery(QueryUsersByState("ap"));
			ElasticQuery(QueryByCreatedDate(new DateTime(2011, 1, 1)));
			ElasticQuery(QueryByNestedField("Value0"));
			SearchDescriptorToJson();
		}

		private List<User> GetUsers()
		{
			List<User> users = new List<User>();
			RandomGenerator random = new RandomGenerator();
			for (int i=0; i<88; i++)
			{
				User user = random.GetUser(i+1);
				users.Add(user);
			}
			return users;
		}
	}
}
