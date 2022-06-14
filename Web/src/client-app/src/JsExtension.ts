declare global {
  interface StringConstructor {
    readonly empty: "";
    isUnvalidOrWhiteSpace: (str: string | null | undefined) => str is null | undefined;
  }

  interface Array<T> {
    take: (n: number) => T[];
    last: () => T;
    first: () => T;
    any: () => boolean;
    single: () => boolean;
  }

  interface ReadonlyArray<T> {
    last: () => T;
    first: () => T;
    any: () => boolean;
  }
}

if (String.empty === undefined) {
  Object.defineProperty(String, 'empty', {
    get: () => "",
  });
}

function isWhiteSpace(c: string) {
  switch (c) {
    case ' ':
    case '\r':
    case '\n':
    case '\t':
      return true;
  }
}

if (String.isUnvalidOrWhiteSpace === undefined) {
  Object.defineProperty(String, 'isUnvalidOrWhiteSpace', {
    value: function (str: string | null | undefined): str is string {
      if (str === null || str === undefined) return true;
      for (let index = 0; index < str.length; index++) {
        if (!isWhiteSpace(str[index])) return false;;

      }

      return true;
    },
    writable: false
  });
}

if (!Array.prototype.last) {
  // eslint-disable-next-line
  Object.defineProperty(Array.prototype, 'last', {
    value: function () {
      return this[this.length - 1];
    },
    writable: false,
  });
}

if (!Array.prototype.first) {
  // eslint-disable-next-line
  Object.defineProperty(Array.prototype, 'first', {
    value: function () {
      return this[0];
    },
    writable: false,
  });
}

if (!Array.prototype.any) {
  // eslint-disable-next-line
  Object.defineProperty(Array.prototype, 'any', {
    value: function () {
      return (this as Array<any>).length !== 0;
    },
    writable: false,
  });
}

if (!Array.prototype.take) {
  // eslint-disable-next-line
  Object.defineProperty(Array.prototype, 'take', {
    value: function (n: number) {
      return (this as Array<any>).slice(0, n);
    },
    writable: false,
  });
}

export default global;