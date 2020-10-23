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

