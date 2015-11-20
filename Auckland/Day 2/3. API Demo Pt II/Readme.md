#API Demo II

If you haven't completed API Demo I, go do that first. API Demo I used a local database called localdb to store all your data - 
but that also means that other people aren't able to access your data through your API because it is stored locally. In this 
part of the demo, we will use a database that's hosted online so that we can access it from any place and any device. We shall 
use MySQL for this.

## 1. Creating an Azure hosted Web App + MySQL Db

Follow the instructions in this link. He uses an existing MySQL database, but you'll have to create a new MySQL database: 
http://blogs.msdn.com/b/devschool/archive/2015/06/30/azure-quick-and-easy-way-to-create-a-dreamspark-web-app-and-mysql-using-the-azure-marketplace.aspx

Publish your azure website. If you've forgotten how to do this, skip to 2:20 of this video and follow the instructions:
https://youtu.be/noJGjwxteig?t=2m20s 

## 2. Connect our project to a MySQL Db

First of all, you'll have to download the latest Connector/Net. Connector/Net is the driver that enables .NET applications 
to communicate with MySQL servers. You can download it at this link:
http://dev.mysql.com/downloads/connector/net/6.9.html

You now need to get the MySQL.Data.Entity nuget package. If you do not know how to do this, skip to 2:20 of this video and 
search for MySQL.data.entity and install that package: https://www.youtube.com/watch?v=F8sx49NdCNk

Next, to make your current project connect to the MySQL database instead of the local database, you’ll need to modify your connection 
string. Navigate to your web.config file, and modify your provider name to MySql.Data.MySqlClient and change connection string 
to the connection string of the MySQL db you just created. To find your connection string, go to your azure portal and navigate 
to "All Resources". Click on the MySQL database you made for this project. Click on "All settings" then click on "Properties".

![2-1](/Others/_images/API II/2-1.PNG)

Scroll down and you should see the connection string of your MySQL db:

![2-2](/Others/_images/API II/2-2.PNG)

Replace the old connection string in the web.config file with your MySQL db connection string. Everything should look something like 
this:

  <connectionStrings>
    <add name="YourAppNameContext" providerName="MySql.Data.MySqlClient" connectionString="Database=msauniapp;Data Source=us-cdbr-azure-southcentral-e.cloudapp.net;User Id=youruserid;Password=yourpassword" />
  </connectionStrings>
  
Lastly, add DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))] on top of your context class declaration, like this:

![2-3](/Others/_images/API II/2-3.PNG)

Publish your web app to azure. You're almost done - onto the final step.

## 3. Enable CORS

Right now, no one else will be able to access your API. To let others access your API, you will need to enable cross origin requests, 
otherwise known as CORS. Navigate to your app start folder, and into your webapiconfig.cs class. Add these two lines into the Register 
method:

  var cors = new EnableCorsAttribute("*", "*", "*");
  config.EnableCors(cors);

For simplicity, we've allowed any origin to access your API for now. However as you can guess, this is not very safe at all. Here is a 
real good explanation of what CORS is, and the link also shows you some better ways you can configure CORS to allow your chosen origins to 
access your API:
http://www.asp.net/web-api/overview/security/enabling-cross-origin-requests-in-web-api

Publish your web app to azure, go to swagger and you should be able to successfully make calls to manipulate the data in your MySQL db. 
Hooray, we've created our own web API!
