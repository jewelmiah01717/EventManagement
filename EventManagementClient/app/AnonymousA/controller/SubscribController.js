app.controller("SubscriberCtrl", function ($scope, SubscriberService) {

    $scope.Subscrib = function () {
        var subscriber = {

            Email: $scope.email,
           

        };

        var send = SubscriberService.Save(subscriber);
        send.then(function (res) {

            alert("You have  have been subscribed successfully.");
          
            clear();


        }, function (err) {
            alert("Something err to be subscribed.");
            
        })
    };
    function clear() {
        $scope.email = ''
         
    };


});