# GoCar

## A fully functional console application written in C#.

Go Car is a console based program which enables users to edit a chosen car rental database. It makes use of a Gui, handled by the ConsoleUI class to ease navigation, and the display of information to the user.
The application allows one to:
* Load data from a selected csv file.
* Load data from a database into a hashtable.
* Add client, rental and client information into the hashtable.
* Remove information from the hashtable.
* Search the hashtable.

# Installation Guide
## What you will need
* Visual studio 2022. Link: https://visualstudio.microsoft.com/downloads/
* Dotnet core and dotnet 8.0.
* SQL server. Link: https://www.microsoft.com/en-us/sql-server/sql-server-downloads
* SQL Server Management Studio (SSMS). Link: https://learn.microsoft.com/en-us/ssms/download-sql-server-management-studio-ssms

### Step one: Get The Code.
The fastest way to get started is to fork the project and initiate a pull request using git.
```git
git pull https://github.com/theoldonee/GoCar.git
```
This program was made using **dotnet 8.0** using the Visual Studio 2022 (recommended IDE for running this program) so you need to ensure your version of dotnet is 8.0 to run this program as intended.

After the pull, ensure that the necessary libraries are installed.
* Microsoft.EntityFrameworkCore, version 9.0.3
* Microsoft.EntityFrameworkCore.SqlServer, version 9.0.3
* Microsoft.EntityFrameworkCore.Tools, version 9.0.3

This can be checked using the **Manage Nuget Packages for Solution** option inside the **Nuget Package Manager** in **Tools**.
![Screenshot 2025-04-14 153230](https://github.com/user-attachments/assets/a7476f32-c1ae-41b8-b655-65469de05661)

The libraries should be fount in the **Installed** tab. However, if one of, or all the libraries are not found in the installed tab, open the **Browse** tab and install.

![image](https://github.com/user-attachments/assets/dadda3b6-c847-43fe-8a4b-27a01239dd97)

### Step two: Get Server Running.
With  **SQL server** and **SQL Server Management Studio** installed, open the SQL Server Management Studio.
If the application opens and the SQL Server window, shown below:

![image](https://github.com/user-attachments/assets/9913be46-eb13-4e9c-8972-58a46c42c9ef)

Does not open automatically, click the plug icon in the **Object Explorer** tool bar

![Screenshot 2025-04-14 1632011](https://github.com/user-attachments/assets/6316d955-a955-493d-90ce-6bac72c00478)

With the SQL Server window open, select your server and ensure **Trust server certificate** is ticked and click **Connect**.
You should see your server in the **Object Explorer** with the server name. Clicking on the server would present you with a drop down menu, shown below.

![Screenshot 2025-04-14 1641322](https://github.com/user-attachments/assets/e360bda2-b14e-482d-b49c-bfea6d3441c4)

Right clicking on the **Database** option would present you with a menu containing the **New Database** option. Click the **New Database** option, name the database appropriately, and click **Ok**.

### Step three: Connect To Database.
To connect to the database, open the **Tools** tab and click on **Connect to Database**.

![image](https://github.com/user-attachments/assets/ff9e077f-6ce1-47b8-9b8f-d7b637559a07)

Clicking **Connect to Database** would open the **Add Connection** window.

![image](https://github.com/user-attachments/assets/93a29d83-beef-47f6-a11e-e5e3bf2eaa2f)

Enter your server name, click **Trust server certificate**, select the database you created and click **Ok**. On the right side of ypur window, you would see the connection to your database with under **Data Connections**.

![image](https://github.com/user-attachments/assets/5bdac73f-4a8e-41c6-a4ff-4ab933475cb7)

Right click on the server connection and select **Properties**. At the bottom right of you window, the **Properties** panel would be open.

![image](https://github.com/user-attachments/assets/928ebb31-2da7-49d9-adb3-e9b4929feaca)

Double clicking the **Connection String** highlights it, copy and replace the connection string in the **OnConfiguring** method of the **CarRentalContext** class.

### Step four: Add migration.
Open the **Nuget Package Manager** and select the **Package Manager Console**.

![image](https://github.com/user-attachments/assets/3c8e82cb-d89b-454f-8ccf-ff36158bbf54)

In the console, run the command
```
Add-Migration InitialMigration
```
#### Note:
If there are any errors in the code, the build will fail.

