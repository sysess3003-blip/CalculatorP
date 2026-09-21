using System;
using System.Windows;
using System.Windows.Controls;

namespace CalculatorP
{
    public partial class MainWindow : Window
    {
        private double firstValue = 0;
        private string operation = "";
        private bool isOperationPerformed = false;

        public MainWindow()
        {
            InitializeComponent();
        }

        // Обработка нажатия на цифры (0-9)
        private void Num_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            string digit = btn.Content.ToString();

            if (txtDisplay.Text == "0" || isOperationPerformed || txtDisplay.Text == "Ошибка")
            {
                txtDisplay.Text = "";
            }

            isOperationPerformed = false;
            txtDisplay.Text += digit;
        }

        // Обработка ввода запятой
        private void Comma_Click(object sender, RoutedEventArgs e)
        {
            if (isOperationPerformed || txtDisplay.Text == "Ошибка")
            {
                txtDisplay.Text = "0,";
                isOperationPerformed = false;
                return;
            }

            if (!txtDisplay.Text.Contains(","))
            {
                txtDisplay.Text += ",";
            }
        }

        // Обработка нажатия на операторы (+, -, *, /, %)
        private void Operator_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            if (txtDisplay.Text == "Ошибка") return;

            if (firstValue != 0 && !isOperationPerformed)
            {
                CalculateResult();
            }

            if (double.TryParse(txtDisplay.Text, out firstValue))
            {
                operation = btn.Content.ToString();
                isOperationPerformed = true;
            }
        }

        // Кнопка "="
        private void Equals_Click(object sender, RoutedEventArgs e)
        {
            if (txtDisplay.Text == "Ошибка") return;
            CalculateResult();
            operation = "";
        }

        // Основная математическая логика
        private void CalculateResult()
        {
            if (string.IsNullOrEmpty(operation)) return;

            if (!double.TryParse(txtDisplay.Text, out double secondValue)) return;

            switch (operation)
            {
                case "+":
                    txtDisplay.Text = (firstValue + secondValue).ToString();
                    break;
                case "-":
                    txtDisplay.Text = (firstValue - secondValue).ToString();
                    break;
                case "*":
                    txtDisplay.Text = (firstValue * secondValue).ToString();
                    break;
                case "/":
                    // Защита от деления на ноль
                    if (secondValue == 0)
                    {
                        txtDisplay.Text = "Ошибка";
                        MessageBox.Show("На ноль делить нельзя! 🌸", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        txtDisplay.Text = (firstValue / secondValue).ToString();
                    }
                    break;
                case "%":
                    txtDisplay.Text = (firstValue * secondValue / 100).ToString();
                    break;
            }

            if (double.TryParse(txtDisplay.Text, out double res))
            {
                firstValue = res;
            }
            isOperationPerformed = true;
        }

        // Очистка полного ввода (C)
        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            txtDisplay.Text = "0";
            firstValue = 0;
            operation = "";
            isOperationPerformed = false;
        }

        // Стирание одного символа (←)
        private void Backspace_Click(object sender, RoutedEventArgs e)
        {
            if (txtDisplay.Text == "Ошибка")
            {
                Clear_Click(sender, e);
                return;
            }

            if (txtDisplay.Text.Length > 1)
            {
                txtDisplay.Text = txtDisplay.Text.Substring(0, txtDisplay.Text.Length - 1);
            }
            else
            {
                txtDisplay.Text = "0";
            }
        }

        // Смена знака числового значения (±)
        private void PlusMinus_Click(object sender, RoutedEventArgs e)
        {
            if (txtDisplay.Text == "Ошибка" || txtDisplay.Text == "0") return;

            if (double.TryParse(txtDisplay.Text, out double val))
            {
                txtDisplay.Text = (-val).ToString();
            }
        }
    }
}