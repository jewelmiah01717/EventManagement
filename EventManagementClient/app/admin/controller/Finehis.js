var app = angular.module("routapp")
    //.constant("activeClass","active")
                      .controller("finehisctrl", function ($scope, remotecall, $http) {


                       

                          function load() {
                              remotecall.get( "http://localhost:3502/odata/Finehis" ).then( function ( data )
                              {
                                  $scope.finehis = data.data.value;
                                  console.log(data.data.value);
                              })
                          }
                          load();
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
