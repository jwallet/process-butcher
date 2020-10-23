service worker in .net core that terminates a process based on a relation master-slave.

for instance, the torrent app (slave) is killed if the vpn (master) was terminated or died. 

windows
```(shell)
> process-butcher.exe --master=nordvpn --slave=utorrent
```

linux/macos
```(shell)
> process-butcher --master=nordvpn --slave=utorrent
```

