namespace DependencyResolver
{
    using BLL.BankSystem.ServicesImplementation;
    using BLL.Interface.Interfaces;
    using DAL.EF;
    using DAL.Fake;
    using DAL.Interface.Interfaces;
    using Ninject;

    public static class ResolverConfig
    {
        public static void ConfigurateResolver(this IKernel kernel)
        {
            kernel.Bind<IBankManager>().To<BankService>();
            kernel.Bind<IGeneratorNumber>().To<GeneratorAccountNumber>();
            /// kernel.Bind<IAccountStorage>().To<FakeStorage>();
            kernel.Bind<IAccountStorage>().To<AccountStorageEF>();
        }
    }
}
