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
        
        private List<Berekening> berekeningen = new List<Berekening>();

        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            // Maak fout- en resultaatvelden leeg
            ErrorTextBlock.Text = "";
            ResultTextBlock.Text = "";

            // Lees invoer
            string num1Input = Number1TextBox.Text;
            string num2Input = Number2TextBox.Text;

            // Valideer getallen
            if (!double.TryParse(num1Input, out double number1))
            {
                ErrorTextBlock.Text = "Ongeldig eerste getal.";
                return;
            }

            if (!double.TryParse(num2Input, out double number2))
            {
                ErrorTextBlock.Text = "Ongeldig tweede getal.";
                return;
            }

            // Haal de geselecteerde operatie op
            string operation = ((ComboBoxItem)OperationComboBox.SelectedItem)?.Content.ToString();

            if (string.IsNullOrEmpty(operation))
            {
                ErrorTextBlock.Text = "Selecteer een bewerking.";
                return;
            }

            double result = 0;

            try
            {
                // Berekening uitvoeren
                switch (operation)
                {
                    case "+":
                        result = number1 + number2;
                        break;
                    case "-":
                        result = number1 - number2;
                        break;
                    case "×":
                        result = number1 * number2;
                        break;
                    case "÷":
                        if (number2 == 0)
                            throw new DivideByZeroException();
                        result = number1 / number2;
                        break;
                }

                // Resultaat weergeven
                ResultTextBlock.Text = result.ToString();

                // Nieuw berekening-item toevoegen aan de lijst
                Berekening nieuweBerekening = new Berekening
                {
                    Number1 = number1,
                    Number2 = number2,
                    Operation = operation,
                    Result = result
                };
                berekeningen.Add(nieuweBerekening);

                // Geschiedenis bijwerken
                UpdateHistoryTextBox();

                // TextBoxen leegmaken
                Number1TextBox.Text = "";
                Number2TextBox.Text = "";
            }
            catch (DivideByZeroException)
            {
                ErrorTextBlock.Text = "Delen door nul is niet toegestaan.";
            }
            catch (Exception ex)
            {
                ErrorTextBlock.Text = $"Er is een fout opgetreden: {ex.Message}";
            }
        }

        private void UpdateHistoryTextBox()
        {
            // Leeg de geschiedenis TextBox eerst
            HistoryTextBox.Text = "";

            // Voeg elke berekening uit de lijst toe aan de TextBox
            foreach (var berekening in berekeningen)
            {
                HistoryTextBox.Text += berekening.ToString() + Environment.NewLine;
            }
        }
    }
}
