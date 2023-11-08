# .NET Core API Example

## Overview

This repository serves as an illustrative example of a .NET Core API that demonstrates the implementation of various key features and technologies. It showcases the use of design patterns, dependency injection, middleware, extensions, JWT (JSON Web Token) authorization, and Entity Framework. Whether you are a beginner looking to learn these concepts or an experienced developer seeking a reference, this project can serve as a valuable resource.

## Features

- **Design Patterns:** The project integrates well-established design patterns to promote code reusability, maintainability, and scalability.

- **Dependency Injection:** Utilizes the built-in dependency injection framework of .NET Core for managing object lifetimes and promoting loosely coupled components.

- **Extensions:** Demonstrates the use of custom extensions to enhance the functionality of classes and components.

- **JWT Authorization:** Implements JWT-based authentication and authorization for securing API endpoints.

- **Entity Framework:** Utilizes Entity Framework Core for data access, providing a robust and efficient way to interact with the database.

- **SignalR:** Utilizes SignalR for Real-Time Bidirectional Communication.

- **Minimal API:** Utilizes Minimal API include only the minimum files, features, and dependencies in ASP.NET Core.

- **OData Support:** Utilizes OData for interacting with data via RESTful interfaces.

- **Auto Mapper:** Employs AutoMapper to Simplify Object Conversion.

- **Versioning:** Controller and Models Versioning.

- **Localization:** Implements Accept-Language in header for Localization.

- **XUnit Test:** Implements XUnit Tests. Using Moq for emualte Services.

- **CI/CD:** Implements GitHub Actions and Azure AppService.

- **Encryptor:** Implements password encryptor.

- **Azure KeyVault:** Access to Azure KeyVaults Secrets for EncKey and JwtKey.

## Deployed

https://netcoreapi85.azurewebsites.net/swagger/index.html?urls.primaryName=V1

## Create Token
https://netcoreapi85.azurewebsites.net/api/v1/user/authenticate

for admin rights
```ruby
{
  "username": "admin",
  "password": "admin123"
}
```

for user right
```ruby
{
  "username": "user",
  "password": "user123"
}
```

## OData Example
https://netcoreapi85.azurewebsites.net/api/odata/v3/car/cars?$filter=Id eq 1 or Id eq 3&$select=name
