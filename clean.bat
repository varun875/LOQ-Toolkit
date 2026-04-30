@echo off

rmdir /s /q .vs
rmdir /s /q _ReSharper.Caches

rmdir /s /q build
rmdir /s /q build_installer

rmdir /s /q LOQToolkit.CLI\bin
rmdir /s /q LOQToolkit.CLI\obj

rmdir /s /q LOQToolkit.Lib\bin
rmdir /s /q LOQToolkit.Lib\obj

rmdir /s /q LOQToolkit.Lib.Automation\bin
rmdir /s /q LOQToolkit.Lib.Automation\obj

rmdir /s /q LOQToolkit.Lib.Macro\bin
rmdir /s /q LOQToolkit.Lib.Macro\obj

rmdir /s /q LOQToolkit.WPF\bin
rmdir /s /q LOQToolkit.WPF\obj

rmdir /s /q LOQToolkit.SpectrumTester\bin
rmdir /s /q LOQToolkit.SpectrumTester\obj
