#Push Notification

In this tutorial, we will expanding on your Windows 10 apps and enabling push notification. 

## 1. Registering device to receive notifications

In Visual Studio, right-click the solution, and then click Manage NuGet Packages. This displays the Manage NuGet Packages dialog box.
Search for WindowsAzure.Messaging.Managed and click Install, select all projects in the solution, and accept the terms of use.

Open the App.xaml.cs project file and add the following using statements. In a universal project, this file is located in the <project_name>.Shared folder.

    using Windows.Networking.PushNotifications;
	using Microsoft.WindowsAzure.Messaging;
	using Windows.UI.Popups;
	
Also in App.xaml.cs, add the following InitNotificationsAsync method definition to the App class:

	private async void InitNotificationsAsync()
	{
		var channel = await PushNotificationChannelManager.CreatePushNotificationChannelForApplicationAsync();
	
		var hub = new NotificationHub("<hub name>", "<connection string with listen access>");
		var result = await hub.RegisterNativeAsync(channel.Uri);

		// Displays the registration ID so you know it was successful
		if (result.RegistrationId != null)
		{
			var dialog = new MessageDialog("Registration successful: " + result.RegistrationId);
			dialog.Commands.Add(new UICommand("OK"));
			await dialog.ShowAsync();
		}
	}

At the top of the OnLaunched event handler in App.xaml.cs, add the following call to the new InitNotificationsAsync method:

	InitNotificationsAsync();
	
This guarantees that the channel URI is registered in your notification hub each time the application is launched.

## 2. Enabling toast

In Solution Explorer, double-click Package.appxmanifest of the Windows Store app, and then in Notifications, set Toast capable to Yes.