using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElasticSearchExamples
{
	public class RandomGenerator
	{
		public static Random random = new Random();
		string[] surnames_list = { "Mohanram", "Thangadurei", "LakshmiNarasimhan", "Ramakrishnan", "Gnanasekar", "Soundararajan", "Ramesh", "Subramanian", "Soundararajan", "Benjamin Franklin",    "AnandKumar", "Jayakumar",    "Palanisamy", "K",      "Selvaraj", "Patibandla", "Ramakrishnan", "VangalThulasidass", "Selvan", "Kasiviswanathan", "Kandasamy", "Subramanian", "Perumal", "M", "A", "Palani", "Rajkumar", "Thulasiraman", "Balasubramanian", "Thirumalai Velu S", "PoyyamozhiR", "Venkatachalapathy", "Gopalakrishnan", "Ramaiah", "Tiruveedula", "Palanisamy", "K", "Logachandran", "Govindharajan", "R", "Selvam", "Balashanmugam", "Jagannathan", "Pandian", "RaviChandran", "Krishnaswamy", "Srinivasan", "Ravi", "Vidyamsetti Subbarao", "Dhinagaran", "SaiPrasad", "Natarajan", "Shaik Mohammad", "Velan", "T G", "Naramsetti", "Rajuthevar Vetriselvan", "Duraisamy", "Paulchamy", "Subramanian", "R", "Suresh", "S", "Royal", "Venkataraman", "U", "Muthusami", "Srinivasan", "Muruganandham", "Subramanian", "Desikan", "Konagalla", "Srivatsa", "Sambasivam", "Vallathur Radhaiah", "Suresh", "K", "P", "B", "Dhanakodi", "Ganapathy", "Dudekula", "Prakash", "Aneel Kumar", "Metikala", "Duraisamy", "Chinnappa", "B", "Sambangi" };
		string[] first_names_list = { "Anand", "Anjana Sherin", "Aravind",         "Arun Guptha",  "Arunkumar",  "AshokKumar",    "Ashwin", "Babu",        "Balakrishnan",  "Christopher Selvaraj", "Dhivya",     "Dinesh Kumar", "Dinesh",     "Ganesh", "Gokul",    "Harish", "Imayaselvan", "JaganMohan", "Janani", "Jeevanram", "Kanagaraj", "Kannan", "Karthick", "Karthika", "Karthikeyan", "Kowsalya Devi", "Krishanth", "Logesh", "Madhan", "Mahesh Kumar", "Manibharathi", "Manimuthuraman", "Mohanakrishnan", "Muthukrishnan", "Nagasatish", "Naveen", "NavinKumar", "Nimrutha", "Pooja", "Pooja", "PrakashKumar", "PrasannaKumar", "PrasannaKumar", "Praveen", "Raghavi", "Ram", "Raman", "Ramya", "Ranganath", "Rekha", "SaiPrasanna", "Saminathan", "Sanaulla", "SangeethKumar", "Sankar Babu", "Sateesh", "Satya", "Selvakumar", "Selvaraj", "Shadana", "ShanmugaPriya", "Sharanya", "Shreenath", "Shyam", "Sivaraman", "Sivashanmugam", "Sowmiya", "Sreenidhi", "Sridhar", "Srikrish", "Sriram", "Sudheer", "Supriya", "Suresh", "Thulasi Ram", "Varna", "Vignesh Ram Nithin", "VigneshKumar", "Vigneshwar", "VijayaKumar", "VinothKumar", "Rahiman", "Madankumar", "Minda", "Narendra Kumar", "Partheepan", "Senthil", "Thirumagal", "Visweswara Rao" };

		string[] cities = { "Agartala", "Agra", "Amaravathi", "Anantapur", "Bengaluru", "Bhopal", "Bhubaneswar", "Chengalpattu", "Chennai", "Chidambaram", "Chittoor", "Delhi", "Eluru", "Gudur", "Guntur", "Guruvayur", "Hyderabad", "Indore", "Kadapa", "Kanchipuram", "Kannur", "Kanyakumari", "Kochi", "Kolkata", "Kozhikode", "Kumbakonam", "Kurnool", "Lucknow", "Madurai", "Mangalagiri", "Mangaluru", "Mumbai", "Mysore", "Nagpur", "Nellore", "Ongole", "Panaji", "Patna", "Pondicherry", "Pune", "Rajahmundry", "Rameswaram", "Srikakulam", "Srinagar", "Srirangam", "Tambaram", "Tanjore", "Tiruchirappalli", "Tirupathi", "Trivandrum", "Udupi", "Vijayawada", "Visakhapatnam", "Vizianagaram" };
		string[] states = { "TR", "UP", "AP", "AP", "KA", "MP", "OR", "TN", "TN", "TN", "AP", "DL", "AP", "AP", "AP", "KL", "TS", "MP", "AP", "TN", "KL", "TN", "KL", "WB", "KL", "TN", "AP", "UP", "TN", "AP", "KA", "MH", "KA", "MH", "AP", "AP", "GA", "BR", "PY", "MH", "AP", "TN", "AP", "JK", "TN", "TN", "TN", "TN", "AP", "KL", "KA", "AP", "AP", "AP" };

		string[] domains = { "gmail.com", "yahoo.com", "tnsatish.com" };

		public Tuple<string, string> GetNameEmail(int n)
		{
			int r1 = n % first_names_list.Length;
			string firstName = first_names_list[r1];
			string lastName = surnames_list[r1];
			string name = firstName + " " + lastName;
			string email = firstName.Replace(" ", ".").ToLower() + "." + lastName.Replace(" ", ".").ToLower() + "@" + domains[n % domains.Length];
			return new Tuple<string, string>(name, email);
		}

		public Tuple<string, string> GetCityState(int n)
		{
			int r = n % cities.Length;
			return new Tuple<string, string>(cities[r], states[r]);
		}

		public string GetPhoneNumber()
		{
			StringBuilder sb = new StringBuilder();
			for(int i=0; i<10; i++)
			{
				if (i == 0)
					sb.Append(random.Next(3) + 7);
				else
					sb.Append(random.Next(10));
			}
			return sb.ToString();
		}

		public string GetZip()
		{
			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < 6; i++)
			{
				if (i == 0)
					sb.Append(random.Next(6) + 1);
				else
					sb.Append(random.Next(10));
			}
			return sb.ToString();
		}

		public DateTimeOffset GetDate(DateTimeOffset? date)
		{
			if (date == null)
				date = new DateTimeOffset(new DateTime(2000, 1, 1));
			DateTimeOffset now = DateTimeOffset.Now;
			int days = (int)(now - date.Value).TotalDays;
			return date.Value.AddDays(random.Next(days));
		}

		public User GetUser(int id)
		{
			User user = new User();
			Tuple<string, string> nameEmail = GetNameEmail(id);
			user.Name = nameEmail.Item1;
			user.Email = nameEmail.Item2;
			user.Id = id;
			Tuple<string, string> cityState = GetCityState(id);
			user.City = cityState.Item1;
			user.State = cityState.Item2;
			user.PrimaryPhone = GetPhoneNumber();
			user.Zip = GetZip();
			user.IsActive = true;
			user.CreatedDate = GetDate(null);
			user.ModifiedDate = GetDate(user.CreatedDate);
			user.CustomData = new List<NameValuePair>() { new NameValuePair("Name1", "Value" + (id % 20)), new NameValuePair("Name2", "Value" + random.Next(200)) };
			return user;
		}
	}
}
