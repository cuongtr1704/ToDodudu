# Task3 - Todo List Web App

A simple and elegant Todo List web application built with **ASP.NET Core MVC**, **MongoDB**, **Bootstrap**, and **Session-based authentication**.

---

## Features

- User registration and login (with session)
- Password hashing (secure storage)
- Create, update, delete, toggle tasks
- Filter by task status: All / Done / Incomplete
- Each user can only see their own tasks
- Responsive UI with Bootstrap

---

## Tech Stack

- ASP.NET Core MVC
- MongoDB 
- Bootstrap 5 
- Session-based authentication
- Razor Views

---

## How to Run Locally

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [MongoDB Server](https://www.mongodb.com/try/download/community) (running locally or remotely)

---

### Setup Steps

1. **Clone the repository:**

```bash
git clone https://github.com/cuongtr1704/ToDodudu.git
cd Task3
```

2. **Update MongoDB connection string in "appsettings.json":**

```json
    "MongoDB": {
        "ConnectionString": "mongodb+srv://sample:sample@sample.mongodb.net/?retryWrites=true&w=majority&appName=...",
        "DatabaseName": "TodoDB"
    }
```

3. **Restore & run the project:**

```bash
dotnet restore
dotnet run
```
