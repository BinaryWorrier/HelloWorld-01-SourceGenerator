using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld_01
{
    public partial class MyAggregate : AggregateRoot
    {
        //protected override void DoApply(object item)
        //{
        //    throw new NotImplementedException();
        //}
        //protected override void DoApply(object item)
        //{
        //    switch (item)
        //    {
        //        case string typedValue_0:
        //            When(typedValue_0);
        //            break;
        //        case DateTime typedValue_1:
        //            When(typedValue_1);
        //            break;
        //        default: throw new Exception($"When for type '{item.GetType().FullName}' not found");
        //    }
        //}

        public string Name { get; set; }

        private void When(string name)
        {
            Name = name;
            Console.WriteLine(name);
        }

        private void When(DateTime date)
        {
            Console.WriteLine(date);
        }
    }
}
