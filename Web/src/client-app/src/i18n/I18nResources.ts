import { ResourceLanguage } from 'i18next';

interface Resources {
    'Header': {
        'Home': string;
        'Chart': string;
        'Community': string;
        'Sign In': string;
        'Sign Out': string;
        'Liked Chart': string;
        'Discover Chart': string;
        'Playlist': string;
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
    }
}


export type I18nResourcesKeyType = keyof Resources;

export default interface II18nResources extends ResourceLanguage, Resources { }