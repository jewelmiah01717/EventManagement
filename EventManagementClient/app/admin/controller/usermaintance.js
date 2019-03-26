

var app = angular.module("routapp")

                      .controller("UserController", function ($scope, remotecall, $http) {


function load() {
     $http({
        method: "GET",
        url: "http://localhost:3502/api/AspNetUsers"
    }).then(function (result) {
        $scope.userm = result.data;
        //console.log(result);
    }, function (err) {
        $scope.error = err;
    });
}
load();


                    

$scope.delete = function (id) {
   
    $http({
        method: "delete",
        url: "http://localhost:3502/api/AspNetUsers/" + id,
    }).then(function (res) { load(); console.log(res) }, function (msg) {
        $scope.error = msg;
        console.log(msg)
    })

}
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
