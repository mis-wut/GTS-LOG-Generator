# GTSLogGenerator

Porty na których uruchomione są serwisy:
32807 - frontend
5000 - backend
9999 - influx

Aby skonfigurować influxa od zera wystarczy utworzyć dwa buckety "gts_logs_metrics" i "gts_system_metrics", utworzyć token służący do autoryzacji oraz zaktualizować wartość tokena w konfiguracji GTS-LOG. 

Uruchamianie backendu generatora:
1.  Przejść do katalogu /home/rytelk/Magisterka/GTSLogGenerator/deployment_package/out
2. nohup dotnet GTSLogGeneratorApi.dll &> gtslogapi.out & 

Port backendu jest ustawiony na sztywno na 5000, poniżej zamieszczę kod źródłowy backendu oraz frontendu jakby zaszła potrzeba zmiany czegokolwiek. 
Aby sprawdzić na jakim porcie działa webapi najpierw wyszukuje ID procesu - "sudo ps ax | grep GTSLogGeneratorApi.dll", a następnie wykonuje "lsof -i -P -n" (prawdopodobnie istnieje szybszy sposób, żeby to sprawdzić).
image.png

Uruchamianie frontendu generatora:
1.  Przejść do katalogu /home/rytelk/Magisterka/GTSLogGenerator/deployment_package
2. nohup serve -l 38207 -s dist &> gtsfronted.out &

Uruchamianie Influxdb: nohup influxd & > influxd.out &
Wyszukanie procesu frontendu generatora:  sudo ps aux | grep node
Wyszukanie procesu backendu generatora:  sudo ps ax | grep GTSLogGeneratorApi.dll
Połączenie się na gpunode3 przez ssh.mini.pw.edu.pl oraz tunelowaniem portów: ssh -L 38207:localhost:38207 -L 5000:localhost:5000 -L 9999:localhost:9999 -L 2222:localhost:2222  -J rytelk@ssh.mini.pw.edu.pl rytelk@gpunode3

Kod źródłowy znajduje się na repozytorium github: https://github.com/rytelk/GTSLogGenerator proszę o pobranie kodu lub fork repozytorium.
W repozytorium znajduje się zip deploymend_package (https://github.com/rytelk/GTSLogGenerator/blob/master/deployment_package.zip) można go użyć do uruchamiania backendu oraz frontendu generatora.
