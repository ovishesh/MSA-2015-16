using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MSA_Weather
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void getWeatherButton_Click(object sender, RoutedEventArgs e)
        {
            getWeather(cityTextBox.Text);
        }

        public async void getWeather(string city)
        {
            // Makes out progress bar visible so the user knows something is happening.
            loading.Visibility = Visibility.Visible;

            // Setting up our error string so we can check if an error had occured inside our try/catch
            // and display to the user whatever the error was.
            String error = null;

            // We need a try/catch wrapped around our API resquest just incase an error occurs
            // while calling the weather API. If an error does occur the code inside the catch
            // statement is called, otherwise the app skips it and continues with the code.
            // Without a try/catch, if an error does occur the app would not know how to handle it
            // resulting in the app crashing. A try/catch prevents the app from crashing and can be
            // used to inform the user what went wrong.
            try
            {
                // Initializing HTTPClient.
                HttpClient client = new HttpClient();

                // Creating a new Weather Object to bind the results from our API call.
                WeatherObject rootObject;

                // Calling our weather API, passing the string 'city' so we're getting the correct weather returned.
                // The 'await' tag tells the computer to wait for the results to be returned before continuing with
                // the rest of the code. The results are then assigned to string 'x' to be used later in the code.
                string x = await client.GetStringAsync(new Uri("http://msauniapp.azurewebsites.net/api/Students/" + city));

                // We're now binding the returned data assigned to string 'x' to the object 'rootObject'.
                rootObject = JsonConvert.DeserializeObject<WeatherObject>(x);

                // Changing the text block's text to show the user the results.
                cityTextBlock.Text = rootObject.ID.ToString();
                tempTextBlock.Text = rootObject.FirstMidName;
                pressureTextBlock.Text = rootObject.LastName;
                humidityTextBlock.Text = rootObject.EnrollmentDate;
            }

            // Only called if an error occurs.
            catch (Exception ex)
            {
                // Assigning the string error to whatever exception occured.
                error = ex.Message;
            }

            // Makes the progress bar invisible.
            loading.Visibility = Visibility.Collapsed;

            // Checks if the error string is not set to null.
            if (error != null)
            {
                // Displays a message box informing the user if an error occured.
                var messageDialog = new Windows.UI.Popups.MessageDialog("Error: " + error);

                // Shows the message box to the user and waits for the user to dismiss it.
                await messageDialog.ShowAsync();
            }
        }

        private async void getLocationButton_Click(object sender, RoutedEventArgs e)
        {
            loading.Visibility = Visibility.Visible;
            String error = null;

            try
            {
                // Create new instance of Geolocator and define accuracy.
                // Inorder for geolocator to work you will need to enable 'Locations' inside the
                // app's manifest file. Look for 'Package.appxmanifest' inside your 'Solution Explorer'
                // and open it. Locate the 'Capabilities' tab along the top and check the 'Location' box
                // to enable locations. The user will also need to enable location on their machine from
                // the 'Privacy' section in settings.
                Geolocator geolocator = new Geolocator();
                geolocator.DesiredAccuracyInMeters = 100;

                // Get the Geoposition asynchronously.
                Geoposition geoposition = await geolocator.GetGeopositionAsync(
                    // Define maximumAge to make sure data is not too old.
                    maximumAge: TimeSpan.FromMinutes(5),
                    // Define timeout to prevent wastage of CPU cycles and battery.
                    timeout: TimeSpan.FromSeconds(10)
                    );

                // Get latitude and longitude values (2 dp) as strings.
                string lat = geoposition.Coordinate.Point.Position.Latitude.ToString("0.00");
                string lng = geoposition.Coordinate.Point.Position.Longitude.ToString("0.00");

                // Pass geolocation data to the executeWeather method.
                executeWeather(lat, lng);
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            if (error != null)
            {
                var messageDialog = new Windows.UI.Popups.MessageDialog("Error: " + error);
                await messageDialog.ShowAsync();
            }
            loading.Visibility = Visibility.Collapsed;
        }

        private async void executeWeather(string lat, string lng)
        {
            //loading.Visibility = Visibility.Visible;
            //String error = null;

            //try
            //{
            //    HttpClient client = new HttpClient();

            //    WeatherObject.RootObject rootObject;

            //    // Calling our weather API, passing the geolocation data so we're getting the correct weather returned.
            //    // The 'await' tag tells the computer to wait for the results to be returned before continuing with
            //    // the rest of the code. The results are then assigned to string 'x' to be used later in the code.
            //    string x = await client.GetStringAsync(new Uri("http://api.openweathermap.org/data/2.5/weather?lat=" + lat + "&lon=" + lng + "&units=metric&APPID=440e3d0ee33a977c5e2fff6bc12448ee"));

            //    rootObject = JsonConvert.DeserializeObject<WeatherObject.RootObject>(x);

            //    cityTextBlock.Text = rootObject.name;
            //    tempTextBlock.Text = rootObject.main.temp + "°C";
            //    pressureTextBlock.Text = rootObject.main.pressure + "hPa";
            //    humidityTextBlock.Text = rootObject.main.humidity + "%";
            //    windDirTextBlock.Text = rootObject.wind.deg + "°";
            //}
            //catch (Exception ex)
            //{
            //    error = ex.Message;
            //}
            //loading.Visibility = Visibility.Collapsed;
            //if (error != null)
            //{
            //    var messageDialog = new Windows.UI.Popups.MessageDialog("Error: " + error);
            //    await messageDialog.ShowAsync();
            //}
        }

        private void aboutButton_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to the settings page
            this.Frame.Navigate(typeof(About));
        }
    }
}
