using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GarbageCollection
{
    abstract class CompoundExpression : Expression
    {
        protected List<Expression> m_lSubExpressions;
        public IEnumerable<Expression> SubExpressions { get { return m_lSubExpressions; } }
        public CompoundExpression()
        {
            m_lSubExpressions = new List<Expression>();
        }
        public void AddSubExpression(Expression e)
        {
            if (Deleted)
                throw new NullReferenceException();
            m_lSubExpressions.Add(e);
        }
        public abstract Expression ConvertProductOfSumToSumOfProducts();
        public void ConvertSubExpressionsToSumOfProducts()
        {
            if (Deleted)
                throw new NullReferenceException();
            List<Expression> lNewSubExpressions = new List<Expression>();
            foreach (Expression e in SubExpressions)
            {
                if (e is ValueExpression)
                {
                    lNewSubExpressions.Add(e);
                }
                else
                {
                    lNewSubExpressions.Add(((CompoundExpression)e).ConvertProductOfSumToSumOfProducts());
                }
            }
            m_lSubExpressions = lNewSubExpressions;
        }
    }
}
