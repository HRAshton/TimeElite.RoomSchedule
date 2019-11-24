# Скрипт для запуска RasPi в режиме киоска. Должен лежать по пути /home/pi/.config/lxsession/lxde/pi/autostart

xset s off
xset -dpms
xset s noblank
unclutter -idle 0.5 -root &
sed -i 's/"exited_cleanly":false/"exited_cleanly":true/' ~/.config/chromium/Default/Preferences
sed -i 's/"exit_type":"Crashed"/"exit_type":"Normal"/' ~/.config/chromium/Default/Preferences
chromium-browser -noerrdialogs --kiosk http://elite.int file:///home/pi/TimeElite/error.html --no-sandbox --incognito --disable-infobars --disable-translate > /dev/null &
