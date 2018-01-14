using BLToolkit.Data;
using System;
using refactor_me.Bussines;
using refactor_me.Bussines.DataAccessor;
using refactor_me.Domain;
using refactor_me.Domain.DataAccessor;
using Unity;
using Unity.Lifetime;
using Unity.Injection;
using BLToolkit.DataAccess;

namespace refactor_me
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;
        #endregion

        /// <summary>
        /// Registers the type mappings with the Unity container.
        /// </summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>
        /// There is no need to register concrete types such as controllers or
        /// API controllers (unless you want to change the defaults), as Unity
        /// allows resolving a concrete type even if it was not previously
        /// registered.
        /// </remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below.
            // Make sure to add a Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            container.RegisterType<DbManager>(new HierarchicalLifetimeManager(), new InjectionConstructor());

            container.RegisterType<IProductRepository, ProductRepository>();
            container.RegisterType<IProductAccessor>(new InjectionFactory(c => DataAccessor.CreateInstance<ProductAccessor>(c.Resolve<DbManager>())));

            container.RegisterType<IProductOptionRepository, ProductOptionRepository>();
            container.RegisterType<IProductOptionAccessor>(new InjectionFactory(c => DataAccessor.CreateInstance<ProductOptionAccessor>(c.Resolve<DbManager>())));

        }
    }
}