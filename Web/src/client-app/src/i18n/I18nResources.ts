import { ResourceLanguage } from 'i18next';

interface Resources {
    'Header': {
        'Home': string;
        'Chart': string;
        'Community': string;
        'Liked Chart': string;
        'Discover Chart': string;
        'Playlist': string;
        'Sign In': string;
        "Sign Out": string;      
    },
    "UserProfileDropdown": {
        "Account Settings": string;
    },
    "PlaylistTitle": {
        "Playlist": string;
    },
    "PlaylistEdit": {
        "Add playlist": string;
        "Remove playlist": string;
    },
    "Playlist": {
        "List": {
            "Share": string;
        },
        "Song": {
            "Thumbnail of {0}": string;
        }
    },
    "Searchbar": {
        "Search Song": string;
    },
    "Footer": {
        'Terms of Service': string;
        'Privacy Policy': string;
        'Inquiry': string;
    },
    'Home': {
        'Title': string;
        'Artist': string;
        'Rank': string;
        'Liked Song': string;
        'New Song Update': string;
        'More Chart': string;
    },
    "Chart": {
        "Liked Chart": string;
        "Filter": string;
        "Period": string;
        "Daily": string
        "Monthly": string;
        "All": string;
        "Rank": string;
        "Thumbnail": string;
        "Title/Artist": string;
        "Artist": string;
        "Like": string;
        "Thumbnail of {0}": string;
        "Other Users": string;
        "Songs I have sung": string;
        "Follow": string;
        "Blind": string;
    },
}

export type I18nResourcesKeyType = keyof Resources;

export default interface II18nResources extends ResourceLanguage, Resources { }