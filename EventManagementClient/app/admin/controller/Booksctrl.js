/// <reference path="Categoriesctrl.js" />
/// <reference path="Categoriesctrl.js" />

var app = angular.module("routapp")
    //.constant("activeClass","active")
                      .controller("bookctrl", function ($scope, remotecall, $http) {
                          var selectbook = null;
                          $scope.addbook = null;

                          function clear() {
                              $scope.addbook = null;
                          }

                          function load() {
                              remotecall.get("http://localhost:3502/odata/Books", { Authorization: "Bearer " + $scope.auth.accesstoken }, null).then(function (data) {
                                  $scope.books = data.data.value;
                                  console.log($scope.auth.accesstoken);


                              })
                          }
                          load();

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
                          })

