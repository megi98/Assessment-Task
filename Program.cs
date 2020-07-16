using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;
using System.Net.Mail;
using System.Net;
using System.ComponentModel.DataAnnotations;

namespace AssessmentTask
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Path to the file: ");
            string path = Console.ReadLine();

            Console.WriteLine("Sender Email Address: ");
            string senderEmail = Console.ReadLine();
            var foo = new EmailAddressAttribute();
            while (!foo.IsValid(senderEmail))
            {
                Console.WriteLine("Incorrect email. Try again.\nSender Email Address: ");
                senderEmail = Console.ReadLine();
            }

            Console.WriteLine("Sender Password: ");
            string senderPassword = Console.ReadLine();

            Console.WriteLine("Receiver Email Address: ");
            string receiverEmail = Console.ReadLine();
            var foo1 = new EmailAddressAttribute();
            while (!foo1.IsValid(receiverEmail))
            {
                Console.WriteLine("Incorrect email. Try again.\nReceiver Email Address: ");
                receiverEmail = Console.ReadLine();
            }

            List<Person> people = new List<Person>();
            try
            {
                StreamReader reader = new StreamReader(File.OpenRead(path));
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (!String.IsNullOrWhiteSpace(line))
                    {
                        string[] values = line.Split(';');
                        if (values.Length >= 5 && values[0] != "First Name")
                        {
                            Person person = new Person(values[0], values[1], values[2], values[3], int.Parse(values[4]));
                            people.Add(person);
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine("Unable to open the file with this pathname.\nError: " + exc);
            }

            List<string> countries = new List<string>();
            foreach (Person person in people)
            {
                if (!countries.Contains(person.Country))
                {
                    countries.Add(person.Country);
                }
            }

            List<Sample> samples = new List<Sample>();
            foreach (string country in countries)
            {
                List<Person> group = new List<Person>();
                List<int> scores = new List<int>();
                Sample sample = new Sample();
                foreach (Person person in people)
                {
                    if (person.Country == country)
                    {
                        group.Add(person);
                    }
                }
                foreach (Person participant in group)
                {
                    scores.Add(participant.Score);
                }
                sample.Country = country;
                sample.FindAverage(scores);
                sample.FindMedian(scores);
                sample.FindMax(scores);
                sample.FindMaxPerson(group);
                sample.FindMin(scores);
                sample.FindMinPerson(group);
                sample.FindRecordCount(scores);
                samples.Add(sample);
            }
            List<Sample> sortedSamples = samples.OrderByDescending(o => o.Average).ToList();

            string directoryName = Path.GetDirectoryName(path);
            string newPath = directoryName + @"\ReportByCountry.csv";

            try
            {
                using (File.Create(newPath));
                using (StreamWriter writer = new StreamWriter(newPath))
                using (CsvWriter csvWriter = new CsvWriter(writer, System.Globalization.CultureInfo.CurrentCulture))
                {
                    csvWriter.Configuration.Delimiter = ";";
                    csvWriter.Configuration.HasHeaderRecord = true;
                    csvWriter.Configuration.AutoMap<Sample>();
                    csvWriter.WriteHeader<Sample>();
                    csvWriter.NextRecord();
                    csvWriter.WriteRecords(sortedSamples);
                    writer.Flush();
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine("Error pathname or incorrect writing data in the new file.\nError: " + exc);
            }

            try
            {
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(senderEmail);
                mail.To.Add(receiverEmail);
                mail.Subject = "Email with attachment";
                mail.Attachments.Add(new Attachment(newPath));

                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(senderEmail, senderPassword);
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.EnableSsl = true;
                client.Send(mail);
            }
            catch (Exception exc)
            {
                Console.WriteLine("Unable to send email.\nError: " + exc);
            }
        }
    }
}
