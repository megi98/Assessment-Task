using System.Collections.Generic;
using System.Linq;

namespace AssessmentTask
{
	public class Sample
	{
		public string Country { get; set; }
		public double Average { get; set; }
		public double Median { get; set; }
		public int Max { get; set; }
		public string MaxPerson { get; set; }
		public int Min { get; set; }
		public string MinPerson { get; set; }
		public int RecordCount { get; set; }

		public Sample()
		{
			Country = "";
			Average = 0;
			Median = 0;
			Max = 0;
			MaxPerson = "";
			Min = 0;
			MinPerson = "";
			RecordCount = 0;
		}

		public Sample(string country, double average, double median, int max, string maxPerson, int min, string minPerson, int recordCount)
		{
			Country = country;
			Average = average;
			Median = median;
			Max = max;
			MaxPerson = maxPerson;
			Min = min;
			MinPerson = minPerson;
			RecordCount = recordCount;
		}

		public void FindAverage(List<int> scores)
        {			
			Average = (double)scores.Average();
        }

		public void FindMedian(List<int> scores)
        {
			scores.Sort();
			int listSize = scores.Count;
			int mid = listSize / 2;
			double result;
			if (listSize % 2 != 0)
            {
				result = scores[mid];
            }
            else
            {
				result = (double)(scores[mid] + scores[mid - 1]) / 2;
            }
			Median = result;
        }

		public void FindMax(List<int> scores)
        {			
			Max = scores.Max();
		}

		public void FindMaxPerson(List<Person> group)
        {
			string result = "";
			foreach (Person participant in group)
            {
				if (participant.Score == Max)
				{
					if (result != "")
					{
						result = result + ", " + participant.FirstName + " " + participant.LastName;
					}
					else
                    {
						result = participant.FirstName + " " + participant.LastName;
					}
				}
            }
			MaxPerson = result;
        }

		public void FindMin(List<int> scores)
        {
			Min = scores.Min();
        }

		public void FindMinPerson(List<Person> group)
		{
			string result = "";
			foreach (Person participant in group)
			{
				if (participant.Score == Min)
				{
					if (result != "")
					{
						result = result + ", " + participant.FirstName + " " + participant.LastName;
					}
					else
					{
						result = participant.FirstName + " " + participant.LastName;
					}
				}
			}
			MinPerson = result;
		}

		public void FindRecordCount(List<int> scores)
        {
			RecordCount = scores.Count;
        }
	}
}
