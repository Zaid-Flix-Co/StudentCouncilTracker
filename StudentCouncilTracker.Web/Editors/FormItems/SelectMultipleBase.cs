using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using StudentCouncilTracker.Application.Entities.Interfaces.Haves;
using StudentCouncilTracker.Web.Services.Catalogs;

namespace StudentCouncilTracker.Web.Editors.FormItems;

public class SelectMultipleBase<TValue, TItem, TItemValue, TService> : ComponentBase, IDisposable
{
    #region PARAMETERS

    [Parameter]
    public int DebounceMilliseconds { get; set; } = 600;

    [Parameter]
    public TValue Value
    {
        get => _value;
        set
        {
            _value = value;
            CurrentValue = FormatValue(_value);
        }
    }

    [Parameter]
    public EventCallback<TValue> ValueChanged { get; set; }

    [Parameter]
    public EventCallback OnChange { get; set; }

    [Parameter]
    public string Placeholder { get; set; } = string.Empty;

    [Inject]
    protected TService Service { get; set; } = default!;

    #endregion

    private Timer _debounceTimer = null!;

    private TValue _value = default!;

    protected bool IsLoading;

    protected string Search = string.Empty;

    protected IEnumerable<TItemValue> CurrentValue { get; set; } = null!;

    protected bool DebounceEnabled => DebounceMilliseconds != 0;

    protected List<TItem> List { get; set; } = new();

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected void OnSelectedItemsChanged(IEnumerable<TItem> values)
    {
        if (Value is List<TItem> { Count: > 0 } || (values?.Any() ?? false))
            _ = OnValuesChangeAsync(values);
    }

    protected async Task OnValuesChangeAsync(IEnumerable<TItem> values)
    {
        var list = new List<TItem>();
        if (values != null)
            foreach (var item in values)
                list.Add(item);

        await ValueChanged.InvokeAsync((TValue)(object)list);

        if (OnChange.HasDelegate)
            await OnChange.InvokeAsync();
    }

    public IEnumerable<TItemValue> FormatValue(TValue values)
    {
        if (values == null)
            return new List<TItemValue>().AsEnumerable();

        var currentValue = new List<TItem>();
        try
        {
            var d = JsonConvert.DeserializeObject<List<TItem>>(JsonConvert.SerializeObject(values));
            if (d != null)
                currentValue.AddRange(d);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        var itemValues = new List<TItemValue>();
        foreach (var item in currentValue)
        {
            if (item is IHaveId<TItemValue> tmpValue)
                itemValues.Add(tmpValue.Id);
        }

        return itemValues.AsEnumerable();
    }

    public IEnumerable<TItem> FormatValueAsItem(TValue values)
    {
        if (values == null)
            return new List<TItem>().AsEnumerable();

        var currentValue = new List<TItem>();
        try
        {
            var d = JsonConvert.DeserializeObject<List<TItem>>(JsonConvert.SerializeObject(values));
            if (d != null)
                currentValue.AddRange(d);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return currentValue.AsEnumerable();
    }

    protected virtual Dictionary<string, string> GetParameters()
    {
        return new Dictionary<string, string>();
    }

    protected virtual async Task<List<TItem>> GetDataAsync(string query = "")
    {
        IsLoading = true;
        StateHasChanged();

        var result = (await ((BaseCatalogService)(object)Service!)?.GetListAsync(query, GetParameters())!).Value;
        var list = JsonConvert.DeserializeObject<List<TItem>>(result.Items.ToString()!);

        IsLoading = false;
        return list ?? new List<TItem>();
    }

    protected void DebounceChangeSearch(string value)
    {
        _debounceTimer?.Dispose();
        _debounceTimer = new Timer(DebounceTimerIntervalOnTick!, value, DebounceMilliseconds, DebounceMilliseconds);
    }

    protected void DebounceTimerIntervalOnTick(object state)
    {
        InvokeAsync(async () => await ChangeSearch((string)state, true));
    }

    private async Task ChangeSearch(string value, bool ignoreDebounce = false)
    {
        if (DebounceEnabled)
        {
            if (!ignoreDebounce)
            {
                DebounceChangeSearch(value);
                return;
            }

            _debounceTimer?.Dispose();
            if (_debounceTimer != null)
                _debounceTimer = null;
        }

        Search = value;

        var list = await GetDataAsync(value);
            
        List.RemoveAll(r => !CurrentValue.Any(a => a?.Equals(((IHaveId<TItemValue>)r).Id) ?? false) &&
                            !list.Any(a => ((IHaveId<TItemValue>)a).Id?.Equals(((IHaveId<TItemValue>)r).Id) ?? false));

        var toAdd = list.Where(r => !CurrentValue.Any(a => a?.Equals(((IHaveId<TItemValue>)r).Id) ?? false) &&
                                    !List.Any(a => ((IHaveId<TItemValue>)a).Id?.Equals(((IHaveId<TItemValue>)r).Id) ?? false));
            
        List.AddRange(toAdd);

        StateHasChanged();
    }

    protected void OnSearch(string value)
    {
        InvokeAsync(async () => await ChangeSearch(value));
    }

    protected void OnBlur()
    {
        Search = string.Empty;
    }

    protected void OnFocus()
    {
        InvokeAsync(async () => { await ChangeSearch(Search); });
    }

    protected void OnChangeParameters()
    {
        List.Clear();

        InvokeAsync(async () => await ChangeSearch(Search));
        StateHasChanged();
    }
    protected override void OnInitialized()
    {
        if (Value != null)
        {
            List = FormatValueAsItem(Value).ToList();
            CurrentValue = FormatValue(Value).ToList();
        }

        base.OnInitialized();
    }

    protected override Task OnInitializedAsync()
    {
        return Task.CompletedTask;
    }

    protected virtual void Dispose(bool disposing)
    {

    }
}