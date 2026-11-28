# Sudoku

The solution contains three Razor web applications targeting .NET 10 and the
`ipranges` console application targeting .NET Core 3.1, which is out of support.

## Windows development and debugging

Install the .NET 10 SDK and ASP.NET Core 10 runtime. Debugging `ipranges` also
requires the .NET Core 3.1 runtime. The
VS Code build and debug configurations use the per-user installation at
`%USERPROFILE%\.dotnet`. Install the recommended C# extension in VS Code.
If the C# language server cannot find the SDK, set `DOTNET_ROOT` to that
directory and restart VS Code. A system-wide `dotnet` can take precedence over
the user PATH; use `%USERPROFILE%\.dotnet\dotnet.exe` explicitly in that case.

Open the repository folder in VS Code, choose a **Debug** configuration in
Run and Debug, set breakpoints and press F5. The solution is built before
launching. The web configurations listen on HTTP port 5000 and retain the
production path bases:

- icosudoku: http://localhost:5000/icosudokuwebapp
- sudoku: http://localhost:5000/sudokuwebapp
- 100: http://localhost:5000/100webapp

Run one web configuration at a time, or change its port. The `ipranges`
configuration uses the project directory so that it can read `config.json`.
On Ubuntu the build task uses `dotnet` from PATH; install a compatible SDK and
the runtimes needed for the projects you run there.

The web applications' `wwwroot` assets are included for local development.

## Preparing a production release

On Windows, install PuTTY in `C:\Program Files\PuTTY` and load the SSH key in
Pageant if it requires a passphrase. In VS Code, select **Terminal > Run Task
> Publish web apps to server** and provide a new version, server, SSH user,
local `.ppk` file and independently verified SSH host-key fingerprint.

The task publishes each web project with `Release`, targeting Linux x64 and
the server's installed .NET 10 runtime. It copies the results to separate
`100`, `sudoku` and `icosudoku` directories under `~/publish/sudoku/<version>`.
It also copies the repository's `index.html` to that version directory.
It refuses to overwrite an existing version and does not change or restart the
running applications. If a copy fails, the new version directory may be
incomplete; inspect it before retrying with another version.
