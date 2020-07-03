using System;
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
			this.Country = country;
			this.Average = average;
			this.Median = median;
			this.Max = max;
			this.MaxPerson = maxPerson;
			this.Min = min;
			this.MinPerson = minPerson;
			this.RecordCount = recordCount;
		}

		public void FindAverage(List<int> scores)
        {			
			this.Average = (double)scores.Average();
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
			this.Median = (double)result;
        }

		public void FindMax(List<int> scores)
        {			
			this.Max = scores.Max();
		}

		public void FindMaxPerson(List<Person> group)
        {
			string result = "";
			foreach (Person participant in group)
            {
				if (participant.Score == this.Max)
				{
					result = participant.FirstName + " " + participant.LastName;
				}
            }
			this.MaxPerson = result;
        }

		public void FindMin(List<int> scores)
        {
			this.Min = scores.Min();
        }

		public void FindMinPerson(List<Person> group)
		{
			string result = "";
			foreach (Person participant in group)
			{
				if (participant.Score == this.Min)
				{
					result = participant.FirstName + " " + participant.LastName;
				}
			}
			this.MinPerson = result;
		}

		public void FindRecordCount(List<int> scores)
        {
			this.RecordCount = scores.Count;
        }
	}
}
