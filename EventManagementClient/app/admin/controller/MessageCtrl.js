var app = angular.module("routapp")
    //.constant("activeClass","active")
    .controller("MessageCtrl", function ($scope, remotecall, $http) {
        load();
        function load() {
            remotecall.get("http://localhost:3502/odata/Messages").then(function (data) {
                $scope.Messages = data.data.value;
                console.log(data.data.value);


            })
        }
  
        $scope.delete = function (id) {
            remotecall.remove("http://localhost:3502/odata/Messages(" + id + ")").then(function () {
                load();
                clear();
            })
        }
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
    });
