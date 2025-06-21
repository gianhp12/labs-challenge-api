export interface IHttpClient {
  get<T, P = unknown>(
    url: string,
    params?: P,
    headers?: Record<string, string>
  ): Promise<T>;
  post<T, D = unknown>(
    url: string,
    data?: D,
    headers?: Record<string, string>
  ): Promise<T>;
  put<T, D = unknown>(
    url: string,
    data?: D,
    headers?: Record<string, string>
  ): Promise<T>;
  delete<T, P = unknown>(
    url: string,
    params?: P,
    headers?: Record<string, string>
  ): Promise<T>;
}
