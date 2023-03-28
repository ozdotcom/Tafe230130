using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Calculator
{
	/// <summary>
	/// Currency Converter feature of the Universal Calculator
	/// </summary>
	public sealed partial class CurrencyConverter : Page
	{
		public CurrencyConverter()
		{
			this.InitializeComponent();
		}

		private void ReturnToMainMenuButton_Click(object sender, RoutedEventArgs e)
		{
			this.Frame.Navigate(typeof(MainMenu));
		}

		private async void Convert_Click(object sender, RoutedEventArgs e)
		{
			// Initialize variables
			string convertFrom;
			string convertTo;
			double originalAmount;
			double convertedAmount;
			double conversionRate = 0;
			double backwardsConversionRate = 0;


			// Error handling for amount to be converted
			try
			{
				originalAmount = double.Parse(originalAmountTextBox.Text);
			}
			catch
			{
				var dialogMessage = new MessageDialog("Error! Please enter a valid amount.");
				await dialogMessage.ShowAsync();
				originalAmountTextBox.Focus(FocusState.Programmatic);
				originalAmountTextBox.SelectAll();
				return;
			}
			if (originalAmount <= 0)
			{
				var dialogMessage = new MessageDialog("Error! Please enter a valid amount (Greater than $0).");
				await dialogMessage.ShowAsync();
				originalAmountTextBox.Focus(FocusState.Programmatic);
				originalAmountTextBox.SelectAll();
				return;
			}

			// Error handling for original currency
			if (ConvertFrom.SelectedItem != null)
			{
				convertFrom = ConvertFrom.SelectedItem.ToString();
			}
			else
			{
				var dialogMessage = new MessageDialog("Error! Please select a valid currency to convert from.");
				await dialogMessage.ShowAsync();
				ConvertFrom.IsDropDownOpen = true;
				return;
			}

			// Error handling for currency to convert to
			if (ConvertTo.SelectedItem != null)
			{
				convertTo = ConvertTo.SelectedItem.ToString();
			}
			else
			{
				var dialogMessage = new MessageDialog("Error! Please select a valid currency to convert to.");
				await dialogMessage.ShowAsync();
				ConvertTo.IsDropDownOpen = true;
				return;
			}

			// Select conversion rates from the selected currencies
			determineConversionRates(convertFrom, convertTo, ref conversionRate, ref backwardsConversionRate);

			// Calculate output
			convertedAmount = convertCurrency(originalAmount, conversionRate);

			// Print information to screen based on selected currencies and calculated output
			OriginalAmountAndCurrency.Text = "$" + originalAmount.ToString() + " " + convertFrom + "s = ";
			ConvertedAmount.Text = "$" + convertedAmount.ToString() + " " + convertTo + "s";
			ConversionRate.Text = "1 " + convertFrom + " = " + conversionRate + " " + convertTo + "s";
			BackwardsConversionRate.Text = "1 " + convertTo + " = " + backwardsConversionRate + " " + convertFrom + "s";
		}

		private static double convertCurrency(double originalAmount, double conversionRate)
		{
			return originalAmount * conversionRate;
		}

		private static void determineConversionRates(string convertFrom, string convertTo, ref double conversionRate, ref double backwardsConversionRate)
		{
			double USDToEUR = 0.85189982;
			double USDToGBP = 0.72872436;
			double USDToINR = 74.257327;

			double EURToUSD = 1.1739732;
			double EURToGBP = 0.8556672;
			double EURToINR = 87.00755;

			double GBPToUSD = 1.371907;
			double GBPToEUR = 1.1686692;
			double GBPToINR = 101.68635;

			double INRToUSD = 0.011492628;
			double INRToEUR = 0.013492774;
			double INRToGBP = 0.0098339397;

			// If the same currency is selected, conversion rate is 1.
			if (convertFrom == convertTo)
			{
				conversionRate = 1;
				backwardsConversionRate = 1;
			}
			else if (convertFrom == "USD - US Dollar")
			{
				switch (convertTo)
				{
					case "EUR - Euro":
						conversionRate = USDToEUR;
						backwardsConversionRate = EURToUSD;
						break;
					case "GBP - British Pound":
						conversionRate = USDToGBP;
						backwardsConversionRate = GBPToUSD;
						break;
					case "INR - Indian Rupee":
						conversionRate = USDToINR;
						backwardsConversionRate = INRToUSD;
						break;
				}
			}
			else if (convertFrom == "EUR - Euro")
			{
				switch (convertTo)
				{
					case "USD - US Dollar":
						conversionRate = EURToUSD;
						backwardsConversionRate = USDToEUR;
						break;
					case "GBP - British Pound":
						conversionRate = EURToGBP;
						backwardsConversionRate = GBPToEUR;
						break;
					case "INR - Indian Rupee":
						conversionRate = EURToINR;
						backwardsConversionRate = INRToEUR;
						break;
				}
			}
			else if (convertFrom == "GBP - British Pound")
			{
				switch (convertTo)
				{
					case "USD - US Dollar":
						conversionRate = GBPToUSD;
						backwardsConversionRate = USDToGBP;
						break;
					case "EUR - Euro":
						conversionRate = GBPToEUR;
						backwardsConversionRate = EURToGBP;
						break;
					case "INR - Indian Rupee":
						conversionRate = GBPToINR;
						backwardsConversionRate = INRToGBP;
						break;
				}
			}
			else // Last scenario is that convertFrom is INR - Indian Rupee
			{
				switch (convertTo)
				{
					case "USD - US Dollar":
						conversionRate = INRToUSD;
						backwardsConversionRate = USDToINR;
						break;
					case "EUR - Euro":
						conversionRate = INRToEUR;
						backwardsConversionRate = EURToINR;
						break;
					case "GBP - British Pound":
						conversionRate = INRToGBP;
						backwardsConversionRate = GBPToINR;
						break;
				}
			}
		}
	}
}
