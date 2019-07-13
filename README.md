# Ftp Client Quick Start

### Architecture/Project Structure Overview

```
FtpClient/
  |
  |-- FtpCli/
  |     Program.cs <- Entry point for commands
  |     FtpCli.cs <- Provides an interface for Program.cs to interact with
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
  ... Others to be defined at a later date ...
```

### To Run

In the FtpCli/ directory:

  `$> dotnet run`

> This will default run the 'Program.cs' file.

### To Test

In the FtpCli.Test/ directory:

  `$> dotnet test`

> This will run each test file in the directory and output amount of tests passed.

If any issues arise while developing, please message the team on #agile-project in Slack!