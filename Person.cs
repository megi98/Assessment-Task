using System;

namespace AssessmentTask
{
	public class Person
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Country { get; set; }
		public string City { get; set; }
		public int Score { get; set; }

		public Person(string firstName, string lastName, string country, string city, int score)
        {
			this.FirstName = firstName;
			this.LastName = lastName;
			this.Country = country;
			this.City = city;
			this.Score = score;
        }

	}
}
