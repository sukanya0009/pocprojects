using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class ConstructorsDemo1
    {
        private ConstructorsDemo1()
        {
            Console.WriteLine("ConstructorsDemo1 : This is parameterless constructor");
        }
        public ConstructorsDemo1(int a):this()
        {
            //ConstructorsDemo1();
            Console.WriteLine("ConstructorsDemo1 : This is parametered constructor");
        }
    }
    public class ConstructorsDemo
    {
        public ConstructorsDemo()
        {
            Console.WriteLine("ConstructorsDemo : This is parameterless constructor");
        }
        public ConstructorsDemo(int a)
        {
            Console.WriteLine("ConstructorsDemo : This is parametered constructor");
        }
    }

    public class ConstructorsDemoChild : ConstructorsDemo
    {
        public ConstructorsDemoChild()
        {
            Console.WriteLine("ConstructorsDemoChild : This is parameterless constructor");
        }
        public ConstructorsDemoChild(int a) : base()
        {
            Console.WriteLine("ConstructorsDemoChild : This is parametered constructor");
        }
    }
}
