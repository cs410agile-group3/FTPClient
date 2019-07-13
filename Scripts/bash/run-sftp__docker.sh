#! /usr/bin/env bash

runImg () {
  # Passes a port
  docker run -d -p $1:22 mysecureshell
}

if docker -v ; then
  runImg $1
else
  echo "Please install docker, or run the docker daemon in order to run the image"
fi
