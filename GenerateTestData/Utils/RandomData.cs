using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateTestData.Utils
{
	public class RandomData
	{
		private static Random _random = new Random();

		public static bool GetBoolean()
		{
			int random = _random.Next(2);
			return random % 2 == 0; 
		}

		public static T Get<T>(T[] data)
		{
			if (data == null || data.Length == 0)
				return default(T);
			return data[_random.Next(data.Length)];
		}

		public static int Next(int max)
		{
			return _random.Next(max);
		}

		public static string[] EmailDomains = { "gmail.com", "yahoo.com", "hotmail.com", "outlook.com" };

		public static string[] MaleNames = { "James", "John", "Robert", "Michael", "William", "David", "Richard", "Joseph", "Thomas", "Charles", "Christopher", "Daniel", "Matthew", "Anthony", "Donald", "Mark", "Paul", "Steven", "Andrew", "Kenneth", "Joshua", "Kevin", "Brian", "George", "Edward", "Ronald", "Timothy", "Jason", "Jeffrey", "Ryan", "Jacob", "Gary", "Nicholas", "Eric", "Jonathan", "Stephen", "Larry", "Justin", "Scott", "Brandon", "Benjamin", "Samuel", "Frank", "Gregory", "Raymond", "Alexander", "Patrick", "Jack", "Dennis", "Jerry", "Tyler", "Aaron", "Jose", "Henry", "Adam", "Douglas", "Nathan", "Peter", "Zachary", "Kyle", "Walter", "Harold", "Jeremy", "Ethan", "Carl", "Keith", "Roger", "Gerald", "Christian", "Terry", "Sean", "Arthur", "Austin", "Noah", "Lawrence", "Jesse", "Joe", "Bryan", "Billy", "Jordan", "Albert", "Dylan", "Bruce", "Willie", "Gabriel", "Alan", "Juan", "Logan", "Wayne", "Ralph", "Roy", "Eugene", "Randy", "Vincent", "Russell", "Louis", "Philip", "Bobby", "Johnny", "Bradley" };

		public static string[] FemaleNames = { "Mary", "Patricia", "Jennifer", "Linda", "Elizabeth", "Barbara", "Susan", "Jessica", "Sarah", "Karen", "Nancy", "Lisa", "Margaret", "Betty", "Sandra", "Ashley", "Dorothy", "Kimberly", "Emily", "Donna", "Michelle", "Carol", "Amanda", "Melissa", "Deborah", "Stephanie", "Rebecca", "Laura", "Sharon", "Cynthia", "Kathleen", "Amy", "Shirley", "Angela", "Helen", "Anna", "Brenda", "Pamela", "Nicole", "Samantha", "Katherine", "Emma", "Ruth", "Christine", "Catherine", "Debra", "Rachel", "Carolyn", "Janet", "Virginia", "Maria", "Heather", "Diane", "Julie", "Joyce", "Victoria", "Kelly", "Christina", "Lauren", "Joan", "Evelyn", "Olivia", "Judith", "Megan", "Cheryl", "Martha", "Andrea", "Frances", "Hannah", "Jacqueline", "Ann", "Gloria", "Jean", "Kathryn", "Alice", "Teresa", "Sara", "Janice", "Doris", "Madison", "Julia", "Grace", "Judy", "Abigail", "Marie", "Denise", "Beverly", "Amber", "Theresa", "Marilyn", "Danielle", "Diana", "Brittany", "Natalie", "Sophia", "Rose", "Isabella", "Alexis", "Kayla", "Charlotte" };

		public static string[] Names = { "Liam", "Noah", "Oliver", "William", "Elijah", "James", "Benjamin", "Lucas", "Mason", "Ethan", "Alexander", "Henry", "Jacob", "Michael", "Daniel", "Logan", "Jackson", "Sebastian", "Jack", "Aiden", "Owen", "Samuel", "Matthew", "Joseph", "Levi", "Mateo", "David", "John", "Wyatt", "Carter", "Julian", "Luke", "Grayson", "Isaac", "Jayden", "Theodore", "Gabriel", "Anthony", "Dylan", "Leo", "Lincoln", "Jaxon", "Asher", "Christopher", "Josiah", "Andrew", "Thomas", "Joshua", "Ezra", "Hudson", "Charles", "Caleb", "Isaiah", "Ryan", "Nathan", "Adrian", "Christian", "Maverick", "Colton", "Elias", "Aaron", "Eli", "Landon", "Jonathan", "Nolan", "Hunter", "Cameron", "Connor", "Santiago", "Jeremiah", "Ezekiel", "Angel", "Roman", "Easton", "Miles", "Robert", "Jameson", "Nicholas", "Greyson", "Cooper", "Ian", "Carson", "Axel", "Jaxson", "Dominic", "Leonardo", "Luca", "Austin", "Jordan", "Adam", "Xavier", "Jose", "Jace", "Everett", "Declan", "Evan", "Kayden", "Parker", "Wesley", "Kai", "Brayden", "Bryson", "Weston", "Jason", "Emmett", "Sawyer", "Silas", "Bennett", "Brooks", "Micah", "Damian", "Harrison", "Waylon", "Ayden", "Vincent", "Ryder", "Kingston", "Rowan", "George", "Luis", "Chase", "Cole", "Nathaniel", "Zachary", "Ashton", "Braxton", "Gavin", "Tyler", "Diego", "Bentley", "Amir", "Beau", "Gael", "Carlos", "Ryker", "Jasper", "Max", "Juan", "Ivan", "Brandon", "Jonah", "Giovanni", "Kaiden", "Myles", "Calvin", "Lorenzo", "Maxwell", "Jayce", "Kevin", "Legend", "Tristan", "Jesus", "Jude", "Zion", "Justin", "Maddox", "Abel", "King", "Camden", "Elliott", "Malachi", "Milo", "Emmanuel", "Karter", "Rhett", "Alex", "August", "River", "Xander", "Antonio", "Brody", "Finn", "Elliot", "Dean", "Emiliano", "Eric", "Miguel", "Arthur", "Matteo", "Graham", "Alan", "Nicolas", "Blake", "Thiago", "Adriel", "Victor", "Joel", "Timothy", "Hayden", "Judah", "Abraham", "Edward", "Messiah", "Zayden", "Theo", "Tucker", "Grant", "Richard", "Alejandro", "Steven", "Jesse", "Dawson", "Bryce", "Avery", "Oscar", "Patrick", "Archer", "Barrett", "Leon", "Colt", "Charlie", "Peter", "Kaleb", "Lukas", "Beckett", "Jeremy", "Preston", "Enzo", "Luka", "Andres", "Marcus", "Felix", "Mark", "Ace", "Brantley", "Atlas", "Remington", "Maximus", "Matias", "Walker", "Kyrie", "Griffin", "Kenneth", "Israel", "Javier", "Kyler", "Jax", "Amari", "Zane", "Emilio", "Knox", "Adonis", "Aidan", "Kaden", "Paul", "Omar", "Brian", "Louis", "Caden", "Maximiliano", "Holden", "Paxton", "Nash", "Bradley", "Bryan", "Simon", "Phoenix", "Lane", "Josue", "Colin", "Rafael", "Kyle", "Riley", "Jorge", "Beckham", "Cayden", "Jaden", "Emerson", "Ronan", "Karson", "Arlo", "Tobias", "Brady", "Clayton", "Francisco", "Zander", "Erick", "Walter", "Daxton", "Cash", "Martin", "Damien", "Dallas", "Cody", "Chance", "Jensen", "Finley", "Jett", "Corbin", "Kash", "Reid", "Kameron", "Andre", "Gunner", "Jake", "Hayes", "Manuel", "Prince", "Bodhi", "Cohen", "Sean", "Khalil", "Hendrix", "Derek", "Cristian", "Cruz", "Kairo", "Dante", "Atticus", "Killian", "Stephen", "Orion", "Malakai", "Ali", "Eduardo", "Fernando", "Anderson", "Angelo", "Spencer", "Gideon", "Mario", "Titus", "Travis", "Rylan", "Kayson", "Ricardo", "Tanner", "Malcolm", "Raymond", "Odin", "Cesar", "Lennox", "Joaquin", "Kane", "Wade", "Muhammad", "Iker", "Jaylen", "Crew", "Zayn", "Hector", "Ellis", "Leonel", "Cairo", "Garrett", "Romeo", "Dakota", "Edwin", "Warren", "Julius", "Major", "Donovan", "Caiden", "Tyson", "Nico", "Sergio", "Nasir", "Rory", "Devin", "Jaiden", "Jared", "Kason", "Malik", "Jeffrey", "Ismael", "Elian", "Marshall", "Lawson", "Desmond", "Winston", "Nehemiah", "Ari", "Conner", "Jay", "Kade", "Andy", "Johnny", "Jayceon", "Marco", "Seth", "Ibrahim", "Raiden", "Collin", "Edgar", "Erik", "Troy", "Clark", "Jaxton", "Johnathan", "Gregory", "Russell", "Royce", "Fabian", "Ezequiel", "Noel", "Pablo", "Cade", "Pedro", "Sullivan", "Trevor", "Reed", "Quinn", "Frank", "Harvey", "Princeton", "Zayne", "Matthias", "Conor", "Sterling", "Dax", "Grady", "Cyrus", "Gage", "Leland", "Solomon", "Emanuel", "Niko", "Ruben", "Kasen", "Mathias", "Kashton", "Franklin", "Remy", "Shane", "Kendrick", "Shawn", "Otto", "Armani", "Keegan", "Finnegan", "Memphis", "Bowen", "Dominick", "Kolton", "Jamison", "Allen", "Philip", "Tate", "Peyton", "Jase", "Oakley", "Rhys", "Kyson", "Adan", "Esteban", "Dalton", "Gianni", "Callum", "Sage", "Alexis", "Milan", "Moises", "Jonas", "Uriel", "Colson", "Marcos", "Zaiden", "Hank", "Damon", "Hugo", "Ronin", "Royal", "Kamden", "Dexter", "Luciano", "Alonzo", "Augustus", "Kamari", "Eden", "Roberto", "Baker", "Bruce", "Kian", "Albert", "Frederick", "Mohamed", "Abram", "Omari", "Porter", "Enrique", "Alijah", "Francis", "Leonidas", "Zachariah", "Landen", "Wilder", "Apollo", "Santino", "Tatum", "Pierce", "Forrest", "Corey", "Derrick", "Isaias", "Kaison", "Kieran", "Arjun", "Gunnar", "Rocco", "Emmitt", "Abdiel", "Braylen", "Maximilian", "Skyler", "Phillip", "Benson", "Cannon", "Deacon", "Dorian", "Asa", "Moses", "Ayaan", "Jayson", "Raul", "Briggs", "Armando", "Nikolai", "Cassius", "Drew", "Rodrigo", "Raphael", "Danny", "Conrad", "Moshe", "Zyaire", "Julio", "Casey", "Ronald", "Scott", "Callan", "Roland", "Saul", "Jalen", "Brycen", "Ryland", "Lawrence", "Davis", "Rowen", "Zain", "Ermias", "Jaime", "Duke", "Stetson", "Alec", "Yusuf", "Case", "Trenton", "Callen", "Ariel", "Jasiah", "Soren", "Dennis", "Donald", "Keith", "Izaiah", "Lewis", "Kylan", "Kobe", "Makai", "Rayan", "Ford", "Zaire", "Landyn", "Roy", "Bo", "Chris", "Jamari", "Ares", "Mohammad", "Darius", "Drake", "Tripp", "Marcelo", "Samson", "Dustin", "Layton", "Gerardo", "Johan", "Kaysen", "Keaton", "Reece", "Chandler", "Lucca", "Mack", "Baylor", "Kannon", "Marvin", "Huxley", "Nixon", "Tony", "Cason", "Mauricio", "Quentin", "Edison", "Quincy", "Ahmed", "Finnley", "Justice", "Taylor", "Gustavo", "Brock", "Ahmad", "Kyree", "Arturo", "Nikolas", "Boston", "Sincere", "Alessandro", "Braylon", "Colby", "Leonard", "Ridge", "Trey", "Aden", "Leandro", "Sam", "Uriah", "Ty", "Sylas", "Axton", "Issac", "Fletcher", "Julien", "Wells", "Alden", "Vihaan", "Jamir", "Valentino", "Shepherd", "Keanu", "Hezekiah", "Lionel", "Kohen", "Zaid", "Alberto", "Neil", "Denver", "Aarav", "Brendan", "Dillon", "Koda", "Sutton", "Kingsley", "Sonny", "Alfredo", "Wilson", "Harry", "Jaziel", "Salvador", "Cullen", "Hamza", "Dariel", "Rex", "Zeke", "Mohammed", "Nelson", "Boone", "Ricky", "Santana", "Cayson", "Lance", "Raylan", "Lucian", "Eliel", "Alvin", "Jagger", "Braden", "Curtis", "Mathew", "Jimmy", "Kareem", "Archie", "Amos", "Quinton", "Yosef", "Bodie", "Jerry", "Langston", "Axl", "Stanley", "Clay", "Douglas", "Layne", "Titan", "Tomas", "Houston", "Darren", "Lachlan", "Kase", "Korbin", "Leighton", "Joziah", "Samir", "Watson", "Colten", "Roger", "Shiloh", "Tommy", "Mitchell", "Azariah", "Noe", "Talon", "Deandre", "Lochlan", "Joe", "Carmelo", "Otis", "Randy", "Byron", "Chaim", "Lennon", "Devon", "Nathanael", "Bruno", "Aryan", "Flynn", "Vicente", "Brixton", "Kyro", "Brennan", "Casen", "Kenzo", "Orlando", "Castiel", "Rayden", "Ben", "Grey", "Jedidiah", "Tadeo", "Morgan", "Augustine", "Mekhi", "Abdullah", "Ramon", "Saint", "Emery", "Maurice", "Jefferson", "Maximo", "Koa", "Ray", "Jamie", "Eddie", "Guillermo", "Onyx", "Thaddeus", "Wayne", "Hassan", "Alonso", "Dash", "Elisha", "Jaxxon", "Rohan", "Carl", "Kelvin", "Jon", "Larry", "Reese", "Aldo", "Marcel", "Melvin", "Yousef", "Aron", "Kace", "Vincenzo", "Kellan", "Miller", "Jakob", "Reign", "Kellen", "Kristopher", "Ernesto", "Briar", "Gary", "Trace", "Joey", "Clyde", "Enoch", "Jaxx", "Crosby", "Magnus", "Fisher", "Jadiel", "Bronson", "Eugene", "Lee", "Brecken", "Atreus", "Madden", "Khari", "Caspian", "Ishaan", "Kristian", "Westley", "Hugh", "Kamryn", "Musa", "Rey", "Thatcher", "Alfred", "Emory", "Kye", "Reyansh", "Yahir", "Cain", "Mordechai", "Zayd", "Demetrius", "Harley", "Felipe", "Louie", "Branson", "Graysen", "Allan", "Kole", "Harold", "Alvaro", "Harlan", "Amias", "Brett", "Khalid", "Misael", "Westin", "Zechariah", "Aydin", "Kaiser", "Lian", "Bryant", "Junior", "Legacy", "Ulises", "Bellamy", "Brayan", "Kody", "Ledger", "Eliseo", "Gordon", "London", "Rocky", "Valentin", "Terry", "Damari", "Trent", "Bentlee", "Canaan", "Gatlin", "Kiaan", "Franco", "Eithan", "Idris", "Krew", "Yehuda", "Marlon", "Rodney", "Creed", "Salvatore", "Stefan", "Tristen", "Adrien", "Jamal", "Judson", "Camilo", "Kenny", "Nova", "Robin", "Rudy", "Van", "Bjorn", "Brodie", "Mac", "Jacoby", "Sekani", "Vivaan", "Blaine", "Ira", "Ameer", "Dominik", "Alaric", "Dane", "Jeremias", "Kyng", "Reginald", "Bobby", "Kabir", "Jairo", "Alexzander", "Benicio", "Vance", "Wallace", "Zavier", "Billy", "Callahan", "Dakari", "Gerald", "Turner", "Bear", "Jabari", "Cory", "Fox", "Harlem", "Jakari", "Jeffery", "Maxton", "Ronnie", "Yisroel", "Zakai", "Bridger", "Remi", "Arian", "Blaze", "Forest", "Genesis", "Jerome", "Reuben", "Wesson", "Anders", "Banks", "Calum", "Dayton", "Kylen", "Dangelo", "Emir", "Malakhi", "Salem", "Blaise", "Tru", "Boden", "Kolten", "Kylo", "Aries", "Henrik", "Kalel", "Landry", "Marcellus", "Zahir", "Lyle", "Dario", "Rene", "Terrance", "Xzavier", "Alfonso", "Darian", "Kylian", "Maison", "Foster", "Keenan", "Yahya", "Heath", "Javion", "Jericho", "Aziel", "Darwin", "Marquis", "Mylo", "Ambrose", "Anakin", "Jordy", "Juelz", "Toby", "Yael", "Azrael", "Brentley", "Tristian", "Bode", "Jovanni", "Santos", "Alistair", "Braydon", "Kamdyn", "Marc", "Mayson", "Niklaus", "Simeon", "Colter", "Davion", "Leroy", "Ayan", "Dilan", "Ephraim", "Anson", "Merrick", "Wes", "Will", "Jaxen", "Maxim", "Howard", "Jad", "Jesiah", "Ignacio", "Zyon", "Ahmir", "Jair", "Mustafa", "Jermaine", "Yadiel", "Aayan", "Dhruv", "Seven", "Stone", "Rome" };

		public static RandomAddress[] Addresses = new RandomAddress[] {
			  new RandomAddress()
			  {
				Address = "777 Brockton Avenue",
				City = "Abington",
				State = "MA",
				Zip = "2351"
			  },
			  new RandomAddress()
			  {
				Address = "30 Memorial Drive",
				City = "Avon",
				State = "MA",
				Zip = "2322"
			  },
			  new RandomAddress()
			  {
				Address = "250 Hartford Avenue",
				City = "Bellingham",
				State = "MA",
				Zip = "2019"
			  },
			  new RandomAddress()
			  {
				Address = "700 Oak Street",
				City = "Brockton",
				State = "MA",
				Zip = "2301"
			  },
			  new RandomAddress()
			  {
				Address = "66-4 Parkhurst Rd",
				City = "Chelmsford",
				State = "MA",
				Zip = "1824"
			  },
			  new RandomAddress()
			  {
				Address = "591 Memorial Dr",
				City = "Chicopee",
				State = "MA",
				Zip = "1020"
			  },
			  new RandomAddress()
			  {
				Address = "55 Brooksby Village Way",
				City = "Danvers",
				State = "MA",
				Zip = "1923"
			  },
			  new RandomAddress()
			  {
				Address = "137 Teaticket Hwy",
				City = "East Falmouth",
				State = "MA",
				Zip = "2536"
			  },
			  new RandomAddress()
			  {
				Address = "42 Fairhaven Commons Way",
				City = "Fairhaven",
				State = "MA",
				Zip = "2719"
			  },
			  new RandomAddress()
			  {
				Address = "374 William S Canning Blvd",
				City = "Fall River",
				State = "MA",
				Zip = "2721"
			  },
			  new RandomAddress()
			  {
				Address = "121 Worcester Rd",
				City = "Framingham",
				State = "MA",
				Zip = "1701"
			  },
			  new RandomAddress()
			  {
				Address = "677 Timpany Blvd",
				City = "Gardner",
				State = "MA",
				Zip = "1440"
			  },
			  new RandomAddress()
			  {
				Address = "337 Russell St",
				City = "Hadley",
				State = "MA",
				Zip = "1035"
			  },
			  new RandomAddress()
			  {
				Address = "295 Plymouth Street",
				City = "Halifax",
				State = "MA",
				Zip = "2338"
			  },
			  new RandomAddress()
			  {
				Address = "1775 Washington St",
				City = "Hanover",
				State = "MA",
				Zip = "2339"
			  },
			  new RandomAddress()
			  {
				Address = "280 Washington Street",
				City = "Hudson",
				State = "MA",
				Zip = "1749"
			  },
			  new RandomAddress()
			  {
				Address = "20 Soojian Dr",
				City = "Leicester",
				State = "MA",
				Zip = "1524"
			  },
			  new RandomAddress()
			  {
				Address = "11 Jungle Road",
				City = "Leominster",
				State = "MA",
				Zip = "1453"
			  },
			  new RandomAddress()
			  {
				Address = "301 Massachusetts Ave",
				City = "Lunenburg",
				State = "MA",
				Zip = "1462"
			  },
			  new RandomAddress()
			  {
				Address = "780 Lynnway",
				City = "Lynn",
				State = "MA",
				Zip = "1905"
			  },
			  new RandomAddress()
			  {
				Address = "70 Pleasant Valley Street",
				City = "Methuen",
				State = "MA",
				Zip = "1844"
			  },
			  new RandomAddress()
			  {
				Address = "830 Curran Memorial Hwy",
				City = "North Adams",
				State = "MA",
				Zip = "1247"
			  },
			  new RandomAddress()
			  {
				Address = "1470 S Washington St",
				City = "North Attleboro",
				State = "MA",
				Zip = "2760"
			  },
			  new RandomAddress()
			  {
				Address = "506 State Road",
				City = "North Dartmouth",
				State = "MA",
				Zip = "2747"
			  },
			  new RandomAddress()
			  {
				Address = "742 Main Street",
				City = "North Oxford",
				State = "MA",
				Zip = "1537"
			  },
			  new RandomAddress()
			  {
				Address = "72 Main St",
				City = "North Reading",
				State = "MA",
				Zip = "1864"
			  },
			  new RandomAddress()
			  {
				Address = "200 Otis Street",
				City = "Northborough",
				State = "MA",
				Zip = "1532"
			  },
			  new RandomAddress()
			  {
				Address = "180 North King Street",
				City = "Northhampton",
				State = "MA",
				Zip = "1060"
			  },
			  new RandomAddress()
			  {
				Address = "555 East Main St",
				City = "Orange",
				State = "MA",
				Zip = "1364"
			  },
			  new RandomAddress()
			  {
				Address = "555 Hubbard Ave-Suite 12",
				City = "Pittsfield",
				State = "MA",
				Zip = "1201"
			  },
			  new RandomAddress()
			  {
				Address = "300 Colony Place",
				City = "Plymouth",
				State = "MA",
				Zip = "2360"
			  },
			  new RandomAddress()
			  {
				Address = "301 Falls Blvd",
				City = "Quincy",
				State = "MA",
				Zip = "2169"
			  },
			  new RandomAddress()
			  {
				Address = "36 Paramount Drive",
				City = "Raynham",
				State = "MA",
				Zip = "2767"
			  },
			  new RandomAddress()
			  {
				Address = "450 Highland Ave",
				City = "Salem",
				State = "MA",
				Zip = "1970"
			  },
			  new RandomAddress()
			  {
				Address = "1180 Fall River Avenue",
				City = "Seekonk",
				State = "MA",
				Zip = "2771"
			  },
			  new RandomAddress()
			  {
				Address = "1105 Boston Road",
				City = "Springfield",
				State = "MA",
				Zip = "1119"
			  },
			  new RandomAddress()
			  {
				Address = "100 Charlton Road",
				City = "Sturbridge",
				State = "MA",
				Zip = "1566"
			  },
			  new RandomAddress()
			  {
				Address = "262 Swansea Mall Dr",
				City = "Swansea",
				State = "MA",
				Zip = "2777"
			  },
			  new RandomAddress()
			  {
				Address = "333 Main Street",
				City = "Tewksbury",
				State = "MA",
				Zip = "1876"
			  },
			  new RandomAddress()
			  {
				Address = "550 Providence Hwy",
				City = "Walpole",
				State = "MA",
				Zip = "2081"
			  },
			  new RandomAddress()
			  {
				Address = "352 Palmer Road",
				City = "Ware",
				State = "MA",
				Zip = "1082"
			  },
			  new RandomAddress()
			  {
				Address = "3005 Cranberry Hwy Rt 6 28",
				City = "Wareham",
				State = "MA",
				Zip = "2538"
			  },
			  new RandomAddress()
			  {
				Address = "250 Rt 59",
				City = "Airmont",
				State = "NY",
				Zip = "10901"
			  },
			  new RandomAddress()
			  {
				Address = "141 Washington Ave Extension",
				City = "Albany",
				State = "NY",
				Zip = "12205"
			  },
			  new RandomAddress()
			  {
				Address = "13858 Rt 31 W",
				City = "Albion",
				State = "NY",
				Zip = "14411"
			  },
			  new RandomAddress()
			  {
				Address = "2055 Niagara Falls Blvd",
				City = "Amherst",
				State = "NY",
				Zip = "14228"
			  },
			  new RandomAddress()
			  {
				Address = "101 Sanford Farm Shpg Center",
				City = "Amsterdam",
				State = "NY",
				Zip = "12010"
			  },
			  new RandomAddress()
			  {
				Address = "297 Grant Avenue",
				City = "Auburn",
				State = "NY",
				Zip = "13021"
			  },
			  new RandomAddress()
			  {
				Address = "4133 Veterans Memorial Drive",
				City = "Batavia",
				State = "NY",
				Zip = "14020"
			  },
			  new RandomAddress()
			  {
				Address = "6265 Brockport Spencerport Rd",
				City = "Brockport",
				State = "NY",
				Zip = "14420"
			  },
			  new RandomAddress()
			  {
				Address = "5399 W Genesse St",
				City = "Camillus",
				State = "NY",
				Zip = "13031"
			  },
			  new RandomAddress()
			  {
				Address = "3191 County rd 10",
				City = "Canandaigua",
				State = "NY",
				Zip = "14424"
			  },
			  new RandomAddress()
			  {
				Address = "30 Catskill",
				City = "Catskill",
				State = "NY",
				Zip = "12414"
			  },
			  new RandomAddress()
			  {
				Address = "161 Centereach Mall",
				City = "Centereach",
				State = "NY",
				Zip = "11720"
			  },
			  new RandomAddress()
			  {
				Address = "3018 East Ave",
				City = "Central Square",
				State = "NY",
				Zip = "13036"
			  },
			  new RandomAddress()
			  {
				Address = "100 Thruway Plaza",
				City = "Cheektowaga",
				State = "NY",
				Zip = "14225"
			  },
			  new RandomAddress()
			  {
				Address = "8064 Brewerton Rd",
				City = "Cicero",
				State = "NY",
				Zip = "13039"
			  },
			  new RandomAddress()
			  {
				Address = "5033 Transit Road",
				City = "Clarence",
				State = "NY",
				Zip = "14031"
			  },
			  new RandomAddress()
			  {
				Address = "3949 Route 31",
				City = "Clay",
				State = "NY",
				Zip = "13041"
			  },
			  new RandomAddress()
			  {
				Address = "139 Merchant Place",
				City = "Cobleskill",
				State = "NY",
				Zip = "12043"
			  },
			  new RandomAddress()
			  {
				Address = "85 Crooked Hill Road",
				City = "Commack",
				State = "NY",
				Zip = "11725"
			  },
			  new RandomAddress()
			  {
				Address = "872 Route 13",
				City = "Cortlandville",
				State = "NY",
				Zip = "13045"
			  },
			  new RandomAddress()
			  {
				Address = "279 Troy Road",
				City = "East Greenbush",
				State = "NY",
				Zip = "12061"
			  },
			  new RandomAddress()
			  {
				Address = "2465 Hempstead Turnpike",
				City = "East Meadow",
				State = "NY",
				Zip = "11554"
			  },
			  new RandomAddress()
			  {
				Address = "6438 Basile Rowe",
				City = "East Syracuse",
				State = "NY",
				Zip = "13057"
			  },
			  new RandomAddress()
			  {
				Address = "25737 US Rt 11",
				City = "Evans Mills",
				State = "NY",
				Zip = "13637"
			  },
			  new RandomAddress()
			  {
				Address = "901 Route 110",
				City = "Farmingdale",
				State = "NY",
				Zip = "11735"
			  },
			  new RandomAddress()
			  {
				Address = "2400 Route 9",
				City = "Fishkill",
				State = "NY",
				Zip = "12524"
			  },
			  new RandomAddress()
			  {
				Address = "10401 Bennett Road",
				City = "Fredonia",
				State = "NY",
				Zip = "14063"
			  },
			  new RandomAddress()
			  {
				Address = "1818 State Route 3",
				City = "Fulton",
				State = "NY",
				Zip = "13069"
			  },
			  new RandomAddress()
			  {
				Address = "4300 Lakeville Road",
				City = "Geneseo",
				State = "NY",
				Zip = "14454"
			  },
			  new RandomAddress()
			  {
				Address = "990 Route 5 20",
				City = "Geneva",
				State = "NY",
				Zip = "14456"
			  },
			  new RandomAddress()
			  {
				Address = "311 RT 9W",
				City = "Glenmont",
				State = "NY",
				Zip = "12077"
			  },
			  new RandomAddress()
			  {
				Address = "200 Dutch Meadows Ln",
				City = "Glenville",
				State = "NY",
				Zip = "12302"
			  },
			  new RandomAddress()
			  {
				Address = "100 Elm Ridge Center Dr",
				City = "Greece",
				State = "NY",
				Zip = "14626"
			  },
			  new RandomAddress()
			  {
				Address = "1549 Rt 9",
				City = "Halfmoon",
				State = "NY",
				Zip = "12065"
			  },
			  new RandomAddress()
			  {
				Address = "5360 Southwestern Blvd",
				City = "Hamburg",
				State = "NY",
				Zip = "14075"
			  },
			  new RandomAddress()
			  {
				Address = "103 North Caroline St",
				City = "Herkimer",
				State = "NY",
				Zip = "13350"
			  },
			  new RandomAddress()
			  {
				Address = "1000 State Route 36",
				City = "Hornell",
				State = "NY",
				Zip = "14843"
			  },
			  new RandomAddress()
			  {
				Address = "1400 County Rd 64",
				City = "Horseheads",
				State = "NY",
				Zip = "14845"
			  },
			  new RandomAddress()
			  {
				Address = "135 Fairgrounds Memorial Pkwy",
				City = "Ithaca",
				State = "NY",
				Zip = "14850"
			  },
			  new RandomAddress()
			  {
				Address = "2 Gannett Dr",
				City = "Johnson City",
				State = "NY",
				Zip = "13790"
			  },
			  new RandomAddress()
			  {
				Address = "233 5th Ave Ext",
				City = "Johnstown",
				State = "NY",
				Zip = "12095"
			  },
			  new RandomAddress()
			  {
				Address = "601 Frank Stottile Blvd",
				City = "Kingston",
				State = "NY",
				Zip = "12401"
			  },
			  new RandomAddress()
			  {
				Address = "350 E Fairmount Ave",
				City = "Lakewood",
				State = "NY",
				Zip = "14750"
			  },
			  new RandomAddress()
			  {
				Address = "4975 Transit Rd",
				City = "Lancaster",
				State = "NY",
				Zip = "14086"
			  },
			  new RandomAddress()
			  {
				Address = "579 Troy-Schenectady Road",
				City = "Latham",
				State = "NY",
				Zip = "12110"
			  },
			  new RandomAddress()
			  {
				Address = "5783 So Transit Road",
				City = "Lockport",
				State = "NY",
				Zip = "14094"
			  },
			  new RandomAddress()
			  {
				Address = "7155 State Rt 12 S",
				City = "Lowville",
				State = "NY",
				Zip = "13367"
			  },
			  new RandomAddress()
			  {
				Address = "425 Route 31",
				City = "Macedon",
				State = "NY",
				Zip = "14502"
			  },
			  new RandomAddress()
			  {
				Address = "3222 State Rt 11",
				City = "Malone",
				State = "NY",
				Zip = "12953"
			  },
			  new RandomAddress()
			  {
				Address = "200 Sunrise Mall",
				City = "Massapequa",
				State = "NY",
				Zip = "11758"
			  },
			  new RandomAddress()
			  {
				Address = "43 Stephenville St",
				City = "Massena",
				State = "NY",
				Zip = "13662"
			  },
			  new RandomAddress()
			  {
				Address = "750 Middle Country Road",
				City = "Middle Island",
				State = "NY",
				Zip = "11953"
			  },
			  new RandomAddress()
			  {
				Address = "470 Route 211 East",
				City = "Middletown",
				State = "NY",
				Zip = "10940"
			  },
			  new RandomAddress()
			  {
				Address = "3133 E Main St",
				City = "Mohegan Lake",
				State = "NY",
				Zip = "10547"
			  },
			  new RandomAddress()
			  {
				Address = "288 Larkin",
				City = "Monroe",
				State = "NY",
				Zip = "10950"
			  },
			  new RandomAddress()
			  {
				Address = "41 Anawana Lake Road",
				City = "Monticello",
				State = "NY",
				Zip = "12701"
			  },
			  new RandomAddress()
			  {
				Address = "4765 Commercial Drive",
				City = "New Hartford",
				State = "NY",
				Zip = "13413"
			  },
			  new RandomAddress()
			  {
				Address = "1201 Rt 300",
				City = "Newburgh",
				State = "NY",
				Zip = "12550"
			  },
			  new RandomAddress()
			  {
				Address = "255 W Main St",
				City = "Avon",
				State = "CT",
				Zip = "6001"
			  },
			  new RandomAddress()
			  {
				Address = "120 Commercial Parkway",
				City = "Branford",
				State = "CT",
				Zip = "6405"
			  },
			  new RandomAddress()
			  {
				Address = "1400 Farmington Ave",
				City = "Bristol",
				State = "CT",
				Zip = "6010"
			  },
			  new RandomAddress()
			  {
				Address = "161 Berlin Road",
				City = "Cromwell",
				State = "CT",
				Zip = "6416"
			  },
			  new RandomAddress()
			  {
				Address = "67 Newton Rd",
				City = "Danbury",
				State = "CT",
				Zip = "6810"
			  },
			  new RandomAddress()
			  {
				Address = "656 New Haven Ave",
				City = "Derby",
				State = "CT",
				Zip = "6418"
			  },
			  new RandomAddress()
			  {
				Address = "69 Prospect Hill Road",
				City = "East Windsor",
				State = "CT",
				Zip = "6088"
			  },
			  new RandomAddress()
			  {
				Address = "150 Gold Star Hwy",
				City = "Groton",
				State = "CT",
				Zip = "6340"
			  },
			  new RandomAddress()
			  {
				Address = "900 Boston Post Road",
				City = "Guilford",
				State = "CT",
				Zip = "6437"
			  },
			  new RandomAddress()
			  {
				Address = "2300 Dixwell Ave",
				City = "Hamden",
				State = "CT",
				Zip = "6514"
			  },
			  new RandomAddress()
			  {
				Address = "495 Flatbush Ave",
				City = "Hartford",
				State = "CT",
				Zip = "6106"
			  },
			  new RandomAddress()
			  {
				Address = "180 River Rd",
				City = "Lisbon",
				State = "CT",
				Zip = "6351"
			  },
			  new RandomAddress()
			  {
				Address = "420 Buckland Hills Dr",
				City = "Manchester",
				State = "CT",
				Zip = "6040"
			  },
			  new RandomAddress()
			  {
				Address = "1365 Boston Post Road",
				City = "Milford",
				State = "CT",
				Zip = "6460"
			  },
			  new RandomAddress()
			  {
				Address = "1100 New Haven Road",
				City = "Naugatuck",
				State = "CT",
				Zip = "6770"
			  },
			  new RandomAddress()
			  {
				Address = "315 Foxon Blvd",
				City = "New Haven",
				State = "CT",
				Zip = "6513"
			  },
			  new RandomAddress()
			  {
				Address = "164 Danbury Rd",
				City = "New Milford",
				State = "CT",
				Zip = "6776"
			  },
			  new RandomAddress()
			  {
				Address = "3164 Berlin Turnpike",
				City = "Newington",
				State = "CT",
				Zip = "6111"
			  },
			  new RandomAddress()
			  {
				Address = "474 Boston Post Road",
				City = "North Windham",
				State = "CT",
				Zip = "6256"
			  },
			  new RandomAddress()
			  {
				Address = "650 Main Ave",
				City = "Norwalk",
				State = "CT",
				Zip = "6851"
			  },
			  new RandomAddress()
			  {
				Address = "680 Connecticut Avenue",
				City = "Norwalk",
				State = "CT",
				Zip = "6854"
			  },
			  new RandomAddress()
			  {
				Address = "220 Salem Turnpike",
				City = "Norwich",
				State = "CT",
				Zip = "6360"
			  },
			  new RandomAddress()
			  {
				Address = "655 Boston Post Rd",
				City = "Old Saybrook",
				State = "CT",
				Zip = "6475"
			  },
			  new RandomAddress()
			  {
				Address = "625 School Street",
				City = "Putnam",
				State = "CT",
				Zip = "6260"
			  },
			  new RandomAddress()
			  {
				Address = "80 Town Line Rd",
				City = "Rocky Hill",
				State = "CT",
				Zip = "6067"
			  },
			  new RandomAddress()
			  {
				Address = "465 Bridgeport Avenue",
				City = "Shelton",
				State = "CT",
				Zip = "6484"
			  },
			  new RandomAddress()
			  {
				Address = "235 Queen St",
				City = "Southington",
				State = "CT",
				Zip = "6489"
			  },
			  new RandomAddress()
			  {
				Address = "150 Barnum Avenue Cutoff",
				City = "Stratford",
				State = "CT",
				Zip = "6614"
			  },
			  new RandomAddress()
			  {
				Address = "970 Torringford Street",
				City = "Torrington",
				State = "CT",
				Zip = "6790"
			  },
			  new RandomAddress()
			  {
				Address = "844 No Colony Road",
				City = "Wallingford",
				State = "CT",
				Zip = "6492"
			  },
			  new RandomAddress()
			  {
				Address = "910 Wolcott St",
				City = "Waterbury",
				State = "CT",
				Zip = "6705"
			  },
			  new RandomAddress()
			  {
				Address = "155 Waterford Parkway No",
				City = "Waterford",
				State = "CT",
				Zip = "6385"
			  },
			  new RandomAddress()
			  {
				Address = "515 Sawmill Road",
				City = "West Haven",
				State = "CT",
				Zip = "6516"
			  },
			  new RandomAddress()
			  {
				Address = "2473 Hackworth Road",
				City = "Adamsville",
				State = "AL",
				Zip = "35005"
			  },
			  new RandomAddress()
			  {
				Address = "630 Coonial Promenade Pkwy",
				City = "Alabaster",
				State = "AL",
				Zip = "35007"
			  },
			  new RandomAddress()
			  {
				Address = "2643 Hwy 280 West",
				City = "Alexander City",
				State = "AL",
				Zip = "35010"
			  },
			  new RandomAddress()
			  {
				Address = "540 West Bypass",
				City = "Andalusia",
				State = "AL",
				Zip = "36420"
			  },
			  new RandomAddress()
			  {
				Address = "5560 Mcclellan Blvd",
				City = "Anniston",
				State = "AL",
				Zip = "36206"
			  },
			  new RandomAddress()
			  {
				Address = "1450 No Brindlee Mtn Pkwy",
				City = "Arab",
				State = "AL",
				Zip = "35016"
			  },
			  new RandomAddress()
			  {
				Address = "1011 US Hwy 72 East",
				City = "Athens",
				State = "AL",
				Zip = "35611"
			  },
			  new RandomAddress()
			  {
				Address = "973 Gilbert Ferry Road Se",
				City = "Attalla",
				State = "AL",
				Zip = "35954"
			  },
			  new RandomAddress()
			  {
				Address = "1717 South College Street",
				City = "Auburn",
				State = "AL",
				Zip = "36830"
			  },
			  new RandomAddress()
			  {
				Address = "701 Mcmeans Ave",
				City = "Bay Minette",
				State = "AL",
				Zip = "36507"
			  },
			  new RandomAddress()
			  {
				Address = "750 Academy Drive",
				City = "Bessemer",
				State = "AL",
				Zip = "35022"
			  },
			  new RandomAddress()
			  {
				Address = "312 Palisades Blvd",
				City = "Birmingham",
				State = "AL",
				Zip = "35209"
			  },
			  new RandomAddress()
			  {
				Address = "1600 Montclair Rd",
				City = "Birmingham",
				State = "AL",
				Zip = "35210"
			  },
			  new RandomAddress()
			  {
				Address = "5919 Trussville Crossings Pkwy",
				City = "Birmingham",
				State = "AL",
				Zip = "35235"
			  },
			  new RandomAddress()
			  {
				Address = "9248 Parkway East",
				City = "Birmingham",
				State = "AL",
				Zip = "35206"
			  },
			  new RandomAddress()
			  {
				Address = "1972 Hwy 431",
				City = "Boaz",
				State = "AL",
				Zip = "35957"
			  },
			  new RandomAddress()
			  {
				Address = "10675 Hwy 5",
				City = "Brent",
				State = "AL",
				Zip = "35034"
			  },
			  new RandomAddress()
			  {
				Address = "2041 Douglas Avenue",
				City = "Brewton",
				State = "AL",
				Zip = "36426"
			  },
			  new RandomAddress()
			  {
				Address = "5100 Hwy 31",
				City = "Calera",
				State = "AL",
				Zip = "35040"
			  },
			  new RandomAddress()
			  {
				Address = "1916 Center Point Rd",
				City = "Center Point",
				State = "AL",
				Zip = "35215"
			  },
			  new RandomAddress()
			  {
				Address = "1950 W Main St",
				City = "Centre",
				State = "AL",
				Zip = "35960"
			  },
			  new RandomAddress()
			  {
				Address = "16077 Highway 280",
				City = "Chelsea",
				State = "AL",
				Zip = "35043"
			  },
			  new RandomAddress()
			  {
				Address = "1415 7Th Street South",
				City = "Clanton",
				State = "AL",
				Zip = "35045"
			  },
			  new RandomAddress()
			  {
				Address = "626 Olive Street Sw",
				City = "Cullman",
				State = "AL",
				Zip = "35055"
			  },
			  new RandomAddress()
			  {
				Address = "27520 Hwy 98",
				City = "Daphne",
				State = "AL",
				Zip = "36526"
			  },
			  new RandomAddress()
			  {
				Address = "2800 Spring Avn SW",
				City = "Decatur",
				State = "AL",
				Zip = "35603"
			  },
			  new RandomAddress()
			  {
				Address = "969 Us Hwy 80 West",
				City = "Demopolis",
				State = "AL",
				Zip = "36732"
			  },
			  new RandomAddress()
			  {
				Address = "3300 South Oates Street",
				City = "Dothan",
				State = "AL",
				Zip = "36301"
			  },
			  new RandomAddress()
			  {
				Address = "4310 Montgomery Hwy",
				City = "Dothan",
				State = "AL",
				Zip = "36303"
			  },
			  new RandomAddress()
			  {
				Address = "600 Boll Weevil Circle",
				City = "Enterprise",
				State = "AL",
				Zip = "36330"
			  },
			  new RandomAddress()
			  {
				Address = "3176 South Eufaula Avenue",
				City = "Eufaula",
				State = "AL",
				Zip = "36027"
			  },
			  new RandomAddress()
			  {
				Address = "7100 Aaron Aronov Drive",
				City = "Fairfield",
				State = "AL",
				Zip = "35064"
			  },
			  new RandomAddress()
			  {
				Address = "10040 County Road 48",
				City = "Fairhope",
				State = "AL",
				Zip = "36533"
			  },
			  new RandomAddress()
			  {
				Address = "3186 Hwy 171 North",
				City = "Fayette",
				State = "AL",
				Zip = "35555"
			  },
			  new RandomAddress()
			  {
				Address = "3100 Hough Rd",
				City = "Florence",
				State = "AL",
				Zip = "35630"
			  },
			  new RandomAddress()
			  {
				Address = "2200 South Mckenzie St",
				City = "Foley",
				State = "AL",
				Zip = "36535"
			  },
			  new RandomAddress()
			  {
				Address = "2001 Glenn Bldv Sw",
				City = "Fort Payne",
				State = "AL",
				Zip = "35968"
			  },
			  new RandomAddress()
			  {
				Address = "340 East Meighan Blvd",
				City = "Gadsden",
				State = "AL",
				Zip = "35903"
			  },
			  new RandomAddress()
			  {
				Address = "890 Odum Road",
				City = "Gardendale",
				State = "AL",
				Zip = "35071"
			  },
			  new RandomAddress()
			  {
				Address = "1608 W Magnolia Ave",
				City = "Geneva",
				State = "AL",
				Zip = "36340"
			  },
			  new RandomAddress()
			  {
				Address = "501 Willow Lane",
				City = "Greenville",
				State = "AL",
				Zip = "36037"
			  },
			  new RandomAddress()
			  {
				Address = "170 Fort Morgan Road",
				City = "Gulf Shores",
				State = "AL",
				Zip = "36542"
			  },
			  new RandomAddress()
			  {
				Address = "11697 US Hwy 431",
				City = "Guntersville",
				State = "AL",
				Zip = "35976"
			  },
			  new RandomAddress()
			  {
				Address = "42417 Hwy 195",
				City = "Haleyville",
				State = "AL",
				Zip = "35565"
			  },
			  new RandomAddress()
			  {
				Address = "1706 Military Street South",
				City = "Hamilton",
				State = "AL",
				Zip = "35570"
			  },
			  new RandomAddress()
			  {
				Address = "1201 Hwy 31 NW",
				City = "Hartselle",
				State = "AL",
				Zip = "35640"
			  },
			  new RandomAddress()
			  {
				Address = "209 Lakeshore Parkway",
				City = "Homewood",
				State = "AL",
				Zip = "35209"
			  },
			  new RandomAddress()
			  {
				Address = "2780 John Hawkins Pkwy",
				City = "Hoover",
				State = "AL",
				Zip = "35244"
			  },
			  new RandomAddress()
			  {
				Address = "5335 Hwy 280 South",
				City = "Hoover",
				State = "AL",
				Zip = "35242"
			  },
			  new RandomAddress()
			  {
				Address = "1007 Red Farmer Drive",
				City = "Hueytown",
				State = "AL",
				Zip = "35023"
			  },
			  new RandomAddress()
			  {
				Address = "2900 S Mem PkwyDrake Ave",
				City = "Huntsville",
				State = "AL",
				Zip = "35801"
			  },
			  new RandomAddress()
			  {
				Address = "11610 Memorial Pkwy South",
				City = "Huntsville",
				State = "AL",
				Zip = "35803"
			  },
			  new RandomAddress()
			  {
				Address = "2200 Sparkman Drive",
				City = "Huntsville",
				State = "AL",
				Zip = "35810"
			  },
			  new RandomAddress()
			  {
				Address = "330 Sutton Rd",
				City = "Huntsville",
				State = "AL",
				Zip = "35763"
			  },
			  new RandomAddress()
			  {
				Address = "6140A Univ Drive",
				City = "Huntsville",
				State = "AL",
				Zip = "35806"
			  },
			  new RandomAddress()
			  {
				Address = "4206 N College Ave",
				City = "Jackson",
				State = "AL",
				Zip = "36545"
			  },
			  new RandomAddress()
			  {
				Address = "1625 Pelham South",
				City = "Jacksonville",
				State = "AL",
				Zip = "36265"
			  },
			  new RandomAddress()
			  {
				Address = "1801 Hwy 78 East",
				City = "Jasper",
				State = "AL",
				Zip = "35501"
			  },
			  new RandomAddress()
			  {
				Address = "8551 Whitfield Ave",
				City = "Leeds",
				State = "AL",
				Zip = "35094"
			  },
			  new RandomAddress()
			  {
				Address = "8650 Madison Blvd",
				City = "Madison",
				State = "AL",
				Zip = "35758"
			  },
			  new RandomAddress()
			  {
				Address = "145 Kelley Blvd",
				City = "Millbrook",
				State = "AL",
				Zip = "36054"
			  },
			  new RandomAddress()
			  {
				Address = "1970 S University Blvd",
				City = "Mobile",
				State = "AL",
				Zip = "36609"
			  },
			  new RandomAddress()
			  {
				Address = "6350 Cottage Hill Road",
				City = "Mobile",
				State = "AL",
				Zip = "36609"
			  },
			  new RandomAddress()
			  {
				Address = "101 South Beltline Highway",
				City = "Mobile",
				State = "AL",
				Zip = "36606"
			  },
			  new RandomAddress()
			  {
				Address = "2500 Dawes Road",
				City = "Mobile",
				State = "AL",
				Zip = "36695"
			  },
			  new RandomAddress()
			  {
				Address = "5245 Rangeline Service Rd",
				City = "Mobile",
				State = "AL",
				Zip = "36619"
			  },
			  new RandomAddress()
			  {
				Address = "685 Schillinger Rd",
				City = "Mobile",
				State = "AL",
				Zip = "36695"
			  },
			  new RandomAddress()
			  {
				Address = "3371 S Alabama Ave",
				City = "Monroeville",
				State = "AL",
				Zip = "36460"
			  },
			  new RandomAddress()
			  {
				Address = "10710 Chantilly Pkwy",
				City = "Montgomery",
				State = "AL",
				Zip = "36117"
			  },
			  new RandomAddress()
			  {
				Address = "3801 Eastern Blvd",
				City = "Montgomery",
				State = "AL",
				Zip = "36116"
			  },
			  new RandomAddress()
			  {
				Address = "6495 Atlanta Hwy",
				City = "Montgomery",
				State = "AL",
				Zip = "36117"
			  },
			  new RandomAddress()
			  {
				Address = "851 Ann St",
				City = "Montgomery",
				State = "AL",
				Zip = "36107"
			  },
			  new RandomAddress()
			  {
				Address = "15445 Highway 24",
				City = "Moulton",
				State = "AL",
				Zip = "35650"
			  },
			  new RandomAddress()
			  {
				Address = "517 West Avalon Ave",
				City = "Muscle Shoals",
				State = "AL",
				Zip = "35661"
			  },
			  new RandomAddress()
			  {
				Address = "5710 Mcfarland Blvd",
				City = "Northport",
				State = "AL",
				Zip = "35476"
			  },
			  new RandomAddress()
			  {
				Address = "2453 2Nd Avenue East",
				City = "Oneonta",
				State = "AL",
				Zip = "35121"
			  },
			  new RandomAddress()
			  {
				Address = "2900 Pepperrell Pkwy",
				City = "Opelika",
				State = "AL",
				Zip = "36801"
			  },
			  new RandomAddress()
			  {
				Address = "92 Plaza Lane",
				City = "Oxford",
				State = "AL",
				Zip = "36203"
			  },
			  new RandomAddress()
			  {
				Address = "1537 Hwy 231 South",
				City = "Ozark",
				State = "AL",
				Zip = "36360"
			  },
			  new RandomAddress()
			  {
				Address = "2181 Pelham Pkwy",
				City = "Pelham",
				State = "AL",
				Zip = "35124"
			  },
			  new RandomAddress()
			  {
				Address = "165 Vaughan Ln",
				City = "Pell City",
				State = "AL",
				Zip = "35125"
			  },
			  new RandomAddress()
			  {
				Address = "3700 Hwy 280-431 N",
				City = "Phenix City",
				State = "AL",
				Zip = "36867"
			  },
			  new RandomAddress()
			  {
				Address = "1903 Cobbs Ford Rd",
				City = "Prattville",
				State = "AL",
				Zip = "36066"
			  },
			  new RandomAddress()
			  {
				Address = "4180 Us Hwy 431",
				City = "Roanoke",
				State = "AL",
				Zip = "36274"
			  },
			  new RandomAddress()
			  {
				Address = "13675 Hwy 43",
				City = "Russellville",
				State = "AL",
				Zip = "35653"
			  },
			  new RandomAddress()
			  {
				Address = "1095 Industrial Pkwy",
				City = "Saraland",
				State = "AL",
				Zip = "36571"
			  },
			  new RandomAddress()
			  {
				Address = "24833 Johnt Reidprkw",
				City = "Scottsboro",
				State = "AL",
				Zip = "35768"
			  },
			  new RandomAddress()
			  {
				Address = "1501 Hwy 14 East",
				City = "Selma",
				State = "AL",
				Zip = "36703"
			  },
			  new RandomAddress()
			  {
				Address = "7855 Moffett Rd",
				City = "Semmes",
				State = "AL",
				Zip = "36575"
			  },
			  new RandomAddress()
			  {
				Address = "150 Springville Station Blvd",
				City = "Springville",
				State = "AL",
				Zip = "35146"
			  },
			  new RandomAddress()
			  {
				Address = "690 Hwy 78",
				City = "Sumiton",
				State = "AL",
				Zip = "35148"
			  },
			  new RandomAddress()
			  {
				Address = "41301 US Hwy 280",
				City = "Sylacauga",
				State = "AL",
				Zip = "35150"
			  },
			  new RandomAddress()
			  {
				Address = "214 Haynes Street",
				City = "Talladega",
				State = "AL",
				Zip = "35160"
			  },
			  new RandomAddress()
			  {
				Address = "1300 Gilmer Ave",
				City = "Tallassee",
				State = "AL",
				Zip = "36078"
			  },
			  new RandomAddress()
			  {
				Address = "34301 Hwy 43",
				City = "Thomasville",
				State = "AL",
				Zip = "36784"
			  },
			  new RandomAddress()
			  {
				Address = "1420 Us 231 South",
				City = "Troy",
				State = "AL",
				Zip = "36081"
			  },
			  new RandomAddress()
			  {
				Address = "1501 Skyland Blvd E",
				City = "Tuscaloosa",
				State = "AL",
				Zip = "35405"
			  },
			  new RandomAddress()
			  {
				Address = "3501 20th Av",
				City = "Valley",
				State = "AL",
				Zip = "36854"
			  },
			  new RandomAddress()
			  {
				Address = "1300 Montgomery Highway",
				City = "Vestavia Hills",
				State = "AL",
				Zip = "35216"
			  },
			  new RandomAddress()
			  {
				Address = "4538 Us Hwy 231",
				City = "Wetumpka",
				State = "AL",
				Zip = "36092"
			  },
			  new RandomAddress()
			  {
				Address = "2575 Us Hwy 43",
				City = "Winfield",
				State = "AL",
				Zip = "35594"
			  }
		};

	}
}
