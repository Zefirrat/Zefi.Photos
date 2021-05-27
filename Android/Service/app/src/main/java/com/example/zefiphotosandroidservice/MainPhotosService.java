package com.example.zefiphotosandroidservice;

import android.app.Service;
import android.content.Intent;
import android.os.IBinder;
import android.provider.Settings;
import android.provider.Settings.Secure;

public class MainPhotosService extends Service {

    private String _serverurl = "http://localhost:4130";
    private String android_id = Secure.getString(getContentResolver(), Secure.ANDROID_ID);

    @Override
    public IBinder onBind(Intent intent) {
        return null;
    }

    @Override
    public int onStartCommand(Intent intent, int flags, int startId) {
        Thread newThread = new Thread(this::ListenToServer);
        newThread.start();
        return super.onStartCommand(intent, flags, startId);
    }

    private void ListenToServer(){

    }
}
