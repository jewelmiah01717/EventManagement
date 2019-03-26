/// <reference path="Categoriesctrl.js" />
/// <reference path="Categoriesctrl.js" />

var app = angular.module("userapp")
    //.constant("activeClass","active")
                      .controller("bookctrl", function ($scope, remotecall, $http) {
                          var selectbook = null;
                          $scope.addbook = null;

                          function clear() {
                              $scope.addbook = null;
                          }

                          function load() {
                              remotecall.post( "http://localhost:3502/odata/Books/GetBookall" ).then( function ( data )
                              {
                              //remotecall.get("http://localhost:3502/odata/Books").then(function (data) {
                                  $scope.books = data.data.value;
                                  console.log(data.data.value);


                              })
                          }
                          load();

                          $scope.save = function () {
                              console.log($scope.addbook)
                              remotecall.post('http://localhost:3502/odata/Books', null, $scope.addbook)
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
                              $http.get("http://localhost:3502/odata/Books(" + id + ")").then(function (data) {
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
                          })

