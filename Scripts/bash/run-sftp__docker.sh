#! /usr/bin/env bash

runImg () {
  if [ -z $1 ]; then
    echo "Error: Must provide a port number. Usage: ./run-sftp__docker.sh [port]"
    return
  fi
  # Passes a port
  docker run -d -p $1:22 mysecureshell
}

if docker -v ; then
  runImg $1
else
  echo "Please install docker in order to run the image"
fi
