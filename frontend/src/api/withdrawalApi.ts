import axios, { AxiosError } from 'axios';

const API_BASE_URL = import.meta.env.VITE_API_URL || 'http://localhost:5001/api';

/**
 * Interface for withdrawal request payload
 */
export interface WithdrawalRequest {
  amount: number | null;
}

/**
 * Interface for withdrawal response
 */
export interface WithdrawalResponse {
  notes: number[];
  totalAmount: number;
  noteCount: number;
}

/**
 * Interface for error response
 */
export interface ErrorResponse {
  message: string;
  errorType: string;
  statusCode: number;
}

/**
 * API client for ATM withdrawal operations
 */
class WithdrawalApiClient {
  private axiosInstance;

  constructor() {
    this.axiosInstance = axios.create({
      baseURL: API_BASE_URL,
      headers: {
        'Content-Type': 'application/json',
      },
      timeout: 10000,
    });
  }

  /**
   * Process a withdrawal request
   * @param amount - The amount to withdraw
   * @returns Promise with withdrawal result
   */
  async withdraw(amount: number | null): Promise<WithdrawalResponse> {
    try {
      const response = await this.axiosInstance.post<WithdrawalResponse>(
        '/withdrawal',
        { amount } as WithdrawalRequest
      );
      return response.data;
    } catch (error) {
      if (axios.isAxiosError(error)) {
        const axiosError = error as AxiosError<ErrorResponse>;
        if (axiosError.response?.data) {
          throw axiosError.response.data;
        }
      }
      throw {
        message: 'An unexpected error occurred. Please try again.',
        errorType: 'UnknownError',
        statusCode: 500,
      } as ErrorResponse;
    }
  }

  /**
   * Health check endpoint
   * @returns Promise with health status
   */
  async healthCheck(): Promise<{ status: string; service: string }> {
    try {
      const response = await this.axiosInstance.get('/withdrawal/health');
      return response.data;
    } catch (error) {
      throw new Error('API is not available');
    }
  }
}

// Export singleton instance
export const withdrawalApi = new WithdrawalApiClient();
