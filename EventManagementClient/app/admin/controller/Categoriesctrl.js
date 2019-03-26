
var app = angular.module("routapp")
    //.constant("activeClass","active")
                      .controller("categoryctrl", function ($scope, remotecall, $http) {
                        
                          $scope.addcat = null;
                          function clear() {
                              $scope.addcat = null;
                          }

                          function load() {
                              remotecall.get("http://localhost:3502/odata/Categories").then(function (data) {
                                  $scope.cat = data.data.value;
                                  console.log(data.data.value);


                              })
                          }
                          load();

                          $scope.save = function () {
                              console.log($scope.addcat)
                              remotecall.post('http://localhost:3502/odata/Categories', null, $scope.addcat)
                                  .then(function (data) {
                                      load();

                                      //$scope.$apply();
                                      clear();
                                  }, function (ERR) {
                                      //load();

                                      //$scope.$apply();
                                      console.log(ERR);
                                  })

                              //                            $http({
                              //                                url: 'http://localhost:1811/ODATA/Members',
                              //                                method: "POST",
                              //                                data: $scope.addmember
                              //                            })
                              //.then(function (response) {
                              //    // success
                              //},
                              //function (response) { // optional
                              //    // failed
                              //});
                          }
                          $scope.edit = function (id) {
                              $http.get("http://localhost:3502/odata/Categories(" + id + ")").then(function (data) {
                                  $scope.addcat = data.data;
                                  console.log(data);

                              })
                          }

                          $scope.update = function () {
                              console.log($scope.addcat)
                              remotecall.put("http://localhost:3502/odata/Categories(" + $scope.addcat.Id + ")", $scope.addcat).then(function (data) {
                                  load();
                                  console.log(data);
                                  clear();
                              })
                          }
                          $scope.delete = function (id) {
                              remotecall.remove("http://localhost:3502/odata/Categories(" + id + ")").then(function () {
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
