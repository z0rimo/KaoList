import i18n from "i18next";
import { initReactI18next } from "react-i18next";
import en from './en';
import ko from './ko';

// the translations
// (tip move them in a JSON file and import them,
// or even better, manage them separated from your code: https://react.i18next.com/guides/multiple-translation-files)


i18n.use(initReactI18next) // passes i18n down to react-i18next
    .init({
        resources: {
            en: en,
            ko: ko
        },
        lng: window.navigator.language ?? 'en', // if you're using a language detector, do not define the lng option
        fallbackLng: 'ko',
        interpolation: {
            escapeValue: false // react already safes from xss
        }
    });

export default i18n;