---
name: calendar-vsa
description: Lay out CalendarApp as Blazor Server vertical slices. Feature folders live under CalendarApp/Features, the host shell stays in Components, and host-agnostic shared UI lives in the RCL project. Use when adding, moving, or reviewing a CalendarApp page, component, service, or feature folder, and when the user runs /calendar-vsa or asks for vertical slice architecture in CalendarApp.
---

# CalendarApp vertical slices

CalendarApp is a Blazor Server host. The slice style comes from the LivingMessiah PWA feature folders and its Razor Class Library. The rationale and the current ownership map are in `docs/calendar-vsa.md`. Read that map before adding or moving a feature, and update it in the same change.

Before creating a feature, read [references/slice-template.md](references/slice-template.md) and follow it.

## Placement

- A route and the components, models, and helpers that only that route uses go in `CalendarApp/Features/<Feature>/`. The namespace matches the folder.
- Keep the existing URL when you move a page. Put `@page` on `CalendarApp/Features/<Feature>/Index.razor`. Do not put `@page` in the RCL.
- The host shell stays in `CalendarApp/Components`: `App.razor`, `Routes.razor`, `Layout/`, `PageHeader.razor`, and the error pages `Pages/Error.razor` and `Pages/NotFound.razor`.
- A component used by more than one feature that does not use CalendarApp types goes in `RCL/Components`. The RCL must not reference CalendarApp. `SelectorDropDown`, `TableTemplate`, and `LoadingComponent` live there.
- A component used by more than one feature that needs CalendarApp types stays in `CalendarApp/Components`. `PageHeader` takes `Nav`. `DatePickerFilterModal` uses `YearHelper`.
- Enums, constants, models, and services used by more than one feature stay in `CalendarApp/Enums`, `Constants`, `Models`, and `Services`.
- Imports live only in `CalendarApp/_Imports.razor` at the project root. That file imports `RCL.Components`. A `_Imports.razor` under `Components/` does not apply to `Features/`.

## Services

When a feature owns a service, add `Add<Feature>()` in `<Feature>/Data/ServiceCollectionExtensions.cs` and call it from `Program.cs`. Do not add an extension that registers nothing. `ConstantCalendar` stays in `Services` because Home and MonthDaysDiff both use it.

## Leave the PWA host behind

Do not add a service worker, PWA updater, Serilog browser logging, Aspire service discovery, or an HTTP API client. Do not change CalendarApp to WebAssembly.

## Check

After a UI move, build `CalendarApp/CalendarApp.csproj` and verify the affected routes in the browser. The home grid, year filter, event filter, month view, `/about`, `/constellations`, and `/MonthDaysDiff` are the routes this layout serves.
