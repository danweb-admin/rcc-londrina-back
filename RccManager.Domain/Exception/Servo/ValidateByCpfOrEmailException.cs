using System;
namespace RccManager.Domain.Exception.Servo;

public class ValidateByCpfOrEmailException : System.Exception
{
    public ValidateByCpfOrEmailException()
    {

    }

    public ValidateByCpfOrEmailException(string message)
        : base(message)
    {

    }
}

