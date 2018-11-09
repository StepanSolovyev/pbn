namespace RusBounds
{
    class RBParser2
    {
        public struct emitent
        {
            public int emit;
            public string Issuer; //наименование
            //public string discription;
            public string TypeOfBusiness; //основной ОКВЕД
            public string country;
            public string eregion;
            public ulong inn;
            public int okpo;
            public string GosRegData; //данные по регистрации
            public string LowAdress; //юридический адресс
            public string MailingAdress; //почтовый адрес
            public string TypeOfProperty; //вид собственности
            public int CharterCapital; //уставной капитал
            public string CharterCapitalСurrency; //валюта уставного капитала
            public emitent(int emitid)
            {
                emit = emitid;
                Issuer = "";
                //discription = "";
                TypeOfBusiness = "";
                country = "";
                eregion = "";
                inn = 0;
                okpo = 0;
                GosRegData = "";
                LowAdress = "";
                MailingAdress = "";
                TypeOfProperty = "";
                CharterCapital = 0;
                CharterCapitalСurrency = "";
            }
        }
    }
}
