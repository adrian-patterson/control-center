#!/bin/bash
# Script to stop running docker containers, clean and remove them, and build/run new image

echo Killing Docker containers...
docker container kill $(docker ps -q)	# kill all running containers
docker container rm $(docker ps -a -q)	# delete all containers

echo Updating new branch from git (master branch)...
echo Folder contents before pulling:
echo *
cd /home/pi/ControlCenter
git pull origin master

echo Building new Docker container and running it at port 8000:80...
docker build -t control-center . 
docker run --privileged -p 8000:80 control-center

