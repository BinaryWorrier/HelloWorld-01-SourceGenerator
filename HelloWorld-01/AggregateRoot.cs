using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld_01
{
    public abstract class AggregateRoot
    {
        public void Apply(object[] items)
        {
            foreach (var item in items)
                DoApply(item);
        }
        protected abstract void DoApply(object item);
    }
}
