import { Location } from "react-router-dom";
import "../JsExtension";

const mainRoutePathRegExp = "/{0}|^/{0}/"
const customerRoutePathRegExp = "/customer/{0}$|^/customer/{0}/"

const RoutePathRegExp = {
    home: /^\/$/i,
    chart: new RegExp(mainRoutePathRegExp.replaceAll('{0}', 'chart'), 'i'),
    community: new RegExp(mainRoutePathRegExp.replaceAll('{0}', 'community'), 'i'),
    terms: new RegExp(customerRoutePathRegExp.replaceAll('{0}', 'terms'), 'i'),
    policy: new RegExp(customerRoutePathRegExp.replaceAll('{0}', 'policy'), 'i'),
    inquiry: new RegExp(customerRoutePathRegExp.replaceAll('{0}', 'inquiry'), 'i'),
    playlist: new RegExp(mainRoutePathRegExp.replaceAll('{0}', 'playlist'), 'i'),
    mypage: new RegExp(mainRoutePathRegExp.replaceAll('{0}', 'mypage'), 'i')
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

    public static get Playlist(): string {
        return "Playlist"
    }

    public static get MyPage(): string {
        return "MyPage"
    }

    public static get Terms(): string {
        return "Terms"
    }

    public static get Policy(): string {
        return "Policy"
    }

    public static get Inquiry(): string {
        return "Inquiry"
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

    public static PlaylistNavClass(location: Location) {
        return HeaderManageNavPages.PageNavClass(location, HeaderManageNavPages.Playlist)
    }

    public static MyPageNavClass(location: Location) {
        return HeaderManageNavPages.PageNavClass(location, HeaderManageNavPages.MyPage)
    }

    public static TermsNavClass(location: Location) {
        return HeaderManageNavPages.PageNavClass(location, HeaderManageNavPages.Terms)
    }

    public static PolicyNavClass(location: Location) {
        return HeaderManageNavPages.PageNavClass(location, HeaderManageNavPages.Policy)
    }

    public static InquiryNavClass(location: Location) {
        return HeaderManageNavPages.PageNavClass(location, HeaderManageNavPages.Inquiry)
    }

    public static PageNavClass(location: Location, page: string): string | undefined {
        let activePage = HeaderManageNavPages.getPageNameByLocationHelper(location);
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