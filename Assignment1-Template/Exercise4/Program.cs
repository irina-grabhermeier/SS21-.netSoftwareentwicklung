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

        }
    }
}
