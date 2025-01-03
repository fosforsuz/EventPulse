# EventPulse - Event Management and Analysis Platform

## Project Description
EventPulse is a robust event management platform that allows users to create, manage, and participate in events. It incorporates key features such as authentication, authorization with JWT, notifications, and user-friendly APIs adhering to Domain-Driven Design (DDD) principles.

---

## Features
- **User Authentication and Authorization**: Secure login with JWT token-based authentication.
- **Event Management**:
  - Create, update, and view events.
  - Manage event participation.
- **Notifications**:
  - Notifications for event-related activities.
  - Push, in-app, and email notification integration.
- **Domain-Driven Design (DDD)**:
  - Structured layers: API, Application, Infrastructure, and Domain.
  - Commands, Queries, and Handlers for clear responsibility segregation.

---

## Technologies Used
- **Backend**:
  - ASP.NET Core 7.0
  - MediatR (CQRS implementation)
  - FluentValidation (input validation)
  - Entity Framework Core
- **Database**:
  - SQL Server
- **Security**:
  - JWT (JSON Web Token) for authentication and authorization
  - BCrypt for password hashing
- **Additional Tools**:
  - Dependency Injection
  - Response modeling for consistent API responses

---

## Project Structure

```plaintext
EventPulse
├── Api
│   ├── Controllers
│   ├── Models
│   └── Program.cs
├── Application
│   ├── Commands
│   │   ├── User
│   │   ├── Event
│   └── Queries
│       ├── User
│       ├── Event
├── Domain
│   ├── Entities
│   ├── ValueObjects
│   └── Interfaces
├── Infrastructure
│   ├── Persistence
│   ├── Repositories
│   ├── Security
│   │   ├── JwtService.cs
│   │   └── IJwtService.cs
└── Tests
    └── UnitTests
```

### Layers
1. **API Layer**:
   - Handles HTTP requests and responses.
   - Implements controllers for each resource (e.g., UserController, EventController).

2. **Application Layer**:
   - Contains CQRS implementations: Commands and Queries.
   - Includes Handlers to manage business logic.

3. **Domain Layer**:
   - Core domain logic with entities and value objects.
   - Interfaces for repositories.

4. **Infrastructure Layer**:
   - Contains implementations for repositories and external services (e.g., JWT).
   - Database context and migrations.

---

## Current Implementations

### **1. User Authentication**
- JWT-based authentication.
- Command: `AuthenticateCommand`
- Handler: `AuthenticateCommandHandler`
- Token generation in `JwtService`.

### **2. Event Management**
- **Commands**:
  - `CreateEventCommand`
  - `UpdateEventCommand`
- **Queries**:
  - `GetActiveEventsQuery`
  - `GetEventByIdQuery`
- **Handlers**:
  - `CreateEventCommandHandler`
  - `GetActiveEventsQueryHandler`

### **3. Notifications**
- Notifications for event participation.
- `EventParticipantAddedEvent` handled by `EventParticipantAddedNotificationHandler`.

---

## Configuration

### **AppSettings.json**
```json
{
  "Jwt": {
    "Secret": "YourSuperSecureSecretKey123456789!",
    "Issuer": "EventPulseAPI",
    "Audience": "EventPulseFrontend",
    "TokenLifetimeInMinutes": 120
  },
  "ConnectionStrings": {
    "EventPulseConnection": "Server=localhost;Database=EventPulseDB;User Id=sa;Password=YourSecurePassword;"
  }
}
```

---

## How to Run

### Prerequisites
- .NET SDK 7.0 or higher
- SQL Server

### Steps
1. Clone the repository:
   ```bash
   git clone https://github.com/yourusername/EventPulse.git
   ```
2. Navigate to the project directory:
   ```bash
   cd EventPulse
   ```
3. Update the `appsettings.json` file with your configuration.
4. Apply database migrations:
   ```bash
   dotnet ef database update
   ```
5. Run the application:
   ```bash
   dotnet run
   ```
6. Access the API at:
   ```
   http://localhost:5000
   ```

---

## To Do
- **Logging**: Integrate a logging system for monitoring and debugging.
- **Real-Time Notifications**: Add SignalR or WebSocket for real-time updates.
- **Testing**: Expand unit tests and add integration tests.

---

## Contributors
- **Enes Baba**
  - Backend Development
  - API Design
  - Authentication and Authorization

