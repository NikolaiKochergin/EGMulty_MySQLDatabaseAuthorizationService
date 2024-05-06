using System;
using System.Collections.Generic;
using Source.Scripts.Infrastructure.Services.Network;
using Source.Scripts.Infrastructure.Services.StaticData;

namespace Source.Scripts.Infrastructure.Services.Authorization
{
    public class AuthorizationService : IAuthorizationService
    {
        private const string Login = "login";
        private const string Password = "password";
        private const string AuthorizationUri = "authorization.php";
        private const string RegistrationUri = "registration.php";
        
        private readonly IStaticDataService _staticData;
        private readonly INetworkService _network;

        private string _authorizationUri;
        private string _registrationUri;
        
        public AuthorizationService(IStaticDataService staticData, INetworkService network)
        {
            _staticData = staticData;
            _network = network;
        }
        
        public int UserId { get; private set; }
        public bool IsAuthorized { get; private set; }

        public event Action AuthorizationHappened;

        public void Initialize()
        {
            string mainUrl = _staticData.ForMainUrl();
            _authorizationUri = mainUrl + AuthorizationUri;
            _registrationUri = mainUrl + RegistrationUri;
        }

        public void Authorize(string login, string password, Action<string> onSuccessCallback = null, Action<string> onErrorCallback = null)
        {
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                onErrorCallback?.Invoke("Login and/ore password is empty");
                return;
            }
            
            Dictionary<string, string> data = new()
            {
                {Login, login},
                {Password, password}
            };

            _network.SendRequest(_authorizationUri, data, OnSuccess, onErrorCallback).Forget();
            return;

            void OnSuccess(string request)
            {
                string[] result = request.Split('|');
                if (result.Length < 2 || result[0] != "ok")
                {
                    onErrorCallback?.Invoke($"Server request: {request}");
                    return;
                }

                if (int.TryParse(result[1], out int id))
                {
                    UserId = id;
                    IsAuthorized = true;
                    AuthorizationHappened?.Invoke();
                    onSuccessCallback?.Invoke($"User id: {id}");
                }
                else
                {
                    onErrorCallback?.Invoke($"Couldn't parse \"{result[1]}\" to INT. Server request: {request}");
                }
            }
        }

        public void Register(string login, string password, string confirmPassword, Action<string> onSuccessCallback = null, Action<string> onErrorCallback = null)
        {
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
            {
                onErrorCallback?.Invoke("Login and/or password is empty");
                return;
            }

            if (password != confirmPassword)
            {
                onErrorCallback?.Invoke("Passwords don't match");
                return;
            }
            
            Dictionary<string, string> data = new()
            {
                {Login, login},
                {Password, password}
            };
            
            _network.SendRequest(_registrationUri, data, OnSuccess, onErrorCallback).Forget();
            return;

            void OnSuccess(string result)
            {
                if (result == "ok")
                    onSuccessCallback?.Invoke("Registration is successful");
                else
                    onErrorCallback?.Invoke($"Server request: {result}");
            }
        }
    }
}