using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Popups;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation.Collections;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System;
using System.Runtime.InteropServices.WindowsRuntime;




namespace Calculator
{
	public sealed partial class TripCalculator : Page
	{
		// default var
		const int Dayprice = 100;

		public static TripCalculator mainPage { get; set; }

		// default functions
		public TripCalculator()
		{
			InitializeComponent();

			mainPage = this;
		}

		// page functions
		private void pageLoaded(object sender, RoutedEventArgs e)
		{
			// window minimum size
			ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size(256, 384));

			// enable title bar full customiztion
			CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = true;
			// title bar customization
			ApplicationViewTitleBar titleBar = ApplicationView.GetForCurrentView().TitleBar;

			titleBar.ButtonBackgroundColor = Windows.UI.Colors.Transparent;
			titleBar.ButtonInactiveBackgroundColor = Windows.UI.Colors.Transparent;
			titleBar.ButtonInactiveForegroundColor = Windows.UI.Colors.White;
		}

		public async void TripCalculatorButton_Click(object sender, RoutedEventArgs e)
		{
			int Year;
			int Month;
			int Date;
			int StartKilo;
			int EndKilo;
			int TotalAmount;
			int TotalDays;


			try
			{
				Date = int.Parse(DateTextBox.Text);
			}
			catch (Exception ex)
			{
				MessageDialog dialogMessage = new MessageDialog("Error please enter the Correct Date" + ex.Message);
				await dialogMessage.ShowAsync();
				DateTextBox.Focus(FocusState.Programmatic);
				DateTextBox.SelectAll();
				return;
			}
			try
			{
				Month= int.Parse(MonthTextBox.Text);
			}
			catch (Exception ex)
			{
				MessageDialog dialogMessage = new MessageDialog("Error please enter the Correct Month" + ex.Message);
				await dialogMessage.ShowAsync();
				DateTextBox.Focus(FocusState.Programmatic);
				DateTextBox.SelectAll();
				return;
			}
			try
			{
				Year = int.Parse(YearTextBox.Text);
			}
			catch (Exception ex)
			{
				MessageDialog dialogMessage = new MessageDialog("Error please enter the Correct Year" + ex.Message);
				await dialogMessage.ShowAsync();
				DateTextBox.Focus(FocusState.Programmatic);
				DateTextBox.SelectAll();
				return;
			}
			try
			{
				StartKilo = int.Parse(StartKiloTextBox.Text);
			}
			catch (Exception ex)
			{
				MessageDialog dialogMessage = new MessageDialog("Error please enter the Correct StartKilo" + ex.Message);
				await dialogMessage.ShowAsync();
				DateTextBox.Focus(FocusState.Programmatic);
				DateTextBox.SelectAll();
				return;
			}
			try
			{
				EndKilo = int.Parse(EndKiloTextBox.Text);
			}
			catch (Exception ex)
			{
				MessageDialog dialogMessage = new MessageDialog("Error please enter the Correct EndKilo" + ex.Message);
				await dialogMessage.ShowAsync();
				DateTextBox.Focus(FocusState.Programmatic);
				DateTextBox.SelectAll();
				return;
			}
			Month = int.Parse(MonthTextBox.Text);
			DateTime selectedDate = new DateTime(Year, Month, Date);
			TotalDays = TripCalculatorMethod(selectedDate);
			HiredDaysTextBox.Text = TotalDays.ToString();
			TotalAmount = TotalDays * Dayprice;
			TotalAmountTextBox.Text = TotalAmount.ToString();
			DayRateTextBox.Text = Dayprice.ToString();
		}

		private void handleExitButtonClick(object sender, RoutedEventArgs e)
		{
			this.Frame.Navigate(typeof(MainMenu));
		}
		private int TripCalculatorMethod(DateTime selectedDate)
		{
			int Result;
			DateTime currentDate = DateTime.Today;
			TimeSpan difference = currentDate - selectedDate;
			Result = difference.Days;
			return Result;
		}


	}
}