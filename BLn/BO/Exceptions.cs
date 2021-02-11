using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    [Serializable]
    public class BadAdjacentStationsCodesException : Exception
    {
        public int Station1;
        public int Station2;
        public BadAdjacentStationsCodesException( string message, Exception innerException) :
            base(message, innerException)
        { Station1 = ((DO.BadAdjacentStationsCodesException)innerException).Station1; Station2 = ((DO.BadAdjacentStationsCodesException)innerException).Station2; }
        public override string ToString() => base.ToString() + $", bad stations codes: {Station1}, {Station2}";
    }

    

    public class BadLineIdException : Exception
    {
        public int ID;
        public BadLineIdException(int id, string message) :
           base(message) => ID = id;
        public BadLineIdException( string message, Exception innerException) :
            base(message, innerException) => ID = ((DO.BadLineIdException)innerException).ID;

        public override string ToString() => base.ToString() + $", bad line id: {ID}";
    }

    public class BadLineStationIdException : Exception
    {
        public int LineId;
        public int Station; 
        public BadLineStationIdException(int lineId, int station, string message) : base(message) { LineId = lineId; Station = station; }
        public BadLineStationIdException(string message, Exception innerException) :
            base(message, innerException)
        { LineId = ((DO.BadLineStationIdException)innerException).LineId; Station = ((DO.BadLineStationIdException)innerException).Station; }
        public override string ToString() => base.ToString() + $", bad line station {Station} in line id {LineId}";
    }

    public class BadLineTripIdException : Exception
    {
        public int ID;
        public BadLineTripIdException( string message, Exception innerException) :
            base(message, innerException) => ID = ((DO.BadLineTripIdException)innerException).ID;

        public override string ToString() => base.ToString() + $", bad line trip id: {ID}";
    }

    public class BadStationCodeException : Exception
    {
        public int Code;
        public BadStationCodeException(int code, string message) :
            base(message) => Code = code;
        public BadStationCodeException(string message, Exception innerException) :
            base(message, innerException) => Code = ((DO.BadStationCodeException)innerException).Code;

        public override string ToString() => base.ToString() + $", bad station code id: {Code}";
    }


    public class BadUserUserNameException : Exception
    {
        public string UserName;
        public BadUserUserNameException(string userName, string message) :
           base(message) => UserName = userName;
        public BadUserUserNameException( string message, Exception innerException) :
            base(message, innerException) => UserName = ((DO.BadUserUserNameException)innerException).UserName;

        public override string ToString() => base.ToString() + $", bad user user name: {UserName}";
    }

    public class BadManagementPasswordEception : Exception
    {
        public BadManagementPasswordEception(string message) : base(message) { }

        public override string ToString() => base.ToString();
    }
    
}
