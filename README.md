service worker in .net core that terminates a process based on a relation master-slave.

for instance, the torrent app (slave) is killed if the vpn (master) was terminated or died. 

windows
```applescript
> process-butcher.exe --master=nordvpn --slave=utorrent
```

linux/macos
```applescript
> process-butcher --master=nordvpn --slave=utorrent
```

#### about VPN kill-switch
> most often vpn has a kill-switch to kill the torrent app if the vpn gets disconnected, which that works well.
   However, when the vpn suddently dies the torrent keeps working and then fallback to the default connection.
   This service worker will then kill the torrent app, if the vpn is not dected anymore, it will also not allow the torrent app to restart without the vpn running.
