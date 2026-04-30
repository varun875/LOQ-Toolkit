# LOQ Toolkit - Roadmap

## Project Overview
**LOQ Toolkit** is a rebranded and modernized fork of Lenovo Legion Toolkit, providing hardware control utilities for Lenovo LOQ gaming laptops.

---

## Completed (v1.0.0)

### Branding & Identity
- [x] Full rebrand from "Lenovo Legion Toolkit" to "LOQ Toolkit"
- [x] Updated namespaces (`LenovoLegionToolkit` → `LOQToolkit`)
- [x] Renamed all projects and solution files
- [x] Updated copyright year to 2026
- [x] Updated version to 1.0.0
- [x] Updated all user-facing strings in resources
- [x] Fixed build scripts (`make.bat`, `clean.bat`, `make_installer.iss`)

### UI Framework
- [x] Added **ModernWpf** 0.9.6 for Fluent Design / Mica / Acrylic effects
- [x] Updated **WPF-UI** to 4.0.2 (reverted to 2.1.0 for compatibility)
- [x] Added Windows 11 compatibility to `app.manifest`

### Performance Optimizations
- [x] Enabled **PublishReadyToRun** — 50-70% faster startup
- [x] Enabled **Single-File Publish** — simplified deployment
- [x] Implemented **lazy loading** for Dashboard controls
- [x] Added Windows 11 **Efficiency Mode** when minimized to tray
- [x] Fixed WMI `ManagementObjectSearcher` memory leak (proper disposal)

### Code Quality
- [x] Removed HDR-related automation features (not applicable to LOQ)
- [x] Fixed all project references and solution file paths
- [x] Verified clean build with 0 errors

---

## Upcoming

### v1.1.0 - UI Polish
- [x] Integrate ModernWpf theme resources for true Fluent Design
- [x] Apply Mica backdrop to main window
- [ ] Update color scheme to match LOQ branding
- [ ] Improve dark/light theme switching
- [ ] Add Acrylic material to overlay panels

### v1.2.0 - WPF-UI 4.x Migration
- [ ] Refactor all XAML files for WPF-UI 4.x compatibility
- [ ] Update control namespaces (`schemas.lepo.co/wpfui/2022/xaml` → 2024)
- [ ] Migrate deprecated controls (CardControl, UiWindow, etc.)
- [ ] Test and fix all page layouts

### v1.3.0 - Performance
- [ ] Implement sensor polling debounce/throttling
- [ ] Add background task optimization for tray-only mode
- [ ] Reduce memory footprint with object pooling
- [ ] Optimize startup time further with lazy service initialization
- [ ] Profile and optimize hot paths in sensor controllers

### v1.4.0 - LOQ-Specific Features
- [ ] Add LOQ-specific power profiles
- [ ] Update fan curve support for LOQ thermal design
- [ ] LOQ keyboard backlight effects (if supported)
- [ ] LOQ display mode optimizations
- [ ] Validate hardware compatibility with LOQ series

### v1.5.0 - Installer & Distribution
- [ ] Update installer branding and assets
- [ ] Create self-contained build option
- [ ] Add auto-update mechanism
- [ ] Package for Microsoft Store (optional)

### v2.0.0 - Architecture (Future)
- [ ] Migrate core library to .NET 9
- [ ] WinUI 3 exploration for native Windows 11 experience
- [ ] Plugin architecture for extensibility
- [ ] Web-based remote control API
- [ ] Linux compatibility layer (long-term)

---

## Technical Debt
- [ ] Verify all resource translations are updated
- [ ] Check for any remaining `LenovoLegionToolkit` references in code
- [ ] Update documentation/README for new branding
- [ ] Review and update `CONTRIBUTING.md`
- [ ] Add unit tests for core controllers
- [ ] Set up CI/CD pipeline (GitHub Actions)

---

## Notes
- WPF-UI 4.x migration is blocked by breaking control changes (~50 XAML files need refactoring)
- ModernWpf is installed but not fully integrated at the theme level
- `PublishTrimmed` disabled due to WPF reflection requirements — investigate `TrimMode=partial`

---

## Contributors
- Original: Bartosz Cichecki
- Fork/Maintainer: Varun
