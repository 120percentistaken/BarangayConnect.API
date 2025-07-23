# Barangay Connect API 🚀

A centralized digital platform that empowers residents to interact with their Barangay offices.  
Built using **ASP.NET Core Web API** with **Entity Framework Core** and tested with **Postman**.

---

##  Features

- Submit barangay requests (clearance, certificate, blotter)
- View community announcements
- Submit complaints
- Contact barangay officials
- Participate in community polls (future)
- Track request status

---

## 🛠 Tech Stack

- **C# / ASP.NET Core Web API**
- **Entity Framework Core**
- **SQL Server LocalDB**
- **Postman** (for API testing)
- **Visual Studio Code**

---

## API Testing (Postman)

Use the included Postman collection:  
`/postman/BarangayConnect.postman_collection.json`

1. Open Postman
2. Import collection
3. Set base URL to:  
   `https://localhost:5001` or `http://localhost:5000`
4. Use endpoints like:
   - `GET /api/request`
   - `POST /api/request`
   - `GET /api/complaint`
   - etc.

---

## Getting Started

### Prerequisites

- .NET 7 or 8 SDK installed
- SQL Server LocalDB (or change connection string)
- Postman

### Run the project:

```bash
dotnet ef database update
dotnet run
