namespace ExtensionsForReflexDI.Factory
{
    public interface IFactory
    {
    }
    
    public interface IFactory<out TInterface> : IFactory
    {
        TInterface Create();
    }
    
    public interface IFactory<out TInterface, in TParam> : IFactory
    {
        TInterface Create(TParam param);
    } 
    public interface IFactory<out TInterface, in TParam1, in TParam2> : IFactory
    {
        TInterface Create(TParam1 param1, TParam2 param2);
    }
    public interface IFactory<out TInterface, in TParam1, in TParam2, in TParam3> : IFactory
    {
        TInterface Create(TParam1 param1, TParam2 param2, TParam3 param3);
    }
    public interface IFactory<out TInterface, in TParam1, in TParam2, in TParam3, in TParam4> : IFactory
    {
        TInterface Create(TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4);
    }
    public interface IFactory<out TInterface, in TParam1, in TParam2, in TParam3, in TParam4, in TParam5> : IFactory
    {
        TInterface Create(TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5);
    }
}