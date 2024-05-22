
# Online Sharable eNotepad

## Getting Started
Let's start with development & deployment at your own system & server. 
> _It required some software development knowledge._

### Prerequisites
Below things you need to install.
-  Windows 10 or 11 with [Visual Studio 2022](https://visualstudio.microsoft.com/vs/community) >= v.17.9.* `community edition`
- [.NET Framework 4.8](https://dotnet.microsoft.com/download/dotnet-framework/thank-you/net48-developer-pack-offline-installer) SDK `dev` `build`
- Azure Portal Account (https://portal.azure.com/)

### Tech Stack
- [Asp.Net Framework v4.8.*](https://dotnet.microsoft.com/en-us/learn/aspnet/what-is-aspnet)
- [Azure Table Storage](https://learn.microsoft.com/en-us/azure/storage/tables/table-storage-overview)
- [C#](https://dotnet.microsoft.com/en-us/languages/csharp)

### Installation
A step-by-step series of examples that tell you how to get a development environment running.

1. Get the project
    - Clone the repository using `git clone https://github.com/JicoDotNet/e-notepad-AspNet-With-AzureTableStorage.git`
    - Or download the project from Github `https://github.com/JicoDotNet/e-notepad-AspNet-With-AzureTableStorage`
2. Open the solution in Visual Studio.
3. Restore the NuGet packages.
4. Update the connection string in `web.config`
5. Run the application.

### Deployment
You can deploy this project on any Windows based server.

**Deployment requirements**
``` text
1. Windows OS
2. IIS Server
3. .NET Framework 4.8
4. Azure Table Storage Connection String
```

**Steps to Deploy →**
- Change `Web.config` file with
  - Connection String of Azure Table Storage
  - Login Credentials

#### Azure Table Storage 
Azure Table Storage is a cloud-based service that stores structured NoSQL data, providing a key/attribute store with a schematic design. It’s part of the Microsoft Azure cloud platform and allows for the storage of large amounts of non-relational data.
Azure Table Storage is ideal for storing datasets that do not require complex joins or foreign keys, such as user data for web applications, address books, and device information.

_Generate the connection string of **Azure Table Storage** from [Azure Portal](https://portal.azure.com)._

> To generate an Azure Table Storage connection string from the Azure Portal, follow these steps:
 1. Navigate to your **storage account** in the Azure Portal.
 2. In the **Security + networking** section, locate the Access keys setting.
 3. Click on the **Show keys** button at the top of the page to display the account keys and associated connection strings.
 4. Copy the **Connection string** provided there.

**The Connection String looks like this:**
``` xml
DefaultEndpointsProtocol=https;AccountName=[yourAccountName];AccountKey=[yourAccountKey]
```
## Demo 
- URL - https://e-notepad-dotnet48.azurewebsites.net/
- User Email: demo@demo.in
- Password: demo
> This OTP authentication is only demo purpose. ✨
