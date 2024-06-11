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

  protected static getQueryValue = (v: QueryValueType) => {
    if (typeof v === "string") {
      return v;
    }

    if (v instanceof Date) {
      return v.toJSON();
    }

    return v.toString();
  }

  protected static buildQuery(query: QueryType) {
    let q = "";
    Object.keys(query).forEach(key => {
      const v = query[key];
      if (v === undefined) {
        return;
      }

      if (v instanceof Array) {
        for (const el of v) {
          q += `${key}=${this.getQueryValue(el)}&`
        }
      } else {
        q += `${key}=${this.getQueryValue(v)}&`
      }
    });

    return q.slice(0, -1);
  }

  protected getDefaultHeader = () => {
    let headers: { [key: string]: string } = {}

    let token = this.cookieProvider.getBanner();
    if (token !== null) {
      headers["Authorization"] = `Banner ${token}`;
    }

    return headers;
  }

  protected getAsync = (url: string, query?: QueryType, init?: RequestInit) => {
    let headers = this.getDefaultHeader();
    const fullUrl = this.buildUrl(url, query);

    return fetch(fullUrl, {
      ...init,
      method: 'GET',
      headers: {
        ...headers,
        ...init?.headers
      }
    });
  }

  protected postAsync = (url: string, data: any, query?: QueryType, init?: RequestInit) => {
    const fullUrl = this.buildUrl(url, query);

    return fetch(fullUrl, {
      body: data, // body data type must match "Content-Type" header
      ...init,
      method: 'POST',
      headers: {
        ...this.getDefaultHeader(),
        ...init?.headers
      }
    });
  }

  protected postJsonAsync = (url: string, data: any, query?: QueryType, init?: RequestInit) => {
    return this.postAsync(url, JSON.stringify(data), query, {
      ...init,
      headers: {
        "Content-Type": "application/json",
        ...init?.headers
      }
    });
  }

  protected putAsync = (url: string, data?: BodyInit | null | undefined, query?: QueryType, init?: RequestInit) => {
    const fullUrl = this.buildUrl(url, query);

    return fetch(fullUrl, {
      body: data, // body data type must match "Content-Type" header
      ...init,
      method: 'PUT',
      headers: {
        ...this.getDefaultHeader(),
        ...init?.headers
      }
    });
  }

  protected putJsonAsync = (url: string, data: any, query?: QueryType, init?: RequestInit) => {
    return this.putAsync(url, JSON.stringify(data), query, {
      ...init,
      headers: {
        "Content-Type": "application/json",
        ...init?.headers
      }
    });
  }

  protected deleteAsync = (url: string, query?: QueryType, data?: any, init?: RequestInit) => {
    const fullUrl = this.buildUrl(url, query);

    return fetch(fullUrl, {
      body: data, // body data type must match "Content-Type" header
      ...init,
      method: 'DELETE',
      headers: {
        ...this.getDefaultHeader(),
        ...init?.headers
      }
    });
  }

  protected deleteJsonAsync = (url: string, query?: QueryType, data?: any, init?: RequestInit) => {
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
    if (query !== undefined && Object.values(query).filter(query => query !== undefined).length > 0) {
      url += `?${ApiServiceBase.buildQuery(query)}`;
    }

    return url;
  }
}

export default ApiServiceBase;