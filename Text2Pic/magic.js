var gis = require('g-i-s');

module.exports =
    function (callback, query) {
        gis(query, (error, results) => {
            callback(null, JSON.stringify(results, null, '  '));
        });
    };