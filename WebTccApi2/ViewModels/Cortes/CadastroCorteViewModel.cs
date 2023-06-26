using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebTccApi2.Services.Cortes;
using WebTccApi2.Models;
using WebTccApi2.Models.Enuns;
using System.Windows.Input;
using System.Linq;


namespace WebTccApi2.ViewModels.Cortes
{

    [QueryProperty("CorteSelecionadoId","cId")]
    public class CadastroCorteViewModel : BaseViewModel
    {
        private CorteService pService;
        private string corteSelecionadoId;


        public ICommand SalvarCommand { get; }
        public ICommand CancelarCommand { get; set; }
        public CadastroCorteViewModel()
        {
            string token = Preferences.Get("UsuarioToken", string.Empty);
            pService = new CorteService(token);

            _ = ObterClasses();

            SalvarCommand = new Command(async () => { await SalvarCorte(); });
            CancelarCommand = new Command(async => CancelarCorte());
            
            
        }
        

        private int id;
        private string sex;
        private string estilo;
        private string descricao;
        private int preco;

        public int Id
        { 
            get => id;
            set
            {
                id = value;
                OnPropertyChanged();
            }
        }
        public string Sex
        {
            get => sex;
            set
            {
                sex = value;
                OnPropertyChanged();
            }
        }
        public string Estilo
        {
            get => estilo;
            set
            {
                estilo = value;
                OnPropertyChanged();
            }
        }
        public string Descricao
        {
            get => descricao;
            set
            {
                descricao = value;
                OnPropertyChanged();
            }
        }
        public int Preco
        {
            get => preco;
            set
            {
                preco = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<TipoClasse> listaTiposClasse;

        public ObservableCollection<TipoClasse> ListaTiposClasse
        {
            get { return listaTiposClasse; }
            set
            {
                if (value != null)
                {
                    listaTiposClasse = value;
                    OnPropertyChanged();
                }
            }
        }
        public async Task ObterClasses()
        {
            try
            {
                ListaTiposClasse = new ObservableCollection<TipoClasse>();
                ListaTiposClasse.Add(new TipoClasse() { Id = 1, DescricaoCorte = "Degrade" });
                ListaTiposClasse.Add(new TipoClasse() { Id = 2, DescricaoCorte = "TapperFade" });
                ListaTiposClasse.Add(new TipoClasse() { Id = 3, DescricaoCorte = "Surfista" });
                ListaTiposClasse.Add(new TipoClasse() { Id = 4, DescricaoCorte = "Moicano" });
                OnPropertyChanged(nameof(ListaTiposClasse));
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                    .DisplayAlert("Ops1", ex.Message + "Detalhes: " + ex.InnerException, "Ok");
            }
        }
        private TipoClasse tipoClasseSelecionado;

        public TipoClasse TipoClasseSelecionado
        {
            get { return tipoClasseSelecionado; }
            set
            {
                if (value == null)
                {
                    tipoClasseSelecionado = value;
                    OnPropertyChanged();
                }
            }
        }

        public string CorteSelecionadoId 
        { set
            {
                if(value != null)
                {
                    corteSelecionadoId = Uri.UnescapeDataString(value);
                    CarregarCorte();
                }
            }    
        }

        public async Task SalvarCorte()
        {
            try
            {
                Corte model = new Corte()
                {
                    Sex = this.sex,
                    Descricao = this.descricao,
                    Preco = this.preco,
                    Estilo = this.estilo,
                    Id = this.id,
                    Classe = (ClasseEnum)tipoClasseSelecionado.Id

                };
                if (model.Id == 0)
                    await pService.PostCorteAsync(model);
                else
                    await pService.PutCorteAsync(model);

                await Application.Current.MainPage
                    .DisplayAlert("Mensagem", "Dados salvos com sucesso!", "Ok");
                await Shell.Current.GoToAsync(".."); //Remove a página atual da pilha de páginas
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                    .DisplayAlert("Ops3", ex.Message + "Detalhes: " + ex.InnerException, "Ok");
            }
        }
        public async void CancelarCorte()
        {
            await Shell.Current.GoToAsync("..");
        }
        public async void CarregarCorte()
        {
            try
            {
                Corte c = await pService.GetCorteAsync(int.Parse(corteSelecionadoId));

                this.Sex = c.Sex;
                this.Estilo = c.Estilo;
                this.Descricao = c.Descricao;
                this.Preco = c.Preco;
                this.Id = c.Id;

                TipoClasseSelecionado = this.ListaTiposClasse
                    .FirstOrDefault(tClasse => tClasse.Id == (int)c.Classe);
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                    .DisplayAlert("Ops3", ex.Message + "Detalhes: " + ex.InnerException, "Ok");
            }
        }


    }
}
