using HomeSite.Models.Repository;
using HomeSiteDomain.Models.Photos;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HomeSite.Infrastructure
{
    public class NInjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NInjectDependencyResolver(IKernel kernelParam) 
        { 
            this.kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.GetService(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            kernel.Bind<IAboutMeRepository>().To<AboutmeRepository>();
            kernel.Bind<IPhotoRepository>().To<PhotoRepository>();
            kernel.Bind<IHomeRepository>().To<HomeRepository>();
            kernel.Bind<IErrorRepository>().To<ErrorRepository>();
        }
    }
}