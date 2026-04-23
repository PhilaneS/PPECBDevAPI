System Overview

- Frontend (React + TypeScript)
        
- Backend API (.NET 8 + JWT + EF Core)
        
- SQL Server + Cloudinary

Repositories

- API Repo => https://github.com/PhilaneS/PPECBDevAPI.git
- Web Repo => https://github.com/PhilaneS/PPECBWeb.git

1. Backend Setup (.NET API)
   -
   From powershel or Terminal (VS)
  git clone https://github.com/PhilaneS/PPECBDevAPI.git
   cd API

- Config appsettings.json
   {
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=ProductDb;Trusted_Connection=True;TrustServerCertificate=True;"
  },
  "JwtSettings": {
    "Key": "your_super_secret_key_32_chars_min",
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
- Apply database migrations
   dotnet ef database update
   
- Run API
   dotnet run
   API will run at: https://localhost:5000
   Swagger: https://localhost:5000/swagger
   
2. Frontend Setup (React + TypeScript)
   -
   - git clone  https://github.com/PhilaneS/PPECBWeb.git
   - cd ppecd-product-app

     Install dependencies
     - npm install

    Configure API URL
   
   src/api/axios.ts

   App runs at:
   
   http://localhost:5173

   Authentication Flow
    1. Go to /login
    2. Enter credentials
    3. API returns JWT
    4. Token stored in localStorage
    5. Axios attaches token automatically:
         Authorization: Bearer <token>

    Features Implemented
        Backend          
        ✔ JWT Authentication
        ✔ Clean Architecture
        ✔ Product CRUD
        ✔ Category management
        ✔ Image upload (Cloudinary)
        ✔ Excel import (EPPlus)
        ✔ Concurrency handling (RowVersion)
        ✔ Global exception middleware
        ✔ Standard API responses
        
        Frontend      
        ✔ React + TypeScript
        ✔ Login system
        ✔ Protected routes
        ✔ Product list
        ✔ Excel upload UI
        ✔ Axios interceptor (JWT)
   
3. How to Run the Full System
   -
        Step 1
        Run API:  
        dotnet run
        
        Step 2
        Run React:
        npm run dev 

Step 3
Open browser:
http://localhost:5173

Step 4

Test flow:

Login
View products
Upload Excel
Create product



