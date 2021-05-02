using AppTrackingTransparency;
using AppTrackingTransparency.Common;
using UnityEngine;
using UnityEngine.UI;

namespace RequestTrackingSample
{
    public class RequestTrackingSampleMenu : MonoBehaviour
    {
        public Text idfaLabel;
        public Text authorizedStatusLabel;
        public Button requestAuthorizationButton;

        private IAppTrackingTransparencyManager _appTrackingTransparencyManager;

        private void Start()
        {
            if (AppTrackingTransparencyModule.IsSupported)
            {
                this._appTrackingTransparencyManager = AppTrackingTransparencyModule.CreateManager();
            }

            this.RefreshUserInterface();
            this.requestAuthorizationButton.onClick.AddListener(RequestAuthorizationButtonClicked);
        }

        private void Update()
        {
            if (this._appTrackingTransparencyManager == null)
            {
                return;
            }

            this._appTrackingTransparencyManager.Update();
            this.RefreshUserInterface();
        }

        private void RequestAuthorizationButtonClicked()
        {
            if (this._appTrackingTransparencyManager == null)
            {
                return;
            }

            this._appTrackingTransparencyManager.RequestTrackingAuthorization(authStatus =>
            {
                Debug.Log("Authorization status changed: " + authStatus);
                this.RefreshUserInterface();
            });
        }

        private void RefreshUserInterface()
        {
            if (this._appTrackingTransparencyManager != null)
            {
                this.idfaLabel.text = "<b>IDFA:</b> " + this._appTrackingTransparencyManager.Idfa;
                this.authorizedStatusLabel.text = "<b>Tracking Authorization Status:</b> " + this._appTrackingTransparencyManager.TrackingAuthorizationStatus;
                this.requestAuthorizationButton.gameObject
                    .SetActive(this._appTrackingTransparencyManager.TrackingAuthorizationStatus == AppTrackingTransparencyAuthorizationStatus.NotDetermined);
            }
            else
            {
                this.idfaLabel.text = "<b>IDFA:</b> Unsupported platform";
                this.authorizedStatusLabel.text = "<b>Tracking Authorization Status:</b> Unsupported platform";
                this.requestAuthorizationButton.gameObject.SetActive(false);
            }
        }
    }
}
