package com.pbaileyapps.qrscannertest

import android.app.SearchManager
import android.content.DialogInterface
import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.widget.ImageButton
import android.widget.TextView
import androidx.appcompat.app.AlertDialog
import com.google.zxing.integration.android.IntentIntegrator
import org.w3c.dom.Text

class MainActivity : AppCompatActivity() {

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)
        val qrButton:ImageButton = findViewById(R.id.qr_button)

        qrButton.setOnClickListener({
            val intentIntegrator = IntentIntegrator(this)
            intentIntegrator.setDesiredBarcodeFormats(listOf(IntentIntegrator.QR_CODE))
            intentIntegrator.initiateScan()
        })

    }

    override fun onActivityResult(requestCode: Int, resultCode: Int, data: Intent?) {
        super.onActivityResult(requestCode, resultCode, data)
        val result = IntentIntegrator.parseActivityResult(resultCode, data)
        val resultDecript = EncrypTxt("regshajshsdrkrmwcbsgskfe".toByteArray(),"dkr*vcd@".toByteArray())
        if (result != null) {
          val message =(resultDecript.decryptText(result.contents))
            val list = message.split("||")
            val txtName:TextView = findViewById(R.id.txtName)
            val txtCedula:TextView = findViewById(R.id.txtCedula)
            val txtLastName:TextView=findViewById(R.id.txtLastName)
            val txtAddress:TextView = findViewById(R.id.txtAdress)
            val txtPhone:TextView = findViewById(R.id.txtCelular)

            txtCedula.text = list[0]
            txtName.text=list[1]
            txtLastName.text=list[2]
            txtPhone.text = list[3]
            txtAddress.text=list[4]
        }
    }
}