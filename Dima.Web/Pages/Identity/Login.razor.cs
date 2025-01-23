using Dima.Core.Handlers;
using Dima.Core.Requests.Account;
using Dima.Core.Validators;
using Dima.Web.Auth;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Dima.Web.Pages.Identity;

public partial class Login : ComponentBase
{
    #region Dependencies
    
    [Inject]
    private ICookieAuthenticationStateProvider AuthStateProvider { get; set; } = null!;
    
    [Inject]
    private IAccountHandler AccountHandler { get; set; } = null!;
    
    [Inject]
    private ISnackbar SnackBar { get; set; } = null!;
    
    [Inject]
    private NavigationManager NavigationManager { get; set; } = null!;
    
    #endregion
    
    #region Properties
    
    private MudForm _form = null!;
    private readonly LoginRequest _model = new();
    private readonly LoginRequestValidator _validator = new();

    private string _passwordIcon = Icons.Material.Filled.VisibilityOff;
    private InputType _passwordType = InputType.Password;

    private bool _showPassword;
    private bool ShowPassword
    {
        get => _showPassword;
        set
        {
            _showPassword = value;
            if (_showPassword)
            {
                _passwordIcon = Icons.Material.Filled.Visibility;
                _passwordType = InputType.Text;
            }
            else
            {
                _passwordIcon = Icons.Material.Filled.VisibilityOff;
                _passwordType = InputType.Password;
            }
        }
    }
    
    #endregion

    protected override async Task OnInitializedAsync()
    {
        var authState = await AuthStateProvider.GetAuthenticationStateAsync();
        if (authState is { User.Identity.IsAuthenticated: true })
            NavigationManager.NavigateTo("/");
    }

    private void OnPasswordVisibilityClick()
    {
        ShowPassword = !ShowPassword;
    }

    private async Task Submit()
    {
        await _form.Validate();
        if (!_form.IsValid) return;
        var response = await AccountHandler.Login(_model);
        if (response.IsSuccess)
        {
            NavigationManager.NavigateTo("/login");
        }
        else
        {
            SnackBar.Add(response.Message, Severity.Error);
        }
    }
}