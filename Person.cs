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
			FirstName = firstName;
			LastName = lastName;
			Country = country;
			City = city;
			Score = score;
        }
	}
}
