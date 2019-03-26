angular.module("sportStore")
.controller("productsCtrl", function ($scope, remoteCallSvc
       ) {
    remoteCallSvc.get("http://localhost:3502/odata/SBooks", null, null)
         .then(function (result) {
             $scope.model.sbooks = result.data.value;
            console.log($scope.model.sbooks);
         }, function (response) {
             console.log(response);
         });

});