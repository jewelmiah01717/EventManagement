
var app = angular.module("routapp")
    //.constant("activeClass","active")
                      .controller("historyctrl", function ($scope, remotecall, $http) {

                          $scope.addcat = null;
                          function clear() {
                              $scope.addcat = null;
                          }

                          function load() {
                              remotecall.get("http://localhost:3502/odata/IssueBookHistories").then(function (data) {
                                  $scope.his = data.data.value;
                                  console.log(data.data.value);


                              })
                          }
                          load();

                          //$scope.save = function () {
                          //    console.log($scope.addcat)
                          //    remotecall.post('http://localhost:1811/odata/IssueBookHistories', null, $scope.addcat)
                          //        .then(function (data) {
                          //            load();

                          //            //$scope.$apply();
                          //            clear();
                          //        }, function (ERR) {
                          //            //load();

                          //            //$scope.$apply();
                          //            console.log(ERR);
                          //        })

                             
                          //}
                         

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
