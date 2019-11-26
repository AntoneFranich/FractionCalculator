using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Text.RegularExpressions;

namespace FractionCalculator
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        //Ensure we can only enter numbers in our text boxes. 
        private static readonly Regex _regex = new Regex("[^0-9.-]+");
        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }
        private void NumberBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(((TextBox)sender).Text + e.Text);
        }

        //The value of one of the text boxes changed.
        private void NumberBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //Clear the results if there are any.
            if (NumerResult.Text != "")
                NumerResult.Text = "";
            if (DenomResult.Text != "")
                DenomResult.Text = "";

            //I removed the check here as it felt bad to crash everytime the user enters 0 in a denominator.
            //Check is done when the user attempts to trigger an operation instead.
            //if(Int32.Parse(Denom1.Text) == 0 || Int32.Parse(Denom2.Text) == 0)
            //	throw new DenominatorZeroException("The Denominator Cannot be Zero!");

            //Check to see if we need to enable the Results button. Otherwise Disable the results button.
            if (Numer1.Text != "" && Numer2.Text != "" && Denom1.Text != "" && Denom2.Text != "")
                Results.IsEnabled = true;
            else
                Results.IsEnabled = false;
        }

        //We clicked on the results button. Create our fractions, perform the operation, and report the results.
        private void Results_Click(object sender, RoutedEventArgs e)
        {
            Fraction first = new Fraction(Int32.Parse(Numer1.Text), Int32.Parse(Denom1.Text));
            Fraction second = new Fraction(Int32.Parse(Numer2.Text), Int32.Parse(Denom2.Text));

            Fraction result = null;
            switch (Operator.SelectedIndex)
            {
                case 0:
                    result = first + second;
                    break;
                case 1:
                    result = first - second;
                    break;
                case 2:
                    result = first * second;
                    break;
                case 3:
                    result = first / second;
                    break;

            }
            if (result == null)
                throw new UnknownOperationException("The currently selected fraction operation is unknown!");

            NumerResult.Text = result.Numerator.ToString();
            DenomResult.Text = result.Denominator.ToString();
        }
    }

    //We shouldn't ever throw this, but it's there in case we someone select something other than the 4 operators.
    public class UnknownOperationException : Exception
    {
        public UnknownOperationException(string message)
            : base(message)
        {
        }
    }
}
