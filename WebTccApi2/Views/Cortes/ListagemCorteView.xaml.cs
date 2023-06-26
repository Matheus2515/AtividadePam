using WebTccApi2.ViewModels.Cortes;

namespace WebTccApi2.Views.Cortes;

public partial class ListagemCorteView : ContentPage
{
	ListagemCorteViewModel viewModel;
	public ListagemCorteView()
	{
		InitializeComponent();
		viewModel = new ListagemCorteViewModel();
		BindingContext = viewModel;
		Title = "Cortes - App Cortes Maserati";
	}
    protected override void OnAppearing()
    {
        base.OnAppearing();
		_ = viewModel.ObterCortes();
    }
}