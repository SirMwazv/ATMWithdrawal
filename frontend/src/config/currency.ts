/**
 * Currency configuration for the ATM application
 * Change these values to update the currency throughout the app
 */
export const CURRENCY_CONFIG = {
  symbol: 'R',
  name: 'Rands',
  code: 'ZAR',
} as const;

/**
 * Format a number as currency
 */
export const formatCurrency = (amount: number, decimals: number = 2): string => {
  return `${CURRENCY_CONFIG.symbol}${amount.toFixed(decimals)}`;
};

/**
 * Get currency symbol
 */
export const getCurrencySymbol = (): string => {
  return CURRENCY_CONFIG.symbol;
};
