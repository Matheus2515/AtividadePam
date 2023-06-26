using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebTccApi2.Services.Cortes;
using WebTccApi2.Models;
using System.Windows.Input;
using System.Linq.Expressions;

namespace WebTccApi2.ViewModels.Cortes
{
    public class ListagemCorteViewModel : BaseViewModel
    {
        private CorteService pService;
        private Corte corteSelecionado;
        public ObservableCollection<Corte> Cortes { get; set; }
        public ICommand NovoCorte { get; }
        public ICommand RemoverCorteCommand { get; }
        public Corte CorteSelecionado 
        { get { return corteSelecionado; }
            set
            {
                if(value != null)
                {
                    corteSelecionado = value;
                    Shell.Current.GoToAsync($"cadCorteView?cId={corteSelecionado.Id}");
                }
            } 
        }

        public ListagemCorteViewModel()
        {
            string token = Preferences.Get("UsuárioToken ", string.Empty);
            pService = new CorteService(token);
            Cortes = new ObservableCollection<Corte>();

            _ = ObterCortes();

            NovoCorte = new Command(async () => { await ExibirCadastroCorte(); });
            RemoverCorteCommand = new Command<Corte>(async (Corte c) => { await RemoverCorte(c); });
        }
        

        public async Task ObterCortes()
        {
            try
            {
                Cortes = await pService.GetCortesAsync();
                OnPropertyChanged(nameof(Cortes));
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                    .DisplayAlert("Ops4", ex.Message + "Detalhes: ", ex.InnerException + "Ok");
            }
        }
        public async Task RemoverCorte(Corte c)
        {
            try
            {
                if (await Application.Current.MainPage
                    .DisplayAlert("Confirmação", $"Confirma a remoção de {c.Estilo}, {c.Sex} ?", "Sim", "Não"))
                {
                    await pService.DeleteCorteAsync(c.Id);

                    await Application.Current.MainPage.DisplayAlert("Mensagem", "Corte removido com sucesso!", "Ok");

                    _ = ObterCortes();
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                    .DisplayAlert("Ops5", ex.Message + "Detalhes: " + ex.InnerException, "Ok");
            }
        }

        public async Task ExibirCadastroCorte()
        {
            try
            {
                await Shell.Current.GoToAsync("cadCorteView");
            }
            catch(Exception ex)
            {
                await Application.Current.MainPage
                    .DisplayAlert("Ops6", ex.Message + "Detalhes: " + ex.InnerException, "Ok");
            }
        }







    }
}
