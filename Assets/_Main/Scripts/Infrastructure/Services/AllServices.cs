using System;
using System.Collections.Generic;

namespace Infrastructure.Services
{
    public class AllServices
    {
        private static AllServices _instance;

        public static AllServices Container => _instance ??= new AllServices();

        private Dictionary<Type, IService> _servicesMap;

        public void RegisterSingle<TService>(TService service) where TService : IService
        {
            if (_servicesMap == null)
                InitMap();
            
            _servicesMap.Add(typeof(TService), service);
        }

        public TService Single<TService>() where TService : IService
        {
            return (TService)_servicesMap[typeof(TService)];
        }

        private void InitMap() => _servicesMap = new Dictionary<Type, IService>();
    }
}