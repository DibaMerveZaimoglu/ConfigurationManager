using System;
using System.Collections.Generic;

public static class ParameterValueType
{
    public const string Integer = "Integer";
    public const string Decimal = "Decimal";
    public const string Bool = "Bool";
    public const string DateTime = "DateTime";

    public static string[] GetAll(){
        return new [] { Integer, Decimal, Bool, DateTime };
    }
}