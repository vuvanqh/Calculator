using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calculator;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    Operation op;
    double last=0;
    public MainWindow()
    {
        InitializeComponent();
    }

#pragma warning disable
    private void NumClick(object sender, RoutedEventArgs e)
    {
        int num = int.Parse((sender as Button).Content.ToString());
        if (num == 0) Result.Content = Result.Content.ToString() == "0" ? "0" : $"{Result.Content}0";
        else
            Result.Content = Result.Content.ToString() == "0" ? $"{num}" : $"{Result.Content}{num}"; 
    }
    private void OperationClick(object sender, RoutedEventArgs e)
    {
        /*
        Result.Content = $"{Result.Content} {(sender as Button).Content} ";
        if ((Result.Content.ToString()?.Length - 1) * Result.FontSize >= MyWindow.Width)
        {
            Result.FontSize = Result.FontSize > 20 ? Result.FontSize - 8 : Result.FontSize;
        }*/
        if(double.TryParse(Result.Content.ToString(),out last))
        {
            Result.Content = "0";
        }
        switch ((sender as Button).Content.ToString())
        {
            case "*": 
                op = Operation.Multiplication;
                break;
            case "/":
                op = Operation.Division; break;
            case "+":
                op = Operation.Addition; break;
            case "-":
                op = Operation.Substraction; break;
            case "%":
                op = Operation.Percent; break;
            default:
                op = Operation.Error; break;
        }
    }
    private void EqualClick(object sender, RoutedEventArgs e)
    {
        double result = 0;
        if (double.TryParse(Result.Content.ToString(), out double num))
        {
            switch (op)
            {
                case Operation.Multiplication:
                    result = CalculatorOP.Multiply(last, num);
                    break;
                case Operation.Substraction:
                    result = CalculatorOP.Subtract(last, num);
                    break;
                case Operation.Addition:
                    result = CalculatorOP.Add(last, num);
                    break;
                case Operation.Division:
                    if (num == 0)
                        op = Operation.Error;
                    else
                        result = CalculatorOP.Divide(last, num);
                    break;
                case Operation.Percent:
                    result = CalculatorOP.Percent(last, num);
                    break;
            }
        }
        Result.Content = op == Operation.Error ? "ERROR" : result.ToString();
        /*
        var parts = Result.Content.ToString()?.Split(" ");

        if (parts.Length <= 1) return;

        double.TryParse(parts[0], out double result);
        for(int i=1;i<parts.Length;i+=2)
        {
            if (!double.TryParse(parts[i + 1], out double val2))
            {
                MessageBox.Show("huh");
                continue;
            }
            switch (parts[i])
            {
                case "*":
                    result *= val2;
                    break;
                case "+":
                    result += val2;
                    break;
                case "-":
                    result -= val2;
                    break;
                case "/":
                    if (val2 == 0)
                    {
                        Result.Content = "ERROR";
                        MessageBox.Show("ERROR");
                        return;
                    }
                    else
                    {
                        result /= val2;
                    }
                    break;
                default: break;
            }
        }
        Result.Content = result.ToString();
        */
    }
    private void DotClick(object sender, RoutedEventArgs e)
    {
    }
    private void SignClick(object sender, RoutedEventArgs e)
    {
        if(double.TryParse(Result.Content.ToString(), out double value))
        {
            value *= -1;
            Result.Content = value.ToString();
        }
    }
    private void ACclick(object sender, RoutedEventArgs e)
    {
        Result.Content = "0";
        Result.FontSize = 70;
    }
    
    public enum Operation
    {
        Addition,
        Substraction,
        Multiplication,
        Division,
        Percent,
        Error
    }
    public static class CalculatorOP
    {
        public static double Add(double a, double b)
        {
            return a + b;
        }
        public static double Subtract(double a, double b)
        {
            return a - b;
        }
        public static double Multiply(double a, double b)
        {
            return a * b;
        }
        public static double Divide(double a, double b)
        {
            return a / b;
        }
        public static double Percent(double a, double b)
        {
            return a /100 * b;
        }
    }
}
