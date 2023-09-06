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
    "MyPage": {
        "Privacy": string;
        "Profile information": string;
        "Email address": string;
        "Nickname": string;
        "Last modified": string;
        "Change password": string;
        "Agree to receive email": string;
        "Show up personal songs": string;
        "Show up follower list": string;
        "Social links": string;
        "Add social link": string;
        "Added social link": string;
        "YouTube": string;
        "Spotify": string;
        "Twitch": string;
        "Instagram": string;
        "Facebook": string;
        "Twitter": string;
        "External login": string;
        "Delete account": string;
        "More": string;
        "Activity data manage": string;
        "Posting list": string;
        "Recent posts list": string;
        "Recent comments list": string;
        "Recent sang list": string;
        "Song follow list": string;
        "User follow list": string;
        "User block list": string;
        "Playlist list": string;
        "Activity list": string;
        "Recent song search history": string;
        "Recent post search history": string;
        "Access history": string;
        "Search": string;
        "No results": string;
        "Hasn't posted anything": string;
        "Hasn't commented anything": string;
        "Hasn't sang anything": string;
        "Hasn't followed anything": string;
        "Hasn't followed anyone": string;
        "Hasn't blocked anything": string;
        "Hasn't blocked anyone": string;
        "Last updated": string;
        "Sync your YouTube playlists": string;
        "Syncing YouTube playlists": string;
        "No playlist": string;
        "No song search history": string;
        "No post search history": string;
        "Search history clear": string;
        "Access history clear": string;
        "Posts": string;
        "Comments": string;
        "Songs": string;
        "Follow": string;
        "Block": string;
        "List": string;
        "History": string;
        "Representative song": string;
        "Change": string;
    },
}

export type I18nResourcesKeyType = keyof Resources;

export default interface II18nResources extends ResourceLanguage, Resources { }