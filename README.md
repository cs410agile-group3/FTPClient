# Ftp Client Quick Start

### Architecture/Project Structure Overview

```
rootDir/
  
  FtpClient/
    |
    |-- FtpCli/
    |     Program.cs <- Entry point for commands
    |     FtpCli.cs  <- Provides an interface for Program.cs to interact with
    |     InitializeSession.cs <- Initializes session with user-provided input
    |     Data.cs    <- Storage class for session data
    |     Commands/
    |     |
    |     |--> This is the directory where interfaces for a command will be defined
    |     Packages/
    |     |
    |     |--> This is where domains of functionality will be defined. For example, a 'Cli' package would provide methods for interacting with parsing args/routing commands.
    |
    |-- FtpCli.Test/
    |   |
    |   |--> Contains the unit test files
    |
  Scripts/
    |
    |-- bash/
    |   |-- connect-sftp.sh
    |   |-- pull-sftp__docker.sh
    |   |-- run-sftp__docker.sh
    |
    ... Others to be defined at a later date ...
```

### To Run

Setup `docker` per the [docker usage](#docker)  instructions.

Start the server by running the docker script `./run-sftp__docker.sh 8080`

In the FtpCli/ directory:

  `$> dotnet run`

> This will default run the 'Program.cs' file. You can also run the program with the following parameters. An invalid number of parameters will result in user prompts for login parameters:

  `$> dotnet run -server [servername] -port [port] -user [username]`
 
 You can optionally pass in `-save [alias]` to save the connection information under the given alias. Once it is saved, it can be recalled using:

  `$> dotnet run [alias]`

Example Usage:

  `$> dotnet run 127.0.0.1 8080 mssuser mssuser`

### To Test

In the FtpCli.Test/ directory:

  `$> dotnet test`

> This will run each test file in the directory and output amount of tests passed.

If any issues arise while developing, please message the team on #agile-project in Slack!

### [Docker Usage](#docker)

In this project, there are some scripts in the `Scripts/` folder which are essentially wrappers around docker commands. There names correspond the the functionality they each try to to provide.

For instance the `pull-sftp__docker.sh` script pulls/builds the docker image on your local machine, and makes it available to run in order to test connecting to an actual SFTP server.

### Scripts

The `run-sftp__docker.sh` and `connect-sftp.sh` can take a -h|--help flag as a first argument to print a help message.

#### `pull-sftp__docker.sh`

This command is used to get the docker image and build it on you local machine.

The command takes no arguments to run, as the Dockerfile is hosted/pulled from Github.

Usage: `./pull-sftp__docker.sh`

#### `run-sftp__docker.sh`

This command boots the container and makes the sftp server available on the specified port @ 127.0.0.1.

Mandatory argument(s):
* port -> port number to make server available on

Usage: `./run-sftp__docker.sh`

#### `connect-sftp.sh`

This command will access the SFTP server via cli.

NOTE: When pulling the docker image, the image creates a base user with the following credentials:

username: mssuser
password: mssuser

Mandatory argument(s):
* username

Optional argument(s):
* port -> port number to connect to on 127.0.0.1 


#### Steps for using the scripts

1. Run `./pull-sftp__docker.sh` (Note: command may take a few minutes)
2. Run `./run-sftp__docker.sh [port]` (Port number is not optional and must be provided)
3. Run `./connect-sftp.sh [username] [port: default 7667]` 
> The default username/password is: mssuser/mssuser
If all goes according to plan, then you should be connected to the sftp server over a cli interface.

#### To stop the server

In order to stop the server first run:

`docker ps`

This is similar the the `ps` linux command, and the output will look similar to:
```
CONTAINER ID        IMAGE               COMMAND               CREATED             STATUS              PORTS                  NAMES
36f381e3cee7        mysecureshell       "/usr/sbin/sshd -D"   4 seconds ago       Up 3 seconds        0.0.0.0:7667->22/tcp   recursing_grothendieck
```

Then once you have this information, all you have to do is run:

`docker stop 36f381e3cee7`

or, you can use the NAME of the container your wish to stop:

`docker stop recursing_grothendieck`


