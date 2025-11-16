# ATM Withdrawal System

A full-stack solution that simulates ATM cash note delivery with minimum note optimization.

## 🏗️ Architecture

### Backend (C# / ASP.NET Core)
- **Domain Layer**: Custom exceptions and business models
- **Application Layer**: Withdrawal service with greedy algorithm
- **API Layer**: REST endpoints with Swagger documentation
- **Tests**: Comprehensive xUnit test coverage

### Frontend (React + TypeScript + Vite)
- Modern, responsive UI with beautiful design
- Real-time error handling
- Visual note breakdown display

### Infrastructure
- Docker containerization for both services
- Docker Compose orchestration
- Health checks and restart policies

## 🚀 Quick Start

### Prerequisites
- Docker and Docker Compose installed
- .NET 8.0 SDK (for local development)
- Node.js 20+ (for local development)

### Run with Docker (Recommended)

```bash
# From the project root
docker compose up --build
```

The services will be available at:
- **Frontend**: http://localhost:3000
- **Backend API**: http://localhost:5000
- **Swagger UI**: http://localhost:5000

### Run Locally

#### Backend
```bash
cd backend
dotnet restore
dotnet run --project API/API.csproj
```

#### Frontend
```bash
cd frontend
npm install
npm run dev
```

### Run Tests
```bash
cd backend
dotnet test
```

## 📋 API Endpoints

### POST `/api/withdrawal`
Process a withdrawal request.

**Request:**
```json
{
  "amount": 30.00
}
```

**Response (Success):**
```json
{
  "notes": [20.00, 10.00],
  "totalAmount": 30.00,
  "noteCount": 2
}
```

**Response (Error):**
```json
{
  "message": "Cannot dispense $125.00. The amount cannot be formed with available notes.",
  "errorType": "NoteUnavailable",
  "statusCode": 400
}
```

## 🎯 Business Rules

1. **Minimum Notes**: Always returns the minimum number of notes
2. **Available Notes**: 100, 50, 20, 10
3. **Impossible Amounts**: Throws `NoteUnavailableException` (e.g., 125, 35, 15)
4. **Negative Amounts**: Throws `InvalidArgumentException`
5. **Null/Empty**: Returns empty set
6. **Infinite Balance**: No balance constraints

## 🧪 Test Coverage

- ✅ Valid withdrawal amounts (30, 80, 100, etc.)
- ✅ Edge cases (null, zero, large amounts)
- ✅ Exception scenarios (negative, impossible amounts)
- ✅ Algorithm verification (greedy approach)
- ✅ Domain exception handling

## 🛠️ Technology Stack

**Backend:**
- ASP.NET Core 8.0 Web API
- C# with nullable reference types
- Swagger/OpenAPI documentation
- xUnit for testing
- SOLID principles & Clean Architecture

**Frontend:**
- React 18
- TypeScript
- Vite
- Axios
- CSS3 with animations

**DevOps:**
- Docker
- Docker Compose
- Multi-stage builds
- Health checks

## 📝 Design Decisions

1. **Greedy Algorithm**: Uses largest notes first for optimal solution
2. **Layered Architecture**: Clear separation of concerns (Domain → Application → API)
3. **Dependency Injection**: Proper DI configuration in ASP.NET Core
4. **Exception Middleware**: Global error handling for consistent API responses
5. **Async/Await**: All operations use async patterns for scalability
6. **Type Safety**: Strongly typed throughout (C# + TypeScript)

## 🎨 Frontend Features

- Beautiful gradient UI design
- Real-time input validation
- Loading states with animations
- Visual note breakdown
- Responsive design for mobile/desktop
- Error display with clear messaging

## 📦 Project Structure

```
ATMWithdrawal/
├── backend/
│   ├── Domain/              # Business entities & exceptions
│   ├── Application/         # Business logic & services
│   ├── API/                 # Controllers & middleware
│   ├── Tests/               # Unit tests
│   ├── Dockerfile
│   └── ATMWithdrawal.sln
├── frontend/
│   ├── src/
│   │   ├── api/            # API client
│   │   ├── components/     # React components
│   │   └── main.tsx
│   ├── Dockerfile
│   └── package.json
└── docker-compose.yml
```

## 🔐 CORS Configuration

The backend is configured to accept requests from:
- `http://localhost:5173` (Vite dev server)
- `http://localhost:3000` (Docker frontend)

## 📄 License

This project is provided as-is for demonstration purposes.
