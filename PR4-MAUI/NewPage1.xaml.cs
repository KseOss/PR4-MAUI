using Microsoft.Maui.Controls;
using System.Text.RegularExpressions;
namespace PR4_MAUI;

public partial class NewPage1 : ContentPage
{
    private string selectedOperation;
    public NewPage1()
	{
		InitializeComponent();
	}
    private void OnInputChanged(object sender, TextChangedEventArgs e)
    {
        ResultEntry.Text = string.Empty;
    }

    private void OnCalculateClicked(object sender, EventArgs e)
    {
        try
        {
            double result = 0;

            // Пытаемся сначала распарсить выражение из единого поля
            if (!string.IsNullOrWhiteSpace(ExpressionEntry.Text))
            {
                result = CalculateFromExpression(ExpressionEntry.Text);
            }
            // Если выражение не введено, используем отдельные поля
            else if (!string.IsNullOrWhiteSpace(Num1Entry.Text) &&
                     !string.IsNullOrWhiteSpace(Num2Entry.Text) &&
                     OperationPicker.SelectedItem != null)
            {
                double num1 = double.Parse(Num1Entry.Text);
                double num2 = double.Parse(Num2Entry.Text);
                string operation = OperationPicker.SelectedItem.ToString();

                result = PerformOperation(num1, num2, operation);
            }
            else
            {
                DisplayAlert("Ошибка", "Введите выражение или заполните все поля отдельно!", "OK");
                return;
            }

            ResultEntry.Text = result.ToString("F4");
        }
        catch (Exception ex)
        {
            DisplayAlert("Ошибка", $"Некорректный ввод: {ex.Message}", "OK");
        }
    }

    private double CalculateFromExpression(string expression)
    {
        // Убираем лишние пробелы
        expression = expression.Trim();

        // Регулярное выражение для поиска чисел и оператора
        var match = Regex.Match(expression, @"^\s*([-+]?\d*\.?\d+)\s*([+\-*/%^])\s*([-+]?\d*\.?\d+)\s*$");

        if (!match.Success)
        {
            throw new ArgumentException("Некорректный формат выражения. Используйте: число операция число");
        }

        double num1 = double.Parse(match.Groups[1].Value);
        string operation = match.Groups[2].Value;
        double num2 = double.Parse(match.Groups[3].Value);

        return PerformOperation(num1, num2, operation);
    }

    private double PerformOperation(double num1, double num2, string operation)
    {
        switch (operation)
        {
            case "+":
                return num1 + num2;

            case "-":
                return num1 - num2;

            case "*":
                return num1 * num2;

            case "/":
                if (num2 == 0)
                {
                    throw new DivideByZeroException("Деление на ноль невозможно!");
                }
                return num1 / num2;

            case "%":
                if (num2 == 0)
                {
                    throw new DivideByZeroException("Деление на ноль невозможно!");
                }
                return num1 % num2;

            case "^":
                return Math.Pow(num1, num2);

            default:
                throw new ArgumentException($"Неизвестная операция: {operation}");
        }
    }
}
