using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ThreadingExperiments.Examples
{
    internal abstract class Example
    {
        protected List<int> RefList = new List<int>();
        public abstract int Run();
       
    }
}
