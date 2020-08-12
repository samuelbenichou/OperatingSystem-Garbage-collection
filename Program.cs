using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace GarbageCollection
{
    class Program
    {
        static void TestExpressionsI()
        {
            Expression eOne = MemoryAllocationUnit.GetInstance().NewValueExpression(1);
            Expression eTwo = MemoryAllocationUnit.GetInstance().NewValueExpression(2);
            Expression eThree = MemoryAllocationUnit.GetInstance().NewValueExpression(3);
            SumExpression eSum12 = MemoryAllocationUnit.GetInstance().NewSumExpression();
            eSum12.AddSubExpression(eOne);
            eSum12.AddSubExpression(eTwo);
            SumExpression eSum23 = MemoryAllocationUnit.GetInstance().NewSumExpression();
            eSum23.AddSubExpression(eTwo);
            eSum23.AddSubExpression(eThree);
            ProductExpression eProd = MemoryAllocationUnit.GetInstance().NewProductExpression();
            eProd.AddSubExpression(eSum12);
            eProd.AddSubExpression(eSum23);
            Debug.WriteLine(eProd);

            Debug.WriteLine(eProd.Evaluate());

            Expression eConverted = eProd.ConvertProductOfSumToSumOfProducts();
            Debug.WriteLine(eConverted);

            Debug.WriteLine(eConverted.Evaluate());

            List<Expression> lRoot = new List<Expression>();
            lRoot.Add(eConverted);
            MemoryAllocationUnit.GetInstance().GarbageCollection(lRoot);
            int cCollectedObjects = MemoryAllocationUnit.GetInstance().CollectedCount;
            //should be 5 with the example above
            int iResult = eConverted.Evaluate();
        }

        static void TestExpressionsII(bool bPreserveProduct)
        {
            Expression eOne = MemoryAllocationUnit.GetInstance().NewValueExpression(1);
            Expression eTwo = MemoryAllocationUnit.GetInstance().NewValueExpression(2);
            Expression eThree = MemoryAllocationUnit.GetInstance().NewValueExpression(3);
            SumExpression eSum12 = MemoryAllocationUnit.GetInstance().NewSumExpression();
            eSum12.AddSubExpression(eOne);
            eSum12.AddSubExpression(eTwo);
            SumExpression eSum23 = MemoryAllocationUnit.GetInstance().NewSumExpression();
            eSum23.AddSubExpression(eTwo);
            eSum23.AddSubExpression(eThree);
            ProductExpression eProd23 = MemoryAllocationUnit.GetInstance().NewProductExpression();
            eProd23.AddSubExpression(eTwo);
            eProd23.AddSubExpression(eThree);

            ProductExpression eProd = MemoryAllocationUnit.GetInstance().NewProductExpression();
            eProd.AddSubExpression(eSum12);
            eProd.AddSubExpression(eSum23);
            Debug.WriteLine(eProd);

            SumExpression eSum = MemoryAllocationUnit.GetInstance().NewSumExpression();
            eSum.AddSubExpression(eProd23);
            eSum.AddSubExpression(eThree);

            List<Expression> lRoot = new List<Expression>();

            if (bPreserveProduct)
            {
                lRoot.Add(eProd);
                MemoryAllocationUnit.GetInstance().GarbageCollection(lRoot);
                int cCollectedObjects = MemoryAllocationUnit.GetInstance().CollectedCount;
                //should be 2 with the example above:
                //eSum and eProd23 should be removed because they do not appear under eProd
                int iResult = eProd.Evaluate();
                eSum.Evaluate(); //should generate an exception
            }
            else
            {
                lRoot.Add(eSum);
                MemoryAllocationUnit.GetInstance().GarbageCollection(lRoot);
                int cCollectedObjects = MemoryAllocationUnit.GetInstance().CollectedCount;
                //should be 4 with the example above:
                //eProd, eSum12, eSum23, eOne do not appear under eSum
                int iResult = eSum.Evaluate();
                eProd.Evaluate(); //should generate an exception
            }
        }

        static void Main(string[] args)
        {
            Debug.Listeners.Add(new TextWriterTraceListener(Console.Out));
            //TestExpressionsI();
            TestExpressionsII(true);
            //TestExpressionsII(false);
        }
    }
}
