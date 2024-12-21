# Socialix Server

## Overview
Socialix is a social networking platform designed for the community of [your niche, e.g., art enthusiasts, sports lovers, tech geeks,...]. The server-side of this application is built using .NET 8, Entity Framework Core (EF Core) for database management, and JWT (JSON Web Tokens) for secure authentication and authorization.


## Technologies Used

- .NET 8: A powerful framework for building web applications and APIs by Microsoft, supporting RESTful APIs and WebSockets.

- Entity Framework Core (EF Core): ORM (Object-Relational Mapping) to manage database interactions, specifically with SQL Server.

- JWT (JSON Web Token): Used for authentication and securing APIs. JWT enables the transfer of user identity information between the client and server without maintaining session state.

- ASP.NET Core: Framework for building APIs, with built-in support for dependency injection, middleware, routing, and security features.

- SQL Server: Relational database used for storing user data, posts, groups, and other application-related information.

- Swagger/OpenAPI: Automatically generates API documentation and provides an interface for testing endpoints via the browser.

## System Requirements

- .NET SDK 8.x or higher

- SQL Server (Local or Cloud-based)

- Development Tools: Visual Studio, Visual Studio Code, or any IDE supporting .NET Core

- Docker (optional, if you want to containerize the application)

## Additional Features

- Real-Time Chat: In the future, we plan to implement WebSockets to support real-time messaging functionality.

- Livestreaming: We are also exploring options for integrating live video streaming features.

- User Recommendations: An algorithm will be implemented to recommend users, posts, or groups based on user interests.

## Conclusion

The Socialix server-side is designed to be scalable, secure, and ready for production use. By leveraging .NET 8, EF Core, and JWT tokens, it provides a solid foundation for building and expanding a social networking platform. The integration of SQL Server ensures that the application can scale as needed while maintaining strong data consistency and security.
