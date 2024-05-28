using System;
using System.Threading.Tasks;
using System.Windows;
using Windows.Security.Credentials.UI;

namespace RemoteCheckPosForNet
{
    public class MainWindowViewModel
    {
        public async Task<string> CheckFingerprintAvailability()
        {
            string returnMessage = "";

            try
            {
                // Check the availability of fingerprint authentication.
                var ucvAvailability = await UserConsentVerifier.CheckAvailabilityAsync();

                switch (ucvAvailability)
                {
                    case UserConsentVerifierAvailability.Available:
                        returnMessage = "Fingerprint verification is available.";
                        break;
                    case UserConsentVerifierAvailability.DeviceBusy:
                        returnMessage = "Biometric device is busy.";
                        break;
                    case UserConsentVerifierAvailability.DeviceNotPresent:
                        returnMessage = "No biometric device found.";
                        break;
                    case UserConsentVerifierAvailability.DisabledByPolicy:
                        returnMessage = "Biometric verification is disabled by policy.";
                        break;
                    case UserConsentVerifierAvailability.NotConfiguredForUser:
                        returnMessage = "The user has no fingerprints registered. Please add a fingerprint to the " +
                                        "fingerprint database and try again.";
                        break;
                    default:
                        returnMessage = "Fingerprints verification is currently unavailable.";
                        break;
                }
            }
            catch (Exception ex)
            {
                returnMessage = "Fingerprint authentication availability check failed: " + ex.ToString();
            }

            return returnMessage;
        }

        public async Task IsFingerprintPresent()
        {
            string result = await CheckFingerprintAvailability();
            MessageBox.Show(result);
        }
    }
}
