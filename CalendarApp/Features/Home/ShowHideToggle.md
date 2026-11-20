
```html
<span class="my-1">
	<button @onclick="ButtonClicked"
					class="@ButtonCSS" type="button" title="@ButtonTitle">
		@Title <i class='@Icon'></i>
	</button>
</span>
@code 
```

```csharp
{
	[Parameter, EditorRequired] public bool ContentIsShowing { get; set; }
	[Parameter] public EventCallback OnToggleSelected { get; set; }

	private string Title = ""; // => ContentIsShowing ? "Hide " : "Details";
	private string ButtonTitle => ContentIsShowing ? "Hide " : "Details";
	private string Icon => ContentIsShowing ? "fas fa-chevron-up" : "fas fa-chevron-down";
	private string ButtonCSS = "btn btn-sm"; // btn-sm float-end btn-primary  py-1

	private void ButtonClicked()
	{
		OnToggleSelected.InvokeAsync();
	}
}
```

## Usage
```html
	<ShowHideToggle ContentIsShowing="ShowingDetails" OnToggleSelected="ReturnedToggle" />
```

```csharp

	bool ShowingDetails;
	private async Task ReturnedToggle()
	{
		ShowingDetails = !ShowingDetails;
	}

```
