#! /usr/bin/env bash

pullImage() {
  # Pulls/Tags the dockerfile pulled from the url
  docker build -t mysecureshell https://raw.githubusercontent.com/brent-soles/mysecureshell/master/deployment-tools/docker/debian/buster/Dockerfile
}

if docker -v ; then
  pullImage
else
  echo "You do not have docker installed. Please install docker in order to use install the server image"
fi

