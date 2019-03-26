/// <reference path="Categoriesctrl.js" />
/// <reference path="Categoriesctrl.js" />

var app = angular.module("routapp")
    //.constant("activeClass","active")
                      .controller("IssueBook", function ($scope, remotecall, $http) {
                          $scope.addissue = null;

                          function clear() {
                              $scope.addissue = null;
                          }
                          function clear1()
                          {
                              $scope.search = null;
                          }
                          function clear2()
                          {
                              $scope.userm = null;
                          }
                          
                          $scope.userm
                          function load() {
                              remotecall.get("http://localhost:3502/odata/IssueBooks").then(function (data) {
                                  $scope.Issue = data.data.value;
                                  console.log(data.data.value);
                              })
                          }
                          load();



                          


                          $scope.BookSearch = function (id) {                             
                              $http.get("http://localhost:3502/odata/Books(" + id + ")").then(function (data) {
                                  $scope.search = data.data;
                                  console.log(data.data);

                              }, function ( err )
                              {
                                  alert( "Does not Match Book ID" );
                              } )
                          }

                          //$scope.userSearch = function (id) {
                          //    $http.get("http://localhost:1811/odata/Books(" + id + ")").then(function (data) {
                          //        $scope.search = data.data;
                          //        console.log(data.data);

                          //    })
                          //}

                          $scope.userSearch = function (Email) {

                              $http({
                                  method: "GET",
                                  //url: "http://localhost:1811/api/AspNetUsers?Email=admin@gmail.com"
                                  url: "http://localhost:3502/api/AspNetUsers?Email="+Email+""

                                  //cache: $templateCache,
                                  //params: { query: "id" }
                              } ).then( function ( result )
                              {

                                  $scope.userm = result.data;
                                  if ( $scope.userm == null )
                                  {
                                      alert( "Does not Match Email Address" )
                                  }
                                  console.log(result.data) }, function (msg) {
                              
                                  
                                  $scope.error = msg;

                                  console.log(msg)
                              })

                          }


                          $scope.mod = function (id) {
                              $http.get("http://localhost:3502/odata/Books(" + id + ")").then(function (data) {
                                  $scope.modal = data.data;
                                  console.log(data.data);

                              })
                          }
                         
                          $scope.bookreturn = function (id) {
                              $http.get("http://localhost:3502/odata/IssueBooks(" + id + ")").then(function (data) {
                                  $scope.ret = data.data;
                                  console.log(data.data);

                              })
                          }

                          $scope.forfine = function (id) {
                              $http.get("http://localhost:3502/odata/IssueBooks(" + id + ")").then(function (data) {
                                  $scope.fin = data.data;
                                  console.log(data.data);

                              })
                          }



                         
                         
                          $scope.save = function () {
                             
                           
                              if ($scope.search.AvilableBook <= $scope.search.IssueBook)
                              {
                                  alert("Book is not avilable");
                              }
                              else {
                              console.log($scope.addissue)
                              remotecall.post('http://localhost:3502/odata/IssueBooks',null, $scope.addissue)

                                  .then(function (data) {              
                                      var issueBook = {
                                          Id: data.data.BookId,
                                          IssueBook: $scope.search.IssueBook + 1
                                      }

                                      console.log("-------------");
                                      console.log(data.data.BookId);
                                      console.log(issueBook);

                                      remotecall.patch('http://localhost:3502/odata/Books(' + data.data.BookId + ')', issueBook)
                                        .then(function (data) {
                                            console.log(data);
                                        }, function (err) {
                                            console.log(err);
                                        })
                                      load();
                                           //$scope.search
                                      //$scope.$apply();

                                           clear();
                                           clear1();
                                           clear2();
                                           alert( "Book Issue Successfull" )
                                           
                                  }, function (ERR) {
                                      //load();
                                            
                                      //$scope.$apply();
                                      console.log(ERR);

                                  })
                                                        
                          }
                          }


                          $scope.savehis = function () {
                              remotecall.post('http://localhost:3502/odata/IssueBookHistories', null, $scope.addissue)
                                    .then(function (data) {
                                        console.log(data);
                                        console.log("");

                                    }, function (err) {
                                        console.log(err);
                                    })
                          }

                         
                          
                          $scope.savefine = function () {
                              console.log("-----------");
                              console.log($scope.fin);
                              remotecall.post('http://localhost:3502/odata/Fines', null, $scope.fin)
                                    .then(function (data) {
                                        console.log(data);
                                        console.log("");

                                    }, function (err) {
                                        console.log(err);
                                    })
                             
                          }

                          $scope.edit = function (id) {
                              $http.get("http://localhost:3502/odata/IssueBooks(" + id + ")").then(function (data) {
                                  $scope.addissue = data.data;
                                  console.log(data);

                              })
                          }

                          //$scope.deleteforfine = function (id) {
                          //    remotecall.remove("http://localhost:1811/odata/IssueBooks(" + id + ")").then(function () {
                          //        //load();
                          //    }, function (ERR) {
                          //        console.log(ERR);
                          //    })
                          //}

                          $scope.delete = function (id) {
                              remotecall.remove("http://localhost:3502/odata/IssueBooks(" + id + ")").then(function () {
                                  load();
                                  clear();
                              })
                          }

                          $scope.update = function () {
                              console.log($scope.addissue)
                              remotecall.put("http://localhost:3502/odata/IssueBooks(" + $scope.addissue.Id + ")", $scope.addissue).then(function (data) {
                                  load();
                                  console.log(data);
                                  clear();
                              })
                          }
                          $scope.Accept = function (id) {
                              remotecall.remove("http://localhost:3502/odata/IssueBooks(" + id + ")").then(function () {




                                  var issueBook = {
                                      Id: $scope.modal.Id,
                                      IssueBook: $scope.modal.IssueBook-1
                                  }

                                  console.log("-------------");
                                  //console.log(data.data.BookId);
                                  console.log(issueBook);

                                  remotecall.patch('http://localhost:3502/odata/Books(' + $scope.modal.Id + ')', issueBook)
                                    .then(function (data) {
                                        console.log(data);
                                    }, function (err) {
                                        console.log(err);
                                    })
                                  load();

                                 
                                  clear();
                              }, function (ERR) {
                                

                                  console.log(ERR);

                              })
                          }



                          /////datepicker
                          $scope.dateOptions = {
                              formatYear: 'yy',
                              maxDate: new Date(new Date().getFullYear() + 1, 12, 31),
                              minDate: new Date(new Date().getFullYear() - 2, 1, 1),
                              startingDay: 1
                          };
                          $scope.popup = {
                              opened: false
                          };
                          $scope.openfn = function () {

                              $scope.popup.opened = true;
                              
                          };
                          $scope.popup2 = {
                              opened: false
                          };
                          $scope.popup3 = {
                              opened: false
                          };
                          $scope.openfn2 = function () {
                              $scope.popup2.opened = true;
                          };
                          $scope.openfn3 = function () {
                              $scope.popup3.opened = true;
                          };

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

