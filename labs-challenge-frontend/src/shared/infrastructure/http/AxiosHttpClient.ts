import { IHttpClient } from "@/shared/interface_adapters/IHttpClient";
import axios, { AxiosInstance } from "axios";

export class AxiosHttpClient implements IHttpClient {
  private axiosInstance: AxiosInstance;

  constructor(baseURL: string) {
    this.axiosInstance = axios.create({ baseURL });
  }

  async get<T, P = unknown>(
    url: string,
    params?: P,
    headers?: Record<string, string>
  ): Promise<T> {
    const response = await this.axiosInstance.get<T>(url, { params, headers });
    return response.data;
  }

  async post<T, D = unknown>(
    url: string,
    data?: D,
    headers?: Record<string, string>
  ): Promise<T> {
    const response = await this.axiosInstance.post<T>(url, data, { headers });
    return response.data;
  }

  async put<T, D = unknown>(
    url: string,
    data?: D,
    headers?: Record<string, string>
  ): Promise<T> {
    const response = await this.axiosInstance.put<T>(url, data, { headers });
    return response.data;
  }

  async delete<T, P = unknown>(
    url: string,
    params?: P,
    headers?: Record<string, string>
  ): Promise<T> {
    const response = await this.axiosInstance.delete<T>(url, {
      params,
      headers,
    });
    return response.data;
  }
}
