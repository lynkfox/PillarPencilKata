# PillarPencilKata
Pencil Simulation Kata for Pillar Technology


The Build and Run commands assume that you alrady know where your download of this directory is stored, and that you are using dotNet Core 2.2



# Tests

## Build Tests

* dotnet build PencilLib\PencilLib.csproj
* dotnet build PencilTests\PencilSimulationTests.csproj

## Run Tests

* dotnet vstest PencilTests\bin\Debug\netcoreapp2.2\PencilSimulationTests.dll


# Wrapper 
The Wrapper is a very simple, with very little error checking command prompt program to make use of Pencil and Paper objects. This was mostly for me to back up the TDD - I can see the Tests are Green, but was wondering if there was a concept I did not think of. I built a wrapper to hold both concepts together in my head (TDD and "Live" environment) to be able to compare how I think about it. 

## Build Wrapper

* dotnet build PencilSimulationSolution\PencilSimulation\PencilSimulation.csproj

## Run Wrapper

* dotnet run dotnet run \Debug\netcoreapp2.2\PencilSimulation.dll

*assuming you let default dotnet build procede without arguments.*
