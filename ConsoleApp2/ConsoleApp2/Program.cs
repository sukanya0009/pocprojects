using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Linq;
using System.Collections;

namespace ConsoleApp2
{
    enum PaymentType
    {
        Cash = 1,
        Cheque = 2,
        MobileApp = 3,
        DD = 4,
        ManagementSettlement = 5,
        CardPayment = 6
    }

    class Program
    {
        static void Main(string[] args)
        {
            IAnimal animal = new Feline();
            //animal.Breath(); // Feline Breaths
            //animal.Eat();// Feline Eats
            //animal.Sleep();// Feline Sleeps
            //Console.WriteLine(animal.GetType()); // Feline
            //animal.MakeSound(); // Feline Makes Sound

            IAnimal animal2 = new Cat();
            //animal2.Breath();// Feline Breaths
            //animal2.Eat();// Feline Breaths
            //animal2.Sleep();// Cat Breaths
            //Console.WriteLine(animal2.GetType()); // Cat   
            //animal2.MakeSound(); // Feline Makes Sound

            Feline animal1 = new Feline();
            //animal1.Breath(); // Feline Breaths
            //animal1.Eat();// Feline Eats
            //animal1.Sleep();// Feline Sleeps
            //Console.WriteLine(animal.GetType()); // Feline          
            //animal1.MakeSound();// Feline Makes Sound

            Feline animalx = new Cat();
            animalx.Breath();// Feline Breaths
            animalx.Eat();// Cat Breaths  -- method hiding
            animalx.Sleep();// Cat Breaths -- method overriding
            Console.WriteLine(animalx.GetType()); // Cat
            animalx.MakeSound(); // Cat Makes Sound

            //Cat animal3 = new Cat();
            //animal3.Breath();// Feline Breaths
            //animal3.Eat();// Cat Breaths  -- method hiding
            //animal3.Sleep();// Cat Breaths -- method overriding
            //Console.WriteLine(animal3.GetType()); // Cat
            //animal3.MakeSound(); // Cat Makes Sound

            IAnimal animal4 = new Tom();
            //animal4.Eat();
            //animal4.MakeSound();// Feline Makes Sound
            ////Console.WriteLine(animal4.GetType()); // Tom

            //Feline animal5 = new Tom();
            //animal5.MakeSound();// Feline Makes Sound

            //Cat animal6 = new Tom();
            //animal4.Eat();
            //animal6.MakeSound();// Tom Makes Sound

            Console.ReadLine();
        }

    }

    public interface IAnimal
    {
        void Sleep();
        void Eat();
        void Breath();
        void MakeSound();
    }

    public class Feline : IAnimal
    {
        public void Breath() => Console.WriteLine("Feline Breaths");
        public void Eat() => Console.WriteLine("Feline Eats");
        public virtual void Sleep() => Console.WriteLine("Feline Sleeps");
        public virtual void MakeSound() => Console.WriteLine("Feline Makes Sound");
    }
    public class Cat : Feline
    {
        // Warning CS0108  'Cat.Breath()' hides inherited member 'Feline.Breath()'
        // Use the new keyword if hiding was intended.
        public void Breath() =>  Console.WriteLine("Cat Breaths");       
        public new void Eat() => Console.WriteLine("Cat Eats");        
        public override void Sleep() => Console.WriteLine("Cat Sleeps");
        public new virtual void MakeSound() => Console.WriteLine("Cat Makes Sound");
    }
    public class Tom : Cat
    {
       public override void MakeSound() => Console.WriteLine("Tom Makes Sound");
    }
}
