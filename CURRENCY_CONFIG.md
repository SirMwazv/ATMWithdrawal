# Currency Configuration Guide

## Overview

The ATM Withdrawal application's currency can be easily changed by updating configuration files in both the frontend and backend.

## Frontend Configuration

**Location:** `frontend/src/config/currency.ts`

Update the `CURRENCY_CONFIG` object:

```typescript
export const CURRENCY_CONFIG = {
  symbol: 'R',      // Change to your currency symbol (e.g., '$', '€', '£')
  name: 'Rands',    // Change to your currency name (e.g., 'Dollars', 'Euros')
  code: 'ZAR',      // Change to your currency code (e.g., 'USD', 'EUR', 'GBP')
} as const;
```

### Examples

**US Dollars:**
```typescript
export const CURRENCY_CONFIG = {
  symbol: '$',
  name: 'Dollars',
  code: 'USD',
} as const;
```

**Euros:**
```typescript
export const CURRENCY_CONFIG = {
  symbol: '€',
  name: 'Euros',
  code: 'EUR',
} as const;
```

**British Pounds:**
```typescript
export const CURRENCY_CONFIG = {
  symbol: '£',
  name: 'Pounds',
  code: 'GBP',
} as const;
```

## Backend Configuration

**Location:** `backend/API/Config/CurrencyConfig.cs`

Update the constants in the `CurrencyConfig` class:

```csharp
public static class CurrencyConfig
{
    public const string Symbol = "R";      // Change to your currency symbol
    public const string Name = "Rands";    // Change to your currency name
    public const string Code = "ZAR";      // Change to your currency code
}
```

### Examples

**US Dollars:**
```csharp
public static class CurrencyConfig
{
    public const string Symbol = "$";
    public const string Name = "Dollars";
    public const string Code = "USD";
}
```

**Euros:**
```csharp
public static class CurrencyConfig
{
    public const string Symbol = "€";
    public const string Name = "Euros";
    public const string Code = "EUR";
}
```

## After Making Changes

### Frontend
The frontend will automatically reload if you have the dev server running:
```bash
cd frontend
npm run dev
```

### Backend
Restart the backend server for changes to take effect:
```bash
cd backend
dotnet run --project API/API.csproj
```

Or if using Docker:
```bash
docker compose down
docker compose up --build
```

## Notes

- The currency configuration affects all displays of amounts throughout the application
- Both frontend and backend should use the same currency for consistency
- The note denominations (100, 50, 20, 10) remain the same regardless of currency
- Error messages will automatically display the correct currency symbol
