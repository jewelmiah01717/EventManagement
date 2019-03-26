angular.module("routapp")
.controller("bookInfoCtrl", function ($scope, $http, $location, remotecall,
    authStore) {
    $scope.bookingError;
    function load() {
        remotecall.post("http://localhost:3502/odata/Books/GetBookall", { Authorization: "Bearer " + $scope.auth.accesstoken }, null).then(function (result) {
        //remotecall.get("http://localhost:3502/odata/Books", { Authorization: "Bearer " + $scope.auth.accesstoken }, null)
            //.then(function (result) {
                //$scope.model.bookingData = result.data.value;
                $scope.books = result.data.value;
                console.log($scope.auth);
            }, function (err) {
                console.log(err);

            })
    }
    load();
    function clear() {
        $scope.addbook = null;
    }
    $scope.save = function () {
        console.log($scope.addbook)
        remotecall.post('http://localhost:3502/odata/Books', { Authorization: "Bearer " + $scope.auth.accesstoken }, $scope.addbook)
            .then(function (data) {
                load();

                //$scope.$apply();
                clear();
            }, function (ERR) {
                //load();

                //$scope.$apply();
                console.log(ERR);

            })
    }



    $scope.edit = function (id) {
        $http.get("http://localhost:3502/odata/Books(" + id + ")", { Authorization: "Bearer " + $scope.auth.accesstoken }, null).then(function (data) {
            $scope.addbook = data.data;
            console.log(data);

        })
    }

    $scope.update = function () {
        console.log($scope.addbook)
        remotecall.put("http://localhost:3502/odata/Books(" + $scope.addbook.Id + ")", $scope.addbook).then(function (data) {
            load();
            console.log(data);
            clear();
        })
    }
    $scope.delete = function (id) {
        remotecall.remove("http://localhost:3502/odata/Books(" + id + ")").then(function () {
            load();
            clear();
        })
    }
    //$scope.sortColumn = "PassengerName";
    //$scope.reverseSort = false;
    //$scope.sort = function (column) {
    //    $scope.sortColumn = column;
    //    $scope.reverseSort = $scope.sortColumn == column ? !$scope.reverseSort : false;
    //}
    $scope.sort = function (key) {
        $scope.sortKey = key;
        $scope.reverse = !$scope.reverse;
    }
    $scope.selectedPage = 1;
    $scope.pageSize = 4;

    $scope.selectPage = function (newPage) {
        $scope.selectedPage = newPage;
        console.log(newPage);
        console.log($scope.selectedPage);
    };
    $scope.getPageClass = function (page) {

        return $scope.selectedPage == page ? "active" : "";
    };

})