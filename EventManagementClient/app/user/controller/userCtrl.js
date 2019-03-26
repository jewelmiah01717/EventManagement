

var app = angular.module("userapp");
app.controller("userCtrl", function ($scope, $http, $location, remotecall,
    authStore, loginSvc) {
    $scope.model = {};
    $scope.loginModel = {};
    $scope.auth = authStore.get();
    if (!$scope.auth.authenticated) {
        $location.path("/login")
    }
    /////login tasks
    $scope.loginErr = "";
    $scope.signin = function () {
        loginSvc.signin($scope.loginModel.username, $scope.loginModel.password)
        .then(function (result) {
            console.log(result);
            $scope.loginErr = "";
            authStore.save($scope.loginModel.username, result.data.access_token);
            console.log(result.data.access_token);
            $scope.loginModel = null;
            $scope.auth = authStore.get();
            $location.path("/book");


        }, function (res) {
            console.log(res);
            //$scope.loginErr = res.data.error_description;
            $scope.loginErr = res.data;
        })

    },
    $scope.signout = function () {
        authStore.remove();
        $scope.auth = {};
        $location.path("/login");
    }

    //For Print The Today's Schedule

    $scope.printToCart = function (printSchedule) {
        var innerContents = document.getElementById('printSchedule').innerHTML;

        var popupWinindow = window.open('', '_blank', 'width=600,height=700,scrollbars=no,menubar=no,toolbar=no,location=no,status=no,titlebar=no');

        popupWinindow.document.open();

        popupWinindow.document.write('<html><head><link rel="stylesheet" href="/Content/bootstrap.min.css" integrity="sha384-BVYiiSIFeK1dGmJRAkycuHAHRg32OmUcww7on3RYdg4Va+PmSTsz/K68vbdEjh4u" crossorigin="anonymous"></head><body onload="window.print()">' + innerContents + '</html>');

        popupWinindow.document.close();
    }

    $scope.sortColumn = "CoachNo";
    $scope.reverseSort = false;
    $scope.sort = function (column) {
        $scope.sortColumn = column;
        $scope.reverseSort = $scope.sortColumn == column ? !$scope.reverseSort : false;
    }
    /////////////Date picker////////////////
    $scope.sort = function ( key )
    {
        $scope.sortKey = key;
        $scope.reverse = !$scope.reverse;
    }
    $scope.selectedPage = 1;
    $scope.pageSize = 4;

    $scope.selectPage = function ( newPage )
    {
        $scope.selectedPage = newPage;
        console.log( newPage );
        console.log( $scope.selectedPage );
    };
    $scope.getPageClass = function ( page )
    {

        return $scope.selectedPage == page ? "active" : "";
    };


});