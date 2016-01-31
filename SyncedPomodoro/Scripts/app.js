(function() {

    var app = angular.module("app", []);

    app.controller("timer", function ($scope, $interval, $http) {

        $scope.timeLeft = 10;
        $scope.resting = true;
        $scope.sprintNumber = 1;

        var update = function () {
            $http.get("/api/timer").then(function(r) {
                $scope.timeLeft = r.data.SecondsLeft;
                $scope.resting = r.data.Resting;
            });
        }

        update();

        $interval(function() {
            $scope.timeLeft--;
            if ($scope.timeLeft === 0) {
                $scope.resting = !$scope.resting;
                $scope.timeLeft = ($scope.resting ? 5 : 25) * 60;
                var audio = new Audio("http://moosti.com/alarm.wav");
                audio.play();
                if (!$scope.resting) $scope.sprintNumber += 1;
            }
        }, 1000);
    });

    function padLeft(nr, n, str) {
        return Array(n - String(nr).length + 1).join(str || '0') + nr;
    }

    app.filter("timer", function() {
        return function (input) {
            var minutes = Math.floor(input / 60);
            var seconds = input % 60;
            return padLeft(minutes, 2) + ":" + padLeft(seconds, 2);
        }
    });

})();