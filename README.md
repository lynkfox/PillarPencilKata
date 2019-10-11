# PillarPencilKata
Pencil Simulation Kata for Pillar Technology


The Build and Run commands assume that you alrady know where your download of this directory is stored, and that you are using dotNet Core 2.2

# Wrapper 
The Wrapper is a very simple, with very little error checking command prompt program to make use of Pencil and Paper objects.

## Build Wrapper

* dotnet build PencilSimulationSolution\PencilSimulation\PencilSimulation.csproj

## Run Wrapper

* dotnet run dotnet run \Debug\netcoreapp2.2\PencilSimulation.dll

*assuming you let default dotnetbuild procede without argumenents.*

# Tests

## Build Tests
* dotnet build PencilTests\PencilSimulationTests.csproj
* dotnet build PencilLib\PencilLib.csproj

## Run Tests

* dotnet vstest PencilTests\bin\Debug\netcoreapp2.2\PencilSimulationTests.dll
