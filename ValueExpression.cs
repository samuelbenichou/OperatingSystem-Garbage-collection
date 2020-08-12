using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GarbageCollection
{
    class ValueExpression : Expression
    {
        public int Value { get; private set; }

        public ValueExpression(int iValue)
        {
            Value = iValue;
        }

        public override int Evaluate()
        {
            if (Deleted)
                throw new NullReferenceException();
            return Value;
        }

        public override string ToString()
        {
            if (Deleted)
                throw new NullReferenceException();
            return Value + "";
        }
    }
}
