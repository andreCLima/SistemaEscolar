using MudBlazor;
using SistemaEscolarWeb.Components.Shared;

namespace SistemaEscolarWeb.Models;

public class Util
{
    private readonly IDialogService DialogService;
    private readonly ISnackbar Snackbar;

    public Util(IDialogService dialogService, ISnackbar snackbar)
    {
        DialogService = dialogService;
        Snackbar = snackbar;
    }

    public async Task<bool> DialogConfirma(String msg, String buttonText, Color color, String titulo)
    {
        var parameters = new DialogParameters<Dialog>
        {
            { x => x.ContentText, msg },
            { x => x.ButtonText, buttonText },
            { x => x.Color, color }
        };

        var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

        var dialog = await DialogService.ShowAsync<Dialog>(titulo, parameters, options);
        var result = await dialog.Result;
        if(result != null && !result.Canceled)
        {
            return true;
        }
        Snackbar.Add(message: "Operação cancelada!", Severity.Warning);
        return false;
    }
}