#! /usr/bin/env bash

pullImage() {
  # Pulls/Tags the dockerfile pulled from the url
  docker build -t mysecureshell https://raw.githubusercontent.com/brent-soles/mysecureshell/master/deployment-tools/docker/debian/buster/Dockerfile
}

if docker -v ; then
  pullImage
else
  echo "You do not have docker installed. You must install docker in order to use this script."
fi

