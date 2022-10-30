# 12FactorApp
## 1.Codebase
One CodeBase and it is hosted on github via git.
has a main and a dev branch

## 2.Dependencies
All dependencies are in the TwelveFactorApp.Api.csproj
When Building the dependencies will be downloaded from nuget and builded and
will be added to the builded files.

So no dependencies are needed on the host except .NET 6 

## 3.Config
Config for Redis Database is used from Environment Variable (for use see ReadMe)

## 4.Backing Services
Redis Caching Database (It writes a log Entry when writing something into the Redis Cache)

## 5.Build, Release,Run
Github Builds when commiting to the Main (via PullRequest) then Releases that to 
dockerHub where you can download and run it.

## 6.Stateless Process
The Cached Data is stored in a Redis Database --> Stateless
a second instant could be started and would have access to the same data

## 7.Port Binding
The API is binding to port 54321 (http) and 43210 (https) so that you can configure how it will be accessed

## 8.Fast Launch / Graceful Shutdown
Implemented a "Placeholder" Code which can be used for Grace Shutdown

## 9.Logs
Logs are written to STDOUT so you can access them via docker logs
or with a docker logs driver