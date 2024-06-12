import KaoListChartsApi from "../../api/services/KaoListChartsApi";
import KaoListSearchsApi from "../../api/services/KaoListSearchsApi";
import KaoListMyPagesApi from "../../api/services/KaoListMyPagesApi";
import KaoListSongsApi from "../../api/services/KaoListSongsApi";

// Mock fetch function
const fetchMock = jest.fn((input: RequestInfo | URL, init?: RequestInit | undefined) =>
    Promise.resolve<Response>({
        json: () => Promise.resolve({ rates: { CAD: 1.42 } }),
    } as Response)
);

global.fetch = fetchMock;

describe("KaoListApi services", () => {
    const baseUrl = "http://testapi.com";
    let chartsApi: KaoListChartsApi;
    let searchsApi: KaoListSearchsApi;
    let myPagesApi: KaoListMyPagesApi;
    let songsApi: KaoListSongsApi;

    beforeAll(() => {
        chartsApi = new KaoListChartsApi(baseUrl);
        searchsApi = new KaoListSearchsApi(baseUrl);
        myPagesApi = new KaoListMyPagesApi(baseUrl);
        songsApi = new KaoListSongsApi(baseUrl);
    });

    beforeEach(() => {
        fetchMock.mockClear();
    });

    it("will build the query expression for chartsApi", async () => {
        await chartsApi.discoverChartList({
            part: ['snippet']
        });

        expect(fetchMock.mock.calls[0][0]).toBe(`${baseUrl}/api/charts/list/discover?part=snippet`);
    });

    it("will build the query expression for searchsApi", async () => {
        await searchsApi.songSearchList({
            q: ['song1', 'song2']
        });

        expect(fetchMock.mock.calls[0][0]).toBe(`${baseUrl}/api/search/list?q=song1&q=song2`);
    });

    it("will build the query expression for songsApi", async () => {
        await songsApi.songDetail({
            id: 'song123'
        });

        expect(fetchMock.mock.calls[0][0]).toBe(`${baseUrl}/api/songs/detail?id=song123`);
    });

    it("will build the query expression for myPagesApi", async () => {
        await myPagesApi.myPageProfile({
            part: ['snippet']
        });

        expect(fetchMock.mock.calls[0][0]).toBe(`${baseUrl}/api/mypage/profile?part=snippet`);
    });
});