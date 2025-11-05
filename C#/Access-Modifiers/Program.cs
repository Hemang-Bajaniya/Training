using System;
using MyLibrary;

namespace AccessModifiersDemo
{
    class BaseClass
    {
        public string PublicField = "Public Field";
        private string PrivateField = "Private Field";
        protected string ProtectedField = "Protected Field";
        internal string InternalField = "Internal Field";
        protected internal string ProtectedInternalField = "Protected Internal Field";

        public void ShowFields()
        {
            Console.WriteLine(PublicField);              // Accessible
            Console.WriteLine(PrivateField);             // Accessible
            Console.WriteLine(ProtectedField);           // Accessible
            Console.WriteLine(InternalField);            // Accessible
            Console.WriteLine(ProtectedInternalField);   // Accessible
        }
    }

    class DerivedClass : BaseClass
    {
        public void ShowInheritedFields()
        {
            Console.WriteLine(PublicField);              // Accessible
            // Console.WriteLine(PrivateField);          // Not accessible
            Console.WriteLine(ProtectedField);           // Accessible
            Console.WriteLine(InternalField);            // Accessible
            Console.WriteLine(ProtectedInternalField);   // Accessible
        }
    }

    namespace LogNamespace
    {
        class Logger : Helper
        {
            public void LogUser()
            {
                System.Console.WriteLine($"Hello User\nTimestamp:{Helper.GetDateTime()}");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            BaseClass baseObj = new BaseClass();
            DerivedClass derivedObj = new DerivedClass();

            Console.WriteLine("BaseClass fields:");
            baseObj.ShowFields();

            Console.WriteLine("\nDerivedClass fields:");
            derivedObj.ShowInheritedFields();

            Console.WriteLine("\nAccessing fields from outside:");
            Console.WriteLine(baseObj.PublicField);              // Accessible
            // Console.WriteLine(baseObj.PrivateField);          // Not accessible
            // Console.WriteLine(baseObj.ProtectedField);        // Not accessible
            Console.WriteLine(baseObj.InternalField);            // Accessible (same assembly)
            Console.WriteLine(baseObj.ProtectedInternalField);   // Accessible (same assembly)

            // var helper = new Helper();

            System.Console.WriteLine();

            System.Console.WriteLine(Helper.Greet());
            // System.Console.WriteLine(Helper.FindSquareRoot(16)); // Internal X

            LogNamespace.Logger logger = new();
            logger.LogUser();
        }
    }
}