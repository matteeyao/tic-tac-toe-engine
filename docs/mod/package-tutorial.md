# Packaging and Hosting a NuGet package on GitHub

1. In `App/App.csproj`, update the package version or remove the package from GitHub Packages to re-upload the current packaged app

2. Remove `TicTacToe.1.0.0.nupkg` file from `bin/Debug` and `bin/Release`

3. Once any changes has been made and version has been updated, run `dotnet pack --configuration Release`

4. With the correct configuration in place, we can push our package to GitHub with:

```
dotnet nuget push ./bin/Release/TicTacToe.1.0.0.nupkg --source "github"
```

## Clearing local folders in the consumer app

[!Important]
> Packages are immutable. If you correct a problem, change the contents of the package and pack again, when you retest you will still be using the old version of the package until you clear your global packages folder. This is especially relevant when testing packages that don't use a unique prerelease label on every build.

If you encounter package installation problems or otherwise want to ensure that you're installing packages from a remote gallery, use the `locals --clear` option (dotnet.exe) or `locals -clear` (nuget.exe), specifying the folder to clear, or `all` to clear all folders:

```cli
# Clear all caches (use either command)
dotnet nuget locals all --clear
nuget locals all -clear

# Clear all caches (use either command)
dotnet nuget locals all --clear
nuget locals all -clear
```

## Reference Links

* [Code Coverage in GitHub with .NET Core](https://samlearnsazure.blog/2021/01/05/code-coverage-in-github-with-net-core/)
