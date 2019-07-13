#! /usr/bin/env bash

connect () {
  if $1 == "-h" || $1 == "--help"; then
    echo "Usage \n \
      ./connect-sftp.sh [username] \
      "
  fi
  
  echo "Connecting on port 7667..."
  sftp -P 7667 $1@127.0.0.1
}

connect $1
