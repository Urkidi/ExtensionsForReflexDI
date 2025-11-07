namespace ExtensionsForReflexDI.Factory
{
    public interface IFactory
    {
    }
    
    public interface IFactory<out TContract> : IFactory
    {
        TContract Create();
    }
    
    public interface IFactory<out TContract, in TParam> : IFactory
    {
        TContract Create(TParam param);
    } 
    public interface IFactory<out TContract, in TParam1, in TParam2> : IFactory
    {
        TContract Create(TParam1 param1, TParam2 param2);
    }
    public interface IFactory<out TContract, in TParam1, in TParam2, in TParam3> : IFactory
    {
        TContract Create(TParam1 param1, TParam2 param2, TParam3 param3);
    }
    public interface IFactory<out TContract, in TParam1, in TParam2, in TParam3, in TParam4> : IFactory
    {
        TContract Create(TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4);
    }
    public interface IFactory<out TContract, in TParam1, in TParam2, in TParam3, in TParam4, in TParam5> : IFactory
    {
        TContract Create(TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5);
    }
}