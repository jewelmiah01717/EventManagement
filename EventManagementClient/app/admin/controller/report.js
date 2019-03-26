/// <reference path="Categoriesctrl.js" />
/// <reference path="Categoriesctrl.js" />

var app = angular.module( "routapp" )
    //.constant("activeClass","active")
                      .controller( "report", function ( $scope, remotecall, $http )
                      {
                          var selectbook = null;
                          $scope.addbook = null;

                          function clear()
                          {
                              $scope.addbook = null;
                          }

                          function load()
                          {
                              remotecall.get( "http://localhost:3502/odata/Books" ).then( function ( data )
                              {
                                  $scope.books = data.data.value;
                                  console.log( data.data.value );
                              } )
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
                          
                      } )

