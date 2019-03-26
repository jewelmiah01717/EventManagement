var app = angular.module("userapp")
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
                         
                      });
