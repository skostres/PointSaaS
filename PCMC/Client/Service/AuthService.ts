// Class will be used for pop-up alerts within the app
class AuthService {

    public static authService: any = {}; 
    public constructor($http, growl) {

        AuthService.authService.session = new Session();

        AuthService.authService.login = function (credentials) {
            return $http
                .post('api/Auth/Login', credentials)
                .then(function (res) {
                    AuthService.authService.session.create(res.data.ID, res.data.LastName, res.data.FirstName, res.data.Password, res.data.Username, res.data.Role);
                    return AuthService.authService.session.currentUser;
                }.bind(this))
                .catch(function (response) {
                    growl.error("Login Failed", { ttl: 10000 });
                }.bind(this))
        }.bind(this);

        AuthService.authService.isAuthenticated = function () {
            return AuthService.authService.session.currentUser == null ? false : !!AuthService.authService.session.currentUser.ID;
        }.bind(this);

        AuthService.authService.isAuthorized = function (authorizedRoles) {
            if (!angular.isArray(authorizedRoles)) {
                authorizedRoles = [authorizedRoles];
            }
            
            return ( authorizedRoles.indexOf("*") !== -1 || (AuthService.authService.isAuthenticated() &&
                authorizedRoles.indexOf(AuthService.authService.session.currentUser.roleStr) !== -1));
        }.bind(this);

        return AuthService.authService
    }
}

