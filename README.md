
# Online Sharable Notepad
**_Online Notepad_** is an open-source project that provides a web-based notepad application built using `ASP.NET` and integrated with Azure Table Storage. The goal of this project is to create a simple yet efficient online notepad tool that allows users to share notes and files among multiple viewer.

## Table of Contents
- [Overview & Description](#overview--description)
  - [Features](#features)
  - [Benefits](#benefits)
- [Usage](#usage)
- [Demo](#demo)
- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Tech Stack](#tech-stack)
  - [Installation](#installation)
  - [Deployment](#deployment)
    - [Azure Table Storage](#azure-table-storage)
- [Authors and Acknowledgment](#authors-and-acknowledgment)
  - [Contributors](#contributors)
- [Contributing](#contributing)
- [Versioning & Change log](#versioning--change-log)
  - [V1.0](#v10)
- [License](#license)
- [Contact](#contact)
- [Project Status](#project-status)

## Overview & Description
An online sharable notepad is a digital platform that allows users to create, edit, and manage notes in real-time. It’s designed for simplicity and ease of use, enabling individuals to jot down ideas, tasks, or any information quickly. 

### Features
An online sharable notepad is a versatile tool that can enhance productivity and organization, whether for personal use or collaborative projects. It’s particularly useful for those who prefer digital over traditional note-taking methods.

- **Accessibility:** Accessible from any device with an internet connection.
- **Real-Time Collaboration:** Multiple users can work on the same document simultaneously.
- **File Sharing:** Multiple file can able to share along with the note.
- **Auto-Save:** Notes are saved automatically, reducing the risk of data loss.
- **Cloud-Based:** Notes are stored in the cloud, ensuring they are backed up and retrievable from anywhere.
- **Security:** Some platforms offer password protection to keep notes private.
- **Shareability:** Notes can be shared with others via a unique URL.

### Benefits
Using this online sharable notepad has some benefits.

- **Convenience:** Eliminates the need for physical paper and is available across all your devices.
- **Organization:** Features like tagging and categorization help keep notes organized.
- **Searchability:** Easy to search through a large collection of notes.

## Usage
Provide examples or instructions on how to use the project. This could include code snippets, command-line examples, or screenshots.

![img1](https://github.com/JicoDotNet/e-notepad-AspNet-With-AzureTableStorage/assets/54305438/40682f6a-8ae9-4771-a836-1a4e48e5736a)

## Demo 
- URL - https://e-notepad-dotnet48.azurewebsites.net/
- User Email: demo@demo.in
- Password: demo
> This OTP authentication is only demo purpose. ✨

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

## Authors and Acknowledgment
- **Soubhik Nandy** - _Initial work_ - [@JicoDotNet](https://github.com/JicoDotNet) - Code Owner
- See also the list of [contributors](#contributors) who participated in this project.

### Contributors
- **Debasish Sahoo** - _Architecture_ [@debasishsahoo](https://github.com/debasishsahoo)
- **Tuhin Paul** - _Code Review_ - [@codewithtuhin](https://github.com/codewithtuhin)


## Contributing
This project is a collaborative effort that can involve various forms of participation. Here’s a guide on how you can contribute:

**Submitting Bug Reports**
- **Identify the Bug:** Clearly describe the issue you’ve encountered. Include details such as the context in which the bug occurred, steps to reproduce it, and the expected vs. actual results.
- **Check Existing Issues:** Before submitting a new bug report, search the project’s issues to ensure it hasn’t been reported already.
- **Use the Template:** Follow any issue template provided by the project. This often includes specific details the maintainers need.
- **Include Logs and Screenshots:** If applicable, add logs and screenshots to help maintainers understand the problem.

**Feature Requests**
- **Suggesting Enhancements**: Propose new features or improvements to existing ones. Explain the benefits and potential impact on the project.
- **Discuss in Issues:** Use the project’s issues section to discuss ideas with maintainers and other contributors.
- **Be Patient:** Remember that maintainers are often volunteers. It may take time for them to respond to your request.

**Pull Requests**
- **Fork the Repository:** Create your own copy of the project to work on.
- **Create a Branch:** Make a new branch in your fork for your changes.
- **Make Changes:** Implement your bug fix or feature, adhering to the project's coding standards.
- **Write Tests:** If the project has tests, add tests for your changes to ensure they work as expected.
- **Pull Request:** Submit a pull request to the original repository. Fill in the provided PR template with details of your changes.
- **Code Review:** Be open to feedback and make requested changes during the code review process.

Remember to always read the project’s CONTRIBUTING.md file, as it will contain specific guidelines tailored to the project’s needs. You are open to contributing. 
Happy contributing! 🚀

## Versioning & Change log
We use SemVer for versioning. For the versions available, see the [tags on this repository](https://github.com/JicoDotNet/e-notepad-AspNet-With-AzureTableStorage).

### V1.0
##### New/Changed/Removed features
Initial implementation of this app
##### Features
- Login Page
- Note edit page
- Notes record page
- Note display page
- File sharing with a note

##### Maintenance/Miscellaneous
N/A

## License
This project is licensed under the MIT License - see the [`LICENSE`](https://github.com/JicoDotNet/e-notepad-AspNet-With-AzureTableStorage/blob/master/LICENSE) file for details.
- [MIT License Details](https://choosealicense.com/licenses/mit/)

## Contact
Anyone can contact us regarding this project.
> Email: `github.connect@soubhiknandy.com`

## Project Status
The project is currently in the `alpha` stage; active development is in progress.
> This project was built and developed in 2021. In January 2024 it is migrated from Azure DevOps to Github.