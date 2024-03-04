# Hotel Management System

This is a learning project aimed at learning Angular, a powerful frontend framework for building web applications. The goal is to create a hotel management system that allows users to register, log in, manage hotel reservations, and perform CRUD operations on rooms and reservations.

## Functional Requirements

### User Stories

- **Registration:** Users can register by providing basic information including name, surname, phone number, email address, password, and confirm password.
- **Authentication:** Users can log in using their registered email address and password.
- **Reservation Management:** Authenticated users can manage hotel reservations, including viewing, adding, updating, and deleting reservations with various dependencies.
- **Room Management:** Users have the ability to manage rooms, including adding, updating, and deleting room information.
- **Archive:** Completed reservations are displayed in a separate view, with the option to display prices in EUR and BAM. User profit is also displayed in both currencies.

## Libraries and Tools

### Backend:
- **Database:** MSSQL
- **Authentication:** JWT token authentication
- **Authorization:** Claims, Role-based authorization
- **Technologies:** .NET Core Framework 8, C#

### Frontend:
- **Framework:** Angular
- **UI Components:** Angular Material
- **CSS Framework:** Tailwind CSS
- **Form Handling:** Reactive Forms
- **Asynchronous Programming:** RxJS
- **State Management:** Redux
- **HTTP Requests:** Fetch API

- ## Getting Started

### Prerequisites

- Install [.NET Core SDK](https://dotnet.microsoft.com/download)
- Install [Node.js](https://nodejs.org/) and npm (Node Package Manager)

- ### Clone the Repository
```bash
git clone https://github.com/milance139/AngularHotel.git
```

### Database Setup

1. Ensure your preferred relational database server is running.
2. Update the connection string in `appsettings.json` with your database credentials.

### Run Migrations
```bash
dotnet ef database update
```
### Frontend Setup

Navigate to the `Client` directory and run:
```bash
npm install
ng serve
```
Now you can access the application by visiting `http://localhost:4200` in your web browser.
