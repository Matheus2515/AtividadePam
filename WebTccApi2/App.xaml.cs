using WebTccApi2.ViewModels.Cortes;
using WebTccApi2.Views.Cortes;

namespace WebTccApi2;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new AppShell();


	}
}
