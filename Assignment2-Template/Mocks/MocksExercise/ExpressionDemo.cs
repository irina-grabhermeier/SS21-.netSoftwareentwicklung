using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MocksExercise
{
    public class ExpressionDemo
    {

        public void ParseExpression<T>(Expression<Action<T>> exprLambda)
        {
            Console.WriteLine("Expression: " + exprLambda.Body.ToString());
            Console.WriteLine("Expression Type: " + exprLambda.Body.NodeType);
            Console.WriteLine("Return Type: " + exprLambda.ReturnType);
            foreach (var param in exprLambda.Parameters)
            {
                Console.WriteLine("Param: " + param.Name + " Type: " + param.Type);
            }
            Console.WriteLine("Name: " + exprLambda.Name);

        }
    }
}
