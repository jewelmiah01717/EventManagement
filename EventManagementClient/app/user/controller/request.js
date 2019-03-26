var app = angular.module( "userapp" )
    //.constant("activeClass","active")
                      .controller( "requestctrl", function ( $scope, remotecall, $http )
                      {

                          $scope.addreq = null;

                          function clear()
                          {
                              $scope.addreq = null;
                          }

                          function load()
                          {
                              remotecall.get( "http://localhost:3502/odata/RequestBooks" ).then( function ( data )
                              {
                                  $scope.request = data.data.value;
                                  console.log( data.data.value );


                              } )
                          }
                          load();

                          $scope.save = function ()
                          {
                              console.log( $scope.addreq )
                              remotecall.post( 'http://localhost:3502/odata/RequestBooks', null, $scope.addreq )
                                  .then( function ( data )
                                  {
                                      load();

                                      //$scope.$apply();
                                      clear();
                                      alert( "Request Send Successfully" );
                                  }, function ( ERR )
                                  {
                                      //load();

                                      //$scope.$apply();
                                      console.log( ERR );

                                  } )
                          }



                          $scope.edit = function ( id )
                          {
                              $http.get( "http://localhost:3502/odata/RequestBooks(" + id + ")" ).then( function ( data )
                              {
                                  $scope.addreq = data.data;
                                  console.log( data );

                              } )
                          }

                          $scope.update = function ()
                          {
                              console.log( $scope.addreq )
                              remotecall.put( "http://localhost:3502/odata/RequestBooks(" + $scope.addreq.Id + ")", $scope.addreq ).then( function ( data )
                              {
                                  load();
                                  console.log( data );
                                  clear();
                              } )
                          }
                          $scope.delete = function ( id )
                          {
                              remotecall.remove( "http://localhost:3502/odata/RequestBooks(" + id + ")" ).then( function ()
                              {
                                  load();
                                  clear();
                              } )
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
                      } )

