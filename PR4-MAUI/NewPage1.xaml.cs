using System;
using Microsoft.Maui.Controls;
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

    private void OnOperationChanged(object sender, CheckedChangedEventArgs e)
    {
        if (e.Value && sender is RadioButton radioButton)
        {
            selectedOperation = radioButton.Value.ToString();
        }
    }

    private void OnCalculateClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(selectedOperation))
        {
            DisplayAlert("Ошибка", "Выберите операцию!", "OK");
            return;
        }

        if (!double.TryParse(Num1Entry.Text, out double num1) ||
            !double.TryParse(Num2Entry.Text, out double num2))
        {
            DisplayAlert("Ошибка", "Введите корректные числа!", "OK");
            return;
        }

        double result = PerformOperation(num1, num2, selectedOperation);
        ResultEntry.Text = result.ToString("F4");
    }

    private double PerformOperation(double num1, double num2, string operation)
    {
        switch (operation.ToLower())
        {
            case "add":
                return num1 + num2;

            case "subtract":
                return num1 - num2;

            case "multiply":
                return num1 * num2;

            case "divide":
                if (num2 == 0)
                {
                    DisplayAlert("Ошибка", "Деление на ноль невозможно!", "OK");
                    return 0;
                }
                return num1 / num2;

            case "modulo":
                if (num2 == 0)
                {
                    DisplayAlert("Ошибка", "Деление на ноль невозможно!", "OK");
                    return 0;
                }
                return num1 % num2;

            case "power":
                return Math.Pow(num1, num2);

            default:
                return 0;
        }
    }
}