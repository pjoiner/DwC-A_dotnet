# ReadMe

To run benchmarks for the current release from the command line and save logs/results use the following command line.

```
dotnet run -c LocalRelease --framework net48 net50 net60 net70
```
To run benchmarks for the previous releases use the following command line.

```
dotnet run -c Release --framework net48 net50 net60 net70
```