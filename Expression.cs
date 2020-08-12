using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GarbageCollection
{
    abstract class Expression
    {
        public bool Deleted { get; private set; }
        public void Delete()
        {
            if (Deleted)
                throw new NullReferenceException();
            Deleted = true;
            MemoryAllocationUnit.GetInstance().CollectedCount++;
        }
        public abstract int Evaluate();
    }
}
