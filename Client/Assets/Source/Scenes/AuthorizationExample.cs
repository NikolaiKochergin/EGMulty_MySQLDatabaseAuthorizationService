using Source.Scripts.Services.Authorization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Scenes
{
    public class AuthorizationExample : MonoBehaviour
    {
        private const string Main = "http://<database>/";

        [SerializeField] private TMP_InputField _login;
        [SerializeField] private TMP_InputField _password;
        [SerializeField] private TMP_InputField _confirmPassword;
        [SerializeField] private TextMeshProUGUI _infoText;
        [SerializeField] private Button _signInButton;
        [SerializeField] private Button _signUpButton;
        [SerializeField] private Button _registrationButton;
        [SerializeField] private Button _authorizationButton;
        [SerializeField] private GameObject _authorizationView;
        [SerializeField] private GameObject _registrationView;
        
        private AuthorizationService _authorization;
        private bool _isRegistrationViewActive;

        private void Awake() => 
            _authorization = new AuthorizationService(Main);

        private void Start()
        {
            _signInButton.onClick.AddListener(OnSignInButtonClicked);
            _signUpButton.onClick.AddListener(OnSignUpButtonClicked);
            _registrationButton.onClick.AddListener(OnRegistrationButtonClicked);
            _authorizationButton.onClick.AddListener(OnAuthorizationButtonClicked);
        }

        private void OnDestroy()
        {
            _signInButton.onClick.RemoveListener(OnSignInButtonClicked);
            _signUpButton.onClick.RemoveListener(OnSignUpButtonClicked);
            _registrationButton.onClick.RemoveListener(OnRegistrationButtonClicked);
            _authorizationButton.onClick.RemoveListener(OnAuthorizationButtonClicked);
        }

        private void OnSignInButtonClicked()
        {
            _authorizationView.gameObject.SetActive(false);
            _authorization.Authorize(_login.text, _password.text, OnSuccess, OnError);
        }

        private void OnSignUpButtonClicked()
        {
            _registrationView.gameObject.SetActive(false);
            _authorization.Register(_login.text, _password.text, _confirmPassword.text, OnSuccess, OnError);
        }

        private void OnRegistrationButtonClicked()
        {
            _isRegistrationViewActive = true;
            UpdateView();
        }

        private void OnAuthorizationButtonClicked()
        {
            _isRegistrationViewActive = false;
            UpdateView();
        }

        private void OnSuccess(string message)
        {
            _infoText.color = Color.green;
            _infoText.SetText(message);
            
            if (!_isRegistrationViewActive) 
                return;
            
            _isRegistrationViewActive = false;
            UpdateView();
        }

        private void OnError(string message)
        {
            _infoText.color = Color.red;
            _infoText.SetText(message);
            UpdateView();
        }

        private void UpdateView()
        {
            _authorizationView.gameObject.SetActive(!_isRegistrationViewActive);
            _registrationView.gameObject.SetActive(_isRegistrationViewActive);
        }
    }
}