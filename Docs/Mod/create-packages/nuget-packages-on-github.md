# Hosting NuGet packages on GitHub

Build, host, and consume our own NuGet packages using `GitHub Packages`

## Creating our package using the CLI

CLI allows automating package creation on continuous integration, integrating w/ APIs, webhooks, and even creating end-to-end DevOps workflows.

```
dotnet pack --configuration Release
```

This time, we should see this as output:

```
Microsoft (R) Build Engine version 16.6.0+5ff7b0c9e for .NET Core
Copyright (C) Microsoft Corporation. All rights reserved.

  Determining projects to restore...
  All projects are up-to-date for restore.
  HildenCo.Core -> C:\src\nuget-pkg-demo\src\HildenCo.Core\bin\Release\netstandard2.0\HildenCo.Core.dll
  Successfully created package 'C:\src\nuget-pkg-demo\src\HildenCo.Core\bin\Release\HildenCo.Core.0.0.1.nupkg'.
```

> [!Note]
> You may have realized that we have now built our package as release. This is another immediate benefit from decoupling our builds from VS. On rare occasions should we push packages built as Debug.

## Pushing packages to GitHub

### Generating an API Key

In order to authenticate to GitHub Packages the first thing we'll need is an access token.

Open your GitHub account, go to **Settings** > **Developer Settings** > **Personal access tokens**, click **Generate new Token**, give it a name, select **write:packages** and save:

![](../../Img/new-personal-access-token.png)

## Creating a nuget.config file

With the API key created, let's create our `nuget.config` file.

This file should contain the authentication for the package to be pushed to the remote repo.

A base config is listed below with the fields to be replaced in bold:

```cofig
<?xml version="1.0" encoding="utf-8"?>
<configuration>
    <packageSources>
        <clear />
        <add key="github" value="https://nuget.pkg.github.com/**<your-github-username>**/index.json" />
    </packageSources>
    <packageSourceCredentials>
        <github>
            <add key="Username" value="**<your-github-username>**" />
            <add key="ClearTextPassword" value="**<your-api-key>**" />
        </github>
    </packageSourceCredentials>
</configuration>
```

## Pushing a package to GitHub

W/ the correct configuration in place, we can push our package to GitHub w/:

```
dotnet nuget push ./bin/Release/HildenCo.Core.0.0.1.nupkg --source "github"
```

This is what happened when I pushed mine:

```
dotnet nuget push ./bin/Release/HildenCo.Core.0.0.1.nupkg --source "github"
Pushing HildenCo.Core.0.0.1.nupkg to 'https://nuget.pkg.github.com/hd9'...
  PUT https://nuget.pkg.github.com/hd9/
  OK https://nuget.pkg.github.com/hd9/ 1927ms
Your package was pushed.
```

> [!Note]
> Didn't work? Check if you added RepositoryUrl to your project's metadata as nuget uses it  need it to push to GitHub.
