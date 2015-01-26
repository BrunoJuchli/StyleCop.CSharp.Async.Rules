echo %cd%
"%ChocolateyInstall%\ChocolateyInstall\nuget.exe" pack "StyleCop.CSharp.Async.Rules.nuspec" -Version "0.0.1" -properties "Configuration=Debug%

PAUSE