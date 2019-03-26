angular.module("sportsStoreAdmin")
.controller("stockinCtrl", function ($scope, remoteCallSvc) {
    var fileSelect, fileSelectEdit;
    $scope.sbookToDel = null;
    $scope.model.current = null;
    $scope.model.tempSBook = null;
    remoteCallSvc.post("http://localhost:3502/odata/SBooks/AcSBooks", { "Authorization": "Bearer " + $scope.auth.accesstoken }, null)
         .then(function (result) {
             $scope.model.products = result.data.value;


         }, function (response) {
             console.log(response);
         });
    remoteCallSvc.get("http://localhost:3502/odata/StockIns?$expand=SBook", { "Authorization": "Bearer " + $scope.auth.accesstoken }, null)
         .then(function (result) {
             $scope.model.stockins = result.data.value;


         }, function (response) {
             console.log(response);
         });
    //Add new
    $scope.addNew = function () {
        $scope.model.current = null;
        $scope.model.temp = null;
        $("#insertModal").modal("show");
    }
    //Chack Stock Level
    $scope.checkStockLevel = function () {
        remoteCallSvc.get("http://localhost:3502/odata/StockIns?$expand=SBook", { "Authorization": "Bearer " + $scope.auth.accesstoken }, null)
        .then(function (result) {
            $scope.model.stockins = result.data.value;


        }, function (response) {
            console.log(response);
        });

    }
    //Save new
    $scope.saveSBook = function () {
        console.log("SBook")
        console.log($scope.findSBook(1))
        //$scope.model.currentProduct.Stocklevel = 0;
        console.log($scope.model.current);
        remoteCallSvc.post("http://localhost:3502/odata/StockIns?$expand=SBook", { "Authorization": "Bearer " + $scope.auth.accesstoken }, $scope.model.current)
         .then(function (result) {
             console.log(result.data);
             var d = result.data;
             d.Product = $scope.findSBook(d.SBookId);
             $scope.model.stockins.push(d);
             $("#insertModal").modal("hide");
             //console.log($scope.model.products);
         }, function (response) {
             console.log(response);
         });
    }
    //
    $scope.confirmDelstockin = function (item) {
        //console.log(item);
        //$scope.current = item;
        $scope.sbookToDel = item;
        //$scope.productToDel.Id = 500;
        $("#confirmDialog").modal("show");
    }
    $scope.delSBook = function () {
        remoteCallSvc.remove("http://localhost:3502/odata/StockIns(" + $scope.sbookToDel.StockInId + ")", { "Authorization": "Bearer " + $scope.auth.accesstoken }, null)
        .then(function (result) {
            //console.log(result);
            var i = $scope.model.stockins.indexOf($scope.sbookToDel);
            $scope.model.stockins.splice(i, 1);
            $scope.sbookToDel = null;
            $("#confirmDialog").modal("hide");
            //console.log(i);
        }, function (response) {

            console.log(response.statusText);
        });
    }
    $scope.editstockin = function (stockin) {
        $scope.model.temp = stockin;
        $scope.model.current = { StockInId: stockin.StockInId, Date: stockin.Date, quantity: stockin.quantity, SBookId: stockin.SBookId };
        
        $("#editModal").modal("show");
    }

    //cancel

    $scope.cancel = function () {

        $scope.model.current = null;
        $scope.model.temp = null;
        $("#editModal").modal("hide");
        $("#confirmDialog").modal("hide");
        $("#insertModal").modal("hide");
    }
    $scope.editSBook = function () {
        remoteCallSvc.put("http://localhost:3502/odata/StockIns(" + $scope.model.current.StockInId + ")", { "Authorization": "Bearer " + $scope.auth.accesstoken },
            $scope.model.current)
        .then(function (result) {

            var i = $scope.model.stockins.indexOf($scope.model.temp);
            $scope.model.stockins[i] = $scope.model.current;
            
            $scope.model.current = null;
            $scope.model.temp = null;
            $("#editModal").modal("hide");
        }, function (response) {
            var err = response.data["odata.error"].innererror;
            console.log(err.message);

        });
    }
    $scope.findSBook = function (id) {
        //console.log($scope.model.products[0])
        for (var i = 0; i < $scope.model.sbooks.length; i++) {
            if ($scope.model.sbooks[i].Id == id) {
                return $scope.model.sbooks[i];
            }
        }
    }
});