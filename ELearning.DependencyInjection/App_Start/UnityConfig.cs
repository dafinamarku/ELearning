using E_learning.Repository;
using E_learning.RepositoryInterface;
using E_learning.Service;
using E_learning.ServiceInterface;
using System;

using Unity;

namespace ELearning.DependencyInjection
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
           
            container.RegisterType<IMaterialRepository, MaterialRepository>();
            container.RegisterType<IMaterialService, MaterialService>();

            container.RegisterType<ISectionRepository, SectionRepository>();
            container.RegisterType<ISectionService, SectionService>();

            container.RegisterType<ICourseRepository, CourseRepository>();
            container.RegisterType<ICourseService, CourseService>();

          container.RegisterType<ILevelRepository, LevelRepository>();
          container.RegisterType<ILevelService, LevelService>();
        }
    }
}