function loadScript(url, callback) {

    var script = document.createElement("script")
    script.type = "text/javascript";

    if (script.readyState) {  //IE
        script.onreadystatechange = function () {
            if (script.readyState == "loaded" ||
                    script.readyState == "complete") {
                script.onreadystatechange = null;
                callback();
            }
        };
    } else {  //Others
        script.onload = function () {
            callback();
        };
    }

    script.src = url;
    document.getElementsByTagName("head")[0].appendChild(script);
}


loadScript("https://ajax.googleapis.com/ajax/libs/angularjs/1.5.0/angular.min.js", function () {
    angular.module('murvasbokhandel', [])
	.controller("ResultsController", ['$scope', '$http', function ($scope, $http) {
	    $scope.search_field = "";

	    $scope.search = function (search_field) {
	        $scope.errormessage = null;
	        $scope.result = null;
	        $http({
	            method: "GET",
	            url: "http://localhost:49327/Api/Book/Search/" + search_field
	        }).then(function successCallback(response) {
	            $scope.result = response.data;
	        }, function errorCallback(response) {
	            console.log(response);
	            $scope.errormessage = "Server: "
	            $scope.errorcode = response.statusText;
	        });
	    }
	}])
	.directive('searchWidget', function () {
	    return {
	        restrict: 'E',
	        templateUrl: 'http://localhost:49327/Api/File/Widget/html'
	    };
	}).config(function ($sceProvider) {
	    $sceProvider.enabled(false);
	});
});