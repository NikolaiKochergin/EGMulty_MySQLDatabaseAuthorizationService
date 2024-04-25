using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine.Networking;

namespace Source.Scripts.Services.Authorization
{
    public class AuthorizationService
    {
        private const string Login = "login";
        private const string Password = "password";
        private const string AuthorizationUri = "authorization.php";
        private const string RegistrationUri = "registration.php";
        private const double Timout = 5;
        
        private readonly string _authorizationUri;
        private readonly string _registrationUri;
        
        public AuthorizationService(string mainUrl)
        {
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

            SendRequest(_authorizationUri, data, OnSuccess, onErrorCallback).Forget();
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
                    onSuccessCallback?.Invoke($"User id: {id}");
                else
                    onErrorCallback?.Invoke($"Couldn't parse \"{result[1]}\" to INT. Server request: {request}");
            }
        }

        public void Register(string login, string password, string confirmPassword, Action<string> onSuccessCallback = null, Action<string> onErrorCallback = null)
        {
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
            {
                onErrorCallback?.Invoke("Login and/ore password is empty");
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
            
            SendRequest(_registrationUri, data, OnSuccess, onErrorCallback).Forget();
            return;

            void OnSuccess(string result)
            {
                if (result == "ok")
                    onSuccessCallback?.Invoke("Registration is successful");
                else
                    onErrorCallback?.Invoke($"Server request: {result}");
            }
        }

        private static async UniTaskVoid SendRequest(string uri, Dictionary<string, string> data, Action<string> onSuccessCallback = null, Action<string> onErrorCallback = null)
        {
            using TimeoutController timeout = new TimeoutController();
            try
            {
                using UnityWebRequest webRequest = await UnityWebRequest
                    .Post(uri, data)
                    .SendWebRequest()
                    .WithCancellation(timeout.Timeout(TimeSpan.FromSeconds(Timout)));
                
                timeout.Reset();

                if (webRequest.result == UnityWebRequest.Result.Success)
                {
                    string result = DownloadHandlerBuffer.GetContent(webRequest);
                    onSuccessCallback?.Invoke(result);
                }
            }
            catch (OperationCanceledException)
            {
                if (timeout.IsTimeout())
                    onErrorCallback?.Invoke("Request timeout.");
            }
        }

    }
}