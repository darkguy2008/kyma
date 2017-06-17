# KYMA
Keeps your cryptocurrency mining apps alive!

# Motivation

I have a few friends with a common need: To keep their mining processes alive because they sometimes crash and they have to constantly monitor those apps. I'm not a miner, I'm mostly a C#/JS developer with 8+ years of experience, so I figured I could help the community since they have been looking for a similar app, so I've made it.

This app will help you keep your mining processes alive so you can rest assured that your operations won't be interrupted :). Money is good, but peace of mind is also good y'know?

# Binary instructions

Extract the application from the ZIP file somewhere in your desktop. Then, create a batch (.bat) file with this content in the same folder where you extracted kyma.exe:
```
kyma -n miner.exe -i 1 -w 10 -r "c:\users\your name\desktop\miner\begin.bat"
```
This will run begin.bat if miner.exe doesn't exist when checking every 1 second, and will wait 10 seconds if the process does not exist before starting it up again.

# Source instructions

If you want to build your own version because you're skeptical (that's a good thing in this cryptoworld!) you can do so pretty easily: Just clone this repo, open the solution file with Visual Studio 2015+, make sure you have .NET Framework 4.0+ installed, build and run!.

Also make sure to set your debugging commandline parameters, else you'll run it and it will close immediately, you need to provide it some arguments in order to debug. If you just want to build, build and then use the .exe that Visual Studio created for you in the bin/Debug folder.

# Support this project! I also have bills to pay :P

If you'd like to donate, please do so! I do this in my spare time and I need some extra money to pay off some debts :(. Anything you can chip in will keep me alive (bad pun, haha) to improve the project with your feedback, solving issues and reviewing pull requests :)

### BTC: 17d9WhEJoF9tr7xNTHUHqQf9MRJ4GQrQ2v
### ETH: 0x94623e3dcfbf2234D99986F62385617F4EA97d2a
### LTC: LTxXBCwn8KZ1NZBXNy3MKTAhinZ2nWtG42

# General usage instructions
```
KYMA (Keep Your Miner Alive) 1.0.0

This app will keep your miner's app alive if it dies for some reason, so your mining
does not get interrupted!. It also works with any other kind of app, not just miners :)

Has this project been useful for you? Please consider donating!

Usage: kyma.exe [option] <value> ...

Options:

-v         (Optional)     Verbose output
-n <app>   (Required)     Process name to check for, i.e. miner.exe, notepad.exe, etc.
-i <N>     (Default: 1)   Check every N seconds if process is either alive or not
-w <N>     (Default: 10)  Wait N seconds if the process is dead before starting it up again
-r <file>  (Required)     What to run if the process is dead. It can be a .exe, .cmd. .bat, whatever!
                          Note: If the path contains spaces, please use double quotes!
Example:

Keep miner.exe alive, check every 1s and wait 10s if it's dead before starting begin.bat again:

kyma.exe -n miner.exe -i 1 -w 10 -r "c:\users\your name\desktop\miner\begin.bat"

Have fun!
Made by DARKGuy
```