@echo off

IF "%1"=="" (
	SET VERSION=1.0.0
) ELSE (
	SET VERSION=%1
)
	

SET PATH=%PATH%;"C:\Program Files (x86)\Inno Setup 6"

dotnet publish LOQToolkit.WPF -c release -o build /p:DebugType=None /p:FileVersion=%VERSION% /p:Version=%VERSION% || exit /b
dotnet publish LOQToolkit.SpectrumTester -c release -o build /p:DebugType=None /p:FileVersion=%VERSION% /p:Version=%VERSION% || exit /b
dotnet publish LOQToolkit.CLI -c release -o build /p:DebugType=None /p:FileVersion=%VERSION% /p:Version=%VERSION% || exit /b

iscc make_installer.iss /DMyAppVersion=%VERSION% || exit /b
