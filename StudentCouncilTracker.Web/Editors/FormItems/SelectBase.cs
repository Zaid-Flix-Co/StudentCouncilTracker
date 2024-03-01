using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using StudentCouncilTracker.Application.Entities.Interfaces.Haves;
using StudentCouncilTracker.Web.Services.Catalogs;

namespace StudentCouncilTracker.Web.Editors.FormItems;

public class SelectBase<TValue, TItem, TService> : ComponentBase, IDisposable
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
            CurrentValueAsInt = FormatValueAsInt(value);
            CurrentValueAsGuid = FormatValueAsGuid(value);
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

    private Guid? _currentValueAsGuid;

    private int? _currentValueAsInt;

    private Timer _debounceTimer = null!;

    private TValue _value = default!;

    protected bool IsLoading;

    protected string Search = "";

    protected int? CurrentValueAsInt
    {
        get
        {
            if (_value is IHaveId<int?> niv)
                _currentValueAsInt = niv.Id;
            if (_value is IHaveId iv)
                _currentValueAsInt = iv.Id;
            return _currentValueAsInt;
        }
        set => _currentValueAsInt = value;
    }

    protected Guid? CurrentValueAsGuid
    {
        get
        {
            if (_value is IHaveId<Guid?> ngv)
                _currentValueAsGuid = ngv.Id;
            if (_value is IHaveId<Guid> gv)
                _currentValueAsGuid = gv.Id;
            return _currentValueAsGuid;
        }
        set => _currentValueAsGuid = value;
    }

    protected bool DebounceEnabled => DebounceMilliseconds != 0;

    protected List<TItem> List { get; set; } = new();

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected void OnSelectedItemChanged(TItem values)
    {
        _ = OnValueChangeAsync(values);
    }

    protected async Task OnValueChangeAsync(TItem value)
    {
        await ValueChanged.InvokeAsync((TValue)(object)value!);

        if (OnChange.HasDelegate)
            await OnChange.InvokeAsync();
    }

    protected virtual async Task<List<TItem>> GetDataAsync(string query = "")
    {
        IsLoading = true;
        StateHasChanged();

        var result = (await ((BaseCatalogService)(object)Service).GetListAsync(query, GetParameters())).Value;

        var list = JsonConvert.DeserializeObject<List<TItem>>(result.Items.ToString());

        IsLoading = false;
        return list;
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

        list.RemoveAll(r => !list.Any(a => a?.Equals(((IHaveId<int?>)r).Id) ?? false) &&
                            !list.Any(a => ((IHaveId<int?>)a).Id?.Equals(((IHaveId<int?>)r).Id) ?? false));

        var toAdd = list.Where(r => !list.Any(a => a?.Equals(((IHaveId<int?>)r).Id) ?? false) &&
                                    !List.Any(a => ((IHaveId<int?>)a).Id?.Equals(((IHaveId<int?>)r).Id) ?? false));

        List.AddRange(toAdd);

        StateHasChanged();
    }

    private IEnumerable<TItem> GetFiltered(List<TItem> list)
    {
        var tItemType = typeof(TItem);

        var selectList = FormatValueAsItem(Value).ToList();
        if (selectList.Any())
        {
            if (typeof(IHaveId<int?>).IsAssignableFrom(tItemType))
                selectList.RemoveAll(r => list.Any(a => ((IHaveId<int?>)a!)!.Id == ((IHaveId<int?>)r!)!.Id));
            if (typeof(IHaveId).IsAssignableFrom(tItemType))
                selectList.RemoveAll(r => list.Any(a => ((IHaveId<Guid?>)a!)!.Id == ((IHaveId<Guid?>)r!)!.Id));
            if (typeof(IHaveId<Guid?>).IsAssignableFrom(tItemType))
                selectList.RemoveAll(r =>
                    list.Any(a => (((IHaveId<Guid?>)a!)!).Id == ((IHaveId<Guid?>)r!)!.Id));
            if (typeof(IHaveId<Guid>).IsAssignableFrom(tItemType))
                selectList.RemoveAll(r => list.Any(a => ((IHaveId<Guid>)a!)!.Id == ((IHaveId<Guid>)r!)!.Id));

            list.AddRange(selectList);
        }

        return list;
    }

    protected void OnSearch(string value)
    {
        InvokeAsync(async () => await ChangeSearch(value));
    }

    protected void OnBlur()
    {
        InvokeAsync(async () =>
        {
            await Task.Delay(200);
            Search = "";
        });
    }

    protected void OnFocus()
    {
        InvokeAsync(async () => await ChangeSearch(Search, true));
        StateHasChanged();
    }

    protected override void OnInitialized()
    {
        if (Value != null) List = FormatValueAsItem(Value).ToList();

        base.OnInitialized();
    }

    protected virtual void Dispose(bool disposing)
    {

    }

    private static int? FormatValueAsInt(TValue value)
    {
        if (value == null)
            return null;
        
        var currentValue = default(TItem);
        try
        {
            var d = JsonConvert.DeserializeObject<TItem>(JsonConvert.SerializeObject(value));
            if (d != null)
                currentValue = d;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        int? valuesAsInt = null;
        var nullId = (currentValue as IHaveId<int?>)?.Id;
        if (nullId != null)
            valuesAsInt = nullId;
        if (currentValue is IHaveId id)
            valuesAsInt = id.Id;

        return valuesAsInt;
    }

    private static Guid? FormatValueAsGuid(TValue value)
    {
        if (value == null)
            return null;
        
        var currentValue = default(TItem);
        try
        {
            var d = JsonConvert.DeserializeObject<TItem>(JsonConvert.SerializeObject(value));
            if (d != null)
                currentValue = d;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        Guid? valuesAsGuid = currentValue switch
        {
            IHaveId<Guid?> ngv => ngv.Id ?? Guid.Empty,
            IHaveId<Guid> gv => gv.Id,
            _ => null
        };

        return valuesAsGuid;
    }

    private static IEnumerable<TItem> FormatValueAsItem(TValue value)
    {
        if (value == null)
            return new List<TItem>().AsEnumerable();


        var currentValue = new List<TItem>();
        try
        {
            var d = JsonConvert.DeserializeObject<TItem>(JsonConvert.SerializeObject(value));
            if (d != null)
            {
                if (d is IHaveId<int?> { Id: not null } || d is IHaveId)
                    currentValue.Add(d);
                if (d is IHaveId<Guid?> { Id: not null } || d is IHaveId<Guid>)
                    currentValue.Add(d);
            }
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
}