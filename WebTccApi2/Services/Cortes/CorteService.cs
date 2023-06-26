using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebTccApi2.Models;

namespace WebTccApi2.Services.Cortes
{
    public class CorteService : Request
    {
        private readonly Request _request;
        private const string apiUrlBase = "http://www.vinifmatheusd.somee.com/ApiCortes/Cortes";

        private string _token;
        public CorteService(string token)
        {
            _request = new Request();
            _token = token;
        }
        public async Task<ObservableCollection<Corte>> GetCortesAsync()
        {
            string urlComplementar = string.Format("{0}", "/GetAll");
            ObservableCollection<Models.Corte> listaCortes = await
                _request.GetAsync<ObservableCollection<Models.Corte>>(apiUrlBase + urlComplementar, _token);
            return listaCortes;
        }
        public async Task<Corte> GetCorteAsync(int corteId)
        {
            string urlComplementar = string.Format("/{0}", corteId);
            var corte = await _request.GetAsync<Models.Corte>(apiUrlBase + urlComplementar, _token);
            return corte;
        }
        public async Task<int> PostCorteAsync(Corte c)
        {
            return await _request.PostReturnIntTokenAsync(apiUrlBase, c, _token);
        }
        public async Task<int> PutCorteAsync(Corte c)
        {
            var result = await _request.PutAsync(apiUrlBase, c, _token);
            return result;
        }
        public async Task<int> DeleteCorteAsync(int corteId)
        {
            string urlComplementar = string.Format("/{0}", corteId);

            var result = await _request.DeleteAsync(apiUrlBase + urlComplementar, _token);
            return result;
        }
    }
}
