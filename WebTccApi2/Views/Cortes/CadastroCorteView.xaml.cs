using WebTccApi2.ViewModels.Cortes;

namespace WebTccApi2.Views.Cortes;

public partial class CadastroCorteView : ContentPage
{

	private CadastroCorteViewModel cadViewModel;
	public CadastroCorteView()
	{
		InitializeComponent();
		cadViewModel = new CadastroCorteViewModel();
		BindingContext = cadViewModel;
		Title = "Novo Corte";
	}
}