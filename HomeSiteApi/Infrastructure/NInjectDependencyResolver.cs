using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HomeSite.Models.Repository;

namespace HomeSiteApi.Infrastructure
{
    public class NInjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NInjectDependencyResolver(IKernel kernel)
        {
            this.kernel = kernel;
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

            kernel.Bind<IPhotoRepositoryAlt>().To<PhotoRepository>();
        }
    }
}