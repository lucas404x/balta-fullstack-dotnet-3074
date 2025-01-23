using Dima.Core.Handlers;
using Dima.Core.Requests.Account;
using Dima.Core.Validators;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Dima.Web.Pages.Identity;

public partial class Register : ComponentBase
{
    #region Dependencies
    
    [Inject]
    private IAccountHandler AccountHandler { get; set; } = null!;
    
    [Inject]
    private ISnackbar SnackBar { get; set; } = null!;
    
    [Inject]
    private NavigationManager NavigationManager { get; set; } = null!;
    
    #endregion
    
    #region Properties
    
    private MudForm _form = null!;
    private readonly RegisterRequest _model = new();
    private readonly RegisterRequestValidator _validator = new();

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

    private void OnPasswordVisibilityClick()
    {
        ShowPassword = !ShowPassword;
    }

    private async Task Submit()
    {
        await _form.Validate();
        if (!_form.IsValid) return;
        var response = await AccountHandler.Register(_model);
        if (response.IsSuccess)
        {
            SnackBar.Add("Cadastro realizado com sucesso!", Severity.Success);
            // TODO: Redirect user to home page
            NavigationManager.NavigateTo("/");
        }
        else
        {
            SnackBar.Add(response.Message, Severity.Error);
        }
    }
}