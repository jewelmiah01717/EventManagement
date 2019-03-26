angular.module("userapp")
        .factory("remotecall", function ($http) {
            return {
                get: function (url, data, header) {
                    return $http({
                        url: url,
                        headers: header,
                        params: data,
                        method: "GET"
                    })
                },
                remove: function (url, data, header) {
                    return $http({
                        url: url,
                        headers: header,
                        params: data,
                        method: "DELETE"
                    })
                },
                post: function (url, headers, data) {
                    return $http({
                        url: url,
                        headers: headers,
                        data: data,
                        method: "POST"
                    })
                },
                put: function (url, data, headers) {
                    return $http({
                        url: url,
                        headers: headers,
                        data: data,
                        method: "PUT"
                    })
                },
            }
        });