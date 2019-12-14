using Autofac;
using Testing.Interfaces;

namespace Testing
{
    public class Bootstrapper
    {
        public IContainer BuildContainer(IClassC classCInstance = null)
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<ClassA>().As<IClassA>();
            builder.RegisterType<ClassB>().As<IClassB>();

            if (classCInstance == null)
            {
                builder.RegisterType<ClassC>().As<IClassC>();
            }
            else
            {
                builder.RegisterInstance(classCInstance).As<IClassC>();
            }

            return builder.Build();
        }
    }
}
