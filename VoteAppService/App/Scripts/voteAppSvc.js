'use strict';

angular.module('VoteApp')
.factory('voteAppSvc', ['$http', function ($http) {

    return {
        getTeams: function () {
            return $http.get('/api/VoteAppSvc/Teams');
        },
        getTeamScore: function (team) {
            return $http.get('/api/VoteAppSvc/TeamScore/' + team.TeamId);
        },
        updateTeamScore: function (team) {
            var data = {
                TeamId: team.TeamId,
                Score: team.Score,
            };
            return $http.put('/api/VoteAppSvc/TeamScore/' + team.TeamId, data);
        }
    };
}]);
