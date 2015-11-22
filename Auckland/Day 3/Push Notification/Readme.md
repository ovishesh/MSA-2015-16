# Register your app for the Windows Store

To send push notifications to Windows Store apps, you must associate your app to the Windows Store. You must then configure your notification hub to integrate with WNS.

1. If you have not already registered your app, navigate to http://go.microsoft.com/fwlink/p/?linkid=266582&clcid=0x409, sign in with your Microsoft account, and then click Create a new app.

2. Type a name for your app and click Reserve app name.

This creates a new Windows Store registration for your app.

3. In Visual Studio, create a new Visual C# Store Apps project by using the Blank App template.

4. In Solution Explorer, right-click the Windows Store app project, click Store, and then click Associate App with the Store....

The Associate Your App with the Windows Store wizard appears.

5. In the wizard, click Sign in and then sign in with your Microsoft account.

6. Click the app that you registered in step 2, click Next, and then click Associate.

This adds the required Windows Store registration information to the application manifest.

7. Back on the Windows Dev Center page for your new app, click Services, click Push notifications, and then click Live Services site under Windows Push Notification Services (WNS) and Microsoft Azure Mobile Services.

8. On the App Settings tab, make a note of the values of Client secret and Package security identifier (SID).

#Configure your notification hub

1. Log on to the Azure Management Portal, and then click +NEW at the bottom of the screen.

2. Click on App Services, then Service Bus, then Notification Hub, then Quick Create.

3. Enter a Notification Hub Name. Select your desired region and subscription.

If you already have a service bus namespace that you want create the hub in, select your Namespace Name. Otherwise, you can use the default Namespace Name which will be created based on the hub name as long as the namespace name is available.
Click Create a new Notification Hub.

4. Once the namespace and notification hub are created, your namespaces in service bus will be displayed. Click the namespace that you just created your hub in (usually notification hub name-ns).

5. On your namespace page, click the Notification Hubs tab at the top, and then click on the notification hub you just created. This will open the dashboard for your new notification hub.

6. On the dashboard for your new hub click View Connection String. Take note of the two connection strings. You will use these later.

7. Select the Configure tab at the top, enter the Client secret and Package SID values that you obtained from WNS in the previous section, and then click Save.

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

#Sending Push Notification from console app

## 1. Creating console app

Right-click the solution, select Add and New Project..., and then under Visual C#, click Windows and Console Application, and click OK.

## 2. Adding required NuGet

In Visual Studio, click Tools, click NuGet Package Manager, and then click Package Manager Console. This displays the Package Manager Console in Visual Studio.
In the Package Manager Console window, set the Default project to your new console application project, and then in the console window, execute the following command:

	Install-Package Microsoft.Azure.NotificationHubs
	
Open the Program.cs file and add the following using statement:
	
	using Microsoft.Azure.NotificationHubs;
	
In the Program class, add the following method:

	private static async void SendNotificationAsync()
	{
		NotificationHubClient hub = NotificationHubClient
			.CreateClientFromConnectionString("<connection string with full access>", "<hub name>");
		var toast = @"<toast><visual><binding template=""ToastText01""><text id=""1"">Hello from a .NET App!</text></binding></visual></toast>";
		await hub.SendWindowsNativeNotificationAsync(toast);
	}
	
Make sure to replace the "hub name" placeholder with the name of the notification hub that appears in the portal on the Notification Hubs tab. 
Also, replace the connection string placeholder with the connection string called DefaultFullSharedAccessSignature that you obtained in the section "Configure your notification hub."

Add the following lines in the Main method:

	SendNotificationAsync();
	Console.ReadLine();
	
Right-click the console application project in Visual Studio, and click Set as StartUp Project to set it as the startup project. Then press the F5 key to run the application.
