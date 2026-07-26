# DevBooking

DevBooking is a modern freelance developer booking and scheduling platform built with **ASP.NET Core** using **Clean Architecture**. It allows clients to book developers, manage appointments, and prevents scheduling conflicts through intelligent slot management.

> 🚧 This project is currently under active development as part of a 12-week Build in Public challenge.

## Features

- 🔐 JWT Authentication & Authorization
- 👤 Role-based Access Control (Client & Developer)
- 📅 Developer Availability Management
- 📆 Appointment Booking
- 🚫 Double-booking Prevention
- 💳 Payment Integration (Planned)
- 🤖 AI-powered Time Slot Suggestions (Planned)
- 📊 Dashboard & Analytics
- 📧 Email Notifications (Planned)

---

## Tech Stack

### Backend
- ASP.NET Core (.NET 10)
- ASP.NET Core Identity
- Entity Framework Core
- SQL Server
- JWT Authentication

### Architecture
- Clean Architecture
- Repository Pattern
- Dependency Injection
- CQRS (Planned)
- FluentValidation (Planned)

---

## Project Structure

```
DevBooking
│
├── DevBooking.Api
├── DevBooking.Application
├── DevBooking.Domain
└── DevBooking.Infrastructure
```

---

## Getting Started

### Clone the repository

```bash
git clone https://github.com/yourusername/DevBooking.git
```

### Navigate to the project

```bash
cd DevBooking
```

### Configure the database

Update the connection string in:

```
DevBooking.Api/appsettings.json
```

Example:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost,1433;Database=DevBooking;User Id=sa;Password=YourPassword;TrustServerCertificate=True;"
}
```

### Apply migrations

```bash
dotnet ef database update \
--project DevBooking.Infrastructure \
--startup-project DevBooking.Api
```

### Run the project

```bash
dotnet run --project DevBooking.Api
```

---

## Development Roadmap

- [x] Clean Architecture Setup
- [x] SQL Server Integration
- [x] ASP.NET Core Identity
- [x] User Registration
- [x] User Login
- [x] JWT Authentication
- [x] Role Authorization
- [ ] Developer Profile
- [ ] Booking System
- [ ] Availability Calendar
- [ ] Conflict Detection
- [ ] Email Notifications
- [ ] AI Slot Recommendation
- [ ] Payments
- [ ] Admin Dashboard

---

## Contributing

Contributions, suggestions, and feedback are welcome.

1. Fork the repository
2. Create a feature branch
3. Commit your changes
4. Open a Pull Request

---

## License

This project is licensed under the MIT License.

---

## Author

**Prasann**

Building in public • Learning ASP.NET Core • Clean Architecture • .NET Developer
