app.controller("MessageCtrl", function ($scope, MessageService) {
   
    $scope.SendMessage = function () {
        var message = {

            FirstName: $scope.firstName,
            LastName: $scope.lastName,
            Email: $scope.email,
            Feedback: $scope.feedback,

        };
       
        var send = MessageService.Save(message);
        send.then(function (res) {

            alert("Message has been sent successfully.");
            clear();
          

        }, function (err) {
            alert("Something err to save.");
        })
    };
    function clear() {
        $scope.firstName = '',
            $scope.lastName = '',
            $scope.email = '',
            $scope.feedback = ''
    }

  
});