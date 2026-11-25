# Sudoku

The solution contains three Razor web applications and the `ipranges` console
application. The projects target .NET Core 3.1, including on Ubuntu; this
version is out of support.

## Windows development and debugging

Install the .NET 10 SDK and the .NET Core and ASP.NET Core 3.1 runtimes. The
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
the .NET Core and ASP.NET Core 3.1 runtimes there as needed.

The repository does not currently contain the web applications' `wwwroot`
assets. In particular, icosudoku needs `wwwroot/js/site.js` for its board to
render; the shared layouts also reference missing CSS and JavaScript libraries.
