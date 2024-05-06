using Reflex.Attributes;
using Source.Scripts.Infrastructure.Services.Authorization;
using Source.Scripts.UI.Extensions;
using Source.Scripts.UI.Services.Windows;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Scripts.UI.Windows.Authorization
{
    public sealed class AuthorizationWindow : WindowBase
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private TextMeshProUGUI _infoText;
        [SerializeField] private TMP_InputField _login;
        [SerializeField] private TMP_InputField _password;
        [SerializeField] private Button _signInButton;
        
        private IAuthorizationService _authorization;
        private IWindowService _windows;

        [Inject]
        private void Construct(IAuthorizationService authorization, IWindowService windows)
        {
            _authorization = authorization;
            _windows = windows;
        }

        protected override void SubscribeUpdates()
        {
            base.SubscribeUpdates();
            _signInButton.AddListener(OnSignInButtonClicked);
        }

        protected override void Cleanup()
        {
            base.Cleanup();
            _signInButton.RemoveListener(OnSignInButtonClicked);
        }

        private void OnSignInButtonClicked()
        {
            _canvasGroup.interactable = false;
            _authorization.Authorize(_login.text, _password.text, OnSuccess, OnError);
        }

        private void OnSuccess(string successMessage)
        {
            _infoText.color = Color.green;
            _infoText.SetText(successMessage);
            _windows.CloseWindow(WindowId.Authorization);
            _windows.OpenWindow(WindowId.StartGame);
        }

        private void OnError(string errorMessage)
        {
            _infoText.color = Color.red;
            _infoText.SetText(errorMessage);
            _canvasGroup.interactable = true;
        }
    }
}