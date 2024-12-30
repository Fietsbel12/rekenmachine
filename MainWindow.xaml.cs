using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Rekenmachine
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
        }

        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            Errormsg.Text = "";
            Result.Text = "";

            string num1Input = Number1TextBox.Text;
            string num2Input = Number2TextBox.Text;

            if (!double.TryParse(num1Input, out double number1)) 
            {
                Errormsg.Text = "Ongeldig eerste getal";
                return;
            }

            if (!double.TryParse(num2Input, out double number2)) 
            {
                Errormsg.Text = "Ongeldig tweede getal";
                return;
            }

            string operation = ((ComboBoxItem)OperationComboBox.SelectedItem)?.Content.ToString();
            if (string.IsNullOrEmpty(operation)) 
            {
                Errormsg.Text = "Selecteer een bewerking";
                return;
            }

            double result = 0;
            
            try 
            {
                switch (operation) 
                {
                    case "+":
                        result = number1 + number2;
                        break;
                    case "-":
                        result = number2 - number1;
                        break;
                    case "*":
                        result = number1 * number2;
                        break;
                    case "/":
                        if (number2 == 0)
                            throw new DivideByZeroException();
                        result = number1 / number2;
                        break;
                }
                Result.Text = result.ToString();
            }
            catch (DivideByZeroException) 
            {
                Errormsg.Text = "Delen door nul is niet toegestaan";
            }
            catch (Exception ex) 
            {
                Errormsg.Text = $"Er is een fout opgetreden: {ex.Message}";
            }
        }
    }
}
