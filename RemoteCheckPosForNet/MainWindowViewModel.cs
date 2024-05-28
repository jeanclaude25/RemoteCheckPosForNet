using System;
using System.Threading.Tasks;
using System.Windows;
using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;

namespace RemoteCheckPosForNet
{
    public class MainWindowViewModel
    {
        public async Task<string> CheckFingerprintAvailability()
        {
            string returnMessage = "";

            try
            {
                var availability = await CrossFingerprint.Current.GetAvailabilityAsync();

                switch (availability)
                {
                    case FingerprintAvailability.Available:
                        returnMessage = "Fingerprint verification is available.";
                        break;
                    case FingerprintAvailability.NoSensor:
                        returnMessage = "No biometric device found.";
                        break;
                    case FingerprintAvailability.NoPermission:
                        returnMessage = "No permission to use biometric verification.";
                        break;
                    case FingerprintAvailability.Unknown:
                    default:
                        returnMessage = "Fingerprint verification is currently unavailable.";
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
