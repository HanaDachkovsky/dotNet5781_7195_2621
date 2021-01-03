 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    [Serializable]
    public class BadAdjacentStationsCodesException : Exception
    {
        public int Station1;
        public int Station2;
        public BadAdjacentStationsCodesException(int station1, int station2) : base() { Station1 = station1; Station2 = station2; }
        public BadAdjacentStationsCodesException(int station1, int station2, string message) :base(message) { Station1 = station1; Station2 = station2; }
        public BadAdjacentStationsCodesException(int station1, int station2, string message, Exception innerException) :
            base(message, innerException) { Station1 = station1; Station2 = station2; }
        public override string ToString() => base.ToString() + $", bad stations codes: {Station1}, {Station2}";
    }

    public class BadBusLicenseNumException : Exception
    {
        public int LicenseNum;
        public BadBusLicenseNumException(int licenseNum) : base() => LicenseNum=licenseNum;
        public BadBusLicenseNumException(int licenseNum, string message) :
            base(message) => LicenseNum = licenseNum;
        public BadBusLicenseNumException(int licenseNum, string message, Exception innerException) :
            base(message, innerException) => LicenseNum = licenseNum;

        public override string ToString() => base.ToString() + $", bad bus license number: {LicenseNum}";
    }

    //bus on trip

    public class BadLineIdException : Exception
    {
        public int ID;
        public BadLineIdException(int id) : base() => ID = id;
        public BadLineIdException(int id, string message) :
            base(message) => ID = id;
        public BadLineIdException(int id, string message, Exception innerException) :
            base(message, innerException) => ID = id;

        public override string ToString() => base.ToString() + $", bad line id: {ID}";
    }

    public class BadLineStationIdException : Exception
    {
        public int LineId;
        public int Station;
        public BadLineStationIdException(int lineId, int station) : base() { LineId = lineId; Station = station; }
        public BadLineStationIdException(int lineId, int station, string message) : base(message) { LineId = lineId; Station = station; }
        public BadLineStationIdException(int lineId, int station, string message, Exception innerException) :
            base(message, innerException)
        { LineId = lineId; Station = station; }
        public override string ToString() => base.ToString() + $", bad line station {Station} in line id {LineId}";
    }

    public class BadLineTripIdException : Exception
    {
        public int ID;
        public BadLineTripIdException(int id) : base() => ID = id;
        public BadLineTripIdException(int id, string message) :
            base(message) => ID = id;
        public BadLineTripIdException(int id, string message, Exception innerException) :
            base(message, innerException) => ID = id;

        public override string ToString() => base.ToString() + $", bad line trip id: {ID}";
    }

    public class BadStationCodeException : Exception
    {
        public int Code;
        public BadStationCodeException(int code) : base() => Code = code;
        public BadStationCodeException(int code, string message) :
            base(message) => Code = code;
        public BadStationCodeException(int code, string message, Exception innerException) :
            base(message, innerException) => Code = code;

        public override string ToString() => base.ToString() + $", bad station code id: {Code}";
    }

    //trip

    public class BadUderUserNameException : Exception
    {
        public string UserName;
        public BadUderUserNameException(string userName) : base() => UserName = userName;
        public BadUderUserNameException(string userName, string message) :
            base(message) => UserName=userName;
        public BadUderUserNameException(string userName, string message, Exception innerException) :
            base(message, innerException) => UserName = userName;

        public override string ToString() => base.ToString() + $", bad user user name: {UserName}";
    }
}