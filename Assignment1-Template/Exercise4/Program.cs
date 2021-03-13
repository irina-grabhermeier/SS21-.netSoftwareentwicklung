using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LamdaExercise
{
    class Program
    {
        static void Main(string[] args)
        {

            var population = new List<DataObject>();
            for (int i = 0; i < 10000; i++)
            {
                var obj = DataObject.CreateRandomObject();
                population.Add(obj);
            }

            var verifier = new GenericDataProcessor<DataObject>();

            // Add valid population data.

            verifier.AddDataConstraint(new Predicate<DataObject>(d => d.Gender != null && d.Gender != EGender.Unknown));
            verifier.AddDataConstraint(new Predicate<DataObject>(d => d.Name != null && d.Name != ""));

            verifier.AddDataFiltered(population);

            var countOfValids = verifier.Execute<int>(data => data.Count);

            Console.WriteLine($"Of these 10000 people, {countOfValids} people have valid data available.");

            // Unhealthy females in the age group of 30 – 50.

            var femaleVerifier = new GenericDataProcessor<DataObject>();
            femaleVerifier.AddDataConstraint(new Predicate<DataObject>(d => d.Gender == EGender.Female));
            femaleVerifier.AddDataConstraint(new Predicate<DataObject>(d => d.Name != null && d.Name != ""));
            femaleVerifier.AddDataConstraint(new Predicate<DataObject>(d => !d.IsHealthy));
            femaleVerifier.AddDataConstraint(new Predicate<DataObject>(d => d.Age >= 30 && d.Age <= 50));

            femaleVerifier.AddDataFiltered(population);

            var countOfFilteredListFemale = femaleVerifier.Execute<int>(data => data.Count);

            Console.WriteLine($"Of these 10000 people, {countOfFilteredListFemale} are females between the age of 30 and 50 are unhealthy.");
            // Healthy males in the age group 75 - 90.


            var maleVerifier = new GenericDataProcessor<DataObject>();
            maleVerifier.AddDataConstraint(new Predicate<DataObject>(d => d.Gender == EGender.Male));
            maleVerifier.AddDataConstraint(new Predicate<DataObject>(d => d.Name != null && d.Name != ""));
            maleVerifier.AddDataConstraint(new Predicate<DataObject>(d => d.IsHealthy));
            maleVerifier.AddDataConstraint(new Predicate<DataObject>(d => d.Age >= 75 && d.Age <= 90));

            maleVerifier.AddDataFiltered(population);

            var countOfFilteredListMale = maleVerifier.Execute<int>(data => data.Count);

            Console.WriteLine($"Of these 10000 people, {countOfFilteredListMale} are males between the age of 75 and 90 are healthy.");
            Console.ReadKey();

        }


    }
}
