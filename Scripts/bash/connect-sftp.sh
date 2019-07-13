#! /usr/bin/env bash

connect () {
  username=$1
  port=$2
  
  if [ -z $username ]; then
    echo "Error: no username provided."
    echo "Usage: ./connect-sftp.sh [username] [port: default 7667]"
    return
  fi
  
  if [ $1 == "-h" ] || [ $1 == "--help" ]; then
    echo "Usage: ./connect-sftp.sh [username] [port: default 7667]"
    return
  fi

  if [ -z $port ]; then
    echo "Info: no port number provided."
    echo "Info: using default port 7667"
    port=7667
  fi
  
  echo "Connecting on port 7667..."
  sftp -P $port $username@127.0.0.1
}

connect $1
