import CookieProvider from "../../CookieProvider";

type QueryValueType = string | number | Date | boolean;

export type QueryType<T extends object = { [key: string]: string[] | number[] | QueryValueType }> = {
  [P in keyof T]?: T[P] extends (string[] | number[] | QueryValueType | undefined) ? T[P] : QueryValueType | undefined;
};

export abstract class ApiServiceBase {
  private _origin: string;
  private _cookieProvider: CookieProvider;

  constructor(origin: string, cookieProvider?: CookieProvider) {
    this._origin = origin;
    this._cookieProvider = cookieProvider ?? new CookieProvider();
  }

  protected get origin(): string {
    return this._origin;
  }

  protected set origin(v: string) {
    this._origin = v;
  }

  public get cookieProvider(): CookieProvider {
    return this._cookieProvider;
  }

  protected static getQueryValue = (v: QueryValueType): string => {
    if (typeof v === "string") {
      return v;
    }

    if (v instanceof Date) {
      return v.toISOString();
    }

    return v.toString();
  }

  protected static buildQuery(query: QueryType): string {
    const queryParams = new URLSearchParams();
    Object.entries(query).forEach(([key, value]) => {
      if (value !== undefined) {
        if (Array.isArray(value)) {
          value.forEach(val => queryParams.append(key, this.getQueryValue(val)));
        } else {
          queryParams.append(key, this.getQueryValue(value));
        }
      }
    });

    return queryParams.toString();
  }

  protected getDefaultHeader = (): { [key: string]: string } => {
    let headers: { [key: string]: string } = {}

    let token = this._cookieProvider.getBanner();
    if (token !== null) {
      headers["Authorization"] = `Banner ${token}`;
    }

    return headers;
  }

  protected async getAsync(url: string, query?: QueryType, init?: RequestInit): Promise<Response> {
    let headers = this.getDefaultHeader();
    const fullUrl = this.buildUrl(url, query);

    const response = await fetch(fullUrl, {
      ...init,
      method: 'GET',
      headers: {
        ...headers,
        ...init?.headers
      }
    });

    if (!response.ok) {
      throw new Error(`HTTP error! status: ${response.status}`);
    }

    return response;
  }

  protected async postAsync(url: string, data: any, query?: QueryType, init?: RequestInit): Promise<Response> {
    const fullUrl = this.buildUrl(url, query);

    const response = await fetch(fullUrl, {
      body: data,
      ...init,
      method: 'POST',
      headers: {
        ...this.getDefaultHeader(),
        ...init?.headers
      }
    });

    if (!response.ok) {
      throw new Error(`HTTP error! status: ${response.status}`);
    }

    return response;
  }

  protected async postJsonAsync(url: string, data: any, query?: QueryType, init?: RequestInit): Promise<Response> {
    return this.postAsync(url, JSON.stringify(data), query, {
      ...init,
      headers: {
        "Content-Type": "application/json",
        ...init?.headers
      }
    });
  }

  protected async putAsync(url: string, data?: BodyInit | null, query?: QueryType, init?: RequestInit): Promise<Response> {
    const fullUrl = this.buildUrl(url, query);

    const response = await fetch(fullUrl, {
      body: data,
      ...init,
      method: 'PUT',
      headers: {
        ...this.getDefaultHeader(),
        ...init?.headers
      }
    });

    if (!response.ok) {
      throw new Error(`HTTP error! status: ${response.status}`);
    }

    return response;
  }

  protected async putJsonAsync(url: string, data: any, query?: QueryType, init?: RequestInit): Promise<Response> {
    return this.putAsync(url, JSON.stringify(data), query, {
      ...init,
      headers: {
        "Content-Type": "application/json",
        ...init?.headers
      }
    });
  }

  protected async deleteAsync(url: string, query?: QueryType, data?: any, init?: RequestInit): Promise<Response> {
    const fullUrl = this.buildUrl(url, query);

    const response = await fetch(fullUrl, {
      body: data,
      ...init,
      method: 'DELETE',
      headers: {
        ...this.getDefaultHeader(),
        ...init?.headers
      }
    });

    if (!response.ok) {
      throw new Error(`HTTP error! status: ${response.status}`);
    }

    return response;
  }

  protected async deleteJsonAsync(url: string, query?: QueryType, data?: any, init?: RequestInit): Promise<Response> {
    return this.deleteAsync(url, query, JSON.stringify(data), {
      ...init,
      headers: {
        "Content-Type": "application/json",
        ...init?.headers
      }
    });
  }

  protected buildUrl = (path: string, query?: QueryType): string => {
    let url = this.origin + path;
    if (query && Object.keys(query).length > 0) {
      url += `?${ApiServiceBase.buildQuery(query)}`;
    }

    return url;
  }
}

export default ApiServiceBase;