using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletInspector2.Core.Interfaces;

namespace WalletInspector2.Core.Code.Main
{
    public class ServiceLocator
    {
        private static IUnityContainer Container;

        public static void Init(IUnityContainer container)
        {
            Container = container;
        }

        static ServiceLocator()
        {
            Container = new UnityContainer();
            Container.RegisterInstance<IRepository>(new DataContext());
        }

        public static T Get<T>()
        {
            return Container.Resolve<T>();
        }
    }
}
