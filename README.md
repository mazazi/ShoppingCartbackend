# My .NET Core 7 Application

This is a sample .NET Core 7 application that demonstrates [brief description of what the app does].

## Table of Contents
- [Installation](#installation)
- [Usage](#usage)
- [Configuration](#configuration)
- [Running the Tests](#running-the-tests) 

## Installation

1. **Clone the repository:**

    ```sh
    git clone https://github.com/your-username/your-repository.git
    cd your-repository
    ```

2. **Install the necessary dependencies:**

    Make sure you have the .NET SDK installed. You can download it from [here](https://dotnet.microsoft.com/download/dotnet/7.0).

    Restore the dependencies:

    ```sh
    dotnet restore
    ```

## Usage

1. **Build the project:**

    ```sh
    dotnet build
    ```

2. **Run the application:**

    ```sh
    dotnet run
    ```
    WHEN RUNNING APP, ALL OF DATABASE MIGRATION WILL BE RUN AND CREATE THE DATABASE.
 
3. **Access the application:**

    Open your browser and navigate to `http://localhost:5000` (or the port your application is configured to use).

## Configuration
Just change the connection string for DB into TATWEER.API -> APPSETTING .
and also in Tatweer.Insrastructure -> DATA ->TatweerContextFactory.CS

Configuration settings for the application can be found in the `appsettings.json` file. Here you can set different configurations like database connections, logging, API keys, etc.

Example `appsettings.json`:

```json
{
  "Serilog": {
    "Using": [
      "Serilog.Sinks.RollingFile"
    ],
    "MinimumLevel": {
      "Default": "Information",
      "Override": {
        "Microsoft": "Warning",
        "System": "Warning"
      }
    },
    "Enrich": [ "FromLogContext", "WithMachineName", "WithProcessId", "WithThreadId" ],
    "WriteTo": [
      {
        "Name": "RollingFile",
        "Args": {
          "pathFormat": "c:\\logs\\ShoppingCartApp-{Date}.txt",
          "outputTemplate": "{Timestamp:G}{Message}{NewLine:1}{Exception:1}"
        }
      }
    ]
  },
  "AppSettings": { 
    "ItemsPerPage": 10
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "TatweerConnectionString": "Server=.\\SQLEXPRESS;Database=ShoppingCartDB;User ID=***;Password=***;Encrypt=True;TrustServerCertificate=True;"
  }
}
