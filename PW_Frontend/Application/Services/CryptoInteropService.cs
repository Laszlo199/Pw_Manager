using Microsoft.JSInterop;
namespace PW_Frontend.Application.Services {
    public class CryptoInteropService {
        private readonly IJSRuntime _jsRuntime;

        public CryptoInteropService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task<string> EncryptData(string plaintext, byte[] key) {
            // Convert the key to a hex string before using it to encrypt
            string keyHex = BitConverter.ToString(key).Replace("-", "").ToLower();
            return await _jsRuntime.InvokeAsync<string>("encryptData", plaintext,keyHex);
        }

        public async Task<string> DecryptData(string encryptedData, byte[] key) {
            // Convert the key to a hex string before using it to decrypt
            string keyHex = BitConverter.ToString(key).Replace("-", "").ToLower();
            return await _jsRuntime.InvokeAsync<string>("decryptData", encryptedData,keyHex);
        }
    }
}
