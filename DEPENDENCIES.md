# LOQ Toolkit - NuGet Dependency Tracking

> **Last updated:** 2026-04-22 (post-session update)  
> Run `dotnet list package --outdated` to refresh this list.

---

## Legend

| Symbol | Meaning |
|--------|---------|
| 🔴 | Major version bump — may contain breaking changes |
| 🟡 | Minor version bump — review release notes before updating |
| 🟢 | Patch version — safe to update |
| ⚪ | Already on latest version |
| ❓ | Unknown / custom package |

---

## LOQToolkit.WPF

| Package | Current | Latest | Status | Notes |
|---------|---------|--------|--------|-------|
| Autofac | 8.2.0 | 9.1.0 | 🔴 | Major bump; review breaking changes |
| Humanizer | 2.14.1 | 3.0.10 | 🔴 | Major bump; .NET 8+ target |
| Markdig | 1.1.3 | 1.1.3 | ⚪ | Updated 2026-04-22; clean upgrade |
| Markdig.Wpf | 0.5.0.1 | 0.5.0.1 | ⚪ | Dormant; no newer releases |
| Microsoft.CSharp | ~~4.7.0~~ | ~~4.7.0~~ | ~~⚪~~ | ~~Removed — not needed in .NET 8+ SDK~~ |
| ModernWpfUI | 0.9.6 | 0.9.6 | ⚪ | Dormant; Kinnara/ModernWpf not actively maintained |
| PixiEditor.ColorPicker | 3.4.2 | 3.4.2.3 | 🟢 | Patch update available |
| WPF-UI | 2.1.0 | 4.2.0 | 🔴 | Major bump; requires XAML refactoring (see ROADMAP v1.2.0) |

---

## LOQToolkit.Lib

| Package | Current | Latest | Status | Notes |
|---------|---------|--------|--------|-------|
| Autofac | 8.2.0 | 9.1.0 | 🔴 | Major bump; review breaking changes |
| Ben.Demystifier | 0.4.1 | 0.4.1 | ⚪ | Dormant; no newer releases |
| CoordinateSharp | 3.1.1.1 | 3.4.1.1 | 🟡 | Minor bump; safe to update |
| ManagedNativeWifi | 2.7.0 | 3.0.2 | 🔴 | Major bump; API may have changed |
| Microsoft.CSharp | ~~4.7.0~~ | ~~4.7.0~~ | ~~⚪~~ | ~~Removed — not needed in .NET 8+ SDK~~ |
| Microsoft.Win32.SystemEvents | 9.0.2 | 10.0.7 | 🔴 | .NET 10 runtime dependency |
| Microsoft.Windows.CsWin32 | 0.3.183 | 0.3.275 | 🟢 | Source generator patch update |
| NAudio.Wasapi | 2.2.1 | 2.3.0 | 🟡 | Minor bump; review changelog |
| NeoSmart.AsyncLock | 3.2.1 | 3.2.1 | ⚪ | Dormant; no newer releases |
| Newtonsoft.Json | 13.0.3 | 13.0.4 | 🟢 | Patch security/bugfix update |
| Octokit | 14.0.0 | 14.0.0 | ⚪ | Already latest |
| PubSub | ~~4.0.2~~ | ~~4.0.2~~ | ~~⚪~~ | ~~Removed — replaced with custom `ConcurrentDictionary` implementation in `MessagingCenter.cs`~~ |
| System.Management | 9.0.2 | 10.0.7 | 🔴 | .NET 10 runtime dependency |
| System.ServiceProcess.ServiceController | 9.0.2 | 10.0.7 | 🔴 | .NET 10 runtime dependency |
| TaskScheduler | 2.12.1 | 2.12.2 | 🟢 | Patch update available |
| Varun.NvAPIWrapper.Net | 9.0.2 | ❓ | ❓ | Custom fork; manage manually |
| WindowsDisplayAPI | 1.3.0.13 | 1.3.0.13 | ⚪ | Dormant; no newer releases |

---

## LOQToolkit.Lib.Automation

| Package | Current | Latest | Status | Notes |
|---------|---------|--------|--------|-------|
| Autofac | 8.2.0 | 9.1.0 | 🔴 | Same as LOQToolkit.Lib |
| Microsoft.CSharp | ~~4.7.0~~ | ~~4.7.0~~ | ~~⚪~~ | ~~Removed — not needed in .NET 8+ SDK~~ |
| NeoSmart.AsyncLock | 3.2.1 | 3.2.1 | ⚪ | Same as LOQToolkit.Lib |
| Newtonsoft.Json | 13.0.3 | 13.0.4 | 🟢 | Same as LOQToolkit.Lib |

---

## LOQToolkit.CLI

| Package | Current | Latest | Status | Notes |
|---------|---------|--------|-------|-------|
| System.CommandLine | 2.0.0-beta4.22272.1 | 2.0.7 | 🔴 | Finally left beta! Major upgrade |

---

## LOQToolkit.CLI.Lib

| Package | Current | Latest | Status | Notes |
|---------|---------|--------|--------|-------|
| Newtonsoft.Json | 13.0.3 | 13.0.4 | 🟢 | Same as LOQToolkit.Lib |

---

## LOQToolkit.SpectrumTester

| Package | Current | Latest | Status | Notes |
|---------|---------|--------|--------|-------|
| *(none)* | — | — | — | Only references LOQToolkit.Lib |

---

## LOQToolkit.Lib.Macro

| Package | Current | Latest | Status | Notes |
|---------|---------|--------|--------|-------|
| *(none)* | — | — | — | Only references LOQToolkit.Lib |

---

## Recommended Update Order

1. **Safe / Low-risk** (patch/unchanged)
   - `Newtonsoft.Json` → 13.0.4
   - `PixiEditor.ColorPicker` → 3.4.2.3
   - `Microsoft.Windows.CsWin32` → 0.3.275
   - `TaskScheduler` → 2.12.2

2. **Minor bumps** (review changelogs)
   - `CoordinateSharp` → 3.4.1.1
   - `NAudio.Wasapi` → 2.3.0

3. **Blocked / Requires work**
   - `WPF-UI` → 4.2.0 (requires XAML refactoring across ~50 files)
   - `Autofac` → 9.1.0 (check registration API changes)
   - `Humanizer` → 3.0.10 (verify .NET 8 compatibility)
   - `Markdig` → 1.1.3 (may affect markdown rendering)
   - `ManagedNativeWifi` → 3.0.2 (API review needed)
   - `System.CommandLine` → 2.0.7 (finally stable, but CLI code may need tweaks)

4. **Deferred**
   - `System.Management`, `System.ServiceProcess.ServiceController`, `Microsoft.Win32.SystemEvents` — tied to .NET runtime upgrade (see ROADMAP v2.0.0)

---

## How to Update

```powershell
# Check all outdated packages
dotnet list LOQToolkit.sln package --outdated

# Update a single package
dotnet add LOQToolkit.Lib\LOQToolkit.Lib.csproj package Newtonsoft.Json --version 13.0.4

# Update all packages in a project (use with caution)
dotnet add LOQToolkit.Lib\LOQToolkit.Lib.csproj package Autofac --version 9.1.0
```

---

> **Note:** Always verify the build after updates with `dotnet build LOQToolkit.sln`.
