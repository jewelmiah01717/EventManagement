var app = angular.module("routapp")
    //.constant("activeClass","active")
                      .controller("logctrl", function ($scope, remotecall, $http) {
                        
                          $scope.loginData = null;
                          function clear() {
                              $scope.loginData = null;
                          }

                        

                          $scope.Login1 = function () {
                              console.log($scope.loginData)
                              remotecall.post('http://localhost:3502/odata/login', null, $scope.loginData)
                                  .then(function (data) {
                                      //load();
                                      alter();
                                      //$scope.$apply();
                                      clear();
                                  }, function (ERR) {
                                      //load();

                                      //$scope.$apply();
                                      console.log(ERR);
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
