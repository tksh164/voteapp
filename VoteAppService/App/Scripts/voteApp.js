'use strict';

angular.module('VoteApp', ['ngRoute', 'AdalAngular', 'ngAnimate', 'ui.bootstrap'])
.config(['$routeProvider', '$httpProvider', 'adalAuthenticationServiceProvider', function ($routeProvider, $httpProvider, adalProvider) {

    $routeProvider.when('/', {
        controller: 'homeCtrl',
        templateUrl: '/App/Views/home.html',
        requireADLogin: true,
    }).when('/UserToken', {
        controller: 'userTokenCtrl',
        templateUrl: '/App/Views/userToken.html',
        requireADLogin: true,
    }).otherwise({ redirectTo: '/' });

    adalProvider.init(
        {
            instance: 'https://login.microsoftonline.com/',
            tenant: '[Your Tenant, e.g. *.onmicrosoft.com]',
            extraQueryParameter: 'nux=1',
            requireADLogin: true,

            // for production
            clientId: '[Client ID, e.g. 12345678-9abc-defg-hijk-lmnopqrstuvw]',
            chacheLocation: 'localStorage',

            // for development
            //clientId: '[Client ID for Dev, e.g. 12345678-9abc-defg-hijk-lmnopqrstuvw]',
        },
        $httpProvider
    );

}]);
