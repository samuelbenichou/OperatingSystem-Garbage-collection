using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GarbageCollection
{
    class SumExpression : CompoundExpression
    {
        public override int Evaluate()
        {
            if (Deleted)
                throw new NullReferenceException();
            int iSum = 0;
            foreach (Expression e in SubExpressions)
                iSum += e.Evaluate();
            return iSum;
        }
        public override string ToString()
        {
            if (Deleted)
                throw new NullReferenceException();
            string s = "(+";
            foreach (Expression e in SubExpressions)
                s += " " + e;
            s += ")";
            return s;
        }

        public override Expression ConvertProductOfSumToSumOfProducts()
        {
            if (Deleted)
                throw new NullReferenceException();
            ConvertSubExpressionsToSumOfProducts();
            return this;
        }
    }
}
