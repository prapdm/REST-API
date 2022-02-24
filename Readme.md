# Shop API
> Example of REST API with ASP.NET Core 5 (C#) with Entity Framework
> 
> ```
>Login: admin@manager.pl
> Password: Password123
> ```

>Run in Docker: https://hub.docker.com/repository/docker/avenus/webapi
>
>Check Online: https://webshopapi.azurewebsites.net/swagger/index.html



## Table of Contents
* [General Info](#general-information)
* [Postman documentation](#postman-documentation)
* [Technologies Used](#technologies-used)
* [Features](#features)
* [Database relationshop diagram](#databaseRelationshopDiagram)
* [Dependencies](#dependencies)
* [Project Status](#project-status)
* [Contact](#contact)
* [License](#license)



## General Information
- Manage users, shop and products
- Write with ASP.NET 5 (C#)  
- For education purpose
<!-- You don't have to answer all the questions - just the ones relevant to your project. -->

## Postman documentation

https://documenter.getpostman.com/view/17808423/UVknubvY

## Technologies Used
- ASP.NET Core 5
- SQL Server 2014


## Features
- Register, activate user
- JWT Bearer Token authentication
- Role autorization and custom policy autorization 
- Entity Framework with example data 
- Loging errors and exeptions to file
- Loging request longer than 5s
- Pagination
- Files upload support
- XML/JSON output support
- Custom validators with FluentValidation
- Roles: Admin, Manager, User
- Search users, services, categories 
- Sort tables
- Swagger documentation


## Database relationshop diagram
[https://dbdiagram.io/d/620e1b5d485e433543cb93ff](https://dbdiagram.io/d/620e1b5d485e433543cb93ff)


 
## Dependencies
Packeges used in the project:

- AutoMapper 11.0.1
- AutoMapper.Extensions.Microsoft.DependencyInjection 11.0.0
- FluentValidation 10.3.6
- FluentValidation.AspNetCore 10.3.6
- Microsoft.AspNetCore.Authentication.JwtBearer 5.0.14
- Microsoft.AspNetCore.Mvc.NewtonsoftJson 5.0.14
- Microsoft.EntityFrameworkCore 5.0.14
- Microsoft.EntityFrameworkCore.SqlServer 5.0.14
- Microsoft.EntityFrameworkCore.Tools 5.0.14
- NLog.Web.AspNetCore 4.14.0
- Swashbuckle.AspNetCore 5.6.3


## Project Status
Project is _finished_ and no longer being worked on.  Project is only for example and education purpose.



## Contact
Created by [@pdm](https://www.linkedin.com/in/pawe%C5%82-dmochowski/) - feel free to contact me!


<!-- Optional -->
## License 
This project is open source. Fell free to use and modify.

 