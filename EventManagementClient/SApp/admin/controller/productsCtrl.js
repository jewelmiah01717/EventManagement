angular.module("sportsStoreAdmin")
.controller("productsCtrl", function ($scope, remoteCallSvc) {
    var fileSelect, fileSelectEdit;
    $scope.sbookToDel = null;
    $scope.model.currentSBook = null;
    $scope.model.tempSBook= null;
    remoteCallSvc.post("http://localhost:3502/odata/SBooks/AcSBooks", { "Authorization": "Bearer " + $scope.auth.accesstoken }, null)
         .then(function (result) {
             $scope.model.sbooks = result.data.value;


         }, function (response) {
             console.log(response);
         });
    //Add new
    $scope.addNew = function () {
        $("#insertModal").modal("show");
    }

    //Save new
    $scope.saveSBook = function () {
        console.log("content")
        console.log($scope.model.currentSBook)
        $scope.model.currentSBook.Stocklevel = 0;
        console.log($scope.model.currentSBook);
        remoteCallSvc.post("http://localhost:3502/odata/SBooks", { "Authorization": "Bearer " + $scope.auth.accesstoken }, $scope.model.currentSBook)
         .then(function (result) {
             console.log(result);
             $scope.model.sbooks.push(result.data);
             $("#insertModal").modal("hide");
             //console.log($scope.model.products);
         }, function (response) {
             console.log(response);
         });
    }

    //Delete Product
    $scope.confirmDelSBook = function (item) {
        //console.log(item);
        $scope.sbookToDel = item;
        //$scope.productToDel.Id = 500;
        $("#confirmDialog").modal("show");
    }
    $scope.delSBook = function () {
        remoteCallSvc.remove("http://localhost:3502/odata/SBooks(" + $scope.sbookToDel.Id + ")", { "Authorization": "Bearer " + $scope.auth.accesstoken }, null)
        .then(function (result) {
            //console.log(result);
            var i = $scope.model.sbooks.indexOf($scope.sbookToDel);
            $scope.model.sbooks.splice(i, 1);
            $scope.sbookToDel = null;
            $("#confirmDialog").modal("hide");
            //console.log(i);
        }, function (response) {

            console.log(response.statusText);
        });
    }
    $scope.editSBook = function (sbook) {
        $scope.model.currentSBook = { Id: sbook.Id, Name: sbook.Name, Category: sbook.Category, Price: sbook.Price, Description: sbook.Description, Picture: sbook.Picture, Stocklevel: sbook.Stocklevel };
        $scope.model.tempSBook = sbook;
        $("#editModal").modal("show");
    }
    $scope.cancelEditSBook = function () {

        $scope.model.currentSBook = null;
        $scope.model.tempSBook = null;
        $("#editModal").modal("hide");
    }
    $scope.updateSBook = function () {
        remoteCallSvc.put("http://localhost:3502/odata/SBooks(" + $scope.model.currentSBook.Id + ")",
            { "Authorization": "Bearer " + $scope.auth.accesstoken },
            $scope.model.currentSBook)
        .then(function (result) {

            var i = $scope.model.sbooks.indexOf($scope.model.tempSBook);
            $scope.model.sbooks[i].Name = $scope.model.currentSBook.Name;
            $scope.model.sbooks[i].Category = $scope.model.currentSBook.Category;
            $scope.model.sbooks[i].Price = $scope.model.currentSBook.Price;
            $scope.model.sbooks[i].Description = $scope.model.currentSBook.Description;
            $scope.model.sbooks[i].Picture = $scope.model.currentSBook.Picture;
            $scope.model.sbooks[i].Stocklevel = $scope.modal.currentSBook.Stocklevel;
            $scope.model.currentSBook = null;
            $scope.model.tempSBook = null;
            $("#editModal").modal("hide");
        }, function (response) {
            var err = response.data["odata.error"].innererror;
            console.log(err.message);

        });
    }
    $scope.newSBookPictureClick = function () {

        fileSelect = document.createElement('input'); //input it's not displayed in html, I want to trigger it form other elements
        fileSelect.type = 'file';

        if (fileSelect.disabled) { //check if browser support input type='file' and stop execution of controller
            return;
        }
        //console.log("new pic");
        if (fileSelect) { //activate function to begin input file on click
            fileSelect.click();
        }

        fileSelect.onchange = function () { //set callback to action after choosing file
            var f = fileSelect.files[0],
              r = new FileReader();

            r.onloadend = function (e) { //callback after files finish loading
                $scope.model.currentSBook.Picture = e.target.result;
                $scope.$apply();
                //console.log($scope.model.currentProduct.Picture.replace(/^data:image\/(png|jpg);base64,/, "")); //replace regex if you want to rip off the base 64 "header"
                $("#newSBookPrictrue").attr("src", $scope.model.currentSBook.Picture)
                //here you can send data over your server as desired
            }

            r.readAsDataURL(f); //once defined all callbacks, begin reading the file
        }
    }
    $scope.editPictureClick = function () {
        fileSelectEdit = document.createElement("input");// same as insert
        fileSelectEdit.type = 'file';
        if (fileSelectEdit.disabled) {
            return;
        }
        if (fileSelectEdit) {
            fileSelectEdit.click();
        }
        fileSelectEdit.onchange = function () { //set callback to action after choosing file
            var f = fileSelectEdit.files[0],
            r = new FileReader();

            r.onloadend = function (e) { //callback after files finish loading
                $scope.model.currentSBook.Picture = e.target.result;
                $scope.$apply();
                //console.log($scope.model.currentProduct.Picture.replace(/^data:image\/(png|jpg);base64,/, "")); //replace regex if you want to rip off the base 64 "header"
                $("#newSBookPrictrue").attr("src", $scope.model.currentSBook.Picture)
                //here you can send data over your server as desired
            }

            r.readAsDataURL(f); //once defined all callbacks, begin reading the file
        }
    }
    
});