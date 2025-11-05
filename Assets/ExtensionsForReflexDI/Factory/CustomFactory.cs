using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Reflex.Core;

namespace ExtensionsForReflexDI.Factory
{
    public abstract class CustomFactory<TConcrete>
    {
        private readonly Container _container;
        private readonly ParameterInfo[] _constructorParameters;
        private readonly Func<object[], TConcrete> _activator;

        protected CustomFactory(Container container)
        {
            _container = container;
            var constructor = typeof(TConcrete).GetConstructors().Single();
            _constructorParameters = constructor.GetParameters();
            _activator = CreateActivator(constructor);
        }

        //TLDR, creates the following lambda function (object[])=> new TConcrete((TConstructorParam0Type)object[0], (TConstructorParam1Type)object[1]...)
        private static Func<object[], TConcrete> CreateActivator(ConstructorInfo ctor)
        {
            //defines (Object[]) as an input param
            var paramExpr = Expression.Parameter(typeof(object[]), "args");

            //defines (TConstructorParam0Type)object[0], (TConstructorParam1Type)object[1]...
            var args = ctor.GetParameters().Select((p, i) =>
                //defines the casting (TConstructorParam-i-Type)object[i]
                Expression.Convert(
                    //defines object[i]
                    Expression.ArrayIndex(paramExpr, Expression.Constant(i)), p.ParameterType));

            //defines new ...constructo + parameters
            var body = Expression.New(ctor, args);

            //creates the lambda from the expressions
            var lambda = Expression.Lambda<Func<object[], TConcrete>>(body, paramExpr);
            return lambda.Compile();
        }

        protected TConcrete Create(params object[] passedParameters)
        {
            var parameters = passedParameters;

            if (_constructorParameters.Length != passedParameters.Length)
                parameters = GetResolvedParameters(parameters, _constructorParameters);

            return _activator(parameters);
        }

        private object[] GetResolvedParameters(object[] passedParameters, ParameterInfo[] constructorParameters)
        {
            ParameterInfo[] paramsToResolve = new ParameterInfo[constructorParameters.Length - passedParameters.Length];
            Array.Copy(constructorParameters, passedParameters.Length, paramsToResolve, 0, constructorParameters.Length - passedParameters.Length);
            
            var originalSize = passedParameters.Length;
            Array.Resize(ref passedParameters, constructorParameters.Length);

            for (int i = 0; i < paramsToResolve.Length; i++)
            {
                try
                {
                    var obj = _container.Resolve(paramsToResolve[i].ParameterType);
                    passedParameters[i + originalSize] = obj;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }

            return passedParameters;
        }
    }

    public abstract class CustomFactory<TConcrete, TInterface> : CustomFactory<TConcrete>, IFactory<TInterface>
        where TConcrete : TInterface
    {
        protected CustomFactory(Container container) : base(container)
        {
        }

        public TInterface Create()
        {
            return base.Create();
        }
    }

    public abstract class CustomFactory<TConcrete, TInterface, TParam> : CustomFactory<TConcrete>,
        IFactory<TInterface, TParam> where TConcrete : TInterface
    {
        protected CustomFactory(Container container) : base(container)
        {
        }

        public TInterface Create(TParam param)
        {
            return base.Create(param);
        }
    }

    public abstract class CustomFactory<TConcrete, TInterface, TParam1, TParam2> : CustomFactory<TConcrete>,
        IFactory<TInterface, TParam1, TParam2> where TConcrete : TInterface
    {
        protected CustomFactory(Container container) : base(container)
        {
        }

        public TInterface Create(TParam1 param1, TParam2 param2)
        {
            return base.Create(param1, param2);
        }
    }

    public abstract class CustomFactory<TConcrete, TInterface, TParam1, TParam2, TParam3> : CustomFactory<TConcrete>,
        IFactory<TInterface, TParam1, TParam2, TParam3> where TConcrete : TInterface
    {
        protected CustomFactory(Container container) : base(container)
        {
        }

        public TInterface Create(TParam1 param1, TParam2 param2, TParam3 param3)
        {
            return base.Create(param1, param2, param3);
        }
    }

    public abstract class
        CustomFactory<TConcrete, TInterface, TParam1, TParam2, TParam3, TParam4> : CustomFactory<TConcrete>,
        IFactory<TInterface, TParam1, TParam2, TParam3, TParam4> where TConcrete : TInterface
    {
        protected CustomFactory(Container container) : base(container)
        {
        }

        public TInterface Create(TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4)
        {
            return base.Create(param1, param2, param3, param4);
        }
    }

    public abstract class
        CustomFactory<TConcrete, TInterface, TParam1, TParam2, TParam3, TParam4, TParam5> : CustomFactory<TConcrete>,
        IFactory<TInterface, TParam1, TParam2, TParam3, TParam4, TParam5> where TConcrete : TInterface
    {
        protected CustomFactory(Container container) : base(container)
        {
        }

        public TInterface Create(TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5)
        {
            return base.Create(param1, param2, param3, param4, param5);
        }
    }
}