import { ResourceLanguage } from 'i18next';

interface Resources {
    'Header': {
        'Home': string;
        'Chart': string;
        'Community': string;
        'SignIn': string;
        'Liked Chart': string;
        'Discover Chart': string;
    }
}


export type I18nResourcesKeyType = keyof Resources;

export default interface II18nResources extends ResourceLanguage, Resources { };