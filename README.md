# ControlCenter
IoT home-hub using dotnet, C#, React.js, and docker.
Home-server for controlling LEDs and other home devices. Work in progress; for future I hope to integrate a home security system, calendar, weather, and other features.

### Quick Copy & Paste Docker Run Commands
> `docker build -t control-center . && docker run --privileged -p 8000:80 control-center`

## Full List of Technologies
* **Hardware**
  * Raspberry Pi
  * ws2811 LED strips
* **Software**
  * .NET Core 5.0 web App (running on ARM32 processor)
  * Docker container to build, run, and host app
  * React.js for the front end 
## Dependencies
* .NET core and Node.js (as seen in the [Dockerfile](https://github.com/canadrian72/ControlCenter/blob/master/Dockerfile)
* [rpi_ws281x-csharp library (forked)](https://github.com/d8ahazard/rpi-ws281x-csharp/) for .NET 5.0 Core
  * Installed with NuGet packet manager
* Built rpi_ws281x shared C-Library (ws2811.so) NEEDS to be in working directory of Dockerfile [build instructions](https://github.com/d8ahazard/rpi-ws281x-csharp/tree/master/lib) 
## How to Run
1. Clone this repository from Raspberry Pi running Raspbian
2. `cd` into directory with Dockerfile
3. Build the docker image with
    > `docker build -t <image-name> .`
4. Run the docker container with
    > `docker run --privileged -p <port> <image-name>`
  * **NOTE** that including `--privileged` is necessary for the dockerfile to access the pi GPIOs
