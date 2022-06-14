import { Location } from "react-router-dom";
import "../../JsExtension";

const routePathRegExp = "/{0}|^/{0}/"

const RoutePathRegExp = {
    home: /^\/$/i,
    chart: new RegExp(routePathRegExp.replaceAll('{0}', 'chart'), 'i'),
    community: new RegExp(routePathRegExp.replaceAll('{0}', 'community'), 'i'),
}

export default class HeaderManageNavPages {
    public static get Home(): string {
        return "Home"
    }

    public static get Chart(): string {
        return "Chart"
    }

    public static get Community(): string {
        return "Community"
    }

    public static HomeNavClass(location: Location) {
        return HeaderManageNavPages.PageNavClass(location, HeaderManageNavPages.Home)
    }

    public static ChartNavClass(location: Location) {
        return HeaderManageNavPages.PageNavClass(location, HeaderManageNavPages.Chart)
    }

    public static CommunityNavClass(location: Location) {
        return HeaderManageNavPages.PageNavClass(location, HeaderManageNavPages.Community)
    }

    public static PageNavClass(location: Location, page: string): string | undefined {
        var activePage = HeaderManageNavPages.getPageNameByLocationHelper(location);
        return activePage === page.toLocaleLowerCase() ? "active" : undefined;
    }

    private static getPageNameByLocationHelper(location: Location) {
        const pathname = location.pathname;
        let pageName = undefined;
        for (const key in RoutePathRegExp) {
            if (RoutePathRegExp[key as keyof typeof RoutePathRegExp].test(pathname)) {
                pageName = key;
                break;
            }
        }
        return pageName;
    }
}