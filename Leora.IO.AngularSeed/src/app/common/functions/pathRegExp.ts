module app.common {

    "use strict";

    // https://github.com/angular/angular.js/blob/master/src/ngRoute/route.js#L194-L222

    export function pathRegExp(path: any, opts: any) {

        var insensitive = opts.caseInsensitiveMatch;

        var ret:any = {
                originalPath: path,
                regexp: path
            },
            keys = ret.keys = [];

        path = path
            .replace(/([().])/g, '\\$1')
            .replace(/(\/)?:(\w+)([\?\*])?/g, (_, slash, key, option) => {
            var optional = option === '?' ? option : null;
            var star = option === '*' ? option : null;
            keys.push({ name: key, optional: !!optional });
            slash = slash || '';
            return ''
                + (optional ? '' : slash)
                + '(?:'
                + (optional ? slash : '')
                + (star && '(.+?)' || '([^/]+)')
                + (optional || '')
                + ')'
                + (optional || '');
        })
            .replace(/([\/$\*])/g, '\\$1');

        ret.regexp = new RegExp('^' + path + '$', insensitive ? 'i' : '');
        return ret;
    }

    angular.module("app.common").value("pathRegExp", pathRegExp);

} 