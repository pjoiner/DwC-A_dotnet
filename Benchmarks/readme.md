# ReadMe

To run benchmarks for the current release from the command line and save logs/results use the following command line.

**NOTE:** If you are using an antivirus on Windows and cannot disable or whitelist the Benchmarks app then do this from an Admin prompt

```
dotnet run -c LocalRelease --framework net48 net50 net60 net70 net80 net9.0
```
To run benchmarks for the previous releases use the following command line.

```
dotnet run -c Release --framework net48 net50 net60 net70 net80 net9.0
```

Results are located in `BenchmarkDotNet.Artifacts\results\`