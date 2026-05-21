import axios from 'axios';

function generateCorrelationId(): string {
  return crypto.randomUUID();
}

export const apiClient = axios.create({
  baseURL: 'http://localhost:5148/api',
  headers: {
    'Content-Type': 'application/json',
  },
});

apiClient.interceptors.request.use((config) => {
  config.headers['X-Correlation-ID'] =
    generateCorrelationId();

  return config;
});