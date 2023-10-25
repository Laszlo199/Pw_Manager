// crypto.js

function encryptData(plaintext,keyHex) {
    const key = CryptoJS.enc.Hex.parse(keyHex);
    const encrypted = CryptoJS.AES.encrypt(plaintext, key, {
        mode: CryptoJS.mode.ECB,
        padding: CryptoJS.pad.Pkcs7
    });

    return encrypted.toString();
}

function decryptData(encryptedText,keyHex) {
    const key = CryptoJS.enc.Hex.parse(keyHex);
    const decrypted = CryptoJS.AES.decrypt(encryptedText, key, {
        mode: CryptoJS.mode.ECB,
        padding: CryptoJS.pad.Pkcs7
    });

    return decrypted.toString(CryptoJS.enc.Utf8);
}
