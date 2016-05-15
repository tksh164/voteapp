'use strict';

angular.module('VoteApp')
.controller('homeCtrl', ['$scope', '$location', 'adalAuthenticationService', 'voteAppSvc', function ($scope, $location, adalService, voteAppSvc) {

    $scope.message = '読み込み中...';
    $scope.teams = null;
    
    $scope.initTeams = function () {

        voteAppSvc.getTeams().success(function (results) {

            var teams = results,
                i, len = teams.length;

            if (len === 0) {
                $scope.message = 'エントリーしているチームが 1 つもありません。';
                $scope.teams = [];
                return;
            }

            for (i = 0; i < len; i++) {
                // Team's message.
                teams[i].message = '';

                // for the collapse of team's description.
                teams[i].isDescriptionCollapsed = true;
            }

            $scope.message = '';
            $scope.teams = teams;

        }).error(function (err) {

            var errMessage = buildErrorMessage(err);
            $scope.message = '読み込みに失敗しました。ページを再読み込みしてみてください。きっとうまく行きます。' + errMessage;

        });
    };

    $scope.updateTeamScore = function (team) {

        voteAppSvc.updateTeamScore(team).success(function (results) {

            team.message = '';

        }).error(function (err) {

            var errMessage = buildErrorMessage(err);

            voteAppSvc.getTeamScore(team).success(function (results) {

                // Restore a score value of client-side by a score value of server-side.
                // Because the client-side score value already changed.
                team.Score = results.Score;

            }).error(function (err) {
                errMessage += ', ' + buildErrorMessage(err);
            });

            team.message = '投票に失敗しました。もう一度投票してみてください。きっとうまく行きます。' + errMessage;

        });
    };

    $scope.resetTeamScore = function (team) {

        var beforeResetScore = team.Score;

        // Reset a score value.
        team.Score = 0;

        voteAppSvc.updateTeamScore(team).success(function (results) {

            team.message = '';

        }).error(function (err) {

            var errMessage = buildErrorMessage(err);

            // Restore a score value of model.
            team.Score = beforeResetScore;

            team.message = 'リセットに失敗しました。もう一度リセットしてみてください。きっとうまく行きます。' + errMessage;

        });
    };

    function buildErrorMessage(err) {

        var errMessage = '***';

        if (typeof err === 'object' && err.Message !== undefined) {
            errMessage = ' (' + err.Message + ')';
        }
        else if (err !== undefined && err !== '') {
            errMessage = ' (' + err + ')';
        }

        return errMessage;
    }

}]);
