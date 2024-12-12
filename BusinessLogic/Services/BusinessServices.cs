using BusinessLogic.Options;
using Data.Data.EF.Context;
using Data.Data.EF.Output;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using static BusinessLogic.DomainDataError.BusinessError;
using static System.Net.WebRequestMethods;

namespace BusinessLogic.Services
{
    public class BusinessServices
    {
        private NorthwindContext _dbContext;
        private IOptions<BusinessOptions> _conf;
        public BusinessServices(NorthwindContext dbContext, IOptions<BusinessOptions> conf)
        {
            _dbContext = dbContext;
            _conf = conf;
        }
        public async Task<IEnumerable<Category>> GetData()
        {
            try
            {
                var dati = _dbContext
                    .Categories
                    .AsEnumerable();
                return dati;
            }
            catch(Exception ex) 
            {
                var tt = ex.Message;
                throw ex;
            }
        }
        public async Task<IEnumerable<Category>> GetCategories()
        {
            try
            {
                var dati = _dbContext
                    .QuerySqlDirectAsync<Category>("Select top(5) * from Categories", null);

                var t = await dati;
                return t;
            }
            catch (Exception ex)
            {
                //BusinessMatErrors.OrderSavingErrors.Throw(checkResult.ValidationErrors());
                throw ex;
            }
        }

        public async Task<string> GetNumberFromApi(HttpClient client)
        {
            HttpResponseMessage response = await client.GetAsync(
              "https://api.coindesk.com/v1/bpi/currentprice.json");
            var result = await response.Content.ReadAsStringAsync();
            return result;  
        }
        internal int GetNumber()
        {
            var tt = _conf.Value.Occurrences;
            return tt;
        }
    }
}
