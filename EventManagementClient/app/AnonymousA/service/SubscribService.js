app.service("SubscriberService", function ($http) {

    this.Save = function (user) {
        var req = $http({
            method: 'Post',
            url: "http://localhost:3502/odata/Subscribers",
            data: user
        })
        return req;
    }


   
});