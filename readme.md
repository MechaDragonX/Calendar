# Instructions to Run the Application on Windows using Visual Studio

1. Download the Microsoft Axure Cosmos DB Emulator from [this link](https://aka.ms/cosmosdb-emulator)
2. Install the program
3. Open Command Prompt
4. Type in: `"C:\Program Files\Azure Cosmos DB Emulator\Microsoft.Azure.Cosmos.Emulator.exe" /NoFirewall`
5. Wait for a desktop notification that says the program has completely launched.
6. Once it has launched, open the system tray from the taskbar and right click on the Cosmos DB icon.
7. Select `Open Data Explorer`
8. There are four fields there labelled URI, Primary Key, Primary Connection String, and Mongo Connection String. Make sure to edit the following lines in `~\Calendar\Web.config` in Visual Studio:
```xml
<add key="endpoint" value="Enter the URI value here"/>
<add key="authKey" value="Enter the Primary Key here"/>
```
9. Save the file.
10. Click the button in the top-center of the screen labelled `IIS Express (<Defualt Broswer>)`.
11. Please wait for the application to launch.