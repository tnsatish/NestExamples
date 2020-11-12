using GenerateTestData.ElasticIndices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateTestData
{
	class Program
	{
		static void Main(string[] args)
		{
			UserIndex userIndex = new UserIndex();
			userIndex.CreateIndex();
			userIndex.PopulateData();
		}
	}
}
