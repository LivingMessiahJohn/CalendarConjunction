
Explain and give an example of `@bind` [Grok](https://grok.com/share/bGVnYWN5LWNvcHk_3e98d7d7-a58d-4806-9358-2aedefd8845c)

### Key Points

- `@bind-Value` on the parent expands to `Value="..." ValueChanged="..."`.
- The child must expose both a `Value` parameter and a `ValueChanged` `EventCallback<DateOnly?>`.
- When the user picks a date, the child calls `ValueChanged.InvokeAsync(...)` so the parent’s property updates.
- The `inputId` uses a GUID so multiple instances on the same page don’t collide.

This pattern works for any type (`DateTime?`, `string`, custom models, etc.)—just change the type of `Value` / `ValueChanged`.

### DateSelector.razor

### Parent Component Usage

```razor
<DateSelector @bind-Value="selectedDate"
              Label="Event Date"
              Min="DateOnly.FromDateTime(DateTime.Today)" />

<p>Selected: @selectedDate?.ToString("MMMM d, yyyy")</p>

@code {
    private DateOnly? selectedDate = DateOnly.FromDateTime(DateTime.Today);
}
```
