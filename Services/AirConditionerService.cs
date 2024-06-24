using Repositories;
using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AirConditionerService
    {
        private AirConditionerRepository _repository = new();

        public List<AirConditioner> GetAllAirConditioners()
        {
            return _repository.GetAllAirConditioners();
        }

        public void AddAirConditioner(AirConditioner airConditioner)
        {
            _repository.AddAirConditioner(airConditioner);
        }

        public void UpdateAirConditioner(AirConditioner airConditioner)
        {
            _repository.UpdateAirConditioner(airConditioner);
        }

        public void DeleteAirConditioner(AirConditioner airConditioner)
        {
            _repository.DeleteAirConditioner(airConditioner);
        }

        public List<AirConditioner> SearchByFeatureOrQuantity(string  featureFunction, int quantity)
        {
            return _repository.SearchAirConditioners(featureFunction, quantity);
        }

        public AirConditioner? SearchById(int airConditionerId) 
        {
            return _repository.GetById(airConditionerId);
        }
    }
}
