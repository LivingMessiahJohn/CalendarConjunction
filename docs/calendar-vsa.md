# CalendarApp vertical slices

CalendarApp is a Blazor Server app organized by feature. The pattern comes from the LivingMessiah PWA at `LivingMessiah/PWA` on `main`, adapted for this host. The agent checklist is the `calendar-vsa` skill (`.grok/skills/calendar-vsa/SKILL.md`). This file is the map and the record of how the layout was chosen.

## What the PWA contributed

The PWA groups each route with the components that exist for that route:

```
PWA/Features/<Feature>/
```

`FeastDayPlanner` is the example that also has data access. Its `Data/ServiceCollectionExtensions.cs` exposes `AddFeastDayPlanner()`, and `PWA/Program.cs` calls that method. Pages stay in the feature. Shared chrome stays in `Layout/` and `Components/`.

Components that more than one app renders, and that do not use CalendarApp types, live in the `RCL` Razor Class Library (`Microsoft.NET.Sdk.Razor`). The PWA imports them with `@using RCL.Components`. `TableTemplate`, `SelectorDropDown`, and `LoadingComponent` are the library components. `PageHeader` renders the title and links for Home, About, Constellations, and Month Days Diff. It takes `Nav`, so it stays in `CalendarApp/Components`.

The PWA project itself is Blazor WebAssembly. CalendarApp was already Blazor Server (`Microsoft.NET.Sdk.Web`, interactive server components). This migration kept that host. These PWA pieces were not brought over:

- Service worker and PWA updater
- Serilog browser console
- Aspire service discovery and the Azure Functions HTTP client
- The LivingMessiah `RCL` packages for blob storage

## Resulting layout

`CalendarConjunction.sln` contains `CalendarApp` and `RCL`. The library does not reference the app.

```
RCL/Components/
  LoadingComponent.razor
  SelectorDropDown.razor
  TableTemplate.razor

CalendarApp/
  _Imports.razor                 imports, including RCL.Components
  Program.cs
  Components/                    host shell and app-shared widgets
    App.razor
    Routes.razor
    DatePickerFilterModal.razor
    PageHeader.razor
    Layout/
    Pages/Error.razor
    Pages/NotFound.razor
  Features/
    Home/Index.razor             /
    About/Index.razor            /about
    Constellations/Index.razor   /constellations
    MonthDaysDiff/Index.razor    /MonthDaysDiff
  Enums/  Constants/  Models/  Services/
```

`_Imports.razor` sits next to `CalendarApp.csproj`. A copy under `Components/` would not apply to `Features/`, so there is one file.

## Where each former page went

| Was | Now | Why |
| --- | --- | --- |
| `Components/Pages/Home.razor` | `Features/Home/Index.razor` | Route `/` |
| `Components/FilterCard.razor`, `EventFilterModal.razor`, `Pages/PlannerTable.razor`, `Pages/MonthCalendar.razor` | `Features/Home/` | Only Home renders them. Month view is a Home toggle, not its own route. |
| `Components/Pages/About.razor` | `Features/About/Index.razor` | Route `/about` |
| `Components/Pages/Constellations.razor` and its tables and photo album | `Features/Constellations/` | Route `/constellations`. The page file is `Index.razor`. |
| `Models/ConstellationDTO.cs`, `DecanDTO.cs`, `BibleNamedConstellationsDTO.cs`, `Helpers/KeyVerseHelper.cs` | `Features/Constellations/` | Only that feature uses them. Namespace is `CalendarApp.Features.Constellations`. |
| `Components/Pages/MonthDaysDiff.razor` | `Features/MonthDaysDiff/Index.razor` | Route `/MonthDaysDiff` |
| `Components/Pages/PageHeader.razor` | `Components/PageHeader.razor` | Home, About, Constellations, and Month Days Diff render it. It takes `Nav`, a CalendarApp type. |
| `Components/TableTemplate.razor`, `Components/SelectorDropDown.razor` | `RCL/Components/` | Used across features, no CalendarApp types. `LoadingComponent.razor` is there too. |
| (new) | `Enums/Nav.cs` | SmartEnum of every `@page` route. `PageHeader` reads its title and icon. |
| `Components/DatePickerFilterModal.razor` | unchanged | Home and Month Days Diff both open it, and it uses `YearHelper` |
| `Components/Pages/Error.razor`, `NotFound.razor`, `Layout/`, `App.razor`, `Routes.razor` | unchanged | Host shell. `Routes.razor` still binds `Pages.NotFound`. |
| `Enums/`, `Constants/`, `Models/CalendarGridRow.cs`, `Models/MonthSpan.cs`, `Services/ConstantCalendar.cs` | unchanged | Home and Month Days Diff both depend on this calendar data |

Route URLs were left as they were.

## Adding another feature

Run `/calendar-vsa`, or ask to add a page. The skill tells the agent to put the route in `CalendarApp/Features/<Feature>/`, to put a shared host-agnostic component in `RCL/Components`, and to register a feature service with an `Add*` extension called from `Program.cs`. No feature has a service yet, so `Program.cs` has no `Add*` call. Update the tree in this file when a feature or a shared component is added.
