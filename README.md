#  System Overview

A full-stack product catalog system built with **ASP.NET Core (.NET 8)** and **React + TypeScript**, implementing clean architecture, JWT authentication, image uploads, Excel import/export, and concurrency handling.

---

#  Tech Stack

##  Backend

* ASP.NET Core (.NET 8)
* Entity Framework Core
* SQL Server
* JWT Authentication
* Cloudinary (image storage)
* EPPlus (Excel processing)

##  Frontend

* React (Vite)
* TypeScript
* Axios
* React Router

---

#  Architecture

This project follows **Clean Architecture** principles:

```
API → Application → Domain → Infrastructure
```

* **API Layer**: Controllers, Middleware
* **Application Layer**: Services, DTOs, Interfaces
* **Domain Layer**: Entities, Value Objects
* **Infrastructure Layer**: Repositories, External services

---

#  Features

##  Backend

* JWT Authentication & Authorization
* Product & Category Management
* Image Upload (Cloudinary)
* Excel Import (Bulk Products)
* Concurrency Handling (RowVersion)
* Global Exception Middleware
* Standardized API Responses
* Paging Support

##  Frontend

* Login with JWT
* Protected Routes
* Product Listing
* Excel Upload UI
* API Integration with Axios Interceptors

---

#  System Flow

```
React App → ASP.NET API → SQL Server
                      → Cloudinary
```

---

#  Repositories

| Layer | Repository                                   |
| ----- | -------------------------------------------- |
| API   | https://github.com/ |
| Web   | https://github.com/PhilaneS/PPECBWeb         |

---

#  Backend Setup

## 1️ Clone repository

```bash
git clone https://github.com/
cd product-api
```

---

## 2️ Configure `appsettings.json`

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=ProductDb;Trusted_Connection=True;TrustServerCertificate=True;"
  },
  "JwtSettings": {
    "Key": "your_super_secret_key",
    "Issuer": "Dev",
    "Audience": "DevUser",
    "ExpiryMinutes": 60
  },
  "Cloudinary": {
    "CloudName": "your_cloud_name",
    "ApiKey": "your_api_key",
    "ApiSecret": "your_api_secret"
  }
}
```

---

## 3️ Apply migrations

``
dotnet ef database update
```

---

## 4️ Run API

```
dotnet run
```

 API Base URL:

```
https://localhost:5000
```

 Swagger:

```
https://localhost:5000/swagger
```

---

#  Frontend Setup

## 1️ Clone repository

```
git clone https://github.com/PhilaneS/PPECBWeb.git
cd ppecd-product-app
```

---

## 2️ Install dependencies

```
npm install
```

---

## 3️ Configure API URL

Update:

```
src/api/axios.ts
```

```ts
baseURL: "https://localhost:500/api"
```

---

 4️ Run React App

```
npm run dev
```

 Frontend URL:

```
http://localhost:500
```

---

#  Authentication Flow

1. User logs in via `/login`
2. API returns JWT token
3. Token stored in browser (`localStorage`)
4. Axios attaches token automatically:

```
Authorization: Bearer <token>
```

---

#  Project Structure

## Backend

```
API/
Application/
Domain/
Infrastructure/
```

## Frontend

```
src/
 ├── api/
 ├── models/
 ├── services/
 ├── pages/
 ├── components/
 └── utils/
```

---

#  Excel Upload

* Endpoint: `POST /api/product/upload-excel`
* Content-Type: `multipart/form-data`
* Supported format: `.xlsx`

---

# Image Upload

* Handled via Cloudinary
* Images stored per user folder:





# Author

**Your Name**

---
