var app = angular.module("routapp")
    //.constant("activeClass","active")
                      .controller("rackctrl", function ($scope, remotecall, $http) {
                        
                          $scope.addrack = null;
                          function clear() {
                              $scope.addrack = null;
                          }

                          function load() {
                              remotecall.get("http://localhost:3502/odata/Racks").then(function (data) {
                                  $scope.rack = data.data.value;
                                  console.log(data.data.value);


                              })
                          }
                          load();

                          $scope.save = function () {
                              console.log($scope.addrack)
                              remotecall.post('http://localhost:3502/odata/Racks', null, $scope.addrack)
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
                              $http.get("http://localhost:3502/odata/Racks(" + id + ")").then(function (data) {
                                  $scope.addrack = data.data;
                                  console.log(data);

                              })
                          }

                          $scope.update = function () {
                              console.log($scope.addrack)
                              remotecall.put("http://localhost:3502/odata/Racks(" + $scope.addrack.Id + ")", $scope.addrack).then(function (data) {
                                  load();
                                  console.log(data);
                                  clear();
                              })
                          }
                          $scope.delete = function (id) {
                              remotecall.remove("http://localhost:3502/odata/Racks(" + id + ")").then(function () {
                                  load();
                                  clear();
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
