export interface IApiGlobalOption {
  part?: Array<'snippet'>;
  offset?: number;
  maxResults?: number;
}

export interface IKaoListResponse {
  kind?: string;
  etag?: string;
}

export interface IPageInfo {
  totalResults: number;
  resultsPerPage: number;
}

export interface IKaoListPageResponse extends IKaoListResponse {
  nextPageToken?: number;
  prevPageToken?: number;
  pageInfo?: IPageInfo;
}