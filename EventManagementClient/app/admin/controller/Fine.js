
var app = angular.module("routapp")
    //.constant("activeClass","active")
                      .controller("finectrl", function ($scope, remotecall, $http) {

                          
                          function clear() {
                              $scope.addcat = null;
                          }

                          function load() {
                              remotecall.get("http://localhost:3502/odata/Fines").then(function (data) {
                                  $scope.fin = data.data.value;
                                  console.log(data.data.value);


                              })
                          }
                          load();

                         
                          //$scope.diffDate = function (date1, date2) {
                          //    var dateOut1 = new Date(date1); // it will work if date1 is in ISO format
                          //    var dateOut2 = new Date(date2);

                          //    var timeDiff = Math.abs(date2.getTime() - date1.getTime());
                          //    var diffDays = Math.ceil(timeDiff / (1000 * 3600 * 24));
                          //    alert(diffDays * 3);
                          //    return dateOut;
                          //};


                          $scope.diffDate = function (date1, date2) {
                              var dateOut1 = new Date(date1); // it will work if date1 is in ISO format
                              var dateOut2 = new Date(date2);

                              var timeDiff = Math.abs(date2.getTime() - date1.getTime());
                              var diffDays = Math.ceil(timeDiff / (1000 * 3600 * 24));
                              alert(diffDays * 3);
                              return dateOut;
                             
                          };

                          $scope.forfine = function (id) {
                              $http.get("http://localhost:3502/odata/Fines(" + id + ")").then(function (data) {
                                  $scope.fin1 = data.data;
                                  console.log(data.data);

                              })
                          }

                          $scope.savefinehis = function () {
                              console.log("-----------");
                              console.log($scope.fin1);
                              remotecall.post('http://localhost:3502/odata/Finehis', null, $scope.fin1)
                                    .then(function (data) {
                                        console.log(data);
                                        console.log("");

                                    }, function (err) {
                                        console.log(err);
                                    })

                          }



                          //$scope.delete = function (id) {
                          //    remotecall.remove("http://localhost:1811/odata/Fines(" + id + ")").then(function () {
                          //        load();
                          //        clear();
                          //    })
                          //}


                          $scope.mod = function (id) {
                              $http.get("http://localhost:3502/odata/Books(" + id + ")").then(function (data) {
                                  $scope.modal = data.data;
                                  console.log(data.data);

                              })
                          }
                          //$scope.bookreturn = function (id) {
                          //    $http.get("http://localhost:1811/odata/IssueBooks(" + id + ")").then(function (data) {
                          //        $scope.ret = data.data;
                          //        console.log(data.data);

                          //    })
                          //}

                          $scope.Accept = function (id) {
                              remotecall.remove("http://localhost:3502/odata/Fines(" + id + ")").then(function () {




                                  var issueBook = {
                                      Id: $scope.modal.Id,
                                      IssueBook: $scope.modal.IssueBook - 1
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
