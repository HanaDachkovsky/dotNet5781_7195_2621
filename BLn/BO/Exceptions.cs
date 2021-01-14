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
        //public BadAdjacentStationsCodesException(int station1, int station2) : base() { Station1 = station1; Station2 = station2; }
        //public BadAdjacentStationsCodesException(int station1, int station2, string message) : base(message) { Station1 = station1; Station2 = station2; }
        public BadAdjacentStationsCodesException( string message, Exception innerException) :
            base(message, innerException)
        { Station1 = ((DO.BadAdjacentStationsCodesException)innerException).Station1; Station2 = ((DO.BadAdjacentStationsCodesException)innerException).Station2; }
        public override string ToString() => base.ToString() + $", bad stations codes: {Station1}, {Station2}";
    }

    //public class BadBusLicenseNumException : Exception
    //{
    //    public int LicenseNum;
    //    //public BadBusLicenseNumException(int licenseNum) : base() => LicenseNum = licenseNum;
    //    //public BadBusLicenseNumException(int licenseNum, string message) :
    //       // base(message) => LicenseNum = licenseNum;
    //    public BadBusLicenseNumException( string message, Exception innerException) :
    //        base(message, innerException) => LicenseNum = licenseNum;

    //    public override string ToString() => base.ToString() + $", bad bus license number: {LicenseNum}";
    //}

    //bus on trip

    public class BadLineIdException : Exception
    {
        public int ID;
        //public BadLineIdException(int id) : base() => ID = id;
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
        //public BadLineStationIdException(int lineId, int station) : base() { LineId = lineId; Station = station; }
        public BadLineStationIdException(int lineId, int station, string message) : base(message) { LineId = lineId; Station = station; }
        public BadLineStationIdException(string message, Exception innerException) :
            base(message, innerException)
        { LineId = ((DO.BadLineStationIdException)innerException).LineId; Station = ((DO.BadLineStationIdException)innerException).Station; }
        public override string ToString() => base.ToString() + $", bad line station {Station} in line id {LineId}";
    }

    public class BadLineTripIdException : Exception
    {
        public int ID;
        //public BadLineTripIdException(int id) : base() => ID = id;
        //public BadLineTripIdException(int id, string message) :
        //    base(message) => ID = id;
        public BadLineTripIdException( string message, Exception innerException) :
            base(message, innerException) => ID = ((DO.BadLineTripIdException)innerException).ID;

        public override string ToString() => base.ToString() + $", bad line trip id: {ID}";
    }

    public class BadStationCodeException : Exception
    {
        public int Code;
        //public BadStationCodeException(int code) : base() => Code = code;
        public BadStationCodeException(int code, string message) :
            base(message) => Code = code;
        public BadStationCodeException(string message, Exception innerException) :
            base(message, innerException) => Code = ((DO.BadStationCodeException)innerException).Code;

        public override string ToString() => base.ToString() + $", bad station code id: {Code}";
    }

    //trip

    public class BadUserUserNameException : Exception
    {
        public string UserName;
        //public BadUserUserNameException(string userName) : base() => UserName = userName;
        public BadUserUserNameException(string userName, string message) :
           base(message) => UserName = userName;
        public BadUserUserNameException( string message, Exception innerException) :
            base(message, innerException) => UserName = ((DO.BadUserUserNameException)innerException).UserName;

        public override string ToString() => base.ToString() + $", bad user user name: {UserName}";
    }
    //public class XMLFileLoadCreateException : Exception
    //{
    //    public string xmlFilePath;
    //    public XMLFileLoadCreateException(string xmlPath) : base() { xmlFilePath = xmlPath; }
    //    public XMLFileLoadCreateException(string xmlPath, string message) :
    //        base(message)
    //    { xmlFilePath = xmlPath; }
    //    public XMLFileLoadCreateException(string xmlPath, string message, Exception innerException) :
    //        base(message, innerException)
    //    { xmlFilePath = xmlPath; }

    //    public override string ToString() => base.ToString() + $", fail to load or create xml file: {xmlFilePath}";
    //}
}
