angular.module("routapp")
.controller("adminCtrl", function ($scope, authStore, loginSvc, $location) {
    $scope.loginError = "";
    $scope.loginModel = {};
        $scope.auth = authStore.get();
     console.log($scope.auth);
    if ($scope.auth.authenticated)
        $location.path("/book");
    //$scope.showLoginForm = function () {
    //    $("#loginModal").modal("show");
    //};
    $scope.signin = function () {
       
        //debugger;
        loginSvc.signin($scope.loginModel.username, $scope.loginModel.password)
            .then(function (result) {
                authStore.save($scope.loginModel.username, result.data.access_token);
                $scope.auth = authStore.get();
                console.log($scope.auth);
                $scope.loginModel = null;
                $scope.loginError = "";
                //$("#loginModal").modal("hide");
                $location.path("/IssueBook");
                //$window.location.href = "/app/admin/views/products.html";
            }, function (respose) {
                console.log(respose);
                $scope.loginError = respose.data;
            });
    };
    $scope.signout = function () {
        authStore.remove();
        $scope.auth = {};
        $location.path("/login");
    };

});

