# Add a CalendarApp feature

Follow the placement rules in `SKILL.md`. This file is the file skeleton.

## Files

For a feature named `Example` at `/example`:

```
CalendarApp/Features/Example/Index.razor
```

Add an `Example` member to `CalendarApp/Enums/Nav.cs` with that route. Put child components and types that only Example uses in that same folder. Namespace for C# types: `CalendarApp.Features.Example`.

```razor
@page "/example"

<PageHeader PageEnum="Nav.Example" LinkHref="/" LinkText="Home" />
```

`PageHeader` is `CalendarApp/Components/PageHeader.razor`. It reads the title and icon from `Nav`. `CalendarApp/_Imports.razor` already imports `CalendarApp.Enums` and `RCL.Components`. Do not add a second `_Imports.razor` under the feature.

## When the feature needs a service

Add the extension next to the feature and call it from `Program.cs`. Skip this when the feature has no service.

`CalendarApp/Features/Example/Data/ServiceCollectionExtensions.cs`:

```csharp
using Microsoft.Extensions.DependencyInjection;

namespace CalendarApp.Features.Example.Data;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddExample(this IServiceCollection services)
	{
		services.AddSingleton<ExampleService>();
		return services;
	}
}
```

```csharp
builder.Services.AddExample();
```

## Shared UI

A new component with no CalendarApp types, used by a second feature, goes in `RCL/Components/<Name>.razor`. Leave it without `@page`.

A new component that needs CalendarApp types, used by a second feature, goes in `CalendarApp/Components/<Name>.razor`.

## After the edit

Update the ownership map in `docs/calendar-vsa.md`. Build `CalendarApp/CalendarApp.csproj`. Open the new route and any route that renders a component you moved.
