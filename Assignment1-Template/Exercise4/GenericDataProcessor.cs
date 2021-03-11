using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LamdaExercise
{
    public class GenericDataProcessor<T> : IDataProcessor<T>
    {
        private List<Predicate<T>> _predicates = new List<Predicate<T>>();
        private List<T> _dataBuffer = new List<T>();

        public void AddDataConstraint(Predicate<T> constraint)
        {
            throw new NotImplementedException();
        }

        public void AddDataFiltered(List<T> data)
        {
            throw new NotImplementedException();
        }

        public G Execute<G>(Func<List<T>, G> func)
        {
            throw new NotImplementedException();
        }
    }
}
