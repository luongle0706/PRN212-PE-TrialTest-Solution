using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class AirConditionerRepository
    {
        private AirConditionerShop2024DbContext _context;

        public List<AirConditioner> GetAllAirConditioners()
        {
            _context = new();
            return _context.AirConditioners.ToList();
        }

        public AirConditioner? GetById(int airConditionerId)
        {
            _context = new();
            return _context.AirConditioners.FirstOrDefault(account => account.AirConditionerId.Equals(airConditionerId));
        }

        public void AddAirConditioner(AirConditioner airConditioner)
        {
            _context = new();
            _context.Add(airConditioner);
            _context.SaveChanges();
        }

        public void UpdateAirConditioner(AirConditioner airConditioner)
        {
            _context = new();
            _context.Update(airConditioner);
            _context.SaveChanges();
        }

        public void DeleteAirConditioner(AirConditioner airConditioner)
        {
            _context = new();
            _context.Remove(airConditioner);
            _context.SaveChanges();
        }

        public List<AirConditioner> SearchAirConditioners(string featureFunction, int quantity)
        {
            _context = new();
            return _context.AirConditioners.Where(
                ac => ac.FeatureFunction.Contains(featureFunction) || ac.Quantity.Value == quantity).ToList();
        }
    }
}
