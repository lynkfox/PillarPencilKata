# PillarPencilKata
Pencil Simulation Kata for Pillar Technology


The Build and Run commands assume that you alrady know where your download of this directory is stored, and that you are using dotNet Core 2.2

Navigate to the download directory. From the base directory (~\PillarPencilKata-master) use the following commands as is, or navigate further in to run the commands.



# Tests

## Build Tests

* dotnet build PencilSimulationSolution\PencilLib\PencilLib.csproj
* dotnet build PencilSimulationSolution\PencilTests\PencilSimulationTests.csproj

## Run Tests

* dotnet vstest PencilSimulationSolution\PencilTests\bin\Debug\netcoreapp2.2\PencilSimulationTests.dll
* dotnet vstest PencilSimulationSolution\PencilTests\bin\Debug\netcoreapp2.2\PencilSimulationTests.dll -lt 
 - - This second command will lists the names of tests.






# Wrapper 
The Wrapper is a very simple, with very little error checking command prompt program to make use of Pencil and Paper objects. This was mostly for me to back up the TDD - I can see the Tests are Green, but was wondering if there was a concept I did not think of. I built a wrapper to hold both concepts together in my head (TDD and "Live" environment) to be able to compare how I think about it, to process the two together, and make sure I wasn't missing anything from thinking about coding in a different direction.

## Build Wrapper

* dotnet build PencilSimulationSolution\PencilSimulation\PencilSimulation.csproj

## Run Wrapper

* dotnet run PencilSimulationSolution\PencilTests\bin\Debug\netcoreapp2.2\PencilSimulation.dll

*assuming you let default dotnet build procede without arguments.*
