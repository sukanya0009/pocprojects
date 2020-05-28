using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class OOPs
    {
    }

    #region Overloading
    /// <summary>
    /// Overloading Examples and Rules
    /// </summary>
    public class Parent
    {
        public int Test() { return 1; }
        /// Overloading of methods permits a class, struct, or interface 
        /// to declare multiple methods with the same name, 
        /// provided their signatures are unique within that class, struct, or interface.
        /// The signature of a method consists of the name of the method, 
        /// the number of type parameters and the type and kind (value, reference, or output) 
        /// of each of its formal parameters, considered in the order left to right. 
        /// **** The signature of a method specifically does not include the ***return type***
        //public void Test() { } // error
        //public int Test() { return 1; } // error

        public int Test(int a) { return 1; }
        public int Test(ref int a) { return 1; }
        // error - members declared in a single type cannot differ in signature solely by ref and out.
        //public int Test(out int a) { a = 1; return 1; } 

        public int Test(int a, string b) { return 1; }
        public int Test(string b, int a) { return 1; }
        ///any type parameter of the method that occurs in the type of a formal parameter 
        ///is identified not by its name, 
        ///but by its ordinal position in the type argument list of the method
        //public long Test(string a, int b) { return 1; } // error

        void F(string[] a) { }             // F(string[])
        /// error - The signature of a method specifically 
        /// 1) does not include the return type, 
        /// 2) the params modifier that may be specified for the right-most parameter, 
        /// 3) nor the optional type parameter constraints.
        //void F(params string[] a) { }      // F(string[])     error

        public int Test1(object a) { return 1; }
        /// For the purposes of signatures, the types object and dynamic are considered the same.
        /// Members declared in a single type can not differ in signature solely by object and dynamic.
        //public int Test1(dynamic a) { return 1; } // error

    }
    #endregion


}
