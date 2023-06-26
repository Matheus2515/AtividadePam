using WebTccApi2.Views.Cortes;

namespace WebTccApi2;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute("cadCorteView", typeof(CadastroCorteView));
	}
}
