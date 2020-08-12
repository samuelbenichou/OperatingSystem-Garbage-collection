using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GarbageCollection
{
    class ProductExpression : CompoundExpression
    {
        public override int Evaluate()
        {
            if (Deleted)
                throw new NullReferenceException();
            int iProduct = 1;
            foreach (Expression e in m_lSubExpressions)
                iProduct *= e.Evaluate();
            return iProduct;
        }

        public Expression ToSum()
        {
            if (Deleted)
                throw new NullReferenceException();
            foreach (Expression eProdComponent in SubExpressions)
            {
                if (eProdComponent is SumExpression)
                {
                    SumExpression eSum = (SumExpression)eProdComponent;
                    SumExpression eNew = MemoryAllocationUnit.GetInstance().NewSumExpression();
                    foreach (Expression eSumComponent in eSum.SubExpressions)
                    {
                        ProductExpression eProd = MemoryAllocationUnit.GetInstance().NewProductExpression();
                        eProd.AddSubExpression(eSumComponent);
                        foreach (Expression eOtherProdComponent in SubExpressions)
                            if (eOtherProdComponent != eSum)
                                eProd.AddSubExpression(eOtherProdComponent);
                        eNew.AddSubExpression(eProd);
                    }
                    return eNew;
                }
            }
            return this;
        }
        public override string ToString()
        {
            if (Deleted)
                throw new NullReferenceException();
            string s = "(X";
            foreach (Expression e in SubExpressions)
                s += " " + e;
            s += ")";
            return s;
        }

        public override Expression ConvertProductOfSumToSumOfProducts()
        {
            if (Deleted)
                throw new NullReferenceException();
            CompoundExpression eNew = (CompoundExpression)ToSum();
            eNew.ConvertSubExpressionsToSumOfProducts();
            return eNew;
        }
    }
}
