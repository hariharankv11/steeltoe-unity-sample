using System;
using System.Reflection;
using Unity.Builder;
using Unity.Builder.Strategy;
using Unity.Extension;
using Unity.Lifetime;
using Unity.Policy;

namespace SteeltoeUnityIoC.UnityExtensions
{
    // i have created my extension based on the accepted answer. replaced factory with my custom implmentation. used refelction to get 
    // requested service from microsoft di container, using custom static method. its getting service registrations when called 
    // standalone. refer to debug statements in global.asax.cs,  but its breaking MVC Unity.
    // https://stackoverflow.com/questions/1380375/custom-object-factory-extension-for-unity

    // just came across this solution. this looks as wonky as mine :-) not sure if it works. i did not try.
    // https://stackoverflow.com/questions/39173345/unity-with-asp-net-core-and-mvc6-core

    public class SteeltoeUnityExtension : UnityContainerExtension
    {
        private SteeltoeObjectsBuildStrategy strategy;

        public SteeltoeUnityExtension()
        {

        }

        protected override void Initialize()
        {
            strategy = new SteeltoeObjectsBuildStrategy(Context);
            Context.Strategies.Add(strategy, UnityBuildStage.PreCreation);
            Context.Policies.Set<ParentMarkerPolicy>(new ParentMarkerPolicy(Context.Lifetime), new NamedTypeBuildKey<ParentMarkerPolicy>());
        }
    }

    public class ParentMarkerPolicy : IBuilderPolicy
    {
        private ILifetimeContainer lifetime;

        public ParentMarkerPolicy(ILifetimeContainer lifetime)
        {
            this.lifetime = lifetime;
        }

        public void AddToLifetime(object o)
        {
            lifetime.Add(o);
        }
    }

    public class SteeltoeObjectsBuildStrategy : BuilderStrategy
    {
        private ExtensionContext baseContext;


        public SteeltoeObjectsBuildStrategy(ExtensionContext baseContext)
        {
            this.baseContext = baseContext;
        }

        public override void PreBuildUp(IBuilderContext context)
        {
            var key = (INamedType)context.OriginalBuildKey;
            //var key = (NamedTypeBuildKey)context.OriginalBuildKey;

            if (context.Existing == null)
            {
                // invoke CoreServerConfig.GetService<T>() using reflection
                MethodInfo method = typeof(CoreServerConfig).GetMethod("GetService")
                                             .MakeGenericMethod(new Type[] { key.Type });

                // get T from CoreServerConfig.GetService<T>()
                var diRegistartion = method.Invoke(this, new object[] { });
                context.Existing = diRegistartion;

                // TODO:    ContainerControlledLifetimeManagerr is breaking RESOLVE operation when generic interface types are getting added. 
                //          so choosing SingletonLifetimeManager. assuming there won't be any implications. 
                var ltm = new ContainerControlledLifetimeManager();
                //var ltm = new SingletonLifetimeManager();
                ltm.SetValue(context.Existing);

                // Find the container to add this to
                IPolicyList parentPolicies;
                var parentMarker = context.Policies.Get<ParentMarkerPolicy>(new NamedTypeBuildKey<ParentMarkerPolicy>(), out parentPolicies);

                // TODO: add error check - if policy is missing, extension is misconfigured

                // Add lifetime manager to container
                parentPolicies.Set<ILifetimePolicy>(ltm, new NamedTypeBuildKey(key.Type));
                // And add to LifetimeContainer so it gets disposed
                parentMarker.AddToLifetime(ltm);

                // Short circuit the rest of the chain, object's already created
                context.BuildComplete = true;
            }
        }
    }
}