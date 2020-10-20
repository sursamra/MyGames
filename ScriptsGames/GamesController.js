angular.module('MyGamesApp', [])
    .controller('GamesController', function ($scope, $http, $interval) {
        $scope.ListGames = null;
        getDateTime();
        getallGames();

        //GetServerDateTime
        function getDateTime() {
            console.log("getDateTime")

            try {
                $http.get('/Home/GetDateTime').then(successCallback, errorCallback);

                function successCallback(response) {
                    var oneHour = new Date(Date.parse(response.data) + 60 * 60 * 1000);
                    $scope.CountDown.initializeClock(oneHour);
                    
                }
                function errorCallback(error) {
                    $scope.message = error
                }
            } catch (e) {
                console.log(e);
            }
        };      
        //GetAllGames
        function getallGames() {
            console.log("getallGames")
            
            try {
                $http.get('/Home/GetAllGames').then(successCallback, errorCallback);

                function successCallback(response) {
                    $scope.ListGames = response.data                   
                }
                function errorCallback(error) {
                    $scope.message = error
                }
            } catch (e) {
                console.log(e);
            }
        };       
       
        $scope.playGame = function playGame(gameModel, gameMove) {
            if ($scope.CountDown.minutes == 0 && $scope.CountDown.seconds == 0)
                return
            try {
                $http({
                    method: 'POST',
                    url: '/Home/PlayGame',
                    data: {
                        gameID: gameModel.ID,
                        modeID: gameModel.Mode,
                        userMove: gameMove
                    }
                }).then(successCallback, errorCallback);

                function successCallback(response) {                    
                    for (var i = 0; i < $scope.ListGames.length; i++) {
                        var rec = $scope.ListGames[i]
                        if (rec['ID'] == gameModel.ID && rec['Mode'] == gameModel.Mode) {
                            if (rec['Mode'] == 0) {
                                if (response.data == 0) {
                                    rec['UserScore'] ++;
                                    break;
                                }
                                else {
                                    rec['ComputerScore'] ++;
                                    break;
                                }
                            }
                            else {
                                if (response.data == 0) {
                                    rec['WhiteComputer']++;
                                    break;
                                }
                                else {
                                    rec['BlackComputer'] ++;
                                    break;
                                }
                            }                            
                        }
                    }
                }
                function errorCallback(error) {
                    $scope.message = error
                }

            } catch (e) {
                console.log(e);
            }
        };
        $scope.CountDown = {
            days: 0,
            hours: 0,
            minutes: 0,
            seconds: 0,
            getTimeRemaining: function (endtime) {
                var t = Date.parse(endtime) - Date.parse(new Date());
                var seconds = Math.floor((t / 1000) % 60);
                var minutes = Math.floor((t / 1000 / 60) % 60);
                var hours = Math.floor((t / (1000 * 60 * 60)) % 24);
                var days = Math.floor(t / (1000 * 60 * 60 * 24));
                return {
                    'total': t,
                    'days': days,
                    'hours': hours,
                    'minutes': minutes,
                    'seconds': seconds
                };
            },

            initializeClock: function (endtime) {
                function updateClock() {
                    var t = $scope.CountDown.getTimeRemaining(endtime);

                    $scope.CountDown.days = t.days < 10 ? '0' + t.days : t.days;
                    $scope.CountDown.hours = ('0' + t.hours).slice(-2);
                    $scope.CountDown.minutes = ('0' + t.minutes).slice(-2);
                    $scope.CountDown.seconds = ('0' + t.seconds).slice(-2);

                    if (t.total <= 0) {
                        $interval.cancel(timeinterval);
                    }
                }

                updateClock();
                var timeinterval = $interval(updateClock, 1000);
            }
        }

        
    })