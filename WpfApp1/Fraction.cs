using System;

namespace FractionCalculator
{
    public class Fraction
    {
        //Define the numerator and denominator parameters as readonly.
        private int numerator;
        public int Numerator { get { return numerator; } }
        private int denominator;
        public int Denominator { get { return denominator; } }

        //numerator and denominator need to be defined at creation.
        public Fraction(int num, int dem)
        {
            numerator = num;
            if (dem != 0)
                denominator = dem;
            else
                throw new DenominatorZeroException("The Denominator Cannot be Zero!");
            denominator = dem;
        }




        //Override the required operators, +,-,*,/ for our Fraction class.
        public static Fraction operator +(Fraction f1, Fraction f2)
        {
            Fraction ret = new Fraction((f1.Numerator * f2.Denominator) + (f2.Numerator * f1.Denominator), f1.Denominator * f2.Denominator);
            ret.Simplify();
            return ret;
        }
        public static Fraction operator -(Fraction f1, Fraction f2)
        {
            Fraction ret = new Fraction((f1.Numerator * f2.Denominator) - (f2.Numerator * f1.Denominator), f1.Denominator * f2.Denominator);
            ret.Simplify();
            return ret;
        }
        public static Fraction operator *(Fraction f1, Fraction f2)
        {
            Fraction ret = new Fraction(f1.Numerator * f2.Numerator, f1.Denominator * f2.Denominator);
            ret.Simplify();
            return ret;
        }
        public static Fraction operator /(Fraction f1, Fraction f2)
        {
            Fraction ret = new Fraction(f1.Numerator * f2.Denominator, f1.Denominator * f2.Numerator);
            ret.Simplify();
            return ret;
        }

        //Just a method to simplify this fraction if we can.
        public void Simplify()
        {
            int a = numerator;
            int b = denominator;
            while (a != 0 && b != 0)
            {
                if (a > b)
                    a %= b;
                else
                    b %= a;
            }
            int gdc = a;
            if (a == 0)
                gdc = b;

            numerator /= gdc;
            denominator /= gdc;

        }

        public static void AutomatedTest()
        {
            System.Random rand = new System.Random();

            for(int i=0;i<100;i++)
            {
                Fraction first = new Fraction(rand.Next(-1000, 1000), rand.Next(-1000, 0000));
                Fraction second = new Fraction(rand.Next(-1000, 1000), rand.Next(-1000, 0000));

                int Operation = rand.Next(0, 100);
                Fraction result = null;
                string opString = "";
                if (Operation >= 75)
                {
                    result = first + second;
                    opString = "+";
                }
                else if (Operation >= 50)
                {
                    result = first - second;
                    opString = "-";
                }
                else if (Operation >= 25)
                {
                    result = first * second;
                    opString = "*";
                }
                else
                {
                    result = first / second;
                    opString = "/";
                }

                Console.WriteLine("Test " + (i + 1) + ": first: " + first.Numerator+"/"+first.Denominator +" " +opString+" "+ second.Numerator+"/"+second.Denominator+" = "+ result.Numerator + "/" + result.Denominator);
            }

            //Test our exception as a last ditch.

            try
            {
                Fraction ExceptionFraction = new Fraction(1, 0);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.WriteLine(ex.StackTrace);
            }

        }
    }

    //Define a custom user exception for when the user attempts to enter 0 as a fraction denominator.
    public class DenominatorZeroException : Exception
    {
        public DenominatorZeroException(string message)
            : base(message)
        {
        }
    }


}
