using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class MortgageCalculator : Page
	{

		public MortgageCalculator()
		{
			this.InitializeComponent();
		}
		//The "CalculateButton_Click" method is an event handler for the click event of the calculate button in the user interface
		private async void CalculateButton_Click(object sender, RoutedEventArgs e)
		{
			int years, months;
			double principal, yearRate, monthRate; // monthlyInterestAmount;
			// try catch error message - users must enter an amount in the text boxes 
			try
			{
				years = int.Parse(yearsTextBox.Text);
			}
			catch (Exception ex)
			{
				MessageDialog dialogMessage = new MessageDialog("Error please enter the years" + ex.Message);
				await dialogMessage.ShowAsync();
				yearsTextBox.Focus(FocusState.Programmatic);
				yearsTextBox.SelectAll();
				return;
			}
			try
			{
				months = int.Parse(monthsTextBox.Text);
			}
			catch (Exception ex)
			{
				MessageDialog dialogMessage = new MessageDialog("Error please enter the months " + ex.Message);
				await dialogMessage.ShowAsync();
				monthsTextBox.Focus(FocusState.Programmatic);
				return;
			}
			try
			{
				principal = double.Parse(principleTextBox.Text);
			}
			catch (Exception ex)
			{
				MessageDialog dialogMessage = new MessageDialog("Error please enter a principle loan amount " + ex.Message);
				await dialogMessage.ShowAsync();
				principleTextBox.Focus(FocusState.Programmatic);
				return;
			}
			try
			{
				yearRate = double.Parse(yearlyInterestRateTextBox.Text);
			}
			catch (Exception ex)
			{
				MessageDialog dialogMessage = new MessageDialog("Error please enter the yearly interest rate " + ex.Message);
				await dialogMessage.ShowAsync();
				yearlyInterestRateTextBox.Focus(FocusState.Programmatic);
				return;
			}
			try
			{
				monthRate = double.Parse(monthlyInterestTextBox.Text);
			}
			catch (Exception ex)
			{
				MessageDialog dialogMessage = new MessageDialog("Error please enter the Monthly interest rate " + ex.Message);
				await dialogMessage.ShowAsync();
				monthlyInterestTextBox.Focus(FocusState.Programmatic);
				return;
			}
			double monthlyRepayment = calcMonthlyPayment(principal, years, monthRate); // Calculates monthly repayments
			 //double monthlyInterestAmount = calcMonthlyInterest(); // Calculates monthly interest
			yearlyInterestRateTextBox.Text = yearRate.ToString("C");
			//monthRate = double.Parse(monthlyInterestTextBox.Text);
		}

		//// Calculate monthly payment and return the message to (mRepayment text box) in the user interface
		private double calcMonthlyPayment(double principal, int years, double monthRate)
		{
			int months = years * 12; // coverts years into months
			//double monthlyInterestRate = yearRate / 12;
			//double monthlyInterestAmount = monthlyInterestRate;
			double monthlyRepayment = principal * (monthRate * Math.Pow(1 + monthRate, months)) / (Math.Pow(1 + monthRate, months) - 1);
			//double monthlyRepayment = principal[monthlyInterestAmount(1 + monthRate) ^ months] /[(1 + monthlyInterestAmount) ^ months - 1];
			mRepaymentTextBox.Text = monthlyRepayment.ToString("C");
			//monthlyInterestTextBox.Text = monthRate.ToString("N4"); //monthly interest rate 
			return monthlyRepayment;
		}
		//private double calcMonthlyInterest(double principalAmount)
		//{
		//	//monthRate = yearRate / 12;
		//	//yearlyInterestRateTextBox.Text = yearRate.ToString("C");
		//	double monthlyInterestRate = yearRate / 12;
		//	double monthlyInterestAmount = monthlyInterestRate;
		//	monthlyInterestTextBox.Text = monthlyInterestAmount.ToString("C"); // Writes the monthly interest amount into the monthlyInterest Text Box
		//	return monthlyInterestAmount;
		//}
		// exit button to return to main page on exit 
		private void exitButton_Click(object sender, RoutedEventArgs e)
		{
			this.Frame.Navigate(typeof(MainMenu));
		}
	}
}
