using System;
using Microsoft.Maui.Controls;
namespace PR4_MAUI
{
    public partial class MainPage : ContentPage
    {
        private string selectedOperation;

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnInputChanged(object sender, TextChangedEventArgs e)
        {
            ResultEntry.Text = string.Empty;
            CalculateResult();
        }

        private void OnOperationChanged(object sender, CheckedChangedEventArgs e)
        {
            if (e.Value && sender is RadioButton radioButton)
            {
                selectedOperation = radioButton.Value.ToString();
                CalculateResult();
            }
        }

        private void CalculateResult()
        {
            if (string.IsNullOrEmpty(selectedOperation) ||
                !double.TryParse(Num1Entry.Text, out double num1) ||
                !double.TryParse(Num2Entry.Text, out double num2))
            {
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

        private async void OnEnableManualTabClicked(object sender, EventArgs e)
        {
            // Получаем AppShell и включаем вкладку
            if (Application.Current?.MainPage is AppShell shell)
            {
                shell.EnableManualTab();
                await DisplayAlert("Успех", "Вкладка 'Ручной расчет' теперь доступна!", "OK");
            }
        }
    }
}
