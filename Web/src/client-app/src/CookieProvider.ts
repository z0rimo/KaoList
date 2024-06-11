class CookieProvider {
  setCookie(name: string, value: string, exp: Date) {
    let expires = "expires=" + exp.toUTCString();
    document.cookie = encodeURIComponent(name) + "=" + encodeURIComponent(value) + ";" + expires + ";path=/";   
  }

  getCookie(name: string): string | null {
    let cname = encodeURIComponent(name) + "=";
    let decodedCookie = decodeURIComponent(document.cookie);
    let ca = decodedCookie.split(';');
    for (let i = 0; i < ca.length; i++) {
      let c = ca[i];
      while (c.charAt(0) === ' ') {
        c = c.substring(1);
      }
      if (c.indexOf(cname) === 0) {
        return c.substring(cname.length, c.length);
      }
    }

    return null;
  }

  removeCookie(name: string) {
    document.cookie = name + '=; Max-Age=-99999999;';
  }

  setBanner(value: string, exp: Date) {
    this.setCookie('banner', value, exp);
  }

  getBanner() {
    return this.getCookie('banner');
  }

  removeBanner() {
    this.removeCookie('banner');
  }
}

export default CookieProvider;