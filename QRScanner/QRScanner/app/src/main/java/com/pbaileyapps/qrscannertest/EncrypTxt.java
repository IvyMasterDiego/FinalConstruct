package com.pbaileyapps.qrscannertest;

import android.util.Base64;
import java.util.regex.Pattern;

import javax.crypto.Cipher;
import javax.crypto.SecretKey;
import javax.crypto.spec.IvParameterSpec;
import javax.crypto.spec.SecretKeySpec;


public class EncrypTxt {
    private static final String des = "DESede/CBC/PKCS5Padding";
    private static final String sKey = "DESede";

    private final byte[] key;
    private final byte[] initializationVector;

    public EncrypTxt(byte[] key, byte[] initializationVector){
        this.key=key;
        this.initializationVector=initializationVector;
    }

    public String decryptText(String encryptText) throws Exception{
        byte[] encData = Base64.decode(encryptText,Base64.DEFAULT);
        Cipher decipher = Cipher.getInstance(des);
        byte[] tdesKeyDate = key;
        SecretKey myKey = new SecretKeySpec(tdesKeyDate,sKey);
        IvParameterSpec ivspec = new IvParameterSpec(initializationVector);
        decipher.init(Cipher.DECRYPT_MODE,myKey,ivspec);
        byte[] plaintext=decipher.doFinal(encData);
        return new String(plaintext);
    }
}
