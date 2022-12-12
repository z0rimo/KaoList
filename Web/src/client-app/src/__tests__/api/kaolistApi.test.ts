
import KaoListApi from '../../api/kaolistApi'

// These tests check that the message size doesn't change without us being aware of it and making a conscious decision to increase the size

const fetchMock = jest.fn((input: RequestInfo | URL, init?: RequestInit | undefined) =>
    Promise.resolve<Response>({
        json: () => Promise.resolve({ rates: { CAD: 1.42 } }),
    } as Response)
);

declare var fetch : typeof fetchMock
global.fetch = fetchMock;

describe("Kaolist api", () => {
    beforeEach(() => {
        fetch.mockClear();
    });


    it("will build the query expression.", async () => {        
        await KaoListApi.getAsync('/api/list', {
            part: ['id', 'snippet']
        })
        
        
        expect(fetch.mock.calls[0][0]).toBe('/api/list?part=id&part=snippet');
    });
});