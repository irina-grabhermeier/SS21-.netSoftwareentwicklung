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
            _predicates.Add(constraint);
        }

        public void AddDataFiltered(List<T> data)
        {

            foreach (T t in data)
            {
                bool isValid = true;
                foreach (Predicate<T> p in _predicates)
                {
                    if (!p(t))
                    {
                        isValid = false;
                        break;
                    }
                }
                if (isValid)
                {
                    _dataBuffer.Add(t);
                }
            }
        }

        public G Execute<G>(Func<List<T>, G> func)
        {
            return func(_dataBuffer);
        }

    }
}
