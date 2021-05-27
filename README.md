# Zefi.Photos
## Description
Because of Google Photos are becomes pay to use, I need a service which can upload my photos to some local storage, when smartphone is connecting to home WiFi.
## Development plan:
The services which I will develop here are 3 types:
1. Android (may be iOS in future) service, which will test server connection and if avalible - request settings and start to upload requested photos or videos.
2. Server which will host endpoint, writing to database and store media.
3. Web interface to view sorted and filtered photos like in Google Photos service.

### Android service
Check connection every 5 minutes.
If server avaliable then send request for settings.

Server's response:
```
{
"directories":[
"/sdcard/DCIM",
"/sdcard/Telegram/Media"
],
"fileExtensions":"mp3,png,jpg,mp4,mpeg,avi"
}
```

Android's request payload with file:
```
{
"deviceId":"15b0b4d7-2f53-4baa-9b3d-f16f8951eb1e"
"directory":"/sdcard/DCIM",
"fileName":"IMG1.png"
}
```
Supports Android 11 or higher (API 30)
